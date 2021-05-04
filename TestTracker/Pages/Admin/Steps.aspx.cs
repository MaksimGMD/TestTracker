using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.Admin
{
    public partial class Steps : System.Web.UI.Page
    {
        private string QR = "";
        private bool IsModified;
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrSteps;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlTestFill();
            }
        }
        //Заполнение таблицы данными
        private void gvFill(string qr)
        {
            sdsSteps.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsSteps.SelectCommand = qr;
            sdsSteps.DataSourceMode = SqlDataSourceMode.DataReader;
            gvSteps.DataSource = sdsSteps;
            gvSteps.DataBind();
        }
        //Заполнение списка тестов
        private void ddlTestFill()
        {
            sdsTest.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTest.SelectCommand = DBConnection.qrTests;
            sdsTest.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlTest.DataSource = sdsTest;
            ddlTest.DataTextField = "Название теста";
            ddlTest.DataValueField = "ID";
            ddlTest.DataBind();
        }
        //Очитска полей
        protected void Cleaner()
        {
            SelectedMessage.Visible = false;
            DBConnection.IdRecord = 0;
            ddlTest.SelectedIndex = 0;
            tbName.Text = string.Empty;
            tbNumber.Text = string.Empty;
            IsModified = false;
        }

        protected void gvSteps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSteps, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Удаление данных
        protected void gvSteps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvSteps.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvSteps.Rows[Index].Cells[1].Text.ToString());
                int IdTest = Convert.ToInt32(gvSteps.Rows[Index].Cells[4].Text.ToString());
                procedure.StepDelete(DBConnection.IdRecord, IdTest);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlTestFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Выбор записи
        protected void gvSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvSteps.Rows)
            {
                if (row.RowIndex == gvSteps.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvSteps.SelectedRow.RowIndex;
            GridViewRow rows = gvSteps.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbNumber.Text = rows.Cells[2].Text.ToString();
            tbName.Text = rows.Cells[3].Text.ToString();
            ddlTest.SelectedValue = rows.Cells[4].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;
        }
        //Сортировка данных

        protected void gvSteps_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[StepId]";
                    break;
                case ("Номер этапа"):
                    e.SortExpression = "[StepNumber]";
                    break;
                case ("Название этапа"):
                    e.SortExpression = "[StepName]";
                    break;
                case ("Тест"):
                    e.SortExpression = "[TestName]";
                    break;
            }
            sortGridView(gvSteps, e, out sortDirection, out strField);
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

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                procedures.StepInsert(tbNumber.Text, tbName.Text, Convert.ToInt32(ddlTest.SelectedValue.ToString()));
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlTestFill();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обновление данных
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                procedures.StepUpdate(DBConnection.IdRecord, tbNumber.Text, tbName.Text, Convert.ToInt32(ddlTest.SelectedValue.ToString()), IsModified);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlTestFill();

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись :(')", true);
            }
        }
        //Очистка поиска
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
            ddlTestFill();
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
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[2].Text.Equals(tbSearch.Text) ||
                    row.Cells[3].Text.Equals(tbSearch.Text) ||
                    row.Cells[5].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [StepId] like '%" + tbSearch.Text + "%' or [StepNumber] like '%" + tbSearch.Text + "%' or [StepName] like '%" + tbSearch.Text + "%' or [TestName] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Проверка изменений в полях ввода
        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbName_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }

        protected void tbNumber_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
        }
    }
}