using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using AxisBank.Models;
using CaptchaMvc.HtmlHelpers;

namespace AxisBank.Controllers
{
    [AllowAnonymous]
    public class AxisBankHomeController : Controller
    {

        /*
        Summary
        author: S Rudra Prasad Choudhury
        date of creation: 05/10/2019    
        description : This is a Project of banking system. This is the home controller of the project.
         */


        // GET: AxisBankHome
        public ActionResult Landing()
        {
            return View();
        }


        //Login Form Action

        public ActionResult Login()
        {
            return View();
        }

        //Login form post action
        [HttpPost]
        public ActionResult Login(AxisBank_tblLogin axisAta)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            if (!this.IsCaptchaValid(errorText: ""))
            {
                ViewBag.ErrorMessage = "Captcha is not valid";
                return View(axisAta);
            }
            else
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        //first check the  isActive cplumn is true or False then proced to next step
                        if (db.AxisBank_tblLogin.Any(x => x.UserName == axisAta.UserName && x.IsActive == false))
                        {
                            ViewBag.Message = "Your Account is Deactivated";
                            return View("Login");
                        }
                        else
                        {
                            //check this customerid and password is matching in axisbanl_allaccount and role id should be 1004 in a if block
                            if (db.EmployeeList.Any(x => x.UserName == axisAta.UserName && x.Password == axisAta.Password && x.Role == 1005))
                            {

                                //Get the firstname firstLetter and Role name from axisbank_tblallaccount and axisbank_tblrole
                                var firstName = db.EmployeeList.Where(x => x.UserName == axisAta.UserName).Select(x => x.EmployeeName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1005).Select(x => x.Role).FirstOrDefault();

                                FormsAuthentication.SetAuthCookie(firstName, false);

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;


                                //if true then redirect to user panel
                                return RedirectToAction("AdminPanel", "Admin");
                            }
                            else if (db.EmployeeList.Any(x => x.UserName == axisAta.UserName && x.Password == axisAta.Password && x.Role == 1006))
                            {
                                var firstName = db.EmployeeList.Where(x => x.UserName == axisAta.UserName).Select(x => x.EmployeeName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1006).Select(x => x.Role).FirstOrDefault();

                                FormsAuthentication.SetAuthCookie(firstName, false);

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;
                                return RedirectToAction("CashierPanel", "Cashier");
                            }
                            else if (db.AxisBank_tblLogin.Any(x => x.UserName == axisAta.UserName && x.Password == axisAta.Password && x.RoleId == 1007))
                            {
                                var firstName = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.UserName).Select(x => x.FirstName).FirstOrDefault();
                                var id = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.UserName).Select(x => x.Id).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1007).Select(x => x.Role).FirstOrDefault();
                                var UserId = db.AxisBank_tblAllAccount.Where(x => x.CustomerId == axisAta.UserName).Select(x => x.Id).FirstOrDefault();

                                FormsAuthentication.SetAuthCookie(firstName, false);

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;
                                Session["UserId"] = UserId;
                                Session["Id"] = id;

