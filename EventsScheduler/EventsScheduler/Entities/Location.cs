using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsScheduler.Entities
{
    [Table("Locations")]
    class Location
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
