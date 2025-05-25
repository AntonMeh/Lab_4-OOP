using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;         

namespace Lab_4.Classes 
{
    public class Composition : INotifyPropertyChanged
    {
        private int _numOfRoom;
        private decimal _roomPrice; 
        private List<ConsignmentOfGoods> _info = new List<ConsignmentOfGoods>(); 

        public Composition(int numOfRoom, decimal roomPrice, List<ConsignmentOfGoods> info)
        {
            NumOfRoom = numOfRoom; 
            RoomPrice = roomPrice; 
            Info = info ?? new List<ConsignmentOfGoods>(); 
        }

        public Composition(int numOfRoom, decimal roomPrice)
        {
            NumOfRoom = numOfRoom;
            RoomPrice = roomPrice;
            _info = new List<ConsignmentOfGoods>();
        }

        public int NumOfRoom
        {
            get => _numOfRoom;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Room number must be a positive number.", nameof(NumOfRoom));
                }
                if (_numOfRoom != value)
                {
                    _numOfRoom = value;
                    OnPropertyChanged(nameof(NumOfRoom));
                    OnPropertyChanged(nameof(ShortString));
                }
            }
        }

        public decimal RoomPrice 
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

        public List<ConsignmentOfGoods> Info
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
    }
}