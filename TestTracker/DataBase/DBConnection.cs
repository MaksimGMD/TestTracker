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
            qrProjects = "SELECT ProjectId,  ProjectName, ProjectVersion, (SELECT COUNT(*) FROM Test WHERE IdProject = ProjectUser.IdProject and [TestLogicalDelete] = 'False') AS 'TesCount' FROM ProjectUser " +
            "INNER JOIN Project ON ProjectId = IdProject " +
            "inner join [User] on [IdUser] = [UserId]",
            qrRole = "select [RoleId], [RoleName] from [Role]",
            qrUsers = "select [UserId] as 'ID', [UserSurname] as 'Фамилия', [UserName] as 'Имя', [UserMiddleName] as 'Отчество', " +
            "[UserLogin] as 'Логин', [UserPassword], [UserEmail] as 'Почта', [IdRole], [RoleName] as 'Роль' " +
            "from [User] " +
            "inner join [Role] on [RoleId] = [IdRole]",
            qrProjectsAdmin = "select [ProjectId] as 'ID', [ProjectName] as 'Название проекта', [ProjectVersion] as 'Версия проекта' from [Project]",
            qrUsersConcat = "select [UserId] as 'ID', (CONCAT_WS(' ',[UserSurname],[UserName],[UserMiddleName])) as 'Пользователь' from [User]",
            qrProjectsUsers = "select [ProjectUserId] as 'ID', [ProjectId], [ProjectName] as 'Проект', [UserId], " +
            "(CONCAT_WS(' ',[UserSurname],[UserName],[UserMiddleName])) as [Пользователь] from [ProjectUser] " +
            "inner join [User] on [UserId] = [IdUser] " +
            "inner join [Project] on [ProjectId] = [IdProject]",
            qrTests = "select [TestID] as 'ID', [TestName] as 'Название теста', [TestDescription] as 'Описание теста', [TestDate] as 'Дата теста', " +
            "[TestResult] as 'Результат теста', [TestJiraNumber] as 'Номер в Jira', [IdStatus], [StatusName] as 'Статус', [IdProject], [ProjectName] as 'Проект', " +
            "[TestLogicalDelete] 'Помечен на удаление'  from [Test] " +
            "inner join [Status] on [StatusId] = [IdStatus] " +
            "inner join [Project] on [ProjectId] = [IdProject]",
            qrStatus = "select [StatusId] as 'ID',   [StatusName] as 'Статус' from [Status] order by [ID] desc",
            qrSteps = "select [StepId] as 'ID', [StepNumber] as 'Номер этапа', [StepName] as 'Название этапа', [IdTest], [TestName] as 'Тест' from [Step] " +
            "inner join [Test] on [TestId] = [IdTest]",
            qrComment = "select [CommentId] as 'ID', [CommentContent] as 'Комментарий', [CommentDate] as 'Дата комментария', [IdUser], " +
            "(CONCAT_WS(' ',[UserSurname],[UserName])) as 'Пользователь', [IdTest], [TestName] as 'Тест' from[Comment] " +
            "inner join[User] on[UserId] = [IdUser] " +
            "inner join[Test] on[TestId] = [IdTest]",
            qrTestMain = "select [TestId] as 'ID', [TestName] as 'Название теста', [TestDescription] as 'Описание', [TestDate] as 'Дата', " +
            "[IdStatus], [StatusName] as 'Статус', [TestJiraNumber] as 'Номер задачи', [TestResult] from [Test] inner join [Status] on [StatusId] = [IdStatus] where [TestLogicalDelete] = 'False'",
            qrUsersMail = "select [IdUser], [UserEmail] from [ProjectUser] inner join [User] on [IdUser] = [UserId]",
            qrUsersTest = "select [IdProject], " +
            "[UserSurname] + ' ' + [UserName] + ' ' + [UserMiddleName] as 'Пользователь' from [ProjectUser] " +
            "inner join [User] on [UserId] = [IdUser] " +
            "inner join [Project] on [ProjectID] = [IdProject]",
            qrTestDetails = "select ProjectName, TestDate, StatusName, TestJiraNumber from [Test] " +
            "inner join [Status] on [StatusId] = [IdStatus] " +
            "inner join [Project] on [ProjectId] = [IdProject]",
            qrTestSteps = "select StepId, StepNumber, StepName from [Step]";



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
        /// <summary>
        /// Получает id пользователя
        /// </summary>
        /// <param name="UserName">Логин или электронная почта пользователя</param>
        /// <returns>id пользователя</returns>
        public Int32 GetUserId(string UserName)
        {
            try
            {
                command.CommandText = "select [UserId] from[User] " +
                    "where[UserLogin] like '%" + UserName + "%' or [UserEmail] like '%" + UserName + "%'";
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            catch
            {
                return Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}