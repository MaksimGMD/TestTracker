using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Encryption_Library;

namespace TestTracker
{
    public class DataProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        //Для работы с базой данных из БД
        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        //Добавление пользователя
        public void UserInsert(string UserName, string UserMiddleName, string UserSurname, string UserLogin, string UserPassword, string UserEmail, int IdRole)
        {
            string Password = Encryption.EncryptedDate(UserPassword);
            commandConfig("UserInsert");
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@UserMiddleName", UserMiddleName);
            command.Parameters.AddWithValue("@UserSurname", UserSurname);
            command.Parameters.AddWithValue("@UserLogin", UserLogin);
            command.Parameters.AddWithValue("@UserPassword", Password);
            command.Parameters.AddWithValue("@UserEmail", UserEmail);
            command.Parameters.AddWithValue("@IdRole", IdRole);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление пользователя
        public void UserUpdate(int UserId, string UserName, string UserMiddleName, string UserSurname, string UserLogin, string UserEmail, int IdRole)
        {
            commandConfig("UserUpdate");
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@UserMiddleName", UserMiddleName);
            command.Parameters.AddWithValue("@UserSurname", UserSurname);
            command.Parameters.AddWithValue("@UserLogin", UserLogin);
            command.Parameters.AddWithValue("@UserEmail", UserEmail);
            command.Parameters.AddWithValue("@IdRole", IdRole);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление пароля пользователя
        public void UserPasswordUpdate(int UserId, string UserPassword)
        {
            string Password = Encryption.EncryptedDate(UserPassword);
            commandConfig("UserPasswordUpdate");
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@UserPassword", Password);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление пользователя
        public void UserDelete(int UserId)
        {
            commandConfig("UserDelete");
            command.Parameters.AddWithValue("@UserId", UserId);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление проекта
        public void ProjectInsert(string ProjectName, int ProjectVersion)
        {
            commandConfig("ProjectInsert");
            command.Parameters.AddWithValue("@ProjectName", ProjectName);
            command.Parameters.AddWithValue("@ProjectVersion", ProjectVersion);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление проекта
        public void ProjectUpdate(int ProjectId, string ProjectName, int ProjectVersion)
        {
            commandConfig("ProjectUpdate");
            command.Parameters.AddWithValue("@ProjectId", ProjectId);
            command.Parameters.AddWithValue("@ProjectName", ProjectName);
            command.Parameters.AddWithValue("@ProjectVersion", ProjectVersion);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление проекта
        public void ProjectDelete(int ProjectId)
        {
            commandConfig("ProjectDelete");
            command.Parameters.AddWithValue("@ProjectId", ProjectId);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление пользователей проекта
        public void ProjectUserInsert(int IdProject, int IdUser)
        {
            commandConfig("ProjectUserInsert");
            command.Parameters.AddWithValue("@IdProject", IdProject);
            command.Parameters.AddWithValue("@IdUser", IdUser);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление пользователей проекта
        public void ProjectUserUpdate(int ProjectUserId, int IdProject, int IdUser)
        {
            commandConfig("ProjectUserUpdate");
            command.Parameters.AddWithValue("@ProjectUserId", ProjectUserId);
            command.Parameters.AddWithValue("@IdProject", IdProject);
            command.Parameters.AddWithValue("@IdUser", IdUser);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление пользователей проекта
        public void ProjectUserDelete(int ProjectUserId)
        {
            commandConfig("ProjectUserDelete");
            command.Parameters.AddWithValue("@ProjectUserId", ProjectUserId);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление теста
        public void TestInsert(string TestName, string TestDescription, string TestDate, string TestResult, 
            string TestJiraNumber, bool TestLogicalDelete, int IdStatus, int IdProject)
        {
            commandConfig("TestInsert");
            command.Parameters.AddWithValue("@TestName", TestName);
            command.Parameters.AddWithValue("@TestDescription", TestDescription);
            command.Parameters.AddWithValue("@TestDate", TestDate);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@TestJiraNumber", TestJiraNumber);
            command.Parameters.AddWithValue("@TestLogicalDelete", TestLogicalDelete);
            command.Parameters.AddWithValue("@IdStatus", IdStatus);
            command.Parameters.AddWithValue("@IdProject", IdProject);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление теста
        public void TestUpdate(int TestId, string TestName, string TestDescription, string TestResult,
            string TestJiraNumber, bool TestLogicalDelete, int IdStatus, int IdProject, bool IsModified)
        {
            commandConfig("TestUpdate");
            command.Parameters.AddWithValue("@TestId", TestId);
            command.Parameters.AddWithValue("@TestName", TestName);
            command.Parameters.AddWithValue("@TestDescription", TestDescription);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@TestJiraNumber", TestJiraNumber);
            command.Parameters.AddWithValue("@TestLogicalDelete", TestLogicalDelete);
            command.Parameters.AddWithValue("@IdStatus", IdStatus);
            command.Parameters.AddWithValue("@IdProject", IdProject);
            command.Parameters.AddWithValue("@IsModified", IsModified);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление теста
        public void TestDelete(int TestId)
        {
            commandConfig("TestDelete");
            command.Parameters.AddWithValue("@TestId", TestId);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}