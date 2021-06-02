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
            lblError.Visible = false;
        }

        //Экспорт
        protected MemoryStream Create_Excel_File()
        {
            Response.AppendHeader("content-disposition", "attachment; filename=Отчёт по проекту " + ProjectName + " v" + ProjectVersion + ".xls");
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
        //Заполнение списка получателей
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
            lblStart.Text = StartDate;
            lblEnd.Text = EndDate;
            string newData = QR + "and parse([Дата] as date using 'ru-RU') >= '" + lblStart.Text + "' and parse([Дата] as date using 'ru-RU') <= '" + lblEnd.Text + "'";
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
            lblStart.Text = "";
            lblEnd.Text = "";
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
        public override void VerifyRenderingInServerForm(Control control)
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
                if(lblStart.Text != "" & lblStart.Text !="")
                {
                    using (SqlCommand cmd = new SqlCommand("select DISTINCT [ID теста], [Название теста], [Описание], [Сценарий тестирования], [Результат], " +
                        "[Дата], [Статус], [Комментарий], [Номер задачи]  from [TestExport] where [IdProject] = '" + ProjectId + "' " +
                                        "and parse([Дата] as date using 'ru-RU') >= '" + lblStart.Text + "' and parse([Дата] as date using 'ru-RU') <= '" + lblEnd.Text + "'"))
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
                else
                {
                    using (SqlCommand cmd = new SqlCommand("select DISTINCT [ID теста], [Название теста], [Описание], [Сценарий тестирования], [Результат], " +
                        "[Дата], [Статус], [Комментарий], [Номер задачи] from [TestExport] where [IdProject] = '" + ProjectId + "'"))
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
        }
        //Отправка письма
        protected void btSent_Click(object sender, EventArgs e)
        {
            Project_Data(); // Получение названия и версии проекта
                            //Получает данные для таблицы из БД
            DataTable dt = GetData();
            dt.TableName = "Отчёт";
            using (XLWorkbook wb = new XLWorkbook())
            {
                var wsDep = wb.Worksheets.Add(dt);
                wsDep.Columns("A", "J").AdjustToContents();
                for (int i = 1; i <= dt.Rows.Count + 1; i++)
                {
                    for (int j = 1; j <= dt.Columns.Count; j++)
                    {
                        wsDep.Cell(i, j).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        wsDep.Cell(i, j).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    }
                }
                wsDep.Column("A").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsDep.Column("F").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsDep.Column("G").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsDep.Column("I").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                Project_Data(); //Получение названия и версии проекта
                                //Отправка сообщения на почту
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //Save the Excel Workbook to MemoryStream.
                    wb.SaveAs(memoryStream);

                    //Convert MemoryStream to Byte array.
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    int port = 587;
                    bool enableSSL = true;
                    string emailFrom = "bot.feedback@bk.ru"; //почта отправителя
                    string password = "Privet@12345"; //пароль оправителя
                    string subject = "Отчёт " + ProjectName + " v" + ProjectVersion; //Заголовок сообщения
                    string smtpAddress = "smtp.mail.ru"; //smtp протокол
                    string message = tbMessage.Text; //Сообщение

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailFrom);
                    //прикрепление файла к письму
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Отчёт " + ProjectName + " v" + ProjectVersion + ".xls"));
                    mail.IsBodyHtml = true;
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
                        try
                        {
                            using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                            alert.Visible = true;
                            Cleaner();
                        }
                        catch
                        {
                            alert.Visible = false;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось отправить сообщение :(')", true);
                        }
                    }
                }
            }
        }

        //Возвращение к тестам
        protected void btBack_Click(object sender, EventArgs e)
        {
            //Перейти на страницу Tests.aspx с ProjectId в зашифрованном виде
            Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
    }
}
