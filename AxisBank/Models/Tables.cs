using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace AxisBank.Models
{

    [Table("EmployeeList")]
    public class EmployeeList
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Name Is Required")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        public int Role { get; set; }
        [Required(ErrorMessage = "User Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("AxisBank_tblAllAccount")]
    public partial class AxisBank_tblAllAccount
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Customer Id")]
        public string CustomerId { get; set; }

    
        public string Password { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Account No")]
        public string AccountNo { get; set; }
        [DisplayName("Account Type")]
        public string AccountType { get; set; }
                
        public string Gender { get; set; }
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Id Card Type")]
        public string IdCardType { get; set; }
        [DisplayName("Id Card Number")]
        public string IdCardNumber { get; set; }
       
        public string Address { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("AxisBank_tblLogin")]
    public partial class AxisBank_tblLogin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DisplayName("Password")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

    }

    [Table("AxisBank_tblBalance")]
    public partial class AxisBank_tblBalance
    {
        [Key]
        public int Id { get; set; }
        public double Balance { get; set; }
        public int AccountId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("AxisBank_tblRole")]
    public partial class AxisBank_tblRole
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }


    }

    [Table("tblAccountStatement")]
    public partial class tblAccountStatement
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public int AccountId { get; set; }
    }

    [Table("ImageUploads")]
    public class ImageUploads
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int AccId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }



}

