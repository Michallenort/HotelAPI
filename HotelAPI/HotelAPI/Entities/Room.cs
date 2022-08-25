using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public int GuestsNumber { get; set; }

        public virtual Hotel Hotel { get; set; }
        public int HotelId { get; set; }

        public virtual List<Reservation> Reservations { get; set; }
    }
}
