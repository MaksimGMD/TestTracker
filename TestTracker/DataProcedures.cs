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
    }
}