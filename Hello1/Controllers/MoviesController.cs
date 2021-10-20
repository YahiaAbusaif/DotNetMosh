using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hello1.Models.ViewModel;


namespace Hello1.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "name";

            Movies movies = new Movies();
            movies.MoviesList = _context.MoviesList.ToList();


            return View(movies);
        }

        [Route("Movies/Details/{ID}")]
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");
            var Target = _context.MoviesList.SingleOrDefault(c => c.ID == Id);
            if(Target != null)
                return View(Target);


            return Content("Can't Find Movie with that ID");
        }

        [Route("Movies/Released/{year:regex(\\d{4}):range(1990,2050)}/{month:range(1,12)}")]
        public ActionResult ByReleasedDate(int? year,int? month)
        {
            if (!year.HasValue)
                year = -1;
            if (!month.HasValue)
                month = 13;


            return Content(string.Format("year: {0} month: {1}",year,month));
        }


        public ActionResult New()
        {
            var Movie = new Movie();
            

            return View("Save",Movie);
        }

        [Route("Movies/Edit/{ID}")]
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var movie = _context.MoviesList.SingleOrDefault(c => c.ID == Id);
            if (movie != null)
                return View("Save", movie);

            return Content("Can't Find Movie with that ID");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid) 
                return View("Save",movie);


            if (movie.ID == 0)
                _context.MoviesList.Add(movie);
            else{
                try
                {
                    var MovieInDB = _context.MoviesList.Single(c => c.ID == movie.ID);

                    MovieInDB.Name = movie.Name;
                    MovieInDB.Release = movie.Release;
                    MovieInDB.Dirctor = movie.Dirctor;

                }
                catch
                {
                    //trying to edit non existing Movie
                    return Content("Ops trying to edit non existing Movie");

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


            return RedirectToAction("Index","Movies");
        }


    }
}