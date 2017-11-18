using FullStack.Models;
using FullStack.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FullStack.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        { _context = new ApplicationDbContext(); }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {

            var model = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel model)
        {

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse(string.Format("{0} {1}", model.Date, model.Time)),

                GenreId = model.Genre,
                Venue = model.Venue



            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("index", "Home");


        }
    }

}