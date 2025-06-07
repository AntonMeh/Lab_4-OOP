using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.DTO_s
{
    public class CompositionDTO
    {
        public int NumOfRoom { get; set; }
        public int RoomPrice { get; set; }

        public List<ConsignmentOfGoodsDTO> InfoOfGoods { get; set; } = new List<ConsignmentOfGoodsDTO>();
    }
}
