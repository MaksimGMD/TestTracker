using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TestTracker.DataBase
{
    public class DBConnection
    {
        //Строка подключения к базе данных
        public static SqlConnection connection = new SqlConnection(
            "Data Source=DESKTOP-2OC8HFJ\\MYGRIT; Initial Catalog=TestTracker;" +
            "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
            "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");
    }
}