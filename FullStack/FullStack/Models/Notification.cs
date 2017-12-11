using System;
using System.ComponentModel.DataAnnotations;

namespace FullStack.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }
        public DateTime? OrigianDateTime { get; private set; }
        public string OrinigalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");
            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
            if (type != NotificationType.GigUpdated) return;
            OrigianDateTime = gig.DateTime;
            OrinigalVenue = gig.Venue;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }
        public static Notification GigUpdated(Gig orignalGig, DateTime orignalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, orignalGig);
            notification.OrigianDateTime = orignalDateTime;

            notification.OrinigalVenue = originalVenue;
            return notification;
        }
        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }




    }
}