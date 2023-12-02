using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace AxisBank.Models
{
    public class ForDeposite
    {
        public string AccountNo { get; set; }
        public string FirstName { get; set; }
      
        public double Balance { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class TotalDepositeStatement
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public double Credit { get; set; }
        public double Debit { get; set; }

        public double Balance { get; set; }
        public string FirstName { get; set; }
    }

    public class TransferMoney
    {
        public int Id { get; set; }
        public string SourceAccountNo { get; set; }
        public string SourceName { get; set; }
        public double AmountToTransfer { get; set; }

        public string DestinationAccountNo { get; set; }
        public string DestinationName { get; set; }
        public string Description { get; set; }

    }

    public class Dashboard
    {

        public int totalCustomers { get; set; }
        public int totalEmployees { get; set; }
        public double totalDeposite { get; set; }
        public double totalWithdraw { get; set; }
        public double totalDepositeToday { get; set; }
        public double totalWithdrawToday { get; set; }
        public string todayDay { get; set; }


    }

    public class UserPanelModel
    {
        public float totalBalance { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public double Balance { get; set; }
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdCardType { get; set; }
        public string IdCardNumber { get; set; }
        public byte[] UploadPhoto { get; set; }
        public string Address { get; set; }
    }

    public class AllDetailsOfEmployee
    {     
        public string EmployeeName { get; set; }
       
        public string RoleName { get; set; }
     
        public string UserName { get; set; }
      
        public string Password { get; set; }   
     
    }

    public class UserProfileDetailsModel
    {
     
        public string CustomerId { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdCardType { get; set; }
        public string IdCardNumber { get; set; }
        public byte[] UploadPhoto { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public double TotalBalance { get; set; }

        public byte[] Image { get; set; }
    }

    public class BalanceDetails
    {
        public int Id { get; set; }
        public double Balance { get; set; }
    }

    public class CustomerAndImageViewModel
    {
        public AxisBank_tblAllAccount AxisAta { get; set; }
        
        [DisplayName("Upload Image")]
        public HttpPostedFileBase Imup { get; set; }
    }
}