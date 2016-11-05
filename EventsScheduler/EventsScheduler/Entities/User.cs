﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsScheduler.Entities
{
    [Table("Users")]
    class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role UserRole { get; set; }

        public virtual List<Event> Events { get; set; }

        public enum Role
        {
            Admin,
            User,
            Guest
        }
    }
}