using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsScheduler.DAL.Entities
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
            int prime = 31;
            int result = 1;
            result = prime * result + ((Name == null) ? 0 : Name.GetHashCode());
            result = prime * result + ((StartTime == null) ? 0 : StartTime.GetHashCode());
            result = prime * result + Id.GetHashCode();
            result = prime * result + ((EndTime == null) ? 0 : EndTime.GetHashCode());
            result = prime * result + FreePlaces.GetHashCode();
            result = prime * result + EventLocation.Address.GetHashCode() + EventLocation.Id;
            result = prime * result + ((Creator == null) ? 0 : Creator.GetHashCode());
            result = prime * result + ((Participants == null) ? 0 : Participants.GetHashCode());

            return result;
        }
    }
}
