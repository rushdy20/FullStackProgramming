using System;
using System.ComponentModel.DataAnnotations;

namespace FullStack.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }
        public DateTime? OrigianDateTime { get; set; }
        public string OrinigalVenue { get; set; }

        [Required]
        public Gig Gig { get; set; }


    }
}