using AutoMapper;
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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        // GET api/Customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.CustomersList.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        // GET api/Customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer,CustomerDto>(customer);
        }

        [HttpPost]
        // POST api/Customers
        public CustomerDto CreateCustomer(CustomerDto CustomerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customer = Mapper.Map<CustomerDto,Customer>(CustomerDto);


            try
            {
                _context.CustomersList.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            CustomerDto.ID=customer.ID;

            return CustomerDto;
        }

        // PUT api/Customers/5
        [HttpPut]
        public void PUTCustomer(int id, CustomerDto CustomerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            var customerInDB = _context.CustomersList.SingleOrDefault(c => c.ID == id);

            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(CustomerDto,customerInDB);

            /*
            customerInDB.Name=CustomerDto.Name;
            customerInDB.IsSubscribedToNewsletter=CustomerDto.IsSubscribedToNewsletter;
            customerInDB.MembershipTypeID=CustomerDto.MembershipTypeID;
            customerInDB.BirthDate=CustomerDto.BirthDate;*/
            
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
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
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }
    }
}
