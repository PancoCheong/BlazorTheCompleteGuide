// DataAccess
// Data\HotelRoom.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class HotelRoom
    {
        [Key] // Optional attribute if the field name is ended with Id (eg. Id or HotelRoomId)
        public int Id { get; set; }
        [Required] // Not Null
        public string Name { get; set; }
        [Required] // Not Null
        public int Occupancy { get; set; }
        [Required] // Not Null
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        // or use DateTime.Now for local time for Default Value
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
