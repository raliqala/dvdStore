using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModel;

namespace TypeLibrary.Interface
{
    public interface IDBHandler
    {
        bool BLL_SignUpAdmin(Admin admin);
        bool BLL_AddDVDStock(DvdStorage dvdStorage);
        UspAdminExist BLL_uspAdminExist(string email);
        UspAdminSignIn BLL_SignIn(string email);
        bool BLL_AddCustomer(Customer customer);
        bool BLL_AddRental(Rental rental);
        bool BLL_UpdateDvd(UpdateDvd dvdStorage);
        bool BLL_UpdateCustomer(UpdateCustomer customer);
        bool BLL_UpdateRental(UpdateRental rental);
        UspGetCustomer BLL_GetCustomer(int customerId);
        UspGetRentalById BLL_GetRentalById(int rentalId);
        List<UspGetCustomers> BLL_GetCustomers();
        UspGetDvd BLL_GetDvdById(int dvdId);
        List<UspGetDvdList> BLL_GetDvdList();
        List<UspGetRentals> BLL_GetRentalList();
        List<UspGetRentalByStatus> BLL_GetRentalByStatus(string status);
    }
}
