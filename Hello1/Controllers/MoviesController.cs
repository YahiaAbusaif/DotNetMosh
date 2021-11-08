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
        [AllowAnonymous]
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "name";

            var movies = new Movies() { MoviesList = new List<ViewMovie>() };

            var MoviesList = _context.MoviesList.ToList();
            foreach (var Movie in MoviesList)
            {

                var GenersIDs = _context.MovieGenre
                         .Where(Gm => Gm.MovieID == Movie.ID)
                         .Select(G => G.GenreID).ToList();
                var GenresList = _context.GenresList.Where(G => GenersIDs.Contains(G.ID));

                movies.MoviesList.Add(
                    new ViewMovie
                    {
                        Movie = Movie,
                        Genres = GenresList
                    }
                    );

            }

            if(User.IsInRole(RoleName.CanMangeMovies))
                return View("Index");

            return View("ViewOnlyList");
        }

        [Route("Movies/Details/{ID}")]
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");
            var viewMovie = new ViewMovie();
            viewMovie.Movie = _context.MoviesList.SingleOrDefault(m => m.ID == Id);

            if (viewMovie.Movie == null)
                return Content("Can't Find Movie with that ID");

            
            var GenersIDs = _context.MovieGenre
                            .Where(Gm => Gm.MovieID == Id)
                            .Select(G => G.GenreID).ToList();
            viewMovie.Genres = _context.GenresList.Where(G => GenersIDs.Contains(G.ID));

            

            return View(viewMovie);

        }

        [Route("Movies/Released/{year:regex(\\d{4}):range(1900,2050)}/{month:range(1,12)}")]
        public ActionResult ByReleasedDate(int? year, int? month)
        {
            if (!year.HasValue)
                year = -1;
            if (!month.HasValue)
                month = 13;


            return Content(string.Format("year: {0} month: {1}", year, month));
        }

        [Authorize(Roles = RoleName.CanMangeMovies)]
        public ActionResult New()
        {
            var ViewMovie = new ViewMovie()
            {
                Movie = new Movie(),
                Genres = _context.GenresList.ToList()
            };


            return View("Save", ViewMovie);
        }

        [Route("Movies/Edit/{ID}")]
        [Authorize(Roles = RoleName.CanMangeMovies)]
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var movie = _context.MoviesList.SingleOrDefault(m => m.ID == Id);
            if (movie != null)
            {
                var ViewMovie = new ViewMovie()
                {
                    Movie = movie,
                    Genres = _context.GenresList.ToList()
                };
                return View("Save", ViewMovie);
            }


            return Content("Can't Find Movie with that ID");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanMangeMovies)]
        public ActionResult Save(ViewMovie Viewmovie)
        {
            if (!ModelState.IsValid)
                return View("Save", Viewmovie);


            if (Viewmovie.Movie.ID == 0)
            {
                _context.MoviesList.Add(Viewmovie.Movie);
            }
            else
            {
                try
                {
                    var MovieInDB = _context.MoviesList.Single(m => m.ID == Viewmovie.Movie.ID);

                    MovieInDB.Name = Viewmovie.Movie.Name;
                    MovieInDB.Release = Viewmovie.Movie.Release;
                    MovieInDB.Dirctor = Viewmovie.Movie.Dirctor;
                    MovieInDB.Price = Viewmovie.Movie.Price;
                    MovieInDB.NumberInStock = Viewmovie.Movie.NumberInStock;

                }
                catch (Exception e)
                {
                    //trying to edit non existing Movie
                    return Content("Ops trying to edit non existing Movie, error" + e);

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


            return RedirectToAction("Index", "Movies");
        }

        [Route("Movies/Genres/{ID}")]
        [Authorize(Roles = RoleName.CanMangeMovies)]
        public static void AddGenresToMovie(ViewMovie viewMovie)
        {
            //foreach (var Genres in viewMovie.GenresList)

        }


    }
}