using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TestTracker
{
    public class DBConnection
    {

        //Строка подключения к базе данных
        public static SqlConnection connection = new SqlConnection(
            "Data Source=DESKTOP-2OC8HFJ\\MYGRIT; Initial Catalog=TestTracker;" +
            "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
            "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

        /// <summary>
        /// Строка выбора данных из БД
        /// </summary>
        public static string
            qrProjects = "SELECT ProjectName, ProjectVersion, (SELECT COUNT(*) FROM Test WHERE IdProject = ProjectUser.IdProject) AS 'TesCount' " +
            "FROM ProjectUser " +
            "INNER JOIN Project ON ProjectId = IdProject WHERE IdUser = 1",
            qrRole = "select [RoleId], [RoleName] from [Role]",
            qrUsers = "select [UserId] as 'ID', [UserSurname] as 'Фамилия', [UserName] as 'Имя', [UserMiddleName] as 'Отчество', " +
            "[UserLogin] as 'Логин', [UserPassword], [UserEmail] as 'Почта', [IdRole], [RoleName] as 'Роль' " +
            "from [User] " +
            "inner join [Role] on [RoleId] = [IdRole]",
            qrProjectsAdmin = "select [ProjectId] as 'ID', [ProjectName] as 'Название проекта', [ProjectVersion] as 'Версия проекта' from [Project]";



        private SqlCommand command = new SqlCommand("", connection);
        public static int IdRecord; //Выбранная запись
        /// <summary>
        /// Роль пользователя
        /// </summary>
        /// <param name="UserLogin">Логин или почта пользователя</param>
        /// <returns>id роли</returns>
        public string UserRole(string UserLogin)
        {
            string RoleId;
            try
            {
                command.CommandText = "select [IdRole] from [User] where ([UserLogin] like '%" + UserLogin + "%' or [UserEmail] like '%" + UserLogin + "%')";
                connection.Open();
                RoleId = command.ExecuteScalar().ToString();
                return RoleId;
            }
            catch
            {
                RoleId = "3";
                return RoleId;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Проверка уникальностии логина
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <returns>Количество найденных пользователей</returns>
        public Int32 LoginCheck(string Login)
        {
            int LoginCheck;
            try
            {
                command.CommandText = "select count (*) from [User] where [UserLogin] like '%" + Login + "%'";
                connection.Open();
                LoginCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
                return LoginCheck;
            }
            catch
            {
                LoginCheck = 0;
                return LoginCheck;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Проверка уникальностии почты
        /// </summary>
        /// <param name="Mail">Логин</param>
        /// <returns>Количество найденных пользователей</returns>
        public Int32 MailCheck(string Mail)
        {
            int MailCheck;
            try
            {
                command.CommandText = "select count (*) from [User] where [UserEmail] like '%" + Mail + "%'";
                connection.Open();
                MailCheck = Convert.ToInt32(command.ExecuteScalar().ToString());
                return MailCheck;
            }
            catch
            {
                MailCheck = 0;
                return MailCheck;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}