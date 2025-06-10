    using Lab_4.DTO_s;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;         

    namespace Lab_4.Classes 
    {
        public class Composition : INotifyPropertyChanged, IDataErrorInfo
        {
            private int _numOfRoom;
            private int _roomPrice; 
            private ObservableCollection<ConsignmentOfGoods> _info = new ObservableCollection<ConsignmentOfGoods>(); 

            public Composition(int numOfRoom, int roomPrice, ObservableCollection<ConsignmentOfGoods> info)
            {
                NumOfRoom = numOfRoom; 
                RoomPrice = roomPrice; 
                Info = info; 
            }

            public Composition(int numOfRoom, int roomPrice)
            {
                NumOfRoom = numOfRoom;
                RoomPrice = roomPrice;
            }
            public Composition()
            {
                _info.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(ShortString));
                _info.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Info));
            }
        public Composition Clone()
        {
            return new Composition
            {
                NumOfRoom = this.NumOfRoom,
                RoomPrice = this.RoomPrice
            };
        }

        [Required(ErrorMessage = "Room number is required.")]
            public int NumOfRoom
            {
                get => _numOfRoom;
                set
                {
                    if (_numOfRoom != value)
                    {
                        _numOfRoom = value;
                        OnPropertyChanged(nameof(NumOfRoom));
                        OnPropertyChanged(nameof(ShortString));
                    }
                }
            }

            [Required(ErrorMessage = "Room price is required.")]
            public int RoomPrice 
            {
                get => _roomPrice;
                set
                {
                    if (_roomPrice != value)
                    {
                        _roomPrice = value;
                        OnPropertyChanged(nameof(RoomPrice));
                    }
                }
            }

            public ObservableCollection<ConsignmentOfGoods> Info
            {
                get => _info;
                set
                {
                    value ??= new ObservableCollection<ConsignmentOfGoods>();

                    if (_info != value)
                    {
                        _info = value;
                        OnPropertyChanged(nameof(Info));
                        OnPropertyChanged(nameof(ShortString)); 
                    }
                }
            }

            public void AcceptProductBatch(ConsignmentOfGoods productBatch)
            {
                 if (productBatch != null)
                 {
                     _info.Add(productBatch);
                 }
            }

            public void WriteOffProductBatch(ConsignmentOfGoods productBatch)
            {
                if (productBatch != null && _info.Contains(productBatch))
                {
                    _info.Remove(productBatch);
                }
            }

            public string ToShortString()
            {
                decimal totalGoodsValue = 0;
                int totalQuantity = 0; 

                if (Info != null && Info.Any()) 
                {
                    totalGoodsValue = Info.Sum(cg => cg.TotalCost());
                    totalQuantity = Info.Sum(cg => cg.Quantity); 
                }

                return $"Total Qty: {totalQuantity}, Value: {totalGoodsValue} $";
            }

            public override string ToString()
            {
                return $"Composition No.{NumOfRoom}, Room Price: {RoomPrice:C}, Total Consignments: {Info.Count}";
            }

            public string ShortString => ToShortString();

            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToString)));
            }
        public string Error
        {
            get
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(this);
                Validator.TryValidateObject(this, context, results, true);

                if (Info != null)
                {
                    foreach (var consignment in Info)
                    {
                        foreach (var prop in typeof(ConsignmentOfGoods).GetProperties())
                        {
                            string propError = ((IDataErrorInfo)consignment)[prop.Name];
                            if (!string.IsNullOrEmpty(propError))
                            {
                                results.Add(new ValidationResult(
                                    $"Error in the consignment ({consignment.Vegetables?.Name ?? "N/A"}) - {prop.Name}: {propError}"));
                            }
                        }
                    }
                }

                return string.Join("\n", results.Select(r => r.ErrorMessage));
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(NumOfRoom))
                {
                    if (NumOfRoom < 1 || NumOfRoom > 999)
                        return "The room number must be between 1 and 999.";
                }
                else if (columnName == nameof(RoomPrice))
                {
                    if (RoomPrice < 1)
                        return "Room price must be greater than 0.";
                }
                else if (columnName == nameof(Info))
                {
                    if (Info != null)
                    {
                        foreach (var consignment in Info)
                        {
                            string err = ((IDataErrorInfo)consignment).Error;
                            if (!string.IsNullOrEmpty(err))
                                return $"Error in the consignment({consignment.Vegetables?.Name ?? "N/A"}): {err}";
                        }
                    }
                }

                var prop = GetType().GetProperty(columnName);
                if (prop != null)
                {
                    var value = prop.GetValue(this);
                    var context = new ValidationContext(this) { MemberName = columnName };
                    var results = new List<ValidationResult>();
                    bool valid = Validator.TryValidateProperty(value, context, results);
                    return valid ? string.Empty : results.First().ErrorMessage;
                }

                return string.Empty;
            }
        }

            public CompositionDTO ToDTO()
            {
                return new CompositionDTO
                {
                    NumOfRoom = this.NumOfRoom,
                    RoomPrice = this.RoomPrice,
                    InfoOfGoods = this.Info?.Select(c => c.ToDTO()).ToList() 
                };
            }

            public static Composition FromDTO(CompositionDTO dto)
            {
                if (dto == null) return null;   

                var composition = new Composition 
                {
                    NumOfRoom = dto.NumOfRoom,
                    RoomPrice = dto.RoomPrice
                };

                if (dto.InfoOfGoods != null)
                {
                    foreach (var consignmentDto in dto.InfoOfGoods)
                    {
                        composition.AcceptProductBatch(ConsignmentOfGoods.FromDTO(consignmentDto)); 
                    }
                }
                return composition;
            }
        }
    }