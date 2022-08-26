using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Models
{
    public class CreateHotelDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(25)]
        public string Description { get; set; }
        [MaxLength(15)]
        public string Category { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(25)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
