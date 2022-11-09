using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModel;

namespace TypeLibrary.Interface
{
    public interface IDBAccess
    {
        bool SignUpAdmin(Admin admin);
        bool AddDVDStock(DvdStorage dvdStorage);
        bool AddCustomer(Customer customer);
        bool AddRental(Rental rental);
        bool UpdateDvd(UpdateDvd dvdStorage);
        bool UpdateCustomer(UpdateCustomer customer);
        bool UpdateRental(UpdateRental rental);
        UspAdminSignIn SignIn(string email);
        UspGetCustomer GetCustomer(int customerId);
        UspAdminExist AdminExists(string email);
        UspGetRentalById GetRentalById(int rentalId);
        List<UspGetCustomers> GetCustomers();
        UspGetDvd GetDvdById(int dvdId);
        List<UspGetDvdList> GetDvdList();
        List<UspGetRentals> GetRentalList();
        List<UspGetRentalByStatus> GetRentalByStatus(string status);

    }
}
