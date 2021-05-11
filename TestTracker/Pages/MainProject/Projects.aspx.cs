using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TestTracker.Pages.MainProject
{
    public partial class Projects : System.Web.UI.Page
    {
        private string QR = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            string User = HttpContext.Current.User.Identity.Name.ToString();
            QR = DBConnection.qrProjects + " where [UserLogin] = '" + User + "' or [UserEmail] = '" + User + "'";
            if (!IsPostBack)
            {
                rpFill(QR);
            }
        }
        //Заполнение данными список книг
        private void rpFill(string qr)
        {
            sdsProject.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProject.SelectCommand = qr;
            sdsProject.DataSourceMode = SqlDataSourceMode.DataReader;
            rpProjects.DataSource = sdsProject;
            rpProjects.DataBind();
        }
        //Открывает страницу с тестами на основе ProjectId
        protected void btTests_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            var ProjectId = ((Label)item.FindControl("lblID")).Text;
            //Перейти на страницу Tests.aspx с ProjectId в зашифрованном виде
            Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
    }
}