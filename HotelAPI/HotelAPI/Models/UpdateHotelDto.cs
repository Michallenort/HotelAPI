using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class UpdateHotelDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(25)]
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
