using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class UpdateRoomDto
    {
        public int Number { get; set; }
        public int GuestsNumber { get; set; }
        public int PriceForNight { get; set; }
    }
}
