using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using Encryption_Library;

namespace TestTracker.Pages.Common
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            alert.Visible = false;
        }

        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>id авторизовавшегося пользователя</returns>
        public Int32 Authorization(string login, string password)
        {

            password = Encryption.EncryptedDate(password);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [UserId] FROM [User] WHERE([UserLogin] = '" + login + "' OR [UserEmail] = '" + login + "') " +
                    "AND [UserPassword] = '" + password + "'";
                DBConnection.connection.Open();
                DBConnection.idUser = Convert.ToInt32(command.ExecuteScalar().ToString());
                return (DBConnection.idUser);

            }
            catch
            {
                DBConnection.idUser = 0;
                return (DBConnection.idUser);
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //Вход
        protected void btEnter_Click(object sender, EventArgs e)
        {
            if (Authorization(tbLogin.Text, tbPassword.Text) != 0)
            {
                //FormsAuthenticationTicket tkt;
                //string cookiestr;
                //HttpCookie ck;
                //tkt = new FormsAuthenticationTicket(1, tbLogin.Text, DateTime.Now,
                //DateTime.Now.AddMinutes(60), chkPersistCookie.Checked, "your custom data");
                //cookiestr = FormsAuthentication.Encrypt(tkt);
                //ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                //if (chkPersistCookie.Checked)
                //    ck.Expires = tkt.Expiration;
                //ck.Path = FormsAuthentication.FormsCookiePath;
                //Response.Cookies.Add(ck);

                //string strRedirect;
                //strRedirect = Request["ReturnUrl"];
                //if (strRedirect == null)
                //    strRedirect = "default.aspx";
                //Response.Redirect(strRedirect, true);
                FormsAuthentication.RedirectFromLoginPage(tbLogin.Text, true);
            }
            else
                Response.Redirect("Login.aspx", true);
        }
        protected void del_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("Аuthorization");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx", true);
        }
    }
}