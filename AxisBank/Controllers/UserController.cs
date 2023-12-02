using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AxisBank.Models;

namespace AxisBank.Controllers
{
  
    public class UserController : Controller
    {
        /*
     Summary
     author: S Rudra Prasad Choudhury
     date of creation: 25/10/2019    
     description : This is a Project of banking system. This is the User controller of the project.
      */


        // GET: User
        public ActionResult UserPanel()
        {
           
           // BalanceDetails _model = new BalanceDetails();
            UserProfileDetailsModel _model = new UserProfileDetailsModel();
            try
            {
                using (DBEntities db = new DBEntities())
                {

                    var ID = Convert.ToInt32(Session["Id"]);
                    /*  //join the accountall table and balance table
                _model = db.AxisBank_tblAllAccount.Join(db.AxisBank_tblBalance, ta => ta.Id, tb => tb.AccountId, (ta, tb) => new { ta, tb })
                         .Where(x => x.ta.IsActive == true && x.ta.Id == ID)
                         .Select(s => new BalanceDetails
                         {
                             Id = s.ta.Id,
                             Balance = s.tb.Balance                           
                         }).FirstOrDefault();
                  */
                    _model = db.AxisBank_tblAllAccount
                         .Join(db.AxisBank_tblBalance, ta => ta.Id, tb => tb.AccountId, (ta, tb) => new { ta, tb })
                         .Join(db.ImageUploads, ta_tb => ta_tb.ta.Id, iu => iu.AccId, (ta_tb, iu) => new { ta_tb, iu })
                         .Where(x => x.ta_tb.ta.IsActive == true && x.ta_tb.ta.Id == ID)
                          .Select(s => new UserProfileDetailsModel
                          {                         
                              TotalBalance = s.ta_tb.tb.Balance,
                              Image = s.iu.Image
                          }).FirstOrDefault();
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
            return View(_model);
        }

        //for showing Profile
        public ActionResult Profile()
        {
            UserProfileDetailsModel _model = new UserProfileDetailsModel();
            try
            {
             
                using (DBEntities db = new DBEntities())
                {
                    var ID = Convert.ToInt32(Session["Id"]);
                    _model = db.AxisBank_tblAllAccount
                        .Join(db.AxisBank_tblBalance, ta => ta.Id, tb => tb.AccountId, (ta, tb) => new { ta, tb })
                        .Join(db.ImageUploads, ta_tb => ta_tb.ta.Id, iu => iu.AccId, (ta_tb,iu) => new {ta_tb,iu})
                        .Where(x => x.ta_tb.ta.IsActive == true && x.ta_tb.ta.Id == ID)
                         .Select(s => new UserProfileDetailsModel
                         {
                             CustomerId = s.ta_tb.ta.CustomerId,
                             Password = s.ta_tb.ta.Password,
                             FirstName = s.ta_tb.ta.FirstName,
                             LastName = s.ta_tb.ta.LastName,
                             AccountNo = s.ta_tb.ta.AccountNo,
                             AccountType = s.ta_tb.ta.AccountType,
                             Gender = s.ta_tb.ta.Gender,
                             MobileNo = s.ta_tb.ta.MobileNo,
                             Email = s.ta_tb.ta.Email,
                             MaritalStatus = s.ta_tb.ta.MaritalStatus,
                             DateOfBirth = s.ta_tb.ta.DateOfBirth,
                             IdCardType = s.ta_tb.ta.IdCardType,
                             IdCardNumber = s.ta_tb.ta.IdCardNumber,
                             Address = s.ta_tb.ta.Address,
                             TotalBalance = s.ta_tb.tb.Balance,
                             Image = s.iu.Image
                         }).FirstOrDefault();
                    /* _model = db.AxisBank_tblAllAccount
                         .Join(db.AxisBank_tblBalance, ta => ta.Id, tb => tb.AccountId, (ta, tb) => new { ta, tb })

                          .Where(x => x.ta.IsActive == true && x.ta.Id == ID)
                          .Select(s => new UserProfileDetailsModel
                          {
                              CustomerId = s.ta.CustomerId,
                              Password =s.ta.Password,
                              FirstName =s.ta.FirstName,
                              LastName =s.ta.LastName,
                              AccountNo =s.ta.AccountNo,
                              AccountType =s.ta.AccountType,
                              Gender =s.ta.Gender,
                              MobileNo =s.ta.MobileNo,
                              Email =s.ta.Email,
                              MaritalStatus =s.ta.MaritalStatus,
                              DateOfBirth =s.ta.DateOfBirth,
                              IdCardType =s.ta.IdCardType,
                              IdCardNumber =s.ta.IdCardNumber,
                              UploadPhoto = null,
                              Address =s.ta.Address,                         
                              TotalBalance =s.tb.Balance
                          }).FirstOrDefault(); */
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(_model);
        }

        //for settings
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