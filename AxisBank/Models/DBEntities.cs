  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AxisBank.Models
{
    public class DBEntities : DbContext
    {
        public DBEntities() : base("DbContext")
        {
        }


        public DbSet<AxisBank_tblAllAccount> AxisBank_tblAllAccount { get; set; }
        public DbSet<AxisBank_tblBalance> AxisBank_tblBalance { get; set; }
        public DbSet<AxisBank_tblLogin> AxisBank_tblLogin { get; set; }
        public DbSet<AxisBank_tblRole> AxisBank_tblRole { get; set; }
        public DbSet<tblAccountStatement> tblAccountStatement { get; set; }
        public DbSet<EmployeeList> EmployeeList { get; set; }
        public DbSet<ImageUploads> ImageUploads { get; set; }


    }
}