using Lab_4.DTO_s;
using Lab_4.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Classes
{
    public class ConsignmentOfGoods : INotifyPropertyChanged, IDataErrorInfo
    {
        private Vegetables _vegetables;
        private Delivery _typeOfDelivery;
        private int _quantity;
        private int _priceForOne;
        private int _priceForTransport;
        private DateTime _dateOfDelivery;

        public ConsignmentOfGoods(Vegetables vegetables, Delivery delivery, int quantity, int priceForOne, int priceForTransport, DateTime dateOfDelivery)
        {
            Vegetables = vegetables;
            TypeOfDelivery = delivery;
            Quantity = quantity;
            PriceForOne = priceForOne;
            PriceForTransport = priceForTransport;
            DateOfDelivery = dateOfDelivery;
        }
        public ConsignmentOfGoods() { }

        public ConsignmentOfGoods Clone()
        {
            return new ConsignmentOfGoods
            {
                Vegetables = this.Vegetables.Clone(),
                TypeOfDelivery = this.TypeOfDelivery,
                Quantity = this.Quantity,
                PriceForOne = this.PriceForOne,
                PriceForTransport = this.PriceForTransport,
                DateOfDelivery = this.DateOfDelivery,
            };
        }
        [Required(ErrorMessage = "Vegetables is required.")]
        public Vegetables Vegetables
        {
            get => _vegetables;
            set
            {
                _vegetables = value; OnPropertyChanged(nameof(Vegetables));
            }
        }
        public Delivery TypeOfDelivery
        {
            get => _typeOfDelivery;
            set
            {
                _typeOfDelivery = value; OnPropertyChanged(nameof(TypeOfDelivery));
            }
        }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity
        {
            get => _quantity;
            set
            { 
                _quantity = value; 
                OnPropertyChanged(nameof(Quantity));
            }
        }

        [Required(ErrorMessage = "Price for one item is required.")]
        public int PriceForOne
        {
            get => _priceForOne;
            set
            {
                _priceForOne = value; 
                OnPropertyChanged(nameof(PriceForOne));
            }
        }

        [Required(ErrorMessage = "Transport price is required.")]
        public int PriceForTransport
        {
            get => _priceForTransport;
            set
            {
                _priceForTransport = value; 
                OnPropertyChanged(nameof(PriceForTransport));
            }
        }
        public DateTime DateOfDelivery
        {
            get => _dateOfDelivery;
            set
            {
                _dateOfDelivery = value; OnPropertyChanged(nameof(DateOfDelivery));
            }
        }
        public decimal TotalCost()
        {
            return (PriceForOne * Quantity) + PriceForTransport;
        }
        public decimal CurrentTotalCost => TotalCost();
        public override string ToString()
        {
            return $"Vegetable: {Vegetables.Name}, Delivery: {TypeOfDelivery}, Quantity: {Quantity}, Price: {PriceForOne} $, Price for del.: {PriceForTransport} $, Date of del.: {DateOfDelivery:d}";
        }

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
                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(this, context, results, true);

                var quantityError = this[nameof(Quantity)];
                if (!string.IsNullOrEmpty(quantityError))
                    results.Add(new ValidationResult(quantityError, new[] { nameof(Quantity) }));

                var priceForOneError = this[nameof(PriceForOne)];
                if (!string.IsNullOrEmpty(priceForOneError))
                    results.Add(new ValidationResult(priceForOneError, new[] { nameof(PriceForOne) }));

                var priceForTransportError = this[nameof(PriceForTransport)];
                if (!string.IsNullOrEmpty(priceForTransportError))
                    results.Add(new ValidationResult(priceForTransportError, new[] { nameof(PriceForTransport) }));

                if (Vegetables != null)
                {
                    var vegError = ((IDataErrorInfo)Vegetables).Error;
                    if (!string.IsNullOrEmpty(vegError))
                        results.Add(new ValidationResult($"Error in vegetable data: {vegError}"));
                }

                return string.Join("\n", results.Select(r => r.ErrorMessage));
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Quantity))
                {
                    if (Quantity <= 0)
                        return "The number must be greater than 0.";
                }
                else if (columnName == nameof(PriceForOne))
                {
                    if (PriceForOne < 0)
                        return "The price per unit cannot be negative.";
                }
                else if (columnName == nameof(PriceForTransport))
                {
                    if (PriceForTransport < 0)
                        return "The cost of transportation cannot be negative.";
                }
                else if (columnName == nameof(Vegetables))
                {
                    if (Vegetables == null)
                        return "No information about vegetables is available.";
                    var vegError = ((IDataErrorInfo)Vegetables).Error;
                    if (!string.IsNullOrEmpty(vegError))
                        return $"Error in vegetable data: {vegError}";
                }
                else if (columnName == nameof(DateOfDelivery))
                {
                    if (DateOfDelivery.Date < DateTime.Today.Date)
                        return "The delivery date cannot be in the past.";
                }

                return string.Empty;
            }
        }

        public ConsignmentOfGoodsDTO ToDTO()
        {
            return new ConsignmentOfGoodsDTO
            {
                Vegetables = this.Vegetables?.ToDTO(), 
                TypeOfDelivery = this.TypeOfDelivery,
                Quantity = this.Quantity,
                PriceForOne = this.PriceForOne,
                PriceForTransport = this.PriceForTransport,
                DateOfDelivery = this.DateOfDelivery
            };
        }

        public static ConsignmentOfGoods FromDTO(ConsignmentOfGoodsDTO dto)
        {
            if (dto == null) return null;
            return new ConsignmentOfGoods
            {
                Vegetables = Vegetables.FromDTO(dto.Vegetables), 
                TypeOfDelivery = dto.TypeOfDelivery,
                Quantity = dto.Quantity,
                PriceForOne = dto.PriceForOne,
                PriceForTransport = dto.PriceForTransport,
                DateOfDelivery = dto.DateOfDelivery
            };
        }
    }
}
