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

namespace thôngkdt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-9TAMLUV;Initial Catalog=QuanLyBHMayAnh1;Integrated Security=True";

       
        

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            label6.Text = "Tổng doanh thu: 0";

        }
        private DataTable GetSalesData(DateTime fromDate, DateTime toDate)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT h.MaHDB, h.NgayBan, 
                       ISNULL(ct.SoLuong, 0) AS SoLuong, 
                       ISNULL(ct.ThanhTien, 0) AS ThanhTien
                FROM HOADONBAN h
                LEFT JOIN CHITIETHDB ct ON h.MaHDB = ct.MaHDB
                WHERE (@FromDate IS NULL OR h.NgayBan >= @FromDate)
                  AND (@ToDate IS NULL OR h.NgayBan <= @ToDate)";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate == DateTime.MinValue ? (object)DBNull.Value : fromDate);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate == DateTime.MinValue ? (object)DBNull.Value : toDate);

                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lấy dữ liệu: " + ex.Message);
            }

            return dataTable;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePicker1.Value;
            DateTime toDate = dateTimePicker2.Value;

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataTable data = GetSalesData(fromDate, toDate);

            if (data.Rows.Count > 0)
            {
                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = data;

                // Tính tổng doanh thu
                decimal totalRevenue = 0;
                foreach (DataRow row in data.Rows)
                {
                    totalRevenue += Convert.ToDecimal(row["ThanhTien"]);
                }

                label6.Text = "Tổng doanh thu: " + totalRevenue.ToString("C");
            }
            else
            {
                MessageBox.Show("Không có dữ liệu cho khoảng thời gian này.");
                dataGridView1.DataSource = null; // Xóa dữ liệu cũ nếu không có kết quả
            }
        
    }
    }
}
