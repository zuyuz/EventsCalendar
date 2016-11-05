using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsScheduler.Entities
{
    [Table("Events")]
    class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int FreePlaces { get; set; }

        [Required]
        public Location EventLocation { get; set; }

        public virtual List<User> Participants { get; set; }
    }
}