                                return RedirectToAction("UserPanel", "User");
                            }
                            else if (db.EmployeeList.Any(x => x.UserName == axisAta.UserName && x.Password == axisAta.Password && x.Role == 1008))
                            {
                                var firstName = db.EmployeeList.Where(x => x.UserName == axisAta.UserName).Select(x => x.EmployeeName).FirstOrDefault();
                                var firstLetter = firstName.Substring(0, 1);
                                var roleName = db.AxisBank_tblRole.Where(x => x.Id == 1008).Select(x => x.Role).FirstOrDefault();

                                FormsAuthentication.SetAuthCookie(firstName, false);

                                Session["firstName"] = firstName;
                                Session["roleName"] = roleName;
                                Session["firstLetter"] = firstLetter;


                                return RedirectToAction("WorkerPanel", "Worker");

                            }
                            else
                            {
                                ViewBag.Message = "Invalid User";
                                return View("Login");
                            }

                        }




                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        //for singup form
        public ActionResult SignUp()
        {
            return View();
        }

        // signup form post action
        [HttpPost]
        public ActionResult SignUpNewAccount(CustomerAndImageViewModel axisAll)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {

                        AxisBank_tblAllAccount axisBank_TblAllAccount = new AxisBank_tblAllAccount();
                        if (axisAll.AxisAta != null)
                        {
                            Random random = new Random();
                            var customerId = random.Next(10000, 99999);
                            axisBank_TblAllAccount.CustomerId = customerId.ToString();
                            axisBank_TblAllAccount.Password = axisAll.AxisAta.Password;
                            axisBank_TblAllAccount.FirstName = axisAll.AxisAta.FirstName;
                            axisBank_TblAllAccount.LastName = axisAll.AxisAta.LastName;
                            var accountNo = random.Next(100000000, 999999999);
                            axisBank_TblAllAccount.AccountNo = accountNo.ToString();
                            axisBank_TblAllAccount.AccountType = axisAll.AxisAta.AccountType;
                            axisBank_TblAllAccount.Gender = axisAll.AxisAta.Gender;
                            axisBank_TblAllAccount.MobileNo = axisAll.AxisAta.MobileNo;
                            axisBank_TblAllAccount.Email = axisAll.AxisAta.Email;
                            axisBank_TblAllAccount.MaritalStatus = axisAll.AxisAta.MaritalStatus;
                            axisBank_TblAllAccount.DateOfBirth = axisAll.AxisAta.DateOfBirth;
                            axisBank_TblAllAccount.IdCardType = axisAll.AxisAta.IdCardType;
                            axisBank_TblAllAccount.IdCardNumber = axisAll.AxisAta.IdCardNumber;
                            axisBank_TblAllAccount.Address = axisAll.AxisAta.Address;
                            axisBank_TblAllAccount.RoleId = 1007;
                            axisBank_TblAllAccount.CreatedOn = DateTime.Now;
                            axisBank_TblAllAccount.CreatedBy = axisAll.AxisAta.FirstName;
                            axisBank_TblAllAccount.IsActive = true;

                            db.AxisBank_tblAllAccount.Add(axisBank_TblAllAccount);

                            var i = db.SaveChanges();
                            if (axisAll.Imup != null && axisAll.Imup.ContentLength > 0)
                            {
                                byte[] imageData;

                                using (var binaryReader = new BinaryReader(axisAll.Imup.InputStream))
                                {
                                    imageData = binaryReader.ReadBytes(axisAll.Imup.ContentLength);
                                }

                                var model = new ImageUploads
                                {
                                    Image = imageData,
                                    AccId = axisBank_TblAllAccount.Id,
                                    CreatedBy = axisAll.AxisAta.FirstName,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true

                                };


                                db.ImageUploads.Add(model);
                                db.SaveChanges();
                            }

                            if (axisBank_TblAllAccount.Id > 0)
                            {
                                AxisBank_tblBalance axisBalance = new AxisBank_tblBalance();
                                axisBalance.AccountId = axisBank_TblAllAccount.Id;
                                axisBalance.Balance = 0;

                                axisBalance.CreatedOn = DateTime.Now;
                                axisBalance.CreatedBy = axisAll.AxisAta.FirstName;
                                axisBalance.IsActive = true;


                                db.AxisBank_tblBalance.Add(axisBalance);


                                AxisBank_tblLogin axisLogin = new AxisBank_tblLogin();
                                axisLogin.UserName = customerId.ToString();
                                axisLogin.Password = axisAll.AxisAta.Password;
                                axisLogin.RoleId = 1007;
                                axisLogin.CreatedOn = DateTime.Now;
                                axisLogin.CreatedBy = axisAll.AxisAta.FirstName;
                                axisLogin.IsActive = true;

                                db.AxisBank_tblLogin.Add(axisLogin);

                                var j = db.SaveChanges();
                                ViewBag.JValue = j;
                                if (j > 0)
                                {
                                    return View("signUp");
                                }
                                else
                                {
                                    return View("Login");
                                }

                            }
                        }
                        else
                        {
                            return View("Login");
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return View("Login");
        }

        //for user internet banking operations
       
        public ActionResult InternetBanking()
        {
            return View();
        }

        //for get the customer Id
        [HttpGet]
        public JsonResult getCustomerId(string accNo)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var customerId = db.AxisBank_tblAllAccount.Where(x => x.AccountNo == accNo).Select(x => x.CustomerId).FirstOrDefault();
                    return Json(customerId, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}