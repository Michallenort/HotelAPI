using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int GuestsNumber { get; set; }
        public int PriceForNight { get; set; }
    }
}
