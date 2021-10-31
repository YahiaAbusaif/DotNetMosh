using Hello1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Hello1.Models.ViewModel;

namespace Hello1.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //Customers Customers = new Customers();
            //Customers.CustomersList = _context.CustomersList.Include(c => c.MembershipType).ToList();
            
            return View("Index");
        }


        [Route("Customers/Details/{ID}")]
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var Customer = _context.CustomersList.Include(c => c.MembershipType).SingleOrDefault(c=> c.ID ==Id);
            if(Customer!=null)
                return View(Customer);

            return Content("Can't Find User with that ID");
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypeList.ToList();
            var ViewModel = new NewCustomer()
            {
                customer=new Customer(),
                MembershipTypes= membershipTypes
            };
            

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            { 
                var viewModel= new NewCustomer()
                {
                    customer=customer,
                    MembershipTypes=_context.MembershipTypeList.ToList()
                };
                
                return View("New",viewModel);
            }

            if (customer.ID == 0)
                _context.CustomersList.Add(customer);
            else{
                try
                {
                    var customerInDB = _context.CustomersList.Single(c => c.ID == customer.ID);

                    //TryUpdateModel(customerInDB, "", new string[] { "Name", "BirthDate" });

                    //Mapper.Map(customer,costomerInDB)

                    customerInDB.Name = customer.Name;
                    customerInDB.BirthDate = customer.BirthDate;
                    customerInDB.MembershipTypeID = customer.MembershipTypeID;
                    customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                }
                catch
                {
                    //trying to edit non existing customer
                    return Content("Ops trying to edit non existing customer");
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


            return RedirectToAction("Index","Customers");
        }


        [Route("Customers/Edit/{ID}")]
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return Content("Invalid ID please try again with valid ID");

            var Customer = _context.CustomersList.Include(c => c.MembershipType).SingleOrDefault(c => c.ID == Id);
            if (Customer != null)
            {
                var ViewModel = new NewCustomer() {
                    customer = Customer,
                    MembershipTypes = _context.MembershipTypeList.ToList()
                    };


                return View("New", ViewModel);
            }
             

            return Content("Can't Find User with that ID");
        }

    }
}