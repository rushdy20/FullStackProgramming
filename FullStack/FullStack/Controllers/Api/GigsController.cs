using FullStack.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace FullStack.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            gigs.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Gig = gigs,
                Type = NotificationType.GigCanceled
            };
            var attendees = _context.Attendances.Where(a => a.GigId == gigs.Id).Select(a => a.Attendee).ToList();

            foreach (var userNotification in attendees.Select(attendee => new UserNotification
            {
                Notification = notification,
                User = attendee
            }))
            {
                _context.UserNotifications.Add(userNotification);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
