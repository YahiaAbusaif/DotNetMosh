using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hello1.Controllers
{
    public class GenresController : Controller
    {
        // GET: Customers
        private ApplicationDbContext _context;


        public GenresController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {

            return View("Index");
        }


        [Route("Genres/Details/{ID}")]
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var Genre = _context.GenresList.SingleOrDefault(c=> c.ID ==Id);
            if(Genre!=null)
                return View(Genre);

            return Content("Can't Find Genre with that ID");
        }

        [Authorize(Roles = RoleName.CanMangeMovies)]
        public ActionResult New()
        {
            
            return View("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanMangeMovies)]
        public ActionResult Save(Genre Genre)
        {
            if(!ModelState.IsValid)
            { 
                return View("New");
            }

            if (Genre.ID == 0)
                _context.GenresList.Add(Genre);
            else{
                try
                {
                    var GenreInDB = _context.GenresList.Single(c => c.ID == Genre.ID);

                    GenreInDB.Name = Genre.Name;
                    GenreInDB.MoviesNumber = Genre.MoviesNumber;

                }
                catch
                {
                    //trying to edit non existing customer
                    return Content("Ops trying to edit non existing Genre");
                }
            }

            try
            {
                _context.SaveChanges();

            }
            catch (Exception e)
            {

                return Content("Error can't update DB \n" + e);
            }


            return RedirectToAction("Index","Genres");
        }

        [Authorize(Roles = RoleName.CanMangeMovies)]
        [Route("Genres/Edit/{ID}")]
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var Genre = _context.GenresList.SingleOrDefault(c => c.ID == Id);
            if (Genre != null)
            {

                return View("New");
            }
             

            return Content("Can't Find Genre with that ID");
        }
    }
}