using System.Linq;
using System.Web.Mvc;
using FullStack.Models;
using FullStack.ViewModels;

namespace FullStack.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        { _context = new ApplicationDbContext();}
        // GET: Gigs
        public ActionResult Create()
        {

            var model = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(model);
        }
    }
}