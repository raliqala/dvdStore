using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Interface;
using TypeLibrary.Models;
using TypeLibrary.ViewModel;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class DBHandler : IDBHandler
    {
         readonly IDBAccess db;

        public DBHandler()
        {
            db = new DBAccess();
        }

        public bool BLL_SignUpAdmin(Admin admin)
        {
            return db.SignUpAdmin(admin);
        }

        public UspAdminSignIn BLL_SignIn(string email)
        {
            return db.SignIn(email);
        }

        public UspAdminExist BLL_uspAdminExist(string email)
        {
            return db.AdminExists(email);
        }

        public bool BLL_AddDVDStock(DvdStorage dvdStorage)
        {
            return db.AddDVDStock(dvdStorage);
        }

        public bool BLL_AddCustomer(Customer customer) 
        { 
            return db.AddCustomer(customer);
        }

        public bool BLL_AddRental(Rental rental) 
        { 
            return db.AddRental(rental);
        }

        public bool BLL_UpdateDvd(UpdateDvd dvdStorage)
        {
            return db.UpdateDvd(dvdStorage);
        }

        public bool BLL_UpdateCustomer(UpdateCustomer customer)
        {
            return db.UpdateCustomer(customer);
        }

        public bool BLL_UpdateRental(UpdateRental rental)
        {
            return db.UpdateRental(rental);
        }

        public UspGetCustomer BLL_GetCustomer(int customerId)
        {
            return db.GetCustomer(customerId);
        }

        public UspGetRentalById BLL_GetRentalById(int rentalId) 
        { 
            return db.GetRentalById(rentalId);
        }

        public List<UspGetCustomers> BLL_GetCustomers() 
        { 
            return db.GetCustomers();
        }

        public UspGetDvd BLL_GetDvdById(int dvdId) 
        { 
            return db.GetDvdById(dvdId);
        }

        public List<UspGetDvdList> BLL_GetDvdList() 
        { 
            return db.GetDvdList();
        }

        public List<UspGetRentals> BLL_GetRentalList() 
        { 
            return db.GetRentalList();
        }

        public List<UspGetRentalByStatus> BLL_GetRentalByStatus(string status) 
        { 
            return db.GetRentalByStatus(status);
        }
    }
}
