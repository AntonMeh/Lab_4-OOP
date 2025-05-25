using Lab_4.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private decimal _priceForOne;
        private decimal _priceForTransport;
        private DateTime _dateOfDelivery;

        public ConsignmentOfGoods(Vegetables vegetables, Delivery delivery, int quantity, decimal priceForOne, decimal priceForTransport, DateTime dateOfDelivery)
        {
            Vegetables = vegetables;
            TypeOfDelivery = delivery;
            Quantity= quantity;
            PriceForOne= priceForOne;
            PriceForTransport= priceForTransport;
            DateOfDelivery= dateOfDelivery;
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
        public int Quantity
        {
            get => _quantity;
            set
            { 
                _quantity = value; OnPropertyChanged(nameof(Quantity));
            }
        }
        public decimal PriceForOne
        {
            get => _priceForOne;
            set
            {
                _priceForOne = value; OnPropertyChanged(nameof(PriceForOne));
            }
        }
        public decimal PriceForTransport
        {
            get => _priceForTransport;
            set
            {
                _priceForTransport = value; OnPropertyChanged(nameof(PriceForTransport));
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
    }
}
