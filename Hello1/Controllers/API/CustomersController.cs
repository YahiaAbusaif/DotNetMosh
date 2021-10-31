using AutoMapper;
using Hello1.Dtos;
using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Hello1.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET api/Customers
        public IHttpActionResult GetCustomers()
        {
            var customerdto = _context.CustomersList
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerdto);
        }

        // GET api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        // POST api/Customers
        public IHttpActionResult CreateCustomer(CustomerDto CustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(CustomerDto);


            try
            {
                _context.CustomersList.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //throw new HttpResponseException(HttpStatusCode.InternalServerError);
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError,
                    "Internal Error, database can't add new customer, try later."));

            }

            CustomerDto.ID = customer.ID;

            return Created(new Uri(Request.RequestUri + "/" + customer.ID), CustomerDto);
        }

        // PUT api/Customers/5
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto CustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customerInDB == null)
                return NotFound();

            Mapper.Map(CustomerDto, customerInDB);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError,
                    "Internal Error, database can't edit the customer, try later."));
            }
            return Ok();

        }

        // DELETE api/Customers/5
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {


            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customerInDB == null)
                return NotFound();

            try
            {
                _context.CustomersList.Remove(customerInDB);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse
                  (HttpStatusCode.InternalServerError,
                  "Internal Error, database can't delete the customer, try later."));
            }
            return Ok();
        }
    }
}
