using FullStack.Dto;
using FullStack.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace FullStack.Controllers
{
    [Authorize]
    public class AttendencesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendencesController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userid = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userid && a.GigId == dto.GigId))
                return BadRequest("The attendace already exists ");


            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userid
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
