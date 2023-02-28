using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class ReservationDto
    {
        public string HotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }

        public int RoomNumber { get; set; }
        public int GuestsNumber { get; set; }

        public string GuestName { get; set; }
        public string Email { get; set; }
    }
}
