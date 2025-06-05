using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_4.Classes;
using Lab_4.Enum;

namespace Lab_4.DTO_s
{
    public class ConsignmentOfGoodsDTO
    {
        public Vegetables Vegetables { get; set; }
        public Delivery TypeOfDelivery { get; set; }
        public int Quantity { get; set; }
        public int PriceForOne { get; set; }
        public int PriceForTransport { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}
