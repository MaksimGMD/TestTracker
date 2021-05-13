using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.MainProject
{
    public partial class Profile : System.Web.UI.Page
    {
        int UserId;
        string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProfile;
            if (!IsPostBack)
            {
                DBConnection connection = new DBConnection();
                UserId =  connection.GetUserId(HttpContext.Current.User.Identity.Name.ToString());
                rpUserFill(QR);
            }

        }
        //Заполнение карточки пользователя
        protected void rpUserFill(string qr)
        {
            sdsUser.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUser.SelectCommand = qr + " where [UserId] = '" + Convert.ToString(UserId) + "'";
            sdsUser.DataSourceMode = SqlDataSourceMode.DataReader;
            rpUser.DataSource = sdsUser;
            rpUser.DataBind();
        }
    }
}