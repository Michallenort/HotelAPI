using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }

        public virtual Room Room { get; set; }
        public int RoomId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
