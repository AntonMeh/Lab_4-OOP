using Lab_4.DTO_s;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;         

namespace Lab_4.Classes 
{
    public class Composition : INotifyPropertyChanged
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

        [Required(ErrorMessage = "Room number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be greater than 0.")]
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
        [Range(0, int.MaxValue, ErrorMessage = "Room price cannot be negative.")]
        public int RoomPrice 
        {
            get => _roomPrice;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Room price cannot be negative.", nameof(RoomPrice));
                }
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
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Info), "List of consignments cannot be null.");
                }
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
            if (productBatch == null)
            {
                throw new ArgumentNullException(nameof(productBatch), "Product batch cannot be null.");
            }
            _info.Add(productBatch);
            OnPropertyChanged(nameof(Info)); 
            OnPropertyChanged(nameof(ShortString)); 
        }

        public void WriteOffProductBatch(ConsignmentOfGoods productBatch)
        {
            if (productBatch == null)
            {
                throw new ArgumentNullException(nameof(productBatch), "Product batch cannot be null.");
            }
            if (_info.Remove(productBatch))
            {
                OnPropertyChanged(nameof(Info)); 
                OnPropertyChanged(nameof(ShortString)); 
            }
            else
            {
                throw new InvalidOperationException("Product batch not found on this composition.");
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