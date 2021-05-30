using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace TestTracker.Pages.MainProject
{
    public partial class TestExport : System.Web.UI.Page
    {
        private string QR = "";
        string ProjectId;
        string ProjectName;
        string ProjectVersion;
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectId = Request.QueryString["ProjectID"];
            QR = "select DISTINCT * from [TestExport] where [IdProject] = '" + ProjectId + "'";
            if (!IsPostBack)
            {
                if (ProjectId != null)
                {
                    ProjectData(ProjectId);
                    gvTestExportFill(QR);
                }
                else
                {
                    Response.Redirect("Projects.aspx");
                }
            }
        }
        //Данные о проекте
        private void ProjectData(string ProjectId)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select [ProjectName] from [Project] where [ProjectId] = '" + ProjectId + "'";
            try
            {
                DBConnection.connection.Open();
                ProjectName = command.ExecuteScalar().ToString();
                command.ExecuteNonQuery();
                lblProjectName.Text = ProjectName;
                lblTitleExample.Text = "Отчёт " + ProjectName;
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
                lblProjectVersion.Text = "v" + ProjectVersion;
                lblVersionExample.Text = "v" + ProjectVersion;
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
        //Создание теблица для экспорта
        private void gvTestExportFill(string qr)
        {
            sdsTestsExport.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTestsExport.SelectCommand = qr;
            sdsTestsExport.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTestsExport.DataSource = sdsTestsExport;
            gvTestsExport.DataBind();
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
        //Экспорт
        protected void btExport_Click(object sender, EventArgs e)
        {

            try
            {
                Response.ClearContent();
                Response.AppendHeader("content-disposition", "attachment; filename=Отчёт " + lblProjectName.Text + " " + lblProjectVersion.Text + ".xls");
                Response.ContentType = "appliction/excel";
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                gvTestsExport.RenderControl(htmlTextWriter);
                Response.Write(stringWriter.ToString());
                Response.End();
                Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
            }
            catch
            {

            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        //Возвращение к тестам
        protected void btBack_Click(object sender, EventArgs e)
        {
            //Перейти на страницу Tests.aspx с ProjectId в зашифрованном виде
            Response.Redirect("Tests.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
    }
}