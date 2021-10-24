using Hello1.Dtos;
using Hello1.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hello1.Controllers.API
{
    public class MoviesController : ApiController
    {
         private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }


        // GET api/Movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.MoviesList.ToList().Select(Mapper.Map<Movie,MovieDto>));
        }

        // GET api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var Movie = _context.MoviesList.SingleOrDefault(m => m.ID == id);

            if (Movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(Movie));
        }

        [HttpPost]
        // POST api/Movies
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var Movie = Mapper.Map<MovieDto,Movie>(MovieDto);

            try
            {
                _context.MoviesList.Add(Movie);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, 
                    "Internal Error, database can't add new Movie, try later."));
               
            }

            MovieDto.ID=Movie.ID;

            return Created(new Uri(Request.RequestUri + "/" + Movie.ID),MovieDto);
        }

        // PUT api/Movies/5
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var MovieInDB = _context.MoviesList.SingleOrDefault(m => m.ID == id);

            if (MovieInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(MovieDto,MovieInDB);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError,
                    "Internal Error, database can't edit the Movie, try later."));
            }

            return Ok("Movie updated");

        }

        // DELETE api/Movies/5
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {


            var MovieInDB = _context.MoviesList.SingleOrDefault(c => c.ID == id);

            if (MovieInDB == null)
                return NotFound();

            try
            {
                _context.MoviesList.Remove(MovieInDB);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, 
                    "Internal Error database can't delete the movie, try later"));
            }

            return Ok("Movie deleted");

        }


    }
}
