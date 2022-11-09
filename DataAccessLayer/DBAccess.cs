using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Interface;
using TypeLibrary.Models;
using TypeLibrary.ViewModel;

namespace DataAccessLayer
{
    public class DBAccess : IDBAccess
    {
        public bool SignUpAdmin(Admin admin)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in admin.GetType().GetProperties())
            {
                if (prop.GetValue(admin) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(admin)));
                }
            }
            return DBHelper.NonQuery("uspAdminSignUp", CommandType.StoredProcedure, parameters.ToArray());
        }

        public UspAdminExist AdminExists(string email)
        {
            UspAdminExist verified = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Email", email),
            };
            using (DataTable table = DBHelper.ParamSelect("uspAdminExist", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    verified = new UspAdminExist
                    {
                        Id = Convert.ToInt64(row["Id"]),
                        Name = row["Name"].ToString(),
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString(),
                    };
                }
            }
            return verified;
        }

        public UspAdminSignIn SignIn(string email)
        {
            UspAdminSignIn signIn = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Email", email),
            };
            using (DataTable table = DBHelper.ParamSelect("uspAdminSignIn", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    signIn = new UspAdminSignIn
                    {
                        Password = row["Password"].ToString(),
                    };
                }
            }
            return signIn;
        }

        public bool AddCustomer(Customer customer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in customer.GetType().GetProperties())
            {
                if (prop.GetValue(customer) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(customer)));
                }
            }
            return DBHelper.NonQuery("uspAddCustomer", CommandType.StoredProcedure, parameters.ToArray());
        }

        public bool AddDVDStock(DvdStorage dvdStorage)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in dvdStorage.GetType().GetProperties())
            {
                if (prop.GetValue(dvdStorage) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(dvdStorage)));
                }
            }
            return DBHelper.NonQuery("uspAddDVD", CommandType.StoredProcedure, parameters.ToArray());
        }

        public bool AddRental(Rental rental)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in rental.GetType().GetProperties())
            {
                if (prop.GetValue(rental) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(rental)));
                }
            }
            return DBHelper.NonQuery("uspAddRentals", CommandType.StoredProcedure, parameters.ToArray());
        }

        public List<UspGetRentalByStatus> GetRentalByStatus(string status)
        {
            List<UspGetRentalByStatus> list = new List<UspGetRentalByStatus>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Status", status),
            };

            using (DataTable table = DBHelper.ParamSelect("uspGetRentalByStatus", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        UspGetRentalByStatus rentals = new UspGetRentalByStatus
                        {
                            status = row["status"].ToString(),
                            overDueAmount = Convert.ToDecimal(row["overDueAmount"]),
                            rentalDate = row["rentalDate"].ToString(),
                            returnDate = row["returnDate"].ToString(),
                            Title = row["title"].ToString(),
                            RentalPrice = Convert.ToDecimal(row["RentalPrice"]),
                            Description = row["description"].ToString(),
                            CustomerName = row["CustomerName"].ToString(),
                            PhoneNumber = Convert.ToInt64(row["PhoneNumber"]),
                        };
                        list.Add(rentals);
                    }
                }//end if
            }//end using
            return list;
        }//End GetRentalList

        public UspGetRentalById GetRentalById(int rentalId)
        {
            UspGetRentalById rental = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Id", rentalId),
            };
            using (DataTable table = DBHelper.ParamSelect("uspGetRentalById", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    rental = new UspGetRentalById
                    {
                        status = row["status"].ToString(),
                        overDueAmount = Convert.ToDecimal(row["overDueAmount"]),
                        rentalDate = row["rentalDate"].ToString(),
                        returnDate = row["returnDate"].ToString(),
                        Title = row["title"].ToString(),
                        RentalPrice = Convert.ToDecimal(row["RentalPrice"]),
                        Description = row["description"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PhoneNumber = Convert.ToInt64(row["PhoneNumber"]),
                    };
                }
            }
            return rental;
        }//End GetRentalById

        public List<UspGetRentals> GetRentalList()
        {
            List<UspGetRentals> list = new List<UspGetRentals>();
            using (DataTable table = DBHelper.Select("uspGetRentals",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        UspGetRentals retal = new UspGetRentals
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            status = row["status"].ToString(),
                            overDueAmount = Convert.ToDecimal(row["overDueAmount"]),
                            rentalDate = row["rentalDate"].ToString(),
                            returnDate = row["returnDate"].ToString(),
                            Title = row["title"].ToString(),
                            RentalPrice = Convert.ToDecimal(row["RentalPrice"]),
                            Description = row["description"].ToString(),
                            CustomerName = row["CustomerName"].ToString(),
                            PhoneNumber = Convert.ToInt64(row["PhoneNumber"]),
                        };
                        list.Add(retal);
                    }
                }//end if
            }//end using
            return list;
        }//End GetRentalList

        public List<UspGetDvdList> GetDvdList()
        {
            List<UspGetDvdList> list = new List<UspGetDvdList>();
            using (DataTable table = DBHelper.Select("uspGetDvdList",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        UspGetDvdList dvdStorage = new UspGetDvdList
                        {
                            Id = Convert.ToInt32(row["id"]),
                            Title = row["title"].ToString(),
                            Description = row["description"].ToString(),
                            RentalPrice = Convert.ToDecimal(row["RentalPrice"]),
                            Quantity = Convert.ToInt32(row["Quantity"]),
                            Status = row["status"].ToString(),
                            adminId = Convert.ToInt32(row["adminId"]),
                            Slug = row["slug"].ToString(),
                        };
                        list.Add(dvdStorage);
                    }
                }//end if
            }//end using
            return list;
        }//End GetDvdList

        public UspGetDvd GetDvdById(int dvdId)
        {
            UspGetDvd dvdStorage = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Id", dvdId),
            };
            using (DataTable table = DBHelper.ParamSelect("uspGetDvd", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    dvdStorage = new UspGetDvd
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Title = row["title"].ToString(),
                        Description = row["description"].ToString(),
                        RentalPrice = Convert.ToDecimal(row["RentalPrice"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Status = row["status"].ToString(),
                        adminId = Convert.ToInt32(row["adminId"]),
                        Slug = row["slug"].ToString(),
                    };
                }
            }
            return dvdStorage;
        }//End GetDvdById

        public List<UspGetCustomers> GetCustomers()
        {
            List<UspGetCustomers> list = new List<UspGetCustomers>();
            using (DataTable table = DBHelper.Select("uspGetCustomers",
                CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        UspGetCustomers customers = new UspGetCustomers
                        {
                            Id = Convert.ToInt64(row["id"]),
                            Name = row["name"].ToString(),
                            Address = row["address"].ToString(),
                            PhoneNumber = Convert.ToInt64(row["PhoneNumber"]),
                        };
                        list.Add(customers);
                    }
                }//end if
            }//end using
            return list;
        }//End GetCustomers

        public UspGetCustomer GetCustomer(int customerId)
        {
            UspGetCustomer customer = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@Id", customerId),
            };
            using (DataTable table = DBHelper.ParamSelect("uspGetCustomer", CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    customer = new UspGetCustomer
                    {
                        Id = Convert.ToInt64(row["id"]),
                        Name = row["name"].ToString(),
                        Address = row["address"].ToString(),
                        PhoneNumber = Convert.ToInt64(row["PhoneNumber"]),
                    };
                }
            }
            return customer;
        }//End GetCustomer


        // Updating procedures
        public bool UpdateDvd(UpdateDvd dvdStorage)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in dvdStorage.GetType().GetProperties())
            {
                if (prop.GetValue(dvdStorage) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(dvdStorage)));
                }
            }
            return DBHelper.NonQuery("uspUpdateDvd", CommandType.StoredProcedure, parameters.ToArray());
        }

        public bool UpdateCustomer(UpdateCustomer customer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in customer.GetType().GetProperties())
            {
                if (prop.GetValue(customer) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(customer)));
                }
            }
            return DBHelper.NonQuery("uspUpdateCustomer", CommandType.StoredProcedure, parameters.ToArray());
        }

        public bool UpdateRental(UpdateRental rental)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var prop in rental.GetType().GetProperties())
            {
                if (prop.GetValue(rental) != null)
                {
                    parameters.Add(new SqlParameter("@" + prop.Name.ToString(), prop.GetValue(rental)));
                }
            }
            return DBHelper.NonQuery("uspUpdateRental", CommandType.StoredProcedure, parameters.ToArray());
        }
    }
}