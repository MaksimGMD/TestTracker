using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

namespace TestTracker.Pages.MainProject
{
    public partial class FeedBack : System.Web.UI.Page
    {
        int UserId;
        string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProfile;
            if (!IsPostBack)
            {
                DBConnection connection = new DBConnection();
                alert.Visible = false;
                UserId = connection.GetUserId(HttpContext.Current.User.Identity.Name.ToString());
                lblName.Text = connection.GetUserName(UserId);
                lblEmail.Text = connection.GetMail(UserId);
            }
        }
        //Отправка сообщения
        protected void btSubmit_Click(object sender, EventArgs e)
        {
            //Отправка сообщения на почту
            try
            {
                int port = 587;
                bool enableSSL = true;
                string emailFrom = "bot.feedback@bk.ru"; //почта отправителя
                string password = "Privet@12345"; //пароль оправителя
                string emailTo = "i_m.d.gritsuk@mpt.ru"; //Почта получателя
                string subject = ddlTittle.SelectedValue.ToString(); //Заголовок сообщения
                string smtpAddress = "smtp.mail.ru"; //smtp протокол
                string tittle = ddlTittle.SelectedValue.ToString(); //Заголовок сообщения;
                string name = "от: " + lblName.Text + " почта: " + lblEmail.Text;
                string message = tbMessage.Text; //Сообщение

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = tittle + "\r\n" + name + "\r\n" + "---------------------------------------" + "\r\n" + message; //тело сообщения
                mail.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
                alert.Visible = true;
                tbMessage.Text = string.Empty;
            }
            catch
            {
                alert.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось отправить сообщение :(')", true);
            }
        }
    }
}