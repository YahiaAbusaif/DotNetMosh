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


            lock (_context.MoviesList)
            {
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
        //this function will be called when the customer give the Movie back 
        public IHttpActionResult UpdateRental(RentalDto Rental)
        {
            var rentalInDB = _context.Rentals.Single(c => c.ID == Rental.ID);

            //to impelemnt : Add option for getting Rental based on CustomerID and MovieID
            //this option will help if user miss the rental ID but this case should handel  
            //if the user rents multi copy of the same Movie 
            //var rentalInDB = _context.Rentals.Single(c => c.Customer.ID == Rental.CustomerID && c.Movie.ID == Rental.MovieID);
            
            if(rentalInDB ==null)
                return BadRequest("No rental with that ID");
            rentalInDB.ReturnDate=DateTime.Now;

            var price=_context.MoviesList.Single(c => c.ID == Rental.MovieID).RentalPriceInCentsForDay;

            var durition=(rentalInDB.ReturnDate - rentalInDB.RentalDate ).Value.Days+1 ;

            var idx=0;
            for(;idx<PricingPolicy.MaxNumberOfDays.Length;idx++)
            {
                if(PricingPolicy.MaxNumberOfDays[idx]>durition)
                    break;
            }
            if(idx==PricingPolicy.MaxNumberOfDays.Length)
                idx--;
            var pricepolicy= PricingPolicy.PrecentPrice[idx];
            
            var totalPrice=(pricepolicy/100)*durition*price;


            var customer= _context.CustomersList.Single(c => c.ID == Rental.CustomerID);


            //check for promoCode
            if(Rental.PromoCodeName!="")
            {
                var PromoInDB=_context.PromoCodes.Single(c => c.Name == Rental.PromoCodeName);
                if(PromoInDB!=null)
                {
                    var countUse=_context.Rentals.ToList()
                        .Count(c => c.Promo == PromoInDB && c.Customer.ID==Rental.CustomerID);
                    if(PromoInDB.NumberofUse<countUse)
                    {
                        if(PromoInDB.MembershipRequired==customer.MembershipType)
                        {
                            var newprice=(PromoInDB.Discount/100)*totalPrice;
                            if(totalPrice-newprice>PromoInDB.MaxDiscountInCents)
                                totalPrice=totalPrice-PromoInDB.MaxDiscountInCents;
                            else
                                totalPrice=newprice;

                        }
                    }
                }

            }



             return Ok("OK Movie is back, Required Money is" + totalPrice.ToString());
        }
    }
}
