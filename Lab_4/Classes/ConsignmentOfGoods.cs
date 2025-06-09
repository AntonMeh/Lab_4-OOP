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
    public class ConsignmentOfGoods : INotifyPropertyChanged
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
        public ConsignmentOfGoods() 
        {

        }
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
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity
        {
            get => _quantity;
            set
            { 
                if (value < 0)
                {
                    throw new ArgumentException("Quantity cannot be negative.", nameof(Quantity));
                }
                _quantity = value; 
                OnPropertyChanged(nameof(Quantity));
            }
        }

        [Required(ErrorMessage = "Price for one item is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price for one item cannot be negative.")]
        public int PriceForOne
        {
            get => _priceForOne;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price for one item cannot be negative.", nameof(PriceForOne));
                }
                _priceForOne = value; 
                OnPropertyChanged(nameof(PriceForOne));
            }
        }

        [Required(ErrorMessage = "Transport price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Transport price cannot be negative.")]
        public int PriceForTransport
        {
            get => _priceForTransport;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Transport price cannot be negative.", nameof(PriceForTransport));
                }
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
