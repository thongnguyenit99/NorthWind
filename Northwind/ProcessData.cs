using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind
{
    public class ProcessData
    {
        public static DataTable GetData(string sql)
        {
            string path = "Data Source=thongnguyen;Initial Catalog=northwind1;Integrated Security=True";
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection sqlConnection = new SqlConnection(path);
                SqlDataAdapter sqladt = new SqlDataAdapter(sql, path);
                sqladt.Fill(dataTable);
            }
            catch
            {
                MessageBox.Show("Đường dẫn đến cơ sở dữ liệu không chính xác!!!", "Lỗi truy vấn đến bảng thuộc tính trong Database hiện tại",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
    }
}
