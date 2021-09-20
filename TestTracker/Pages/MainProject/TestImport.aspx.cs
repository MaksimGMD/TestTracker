using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TestTracker.Pages.MainProject
{
    public partial class TestImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //Coneection String by default empty
            string ConStr = "";
            //Extantion of the file upload control saving into ext because 
            //there are two types of extation .xls and .xlsx of excel 
            string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
            //getting the path of the file 
            string path = Server.MapPath("~/MyFolder" + FileUpload1.FileName);
            //saving the file inside the MyFolder of the server
            FileUpload1.SaveAs(path);
            //checking that extantion is .xls or .xlsx
            if (ext.Trim() == ".xls")
            {
                //connection string for that file which extantion is .xls
                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                //connection string for that file which extantion is .xlsx
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            //making query
            string query = "SELECT * FROM [Тесты МПП2$]";
            //Providing connection
            OleDbConnection conn = new OleDbConnection(ConStr);
            //checking that connection state is closed or not if closed the 
            //open the connection
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //create command object
            OleDbCommand cmd = new OleDbCommand(query, conn);
            // create a data adapter and get the data into dataadapter
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            //fill the excel data to data set
            da.Fill(ds);
            DataProcedures procedures = new DataProcedures();
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var Test = ds.Tables[0].Rows[i].ItemArray;
                    procedures.TestInsert(Test[2].ToString(), "Не указано", "16.09.2021", Test[4].ToString(), Test[7].ToString(), false, Convert.ToInt32(Test[5].ToString()), 14, Test[1].ToString());

                    command.CommandText = "SELECT max(TestId) from[Test]";
                    DBConnection.connection.Open();
                    string LastId = command.ExecuteScalar().ToString();
                    DBConnection.connection.Close();

                    string steps = Test[3].ToString();
                    string[] splitsSteps = Regex.Split(steps, @"\d\.\s");
                    foreach (var s in splitsSteps)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            int index = Array.IndexOf(splitsSteps, s);
                            procedures.StepInsert(Convert.ToString(index), s, Convert.ToInt32(LastId));
                        }
                    }

                    if (Test[6].ToString() != "")
                    {
                        procedures.CommentInsert(Test[6].ToString(), "16.09.2021", 9, Convert.ToInt32(LastId));
                    }
                }
            }
            conn.Close();
        }
    }
}