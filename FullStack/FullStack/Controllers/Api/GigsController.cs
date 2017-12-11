using FullStack.Models;
using Microsoft.AspNet.Identity;
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
            _context.SaveChanges();
            return Ok();
        }
    }
}
