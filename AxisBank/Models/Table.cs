using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AxisBank.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? ChackInDate { get; set; }
        public DateTime? ChackOutDate { get; set; }
        public int? WorkingHours { get; set; }
        public string IPAddress { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }

    }

    [Table("Bank_Deposit")]
    public class Bank_Deposit
    {
        [Key]
        public int Dip_Id { get; set; }
        public string Date { get; set; }
        public string Type_of_Pay { get; set; }
        public string Person_Name { get; set; }
        public string Bank_Name { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }

    [Table("Bill_Details")]
    public class Bill_Details
    {
        [Key]
        public int Bill_id { get; set; }
        public string Inv_no { get; set; }
        public string itemid { get; set; }
        public decimal? quantity { get; set; }
        public decimal? price { get; set; }
        public string date { get; set; }
        public bool? is_status { get; set; }
        public string Generate_Time { get; set; }
        public string Delete_Status { get; set; }
        public decimal? Mega_Offer_Discount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public string Bill_type { get; set; }
        public string Credit_NO { get; set; }
        public decimal? DiscPercentage { get; set; }
        public decimal? cgst { get; set; }
        public decimal? sgst { get; set; }
    }

    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
        public bool? Status { get; set; }
    }

    [Table("Counter_Master")]
    public class Counter_Master
    {
        [Key]
        public int Counter_Id { get; set; }
        public string Counter_Name { get; set; }
        public bool? Is_Status { get; set; }
    }

    [Table("Credit_Details")]
    public class Credit_Details
    {
        [Key]
        public int Credit_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public decimal? credit_amount { get; set; }
        public string Tin_No { get; set; }
        public string Customer_Type { get; set; }
        public string Company_Name { get; set; }
        public string Concerned_Person { get; set; }
        public byte? Company_Vat { get; set; }
        public string EmailID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string MemberShipCard { get; set; }
        public string StaffId { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
    }


    [Table("CREDIT_NOBOOK")]
    public class CREDIT_NOBOOK
    {
        [Key]
        public int CreditBookId { get; set; }
        public int? CustomerId { get; set; }
        public string Reason { get; set; }
        public string Inv_no { get; set; }
        public string Date { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? tax { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GrandTotal { get; set; }
    }

    [Table("CREDIT_NOTEBOOK_ITEM_DETAILS")]
    public class CREDIT_NOTEBOOK_ITEM_DETAILS
    {
        [Key]
        public int DT_Id { get; set; }
        public int? Item_Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Tot_Price { get; set; }
        public int? CreditBookId { get; set; }
        public string Date { get; set; }
    }

    [Table("Customer_Master")]
    public class Customer_Master
    {
        [Key]
        public int CustomerIdd { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }
        public string ConcerenedPersonName { get; set; }
        public string CompanyVatNo { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string MemberShipCard { get; set; }
        public string StaffId { get; set; }
        public int? LoyalityPoint { get; set; }
    }

}

