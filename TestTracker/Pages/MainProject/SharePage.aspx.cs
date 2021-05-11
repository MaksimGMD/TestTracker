using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Configuration;
using System.Data;

namespace TestTracker.Pages.MainProject
{
    public partial class SharePage : System.Web.UI.Page
    {
        private string QR = "";
        string ProjectId;
        private string ProjectName;
        private string ProjectVersion;
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectId = Request.QueryString["ProjectID"];
            QR = "select DISTINCT * from [TestExport] where [IdProject] = '" + ProjectId + "'";
            if (!IsPostBack)
            {
                if (ProjectId != null)
                {
                    gvTestExportFill(QR);
                    cbUsersFill();
                    alert.Visible = false;
                    lblError.Visible = false;
                    //gvTestsExport.DataSource = this.GetData();
                    //gvTestsExport.DataBind();
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
        }
        //Очитска полей
        protected void Cleaner()
        {
            tbMessage.Text = string.Empty;
            cbUsers.SelectedIndex = -1;
            alert.Visible = true;
            lblError.Visible = false;
        }
        //Отправка письма
        protected void btSent_Click(object sender, EventArgs e)
        {
            Project_Data(); //Получение названия и версии проекта
            //Отправка сообщения на почту
            try
            {
                int port = 587;
                bool enableSSL = true;
                string emailFrom = "test_tracker@bk.ru"; //почта отправителя
                string password = "Bot7890!"; //пароль оправителя
                string subject = "Отчёт " + ProjectName + " v" + ProjectVersion; //Заголовок сообщения
                string smtpAddress = "smtp.mail.ru"; //smtp протокол
                string message = tbMessage.Text; //Сообщение

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                //Добавление получателей письма на основе списка
                if (cbUsers.SelectedIndex == -1)
                {
                    lblError.Visible = true;
                }
                else
                {
                    foreach (ListItem li in cbUsers.Items)
                    {
                        if (li.Selected)
                        {
                            mail.To.Add(li.Text);
                        }
                    }
                    mail.Subject = subject;
                    mail.Body = message; //тело сообщения
                    mail.IsBodyHtml = false;

                    MemoryStream str = Create_Excel_File();
                    Attachment at = new Attachment(str, "Отчёт " + ProjectName + " v" + ProjectVersion + ".xls");
                    mail.Attachments.Add(at);
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch
            {
                alert.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось отправить сообщение :(')", true);
            }
            finally
            {
                Cleaner();
            }
        }
        //Экспорт
        protected MemoryStream Create_Excel_File()
        {
            Response.AppendHeader("content-disposition", "attachment; filename=Отчёт " + ProjectName + " v" + ProjectVersion + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            gvTestsExport.RenderControl(htmlTextWriter);
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentEncoding = System.Text.Encoding.Default;
            MemoryStream s = new MemoryStream();
            System.Text.Encoding Enc = System.Text.Encoding.Default;
            byte[] mBArray = Enc.GetBytes(stringWriter.ToString());
            s = new MemoryStream(mBArray, false);
            return s;

        }
        //Создание теблица для экспорта
        private void gvTestExportFill(string qr)
        {
            sdsTestsExport.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTestsExport.SelectCommand = qr;
            sdsTestsExport.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTestsExport.DataSource = sdsTestsExport;
            gvTestsExport.DataBind();
        }

        private void cbUsersFill()
        {
            sdsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = DBConnection.qrUsersMail + "where [IdProject] = '" + ProjectId + "'";
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            cbUsers.DataSource = sdsUsers;
            cbUsers.DataTextField = "UserEmail";
            cbUsers.DataValueField = "IdUser";
            cbUsers.DataBind();
        }
        //Применение фильтра по датам
        protected void btFilter_Click(object sender, EventArgs e)
        {
            DateTime theDate1 = DateTime.ParseExact(tbStart.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string StartDate = theDate1.ToString("dd.MM.yyyy");
            DateTime theDate2 = DateTime.ParseExact(tbEnd.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string EndDate = theDate2.ToString("dd.MM.yyyy");
            string newData = QR + "and [Дата] BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
            gvTestExportFill(newData);
            btCancel.Visible = true;
        }
        //Отменить фильтрацию
        protected void btCancel_Click(object sender, EventArgs e)
        {
            gvTestExportFill(QR);
            tbEnd.Text = string.Empty;
            tbStart.Text = string.Empty;
            btCancel.Visible = false;
        }
        protected void Project_Data()
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [ProjectName] from [Project] where [ProjectId] = '" + ProjectId + "'";
            try
            {
                DBConnection.connection.Open();
                ProjectName = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                Response.Redirect("Projects.aspx");
            }
            finally
            {
                DBConnection.connection.Close();
            }
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [ProjectVersion] from [Project] where [ProjectId] = '" + ProjectId + "'";
            try
            {
                DBConnection.connection.Open();
                ProjectVersion = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
            }
            catch
            {
                Response.Redirect("Projects.aspx");
            }
            finally
            {
                DBConnection.connection.Close();
            }
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}
        //Выбор в получателя
        protected void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Получение данных для таблицы
        private DataTable GetData()
        {
            string constr = "Data Source=DESKTOP-2OC8HFJ\\MYGRIT; Initial Catalog=TestTracker;" +
            "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
            "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select DISTINCT * from [TestExport] where [IdProject] = '" + ProjectId + "'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        //Отправка письма
        //protected void btSent_Click(object sender, EventArgs e)
        //{
        //    bool enableSSL = true;
        //    string emailFrom = "test_tracker@bk.ru"; //почта отправителя
        //    string password = "Bot7890!"; //пароль оправителя
        //    /*string emailTo = "i_m.d.gritsuk@mpt.ru"; //Почта получателя
        //    string subject = "Отчёт " + ProjectName + " v" + ProjectVersion; //Заголовок сообщения
        //    string smtpAddress = "smtp.mail.ru"; //smtp протокол
        //    /*string tittle = ddlTittle.SelectedValue.ToString();*/ //Заголовок сообщения;
        //                                                            //string name = "от: " + tbName.Text.ToString() + " почта: " + tbmail.Text;
        //    string message = tbMessage.Text; //Сообщение
        //    //Get the GridView Data from database.
        //    DataTable dt = GetData();

        //    //Set DataTable Name which will be the name of Excel Sheet.
        //    dt.TableName = "GridView_Data";

        //    //Create a New Workbook.
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        //Add the DataTable as Excel Worksheet.
        //        wb.Worksheets.Add(dt);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            //Save the Excel Workbook to MemoryStream.
        //            wb.SaveAs(memoryStream);

        //            //Convert MemoryStream to Byte array.
        //            byte[] bytes = memoryStream.ToArray();
        //            memoryStream.Close();

        //            //Send Email with Excel attachment.
        //            using (MailMessage mail = new MailMessage())
        //            {
        //                foreach (ListItem li in cbUsers.Items)
        //                {
        //                    if (li.Selected)
        //                    {
        //                        mail.To.Add(li.Text);
        //                    }
        //                }
        //                mail.From = new MailAddress(emailFrom);
        //                mail.Subject = "Отчёт " + ProjectName + " v" + ProjectVersion;
        //                mail.Body = "GridView Exported Excel Attachment";
        //                //Add Byte array as Attachment.
        //                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "GridView.xlsx"));
        //                mail.IsBodyHtml = true;
        //                SmtpClient smtp = new SmtpClient();
        //                smtp.Host = "smtp.mail.ru";
        //                smtp.EnableSsl = true;
        //                NetworkCredential credentials = new NetworkCredential();
        //                credentials.UserName = emailFrom;
        //                credentials.Password = password;
        //                smtp.UseDefaultCredentials = true;
        //                smtp.Credentials = credentials;
        //                smtp.Port = 587;
        //                smtp.Send(mail);
        //            }
        //        }
        //    }
        //}
    }
}
