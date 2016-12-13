using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsScheduler.Entities
{
    [Table("Events")]
    public class Event: IEquatable<Event>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int FreePlaces { get; set; }

        [Required]
        public virtual Location EventLocation { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        public virtual List<User> Participants { get; set; }

        public bool Equals(Event ev)
        {
            return Name == ev.Name
                && StartTime == ev.StartTime
                && Id == ev.Id
                && EndTime == ev.EndTime
                && FreePlaces == ev.FreePlaces
                && EventLocation.Address == ev.EventLocation.Address
                && Creator == ev.Creator
                && Participants == ev.Participants ? false : true; 
        }

        public override bool Equals(object obj)
        {
            var ev = obj as Event;
            if(ev != null)
            {
                return Equals(ev);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
