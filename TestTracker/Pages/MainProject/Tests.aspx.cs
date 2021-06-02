using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace TestTracker.Pages.MainProject
{
    public partial class Tests : System.Web.UI.Page
    {
        private string QRrp = "";
        private string QR = "";
        private string QRFilter;
        string ProjectId;
        string ProjectName;
        string ProjectVersion;
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectId = Request.QueryString["ProjectID"];
            QRrp = "select count([TestId]) as 'Всего', (select count([TestId]) from [Test] where [IdStatus] = 1 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False') as 'Успешно', " +
                "(select (select count([TestId]) from [Test] where [IdStatus] = 1 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False')*100/cast((select count([TestId])) as decimal (10,2))) as '% Успешно', " +
                "(select count([TestId]) from [Test] where [IdStatus] = 2 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False') as 'Замечание', " +
                "(select (select count([TestId]) from [Test] where [IdStatus] = 2 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False')*100/cast((select count([TestId])) as decimal (10,2)))  as '% Замечание', " +
                "(select count([TestId]) from [Test] where [IdStatus] = 3 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False') as 'Не успешно',  " +
                "(select (select count([TestId]) from [Test] where [IdStatus] = 3 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False')*100/cast((select count([TestId])) as decimal (10,2))) as '% Не успешно', " +
                "(select count([TestId]) from [Test] where [IdStatus] = 4 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False') as 'Не проводился', " +
                "(select (select count([TestId]) from [Test] where [IdStatus] = 4 and [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False')*100/cast((select count([TestId])) as decimal (10,2))) as '% Не проводился' " +
                "from [Test] where [IdProject] = '" + ProjectId + "' and [TestLogicalDelete] = 'False'";

            QR = DBConnection.qrTestMain + "and [IdProject] = '" + ProjectId + "'";
            if (!IsPostBack)
            {
                if (ProjectId != null)
                {
                    ProjectData(ProjectId);
                    rpFill(QRrp);
                    gvFill(QR);
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

        //Заполнение данными прогресс бара
        private void rpFill(string qr)
        {
            try
            {
                dvProgress.Visible = true;
                sdsProgress.ConnectionString = DBConnection.connection.ConnectionString.ToString();
                sdsProgress.SelectCommand = qr;
                sdsProgress.DataSourceMode = SqlDataSourceMode.DataReader;
                rpProgress.DataSource = sdsProgress;
                rpProgress.DataBind();
                dvProgress.Visible = true;
            }
            catch
            {
                dvProgress.Visible = false;
            }
        }

        //Заполнение таблицы
        private void gvFill(string qr)
        {
            sdsTests.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTests.SelectCommand = qr;
            sdsTests.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTests.DataSource = sdsTests;
            gvTests.DataBind();
        }

        protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false; //ID
            e.Row.Cells[4].Visible = false; //Название теста
            e.Row.Cells[7].Visible = false; //ID status
            e.Row.Cells[10].Visible = false; //ID status
        }
        //Сортировка данных в таблице
        protected void gvTests_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID]";
                    break;
                case ("Название теста"):
                    e.SortExpression = "[Название теста]";
                    break;
                case ("Описание"):
                    e.SortExpression = "[TestDescription]";
                    break;
                case ("Дата"):
                    e.SortExpression = "[TestDate]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[StatusName]";
                    break;
                case ("Номер задачи"):
                    e.SortExpression = "[TestJiraNumber]";
                    break;
            }
            sortGridView(gvTests, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }
        //Удаление теста
        protected void gvTests_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvTests.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvTests.Rows[Index].Cells[1].Text.ToString());
                procedure.TestLogicalDelete(DBConnection.IdRecord, Convert.ToInt32(ProjectId));
                Response.Redirect(Request.Url.AbsoluteUri);
                DBConnection.IdRecord = 0;
                rpFill(QRrp);
                gvFill(QR);
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Добавить запись
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection.TestAddValid = true;
            Response.Redirect("AddTest.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvTests.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[4].Text.Equals(tbSearch.Text) ||
                    row.Cells[5].Text.Equals(tbSearch.Text) ||
                    row.Cells[6].Text.Equals(tbSearch.Text) ||
                    row.Cells[8].Text.Equals(tbSearch.Text) ||
                    row.Cells[9].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }

        //Отмена выбора
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            btCancel.Visible = false;
            gvFill(QR);
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = "select [TestId] as 'ID', [TestName] as 'Название теста', [TestDescription] as 'Описание', [TestDate] as 'Дата', " +
                    "[IdStatus], [StatusName] as 'Статус', [TestJiraNumber] as 'Номер задачи', [TestResult] from [Test] inner join [Status] on [StatusId] = [IdStatus] " +
                    "where ([TestLogicalDelete] = 'False' and [IdProject] = '" + ProjectId + "') and ([TestID] like '%" + tbSearch.Text + "%' or [TestName] like '%" + tbSearch.Text + "%' " +
                    "or [TestDescription] like '%" + tbSearch.Text + "%' or [TestDate] like '%" + tbSearch.Text + "%' or" +
                    "[StatusName] = '" + tbSearch.Text + "' or [TestJiraNumber] like '%" + tbSearch.Text + "%')";
                rpFill(QRrp);
                gvFill(newQR);
                btCancel.Visible = true;
                tbStartDate.Text = string.Empty;
                tbEndDate.Text = string.Empty;
                ddlStatus.SelectedIndex = 0;

            }
        }
        //Открывает страницу предварительного просмотра
        protected void btExport_Click(object sender, EventArgs e)
        {

            Response.Redirect("TestExport.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
        //Открывает страницу отправки письма
        protected void btShare_Click(object sender, EventArgs e)
        {

            Response.Redirect("SharePage.aspx?ProjectID=" + Server.UrlEncode(ProjectId));
        }
        //Применить фильтр по датам
        protected void btDateFilter_Click(object sender, EventArgs e)
        {
            DateTime theDate1 = DateTime.ParseExact(tbStartDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string StartDate = theDate1.ToString("dd.MM.yyyy");
            DateTime theDate2 = DateTime.ParseExact(tbEndDate.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string EndDate = theDate2.ToString("dd.MM.yyyy");
            string newData = QR + "and parse([TestDate] as date using 'ru-RU') >= '" + StartDate + "' and parse([TestDate] as date using 'ru-RU') <= '" + EndDate + "'";
            QRFilter = QRFilter+newData;
            gvFill(newData);
            btCancel.Visible = false;
            ddlStatus.SelectedIndex = 0;
            tbSearch.Text = string.Empty;
        }
        //Отменить фильтр по датам
        protected void btDateFilterCancel_Click(object sender, EventArgs e)
        {
            gvFill(QR);
            tbStartDate.Text = string.Empty;
            tbEndDate.Text = string.Empty;
            btCancel.Visible = false;
            ddlStatus.SelectedIndex = 0;
        }
        //Выбор статуса теста
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newQR;
            tbStartDate.Text = string.Empty;
            tbEndDate.Text = string.Empty;
            btCancel.Visible = false;
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            switch (ddlStatus.SelectedValue)
            {
                case("Статус"):
                    QRFilter = QR;
                    gvFill(QR);
                    break;
                case ("Успешно"):
                    newQR = QR + "and [StatusName] = '" + ddlStatus.SelectedValue + "'";
                    QRFilter = newQR;
                    gvFill(QRFilter);
                    break;
                case ("Замечание"):
                    newQR = QR + "and [StatusName] = '" + ddlStatus.SelectedValue + "'";
                    QRFilter = newQR;
                    gvFill(QRFilter);
                    break;
                case ("Не успешно"):
                    newQR = QR + "and [StatusName] = '" + ddlStatus.SelectedValue + "'";
                    QRFilter = newQR;
                    gvFill(QRFilter);
                    break;
                case ("Не проводился"):
                    newQR = QR + "and [StatusName] = '" + ddlStatus.SelectedValue + "'";
                    QRFilter = newQR;
                    gvFill(QRFilter);
                    break;
            }
        }
    }
}