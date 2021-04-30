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
        private string QR = ""; //Переменная для команд БД
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProjects;
            if (!IsPostBack)
            {
                rpFill(QR);
                if(User.Identity.IsAuthenticated)
                {
                   
                }
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
    }
}