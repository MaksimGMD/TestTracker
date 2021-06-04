using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.Common
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(HttpContext.Current.User.Identity.Name.ToString() == "")
                {
                    btEnter.Text = "Войти";
                }
                else
                {
                    btEnter.Text = "Выйти";
                }
            }
        }

        protected void btEnter_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.Name.ToString() == "")
            {
                Response.Redirect("../Common/Login.aspx", true);
            }
            else
            {
                FormsAuthentication.SignOut();
                btEnter.Text = "Войти";
            }
        }
    }
}