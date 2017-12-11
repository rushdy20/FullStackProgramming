using FullStack.Models;
using FullStack.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gigs"
            };

            return View("GigForm", model);
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userid = User.Identity.GetUserId();

            var gigs = _context.Gigs.Where(g => g.ArtistId == userid && g.DateTime > DateTime.Now && !g.IsCanceled).Include(g => g.Genre).ToList();
            return View(gigs);


        }



        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                model.Heading = "Add a Gig";
                return View("GigForm", model);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = model.GetDateTime(),
                GenreId = model.Genre,
                Venue = model.Venue

            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");


        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var model = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Date = gigs.DateTime.ToString("d MMM yyyy"),
                Time = gigs.DateTime.ToString("HH:mm"),
                Genre = gigs.GenreId,
                Venue = gigs.Venue,
                Id = gigs.Id,
                Heading = "Edit a Gig"

            };

            return View("GigForm", model);
        }
        public ActionResult Update(GigFormViewModel model)
        {
            var userid = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                model.Heading = "Add a Gig";
                return View("GigForm", model);
            }
            var gig = _context.Gigs.Single(g => g.Id == model.Id && g.ArtistId == userid);
            gig.DateTime = model.GetDateTime();
            gig.Venue = model.Venue;
            gig.GenreId = model.Genre;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");


        }
    }

}