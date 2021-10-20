using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hello1.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        // GET api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.CustomersList.ToList();
        }

        // GET api/Customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        [HttpPost]
        // POST api/Customers
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                _context.CustomersList.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return customer;
        }

        // PUT api/Customers/5
        [HttpPut]
        public void PUTCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDB.Name=customer.Name;
            customerInDB.IsSubscribedToNewsletter=customer.IsSubscribedToNewsletter;
            customerInDB.MembershipTypeID=customer.MembershipTypeID;
            customerInDB.BirthDate=customer.BirthDate;
            
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }


        }

        // DELETE api/Customers/5
        [HttpDelete]
        public void DeleteCustomer(int id)
        {


            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            try
            {
                _context.CustomersList.Remove(customerInDB);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }
    }
}
