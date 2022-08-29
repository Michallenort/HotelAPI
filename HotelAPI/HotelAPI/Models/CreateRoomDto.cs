using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class CreateRoomDto
    {
        [Required]
        public int Number { get; set; }
        public string Type { get; set; }
        public int GuestsNumber { get; set; }
        public int Price { get; set; }

        public int HotelId { get; set; }
    }
}
