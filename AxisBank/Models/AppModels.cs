using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxisBank.Models
{
    public class AppModels
    {
        public static SelectList SelectListRole(string Selected = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "- Select -", Value = "" });
           
            return new SelectList(items, "Value", "Text", Selected);
        }

        public static List<SelectListItem> RoleSelectListItemsByRoleId()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (var dbContext = new DBEntities())
            {
                items = dbContext.AxisBank_tblRole.Where(r => r.IsActive == true).OrderBy(o => o.Role).Select(r => new SelectListItem { Text = r.Role, Value = r.Id.ToString() }).ToList();
            }
            items.Insert(0, new SelectListItem { Text = "- Select Role -", Value = "" });
            return items;
        }
    }
}