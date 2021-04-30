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
            if (!IsPostBack)
            {
                alert.Visible = false;
            }
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
            int UserId;
            password = Encryption.EncryptedDate(password);
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [UserId] FROM [User] WHERE([UserLogin] = '" + login + "' OR [UserEmail] = '" + login + "') " +
                    "AND [UserPassword] = '" + password + "'";
                DBConnection.connection.Open();
                UserId = Convert.ToInt32(command.ExecuteScalar().ToString());
                return UserId;

            }
            catch
            {
                UserId = 0;
                return UserId;
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
                // Создать билет, добавить cookie-набор к ответу и 
                // перенаправить на исходную запрошенную страницу
                FormsAuthentication.RedirectFromLoginPage(tbLogin.Text, true);
                alert.Visible = false;
            }
            else
                alert.Visible = true;
        }
    }
}