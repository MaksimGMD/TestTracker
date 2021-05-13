using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TestTracker.Pages.MainProject
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            string UserId = (HttpContext.Current.User.Identity.Name.ToString());
            if (connection.UserRole(UserId) != "1")
            {

            }
            else
            {
                Response.Redirect("../Admin/Users.aspx");
            }
        }

        protected void btLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("../Common/Login.aspx", true);
        }
    }
}