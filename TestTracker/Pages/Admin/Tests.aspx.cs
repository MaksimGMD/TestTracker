using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;

namespace TestTracker.Pages.Admin
{
    public partial class Tests : System.Web.UI.Page
    {
        private string QR = "";
        private bool IsModified;
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrTests;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlProjectFill();
                ddlStatusFill();
            }
        }
        //Заполнение таблицы данными
        private void gvFill(string qr)
        {
            sdsTests.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTests.SelectCommand = qr;
            sdsTests.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTests.DataSource = sdsTests;
            gvTests.DataBind();
        }
        //Заполнение списка статусов
        private void ddlStatusFill()
        {
            sdsStatus.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsStatus.SelectCommand = DBConnection.qrStatus;
            sdsStatus.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlStatus.DataSource = sdsStatus;
            ddlStatus.DataTextField = "Статус";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
        }
        //Заполнение списка проектов
        private void ddlProjectFill()
        {
            sdsProjects.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProjects.SelectCommand = DBConnection.qrProjectsAdmin;
            sdsProjects.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlProject.DataSource = sdsProjects;
            ddlProject.DataTextField = "Название проекта";
            ddlProject.DataValueField = "ID";
            ddlProject.DataBind();
        }
        //Очитска полей
        protected void Cleaner()
        {
            SelectedMessage.Visible = false;
            DBConnection.IdRecord = 0;
            ddlStatus.SelectedIndex = 0;
            ddlProject.SelectedIndex = 0;
            tbDescription.Text = string.Empty;
            tbNumber.Text = string.Empty;
            tbResult.Text = string.Empty;
            tbTestName.Text = string.Empty;
            IsModified = false;
        }
        protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTests, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Удаление данных
        protected void gvTests_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvTests.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvTests.Rows[Index].Cells[1].Text.ToString());
                procedure.TestDelete(DBConnection.IdRecord);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlProjectFill();
                ddlStatusFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Выбор записи
        protected void gvTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTests.Rows)
            {
                if (row.RowIndex == gvTests.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvTests.SelectedRow.RowIndex;
            GridViewRow rows = gvTests.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbTestName.Text = rows.Cells[2].Text.ToString();
            tbDescription.Text = rows.Cells[3].Text.ToString();
            tbResult.Text = rows.Cells[5].Text.Replace("&nbsp;", "");
            tbNumber.Text = rows.Cells[6].Text.ToString();
            ddlProject.SelectedValue = rows.Cells[9].Text.ToString();
            ddlStatus.SelectedValue = rows.Cells[7].Text.ToString();
            CheckBox checkBox = (CheckBox)gvTests.Rows[selectedRow].Cells[11].Controls[0];
            cbDelete.Checked = checkBox.Checked;
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;
        }
        //Сортировка данных в таблице

        protected void gvTests_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[TestID]";
                    break;
                case ("Название теста"):
                    e.SortExpression = "[TestName]";
                    break;
                case ("Описание теста"):
                    e.SortExpression = "[TestDescription]";
                    break;
                case ("Дата теста"):
                    e.SortExpression = "[TestDate]";
                    break;
                case ("Результат теста"):
                    e.SortExpression = "[TestResult]";
                    break;
                case ("Номер в Jira"):
                    e.SortExpression = "[TestJiraNumber]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[StatusName]";
                    break;
                case ("Проект"):
                    e.SortExpression = "[ProjectName]";
                    break;
                case ("Помечен на удаление"):
                    e.SortExpression = "[TestLogicalDelete]";
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
        //Добавление данных
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                bool LogicalDelete;
                if (cbDelete.Checked)
                {
                    LogicalDelete = true;
                }
                else
                {
                    LogicalDelete = false;
                }
                procedures.TestInsert(tbTestName.Text, tbDescription.Text, DateTime.Now.ToString("dd/MM/yyyy"), tbResult.Text, tbNumber.Text,
                    LogicalDelete, Convert.ToInt32(ddlStatus.SelectedValue.ToString()), Convert.ToInt32(ddlProject.SelectedValue.ToString()));
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlProjectFill();
                ddlStatusFill();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обвление данных
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                bool LogicalDelete;
                if (cbDelete.Checked)
                {
                    LogicalDelete = true;
                }
                else
                {
                    LogicalDelete = false;
                }
                procedures.TestUpdate(DBConnection.IdRecord, tbTestName.Text, tbDescription.Text, tbResult.Text, tbNumber.Text,
                    LogicalDelete, Convert.ToInt32(ddlStatus.SelectedValue.ToString()), Convert.ToInt32(ddlProject.SelectedValue.ToString()), IsModified);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlProjectFill();
                ddlStatusFill();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись :(')", true);
            }
        }
        //Отменить выбор
        protected void btCanselSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.IdRecord = 0;
            SelectedMessage.Visible = false;
            gvFill(QR);
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvTests.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[2].Text.Equals(tbSearch.Text) ||
                    row.Cells[3].Text.Equals(tbSearch.Text) ||
                    row.Cells[4].Text.Equals(tbSearch.Text) ||
                    row.Cells[5].Text.Equals(tbSearch.Text) ||
                    row.Cells[6].Text.Equals(tbSearch.Text) ||
                    row.Cells[8].Text.Equals(tbSearch.Text) ||
                    row.Cells[10].Text.Equals(tbSearch.Text) ||
                        row.Cells[11].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [TestID] like '%" + tbSearch.Text + "%' or [TestName] like '%" + tbSearch.Text + "%' " +
                    "or [TestDescription] like '%" + tbSearch.Text + "%' or [TestDate] like '%" + tbSearch.Text + "%' or" +
                    "[TestResult] like '%" + tbSearch.Text + "%' or [TestJiraNumber] like '%" + tbSearch.Text + "%' or [StatusName] like '%" + tbSearch.Text + "%' " +
                    "or [ProjectName] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Отмена поиска
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
            ddlProjectFill();
            ddlStatusFill();
        }
        //Проверка изменений в полях ввода
        protected void tbTestName_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbDescription_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbNumber_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbResult_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }
    }
}