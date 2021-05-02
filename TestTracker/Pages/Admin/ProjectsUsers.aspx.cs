using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace TestTracker.Pages.Admin
{
    public partial class ProjectsUsers : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProjectsUsers;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlUserFill();
                ddlProjectFill();
            }
        }
        //Заполнение таблицы данными
        private void gvFill(string qr)
        {
            sdsProjectsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProjectsUsers.SelectCommand = qr;
            sdsProjectsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            gvProjectsUsers.DataSource = sdsProjectsUsers;
            gvProjectsUsers.DataBind();
        }
        //Заполнение списка пользователей
        private void ddlUserFill()
        {
            sdsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = DBConnection.qrUsersConcat;
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlUser.DataSource = sdsUsers;
            ddlUser.DataTextField = "Пользователь";
            ddlUser.DataValueField = "ID";
            ddlUser.DataBind();
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
            ddlUser.SelectedIndex = 0;
            ddlProject.SelectedIndex = 0;
        }
        protected void gvProjectsUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvProjectsUsers, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvProjectsUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int Index = Convert.ToInt32(e.RowIndex);
                DataProcedures procedure = new DataProcedures();
                GridViewRow rows = gvProjectsUsers.SelectedRow;
                DBConnection.IdRecord = Convert.ToInt32(gvProjectsUsers.Rows[Index].Cells[1].Text.ToString());
                procedure.ProjectUserDelete(DBConnection.IdRecord);
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlProjectFill();
                ddlUserFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось удалить запись :(')", true);
            }
        }
        //Выбор данных в таблице
        protected void gvProjectsUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvProjectsUsers.Rows)
            {
                if (row.RowIndex == gvProjectsUsers.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvProjectsUsers.SelectedRow.RowIndex;
            GridViewRow rows = gvProjectsUsers.SelectedRow;
            DBConnection.IdRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            ddlProject.SelectedValue = rows.Cells[2].Text.ToString();
            ddlUser.SelectedValue = rows.Cells[4].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.IdRecord;
            btUpdate.Visible = true;
        }
        //Сортировка данных в таблице
        protected void gvProjectsUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ProjectUserId]";
                    break;
                case ("Проект"):
                    e.SortExpression = "[ProjectName]";
                    break;
                case ("Пользователь"):
                    e.SortExpression = "Пользователь";
                    break;
            }
            sortGridView(gvProjectsUsers, e, out sortDirection, out strField);
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
            DBConnection connection = new DBConnection();
            try
            {
                procedures.ProjectUserInsert(Convert.ToInt32(ddlProject.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()));
                Response.Redirect(Request.Url.AbsoluteUri);
                gvFill(QR);
                Cleaner();
                ddlProjectFill();
                ddlUserFill();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось добавить запись :(')", true);
            }
        }
        //Изменение данных
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBConnection.IdRecord != 0)
                {
                    DataProcedures procedures = new DataProcedures();
                    procedures.ProjectUserUpdate(DBConnection.IdRecord, Convert.ToInt32(ddlProject.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()));
                    Response.Redirect(Request.Url.AbsoluteUri);
                    gvFill(QR);
                    Cleaner();
                    ddlProjectFill();
                    ddlUserFill();
                }
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Не удалось обновить запись запись :(')", true);
            }
        }
        //Отменить выбор
        protected void btCancelSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.IdRecord = 0;
            SelectedMessage.Visible = false;
            gvFill(QR);
        }
        //Очистка поиска
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = string.Empty;
            btCancel.Visible = false;
            gvFill(QR);
            ddlProjectFill();
            ddlUserFill();
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvProjectsUsers.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
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
                string newQR = QR + "where [ProjectUserId] like '%" + tbSearch.Text + "%' or [ProjectName] like '%" + tbSearch.Text + "%' or (CONCAT_WS(' ',[UserSurname],[UserName],[UserMiddleName])) like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
    }
}