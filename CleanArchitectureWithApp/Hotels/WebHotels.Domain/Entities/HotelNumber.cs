using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHotels.Domain.Entities
{
    public class HotelNumber
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Display(Name = "Hotel Number")]
            public int Hotel_Number { get; set; }

            [ForeignKey("Hotel")]
            public int HotelId { get; set; }
            public Hotel Hotel{ get; set; }
            public string? SpecialDetails { get; set; }
    }
}
