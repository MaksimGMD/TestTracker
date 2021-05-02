using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestTracker.Pages.Admin
{
    public partial class Projects : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProjectsAdmin;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        //Заполнение таблицы данными
        private void gvFill(string qr)
        {
            sdsProjects.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProjects.SelectCommand = qr;
            sdsProjects.DataSourceMode = SqlDataSourceMode.DataReader;
            gvProjects.DataSource = sdsProjects;
            gvProjects.DataBind();
        }
        //Очитска полей
        protected void Cleaner()
        {
            SelectedMessage.Visible = false;
            DBConnection.IdRecord = 0;
            tbName.Text = "";
            tbVersion.Text = "";
        }
        //Добавление данных
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedures procedures = new DataProcedures();
            try
            {
                procedures.ProjectInsert(tbName.Text, Convert.ToInt32(tbVersion.Text));
                gvFill(QR);
                Response.Redirect(Request.Url.AbsoluteUri);
                Cleaner();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Обвноление данных
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBConnection.IdRecord != 0)
                {
                    DataProcedures procedures = new DataProcedures();
                    procedures.ProjectUpdate(DBConnection.IdRecord, tbName.Text, Convert.ToInt32(tbVersion.Text));
                    Response.Redirect(Request.Url.AbsoluteUri);
                    gvFill(QR);
                    Cleaner();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись запись :(')", true);
            }
        }

        protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvProjects, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Сортировка данных
        protected void gvProjects_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ProjectId]";
                    break;
                case ("Название проекта"):
                    e.SortExpression = "[ProjectName]";
                    break;
                case ("Версия проекта"):
                    e.SortExpression = "[ProjectVersion]";
                    break;
            }
            sortGridView(gvProjects, e, out sortDirection, out strField);
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
        //Выбор данных
        protected void gvProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvProjects.Rows)
            {
                if (row.RowIndex == gvProjects.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvProjects.SelectedRow.RowIndex;
            GridViewRow rows = gvProjects.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbName.Text = rows.Cells[2].Text.ToString();
            tbVersion.Text = rows.Cells[3].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;
        }
        //Удаление данных
        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvProjects.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvProjects.Rows[Index].Cells[1].Text.ToString());
                procedure.ProjectDelete(DBConnection.IdRecord);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Очистка поиска
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
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
                foreach (GridViewRow row in gvProjects.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                    row.Cells[2].Text.Equals(tbSearch.Text) ||
                    row.Cells[3].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [ProjectId] like '%" + tbSearch.Text + "%' or [ProjectName] like '%" + tbSearch.Text + "%' or [ProjectVersion] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
    }
}