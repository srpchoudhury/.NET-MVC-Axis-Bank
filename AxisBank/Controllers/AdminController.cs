using AxisBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AxisBank.Controllers
{
  
    public class AdminController : Controller
    {
        /*
       Summary
       author: S Rudra Prasad Choudhury
       date of creation: 17/10/2019    
       description : This is a Project of banking system. This is the Admin controller of the project.
        */
        // GET: Admin
        public ActionResult AdminPanel()
        {
            Dashboard dashboard = new Dashboard();
            try
            {

                using (DBEntities db = new DBEntities())
                {

                    var totalCustomers = db.AxisBank_tblAllAccount.Where(x => x.IsActive == true).Count();
                    var totalEmployees = db.EmployeeList.Where(x => x.IsActive == true).Count();
                    double? totalDeposite = db.tblAccountStatement
                                                .Where(x => x.Credit != 0 && x.Credit != null)
                                                .Sum(x => (double?)x.Credit);

                    double? totalWithdraw = db.tblAccountStatement
                                                 .Where(x => x.Debit != 0 && x.Debit != null)
                                                 .Sum(x => (double?)x.Debit);
                    var totalDepositeToday = db.tblAccountStatement
                                                 .Where(x => x.Credit != 0 && x.Credit != null && x.Date == DateTime.Today)
                                                 .Sum(x => (double?)x.Credit);
                    var totalWithdrawToday = db.tblAccountStatement
                                                  .Where(x => x.Debit != 0 && x.Debit != null && x.Date == DateTime.Today)
                                                  .Sum(x => (double?)x.Debit);
                    var todayDay = DateTime.Today.DayOfWeek.ToString();


                    dashboard.totalCustomers = totalCustomers;
                    dashboard.totalEmployees = totalEmployees;
                    dashboard.totalDeposite = totalDeposite ?? 0;
                    dashboard.totalWithdraw = totalWithdraw ?? 0;
                    dashboard.totalDepositeToday = totalDepositeToday ?? 0;
                    dashboard.totalWithdrawToday = totalWithdrawToday ?? 0;
                    dashboard.todayDay = todayDay;


                    dashboard.todayDay = todayDay;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(dashboard);


        }

        //add employee
        public ActionResult AddEmployee()
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            return View();
        }

        //adding the employee details in the database
        [HttpPost]
        public ActionResult EmployeeAdded(EmployeeList empList)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        empList.CreatedOn = DateTime.Now;
                        empList.CreatedBy = Session["FirstName"] as string;
                        empList.IsActive = true;
                        db.EmployeeList.Add(empList);
                        db.SaveChanges();

                        if (empList.Id > 0)
                        {
                            db.AxisBank_tblLogin.Add(new AxisBank_tblLogin
                            {
                                UserName = empList.UserName,
                                Password = empList.Password,
                                RoleId = empList.Role,
                                CreatedOn = DateTime.Now,
                                CreatedBy = Session["FirstName"] as string,
                                IsActive = true
                            });

                            db.SaveChanges();

                            TempData["SuccessMessage"] = "Successfully registered!";
                            return RedirectToAction("ShowAllEmployee");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Registration failed. Please try again.";
                            return RedirectToAction("AddEmployee");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }

            TempData["ErrorMessage"] = "Registration failed. Please try again.";
            return RedirectToAction("AddEmployee");
        }

        //for show all users
        public ActionResult ShowAllUsers()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.IsActive == true).ToList();
                    
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //for show all employee
        public ActionResult ShowAllEmployee()
        {
            List<AllDetailsOfEmployee> allEmp = new List<AllDetailsOfEmployee>();
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    // var result = db.EmployeeList.Where(x => x.IsActive == true).ToList();
                    //join the two tables EmployeeList and Role table
                    var result = db.EmployeeList.Join(db.AxisBank_tblRole, emp => emp.Role, r => r.Id, (emp, r) => new { emp, r })
                         .Where(x => x.emp.IsActive == true)
                         .Select(s => new AllDetailsOfEmployee
                         {
                             EmployeeName = s.emp.EmployeeName,
                             UserName = s.emp.UserName,
                             Password = s.emp.Password,
                             RoleName = s.r.Role
                         }).ToList();


                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //for create new Account
        public ActionResult CreateNewAccount()
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            return View();

        }

        //for create new Account post action
        [HttpPost]
        public ActionResult CreateNewAccount(CustomerAndImageViewModel civm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        AxisBank_tblAllAccount axisBank_TblAllAccount = new AxisBank_tblAllAccount();
                        Random random = new Random();
                        var customerId = random.Next(10000, 99999);
                        axisBank_TblAllAccount.CustomerId = customerId.ToString();
                        axisBank_TblAllAccount.Password = civm.AxisAta.Password;
                        axisBank_TblAllAccount.FirstName = civm.AxisAta.FirstName;
                        axisBank_TblAllAccount.LastName = civm.AxisAta.LastName;
                        var accountNo = random.Next(100000000, 999999999);
                        axisBank_TblAllAccount.AccountNo = accountNo.ToString();
                        axisBank_TblAllAccount.AccountType = civm.AxisAta.AccountType;
                        axisBank_TblAllAccount.Gender = civm.AxisAta.Gender;
                        axisBank_TblAllAccount.MobileNo = civm.AxisAta.MobileNo;
                        axisBank_TblAllAccount.Email = civm.AxisAta.Email;
                        axisBank_TblAllAccount.MaritalStatus = civm.AxisAta.MaritalStatus;
                        axisBank_TblAllAccount.DateOfBirth = civm.AxisAta.DateOfBirth;
                        axisBank_TblAllAccount.IdCardType = civm.AxisAta.IdCardType;
                        axisBank_TblAllAccount.IdCardNumber = civm.AxisAta.IdCardNumber;
                        axisBank_TblAllAccount.Address = civm.AxisAta.Address;
                        axisBank_TblAllAccount.RoleId = 1007;
                        axisBank_TblAllAccount.CreatedOn = DateTime.Now;
                        axisBank_TblAllAccount.CreatedBy = civm.AxisAta.FirstName;
                        axisBank_TblAllAccount.IsActive = true;

                        db.AxisBank_tblAllAccount.Add(axisBank_TblAllAccount);


                        db.AxisBank_tblAllAccount.Add(axisBank_TblAllAccount);
                        var i = db.SaveChanges();
                        if (axisBank_TblAllAccount.Id > 0)
                        {
                            if (civm.Imup != null && civm.Imup.ContentLength > 0)
                            {
                                byte[] imageData;

                                using (var binaryReader = new BinaryReader(civm.Imup.InputStream))
                                {
                                    imageData = binaryReader.ReadBytes(civm.Imup.ContentLength);
                                }

                                var model = new ImageUploads
                                {
                                    Image = imageData,
                                    AccId = axisBank_TblAllAccount.Id,
                                    CreatedBy = Convert.ToString(Session["FirstName"]),
                                    CreatedOn = DateTime.Now,
                                    IsActive = true

                                };


                                db.ImageUploads.Add(model);
                                db.SaveChanges();
                            }

                            //insert id to all tables
                            db.AxisBank_tblBalance.Add(new AxisBank_tblBalance
                            {
                                AccountId = axisBank_TblAllAccount.Id,
                                Balance = 0,
                                CreatedOn = DateTime.Now,
                                CreatedBy = axisBank_TblAllAccount.FirstName,
                                IsActive = true
                            });

                            db.AxisBank_tblLogin.Add(new AxisBank_tblLogin
                            {
                                UserName = customerId.ToString(),
                                Password = axisBank_TblAllAccount.Password,
                                RoleId = axisBank_TblAllAccount.RoleId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = axisBank_TblAllAccount.FirstName,
                                IsActive = true
                            });
                            var j = db.SaveChanges();

                            TempData["SuccessMessage"] = "Successfully registered!";
                            return RedirectToAction("ShowAllUsers");
                        }
                        else
                        {

                            TempData["ErrorMessage"] = "Registration failed. Please try again.";
                            return RedirectToAction("CreateNewAccount");
                        }


                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            // Registration failed, set an error message
            TempData["ErrorMessage"] = "Registration failed. Please try again.";
            return RedirectToAction("CreateNewAccount");
        }


        //for setting
        public ActionResult Settings()
        {
            return View();
        }
        //for logout
        public ActionResult Logout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AxisBankHome");
        }
    }
}