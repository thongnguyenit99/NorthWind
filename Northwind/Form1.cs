using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       static int row;
        private void UpdateLableNumber()
        {
            lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
        }

        private void loaddata_gridview()
        {
          
            string sql = "select * from Products";
            DataTable data = new DataTable();
            data = ProcessData.GetData(sql);
            dataGridView1.DataSource = data;
            // CSS cho đẹp
            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].Width = (dataGridView1.Width - 60) / 5;
            }
            string[] Colum_name = new string[10] { "ProductID", "ProductName", "SupplierID", "CategoryID", 
                "QuantityPerUnit", "UnitPrice", "UnitsInStock", "UnitsOnOrder", "ReorderLevel", "Discontinued" };
            int j = 0;
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderText = Colum_name[j++];
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Times New Roman", 16, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Times New Roman", 16, GraphicsUnit.Pixel);
            }
            UpdateLableNumber();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loaddata_gridview();
            string sql1 = "Select * from Categories";

            // bang Categories lay ra cot  tên Categories
            DataTable table_Categories = ProcessData.GetData(sql1);
            cbCategory.DataSource = table_Categories;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryID";
            // Load theo ComboBox Category
            int n;
            int.TryParse(cbCategory.SelectedValue.ToString(), out n);
            if (n > 0)
            {
                dataGridView1.DataSource = ProcessData.GetData(string.Format("select * from Products where CategoryID = {0}", int.Parse(cbCategory.SelectedValue.ToString())));
                lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = false;
                dataGridView1.Rows[0].Selected = true;
                txtName.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
                txtPrice.Text = dataGridView1.CurrentRow.Cells["UnitPrice"].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells["UnitsInStock"].Value.ToString();
                txtUnits.Text = dataGridView1.CurrentRow.Cells["QuantityPerUnit"].Value.ToString();
            }
        }
        // hàm này truyền dữ liêu datagridview lên các ô
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtName.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
            txtStock.Text = dataGridView1.CurrentRow.Cells["UnitsInStock"].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells["UnitPrice"].Value.ToString();
            txtUnits.Text = dataGridView1.CurrentRow.Cells["QuantityPerUnit"].Value.ToString();
          
 
        }

        // tao su kien cho combobox Category
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n;
            int.TryParse(cbCategory.SelectedValue.ToString(), out n);
            if (n > 0)
            {
                dataGridView1.DataSource = ProcessData.GetData(string.Format("select * from Products where CategoryID = {0}", int.Parse(cbCategory.SelectedValue.ToString())));
                lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
                txtPrice.Text = dataGridView1.CurrentRow.Cells["UnitPrice"].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells["UnitsInStock"].Value.ToString();
                txtUnits.Text = dataGridView1.CurrentRow.Cells["QuantityPerUnit"].Value.ToString();
            }
        }
        private void btnN_Click(object sender, EventArgs e)
        {
            row = dataGridView1.CurrentCell.RowIndex;
            if (row < dataGridView1.RowCount && row + 1 <dataGridView1.RowCount)
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = false;
                dataGridView1.CurrentCell = dataGridView1.Rows[row + 1].Cells[0];
                dataGridView1.Rows[row + 1].Selected = true;
                lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
            }
        }
        private void btnP_Click(object sender, EventArgs e)
        {
            row = dataGridView1.CurrentCell.RowIndex;
            if (row < dataGridView1.RowCount && row - 1 >= 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = false;
                dataGridView1.CurrentCell = dataGridView1.Rows[row - 1].Cells[0];
                dataGridView1.Rows[row - 1].Selected = true;
                lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
            }
        }
        private void btnF_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            dataGridView1.Rows[0].Selected = true;
            lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
            MessageBox.Show("VỚI LƯỢT CLICK VỪA RỒI ,BẠN ĐÃ ĐẾN ĐẦU TRANG!!!!! THÔNG BÁO CHO BẢNG CATEGORY");
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            row = dataGridView1.CurrentCell.RowIndex;
            if (row < dataGridView1.RowCount )
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = false;
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount-1].Cells[0];
                dataGridView1.Rows[dataGridView1.RowCount - 2].Selected = true;
                lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
                MessageBox.Show("VỚI LƯỢT CLICK VỪA RỒI ,BẠN ĐÃ ĐẾN CUỐI TRANG!!!!! THÔNG BÁO CHO BẢNG CATEGORY");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbnumber.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString() + " / " + dataGridView1.Rows.Count.ToString();
        }
        

       

      
    }
}
