using Hello1.Dtos;
using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hello1.Controllers.API
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult NewRental(NewRentalDto NewRental)
        {
            if(NewRental.MoviesID.Count==0)
                return BadRequest("the list of Movie IDs is empty");

            var customerInDB = _context.CustomersList.Single(c => c.ID == NewRental.CustomerID);
            /*
            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == NewRental.CustomerID);
            if(customerInDB==null)
                return BadRequest("customer not valid");
            */

            var movies = _context.MoviesList.Where(m => NewRental.MoviesID.Contains(m.ID)).ToList();

            /*
            if (movies.Count != NewRental.MoviesID.Count)
                return BadRequest("some Movies IDs are invalid");
            */

            foreach (var movie in movies)
            {
                if(movie.NumberAvailable==0)
                    return BadRequest("Movie "+ movie.Name+ " isn't available.");
            }


            foreach (var CurrentMovie in movies)
            {
                CurrentMovie.NumberAvailable--;
                Rental rental = new Rental()
                {
                    Customer = customerInDB,
                    Movie = CurrentMovie,
                    RentalDate = DateTime.Now
                };
                _context.Rentals.Add(rental);

            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError,
                    "Internal Error, database can't add new Rental, try later."));
            }

            return Ok("done");
            //return Created(new Uri(Request.RequestUri + "/" + Customer.ID),CustomerRentalList);
        }

        [HttpPut]
        public IHttpActionResult UpdateRental(Rental Rental)
        {
            throw new NotImplementedException();
        }
    }
}
