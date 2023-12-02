using AxisBank.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AxisBank.Controllers
{
   
    public class CashierController : Controller
    {

        /*
       Summary
       author: S Rudra Prasad Choudhury
       date of creation: 8/10/2019    
       description : This is a Project of banking system. This is the Cashier controller of the project.
        */



        //for Cashier landing page 
        public ActionResult CashierPanel()
        {
            Dashboard dashboard = new Dashboard();
            try
            {

                using (DBEntities db = new DBEntities())
                {

                    var totalCustomers = db.AxisBank_tblAllAccount.Where(x => x.IsActive == true).Count();
                    var totalDeposite = db.tblAccountStatement
                                               .Where(x => x.Credit != 0 && x.Credit != null)
                                               .Sum(x => (double?)x.Credit);
                    var totalWithdraw = db.tblAccountStatement
                                                 .Where(x => x.Debit != 0 && x.Debit != null)
                                                 .Sum(x => (double?)x.Debit);
                    var todayDay = DateTime.Today.DayOfWeek.ToString();

                    var totalDepositeToday = db.tblAccountStatement
                                                  .Where(x => x.Credit != 0 && x.Credit != null && x.Date == DateTime.Today)
                                                  .Sum(x => (double?)x.Credit);
                    var totalWithdrawToday = db.tblAccountStatement
                                                  .Where(x => x.Debit != 0 && x.Debit != null && x.Date == DateTime.Today)
                                                  .Sum(x => (double?)x.Debit);


                    dashboard.totalCustomers = totalCustomers;
                    dashboard.totalDeposite = totalDeposite ?? 0;
                    dashboard.totalWithdraw = totalWithdraw ?? 0;
                    dashboard.totalDepositeToday = totalDepositeToday ?? 0;
                    dashboard.totalWithdrawToday = totalWithdrawToday ?? 0;
                    dashboard.todayDay = todayDay;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(dashboard);
        }


        // for showing all users
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

        //for create new Account
        public ActionResult CreateNewAccount()
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            return View();

        }

        //create account post method
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

        //for edit first action method
        public ActionResult EditAccountDetails(int id)
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.Id == id).FirstOrDefault();
                    return View(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //for edit account final method
        [HttpPost]
        public ActionResult EditAccount(AxisBank_tblAllAccount ata)
     {
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        //var result = db.AxisBank_tblAllAccount.Where(D => D.IsActive == true && D.Id == data.Id).FirstOrDefault();

                        //get the details of this id from AxisBank_tblAllAccount

                        var result = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == ata.CustomerId && x.IsActive == true).FirstOrDefault();

                        if (result != null)
                        {
                            result.UpdatedOn = DateTime.Now;
                            string FirstName = Convert.ToString(Session["FirstName"]);
                            result.UpdatedBy = FirstName;
                            result.RoleId = 1007;

                            result.FirstName = ata.FirstName;
                            result.LastName = ata.LastName;
                            result.Password = ata.Password;
                            result.Gender = ata.Gender;
                            result.MobileNo = ata.MobileNo;
                            result.Email = ata.Email;
                            result.MaritalStatus = ata.MaritalStatus;
                            result.DateOfBirth = ata.DateOfBirth;
                            result.IdCardType = ata.IdCardType;
                            result.IdCardNumber = ata.IdCardNumber;
                            result.Address = ata.Address;


                            var i = db.SaveChanges();
                            if (result.Id > 0)
                            {


                                var result2 = db.AxisBank_tblLogin.Where(x => x.UserName == ata.CustomerId && x.IsActive == true).FirstOrDefault();
                                if (result2 != null)
                                {
                                    result2.RoleId = 1007;
                                    result2.Password = ata.Password;
                                    result2.UpdatedOn = DateTime.Now;
                                    result2.UpdatedBy = FirstName;
                                }
                                var j = db.SaveChanges();

                                TempData["SMessage"] = "Successfully Updated";
                                return RedirectToAction("ShowAllUsers");
                            }
                        }
                        else
                        {
                            TempData["EMessage"] = "Updation failed. Please try again.";
                            return RedirectToAction("ShowAllUsers");
                        }

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }
            // Registration failed, set an error message
            TempData["EMessage"] = "Updation failed. Please try again.";
            return RedirectToAction("ShowAllUsers");
        }

        //for delete Account Operation 
        public ActionResult DeleteAccount(int id)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.Id == id).FirstOrDefault();
                    if (result != null)
                    {
                        result.IsActive = false;
                        result.UpdatedOn = DateTime.Now;
                        result.UpdatedBy = Session["FirstName"] as string;

                        db.SaveChanges();
                    }
                    var result1 = db.AxisBank_tblBalance.Where(x => x.AccountId == id).FirstOrDefault();
                    if (result1 != null)
                    {
                        result1.IsActive = false;
                        result1.UpdatedOn = DateTime.Now;
                        result1.UpdatedBy = Session["FirstName"] as string;

                        db.SaveChanges();
                    }
                    var result2 = db.AxisBank_tblLogin.Where(x => x.UserName == result.CustomerId).FirstOrDefault();
                    if (result != null)
                    {
                        result2.IsActive = false;
                        result2.UpdatedOn = DateTime.Now;
                        result2.UpdatedBy = Session["FirstName"] as string;

                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ShowAllUsers");
        }

        //For deposite Operation
        public ActionResult DepositeOperation()
        {
            return View();
        }

        //check the source account no is existing or not method
        public ActionResult CheckAccountForDepositeOperation(ForDeposite accNo)
        {
            if (ModelState.IsValid)
            {
                ForDeposite _fd = new ForDeposite();
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == accNo.AccountNo && x.IsActive == true).FirstOrDefault();


                        if (result != null)
                        {
                            // how to get accountNo, firstName, balance from AxisBank_tblAllAccount and AxisBank_tblBalance and it is of DepositeFor model type
                            _fd.AccountNo = result.AccountNo;
                            _fd.FirstName = result.FirstName;
                            return View(_fd);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Account Not Found";
                            return View("DepositeOperation");
                        }

                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View("DepositeOperation");
        }

        //finall deposite amount in account method
        public ActionResult DepositeAmountInAccount(ForDeposite de)
        {
            try
            {

                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == de.AccountNo && x.IsActive == true).FirstOrDefault();


                    if (result != null)
                    {
                        var data = db.AxisBank_tblBalance
                      .Where(x => x.AccountId == result.Id && x.IsActive == true)
                      .FirstOrDefault();
                        if (de.Balance <= 0)
                        {
                            TempData["NotDeposite"] = "Balance Cant not Be Negative Or Zero";
                            return View("DepositeOperation");
                        }
                        else
                        {
                            if (data != null)
                            {

                                // Add the deposited amount to the current balance
                                data.Balance += de.Balance;
                                // Update the balance in the database
                                db.Entry(data).State = EntityState.Modified;

                                data.UpdatedOn = DateTime.Now;
                                data.UpdatedBy = Session["FirstName"] as string;
                                db.SaveChanges();
                                TempData["Deposite"] = "Balance Deposited Successfully";
                                db.tblAccountStatement.Add(new tblAccountStatement
                                {

                                    Date = DateTime.Now,
                                    Description = "Deposite",
                                    Credit = de.Balance,
                                    Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result.Id).Select(x => x.Balance).FirstOrDefault(),
                                    AccountId = result.Id
                                });
                                var k = db.SaveChanges();

                                List<tblAccountStatement> model = new List<tblAccountStatement>();
                                model = db.tblAccountStatement.Where(x => x.AccountId == result.Id).ToList();


                                return View(model);

                            }

                        }
                    }


                    return View("DepositeAmountInAccount");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //for withdraw operation method
        public ActionResult WithdrawOperation()
        {
            return View();
        }
        //check the source account no is existing or not method
        public ActionResult CheckAccountForWithdrawOperation(AxisBank_tblAllAccount accNo)
        {
            if (ModelState.IsValid)
            {
                ForDeposite _fd = new ForDeposite();
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == accNo.AccountNo && x.IsActive == true).FirstOrDefault();
                        if (result != null)
                        {

                            _fd.AccountNo = result.AccountNo;
                            _fd.FirstName = result.FirstName;
                            return View(_fd);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Account Not Found";
                            return View("WithdrawOperation");
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View();
        }

        //finall withdraw amount in account method
        public ActionResult WithdrawAmountInAccount(ForDeposite de)
        {
            try
            {

                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == de.AccountNo && x.IsActive == true).FirstOrDefault();


                    if (result != null)
                    {
                        var data = db.AxisBank_tblBalance
                      .Where(x => x.AccountId == result.Id && x.IsActive == true)
                      .FirstOrDefault();
                        if (de.Balance <= 0)
                        {
                            TempData["Zero"] = "You Can not Withdraw Zero Balance";
                            return View("WithdrawOperation");
                        }
                        else if (de.Balance > data.Balance)
                        {
                            TempData["NotWithdraw"] = "Balance is Less";
                            return View("WithdrawOperation");
                        }
                        else
                        {
                            if (data != null)
                            {
                                data.Balance -= de.Balance;
                                // Update the balance in the database
                                db.Entry(data).State = EntityState.Modified;

                                data.UpdatedOn = DateTime.Now;
                                data.UpdatedBy = Session["FirstName"] as string;
                                db.SaveChanges();

                                db.tblAccountStatement.Add(new tblAccountStatement
                                {
                                    Date = DateTime.Now,
                                    Description = "Withdraw",
                                    Debit = de.Balance,
                                    Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result.Id).Select(x => x.Balance).FirstOrDefault(),
                                    AccountId = result.Id
                                });
                                var k = db.SaveChanges();

                                List<tblAccountStatement> model = new List<tblAccountStatement>();
                                model = db.tblAccountStatement.Where(x => x.AccountId == result.Id).ToList();

                                TempData["Withdraw"] = "Balance Withdrawl Successfully";
                                return View(model);

                            }

                        }
                    }


                    return View("WithdrawAmountInAccount");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //for transfer money
        public ActionResult TransferMoney()
        {
            return View();
        }

        //json method for transfer money
        [HttpGet]
        public JsonResult GetAccountDetailsByUsingAjaxFirst(string accNo)
        {
            bool success = false;
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var result = db.AxisBank_tblAllAccount
                        .Where(x => x.AccountNo == accNo && x.IsActive == true)
                        .FirstOrDefault();

                    if (result != null)
                    {
                        success = false;
                        return Json(new { FirstName = result.FirstName }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Account Not Found";
                        success = true;


                    }
                    var response = new
                    {
                        Success = success,
                        SuccessMessage = TempData["SuccessMessage"],
                        ErrorMessage = TempData["ErrorMessage"]
                    };
                    return Json(response);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, e.g., log it
                return Json(new { Success = false, ErrorMessage = "An error occurred." });
            }
        }

        //json method for transfer money
        [HttpPost]
        public JsonResult TransferMoneyFinally(TransferMoney details)
        {
            bool success = false;
            try
            {

                using (DBEntities db = new DBEntities())
                {

                    //for withdraw first from the source account
                    var result = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == details.SourceAccountNo && x.IsActive == true).FirstOrDefault();
                    if (result != null)
                    {
                        var data = db.AxisBank_tblBalance
                                   .Where(x => x.AccountId == result.Id && x.IsActive == true)
                                   .FirstOrDefault();
                        if (details.AmountToTransfer <= 0)
                        {
                            success = false;
                            TempData["ErrorMessage"] = "Money Not Transfered";

                        }
                        else if (details.AmountToTransfer > data.Balance)
                        {
                            success = false;
                            TempData["ErrorMessage"] = "Money Not Transfered";
                        }
                        else
                        {
                            if (data != null)
                            {
                                data.Balance -= details.AmountToTransfer;
                                // Update the balance in the database
                                db.Entry(data).State = EntityState.Modified;

                                data.UpdatedOn = DateTime.Today;
                                data.UpdatedBy = Session["FirstName"] as string;
                                db.SaveChanges();

                                db.tblAccountStatement.Add(new tblAccountStatement
                                {
                                    Date = DateTime.Today,
                                    Description = "Withdraw",
                                    Debit = details.AmountToTransfer,
                                    Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result.Id).Select(x => x.Balance).FirstOrDefault(),
                                    AccountId = result.Id
                                });
                                var k = db.SaveChanges();
                                var result2 = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == details.DestinationAccountNo && x.IsActive == true).FirstOrDefault();

                                if (result2 != null)
                                {
                                    var data2 = db.AxisBank_tblBalance
                                               .Where(x => x.AccountId == result2.Id && x.IsActive == true)
                                               .FirstOrDefault();

                                    if (data2 != null)
                                    {
                                        data2.Balance += details.AmountToTransfer;
                                        // Update the balance in the database
                                        db.Entry(data2).State = EntityState.Modified;

                                        data2.UpdatedOn = DateTime.Today;
                                        data2.UpdatedBy = Session["FirstName"] as string;
                                        db.SaveChanges();

                                        db.tblAccountStatement.Add(new tblAccountStatement
                                        {
                                            Date = DateTime.Today,
                                            Description = details.Description,
                                            Credit = details.AmountToTransfer,
                                            Balance = db.AxisBank_tblBalance.Where(x => x.AccountId == result2.Id).Select(x => x.Balance).FirstOrDefault(),
                                            AccountId = result2.Id
                                        });
                                        db.SaveChanges();
                                        success = true;
                                        TempData["SuccessMessage"] = "Money Transfered Successfully";

                                    }

                                }

                            }


                        }


                    }
                    else
                    {
                        success = false;
                        TempData["ErrorMessage"] = "Money Not Transfered";
                        return Json(new { Success = false, ErrorMessage = "An error occurred." });
                    }

                    var response = new
                    {
                        Success = success,
                        SuccessMessage = TempData["SuccessMessage"],
                        ErrorMessage = TempData["ErrorMessage"]
                    };
                    return Json(response);
                }
            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ErrorMessage = "An error occurred." });
            }


        }

        //List of deposite list action
        public ActionResult TotalDeposite()
        {
            List<TotalDepositeStatement> _model = new List<TotalDepositeStatement>();
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    //var result = db.tblAccountStatement.Where(x => x.Credit != null).ToList();
                    _model = db.tblAccountStatement.Join(db.AxisBank_tblAllAccount, ta => ta.AccountId, at => at.Id, (ta, at) => new { ta, at })
                        .Where(x => x.ta.Credit != null && x.ta.Credit != 0)
                        .Select(s => new TotalDepositeStatement
                        {
                            Date = s.ta.Date,
                            Description = s.ta.Description,
                            Credit = s.ta.Credit,
                            Balance = s.ta.Balance,
                            FirstName = s.at.FirstName
                        }).ToList();
                    return View(_model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //List of withdraw action
        public ActionResult TotalWithdraw()
        {
            List<TotalDepositeStatement> _model = new List<TotalDepositeStatement>();
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    //var result = db.tblAccountStatement.Where(x => x.Credit != null).ToList();
                    _model = db.tblAccountStatement.Join(db.AxisBank_tblAllAccount, ta => ta.AccountId, at => at.Id, (ta, at) => new { ta, at })
                        .Where(x => x.ta.Debit != null && x.ta.Debit != 0)
                        .Select(s => new TotalDepositeStatement
                        {
                            Date = s.ta.Date,
                            Description = s.ta.Description,
                            Debit = s.ta.Debit,
                            Balance = s.ta.Balance,
                            FirstName = s.at.FirstName
                        }).ToList();
                    return View(_model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //for setting feature action
        public ActionResult Settings()
        {
            return View();
        }

        //Logout action
        public ActionResult Logout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AxisBankHome");
        }
    }
}