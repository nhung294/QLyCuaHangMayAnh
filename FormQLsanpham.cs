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

namespace qlsp2
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=DESKTOP-9TAMLUV;Initial Catalog=QuanLyBHMayAnh1;Integrated Security=True";


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaHH AS [Mã hàng], TenHH AS [Tên hàng], SoLuong AS [Số lượng], DonGiaNhap AS [Đơn giá nhập], DonGiaBan AS [Đơn giá bán] FROM DANHMUCHANGHOA";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"INSERT INTO DANHMUCHANGHOA (MaHH, TenHH, SoLuong, DonGiaNhap, DonGiaBan) 
                                         VALUES (@MaHH, @TenHH, @SoLuong, @DonGiaNhap, @DonGiaBan)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaHH", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@TenHH", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(textBox3.Text.Trim()));
                        command.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(textBox4.Text.Trim()));
                        command.Parameters.AddWithValue("@DonGiaBan", Convert.ToDecimal(textBox5.Text.Trim()));

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadProducts(); // Làm mới danh sách
                        ClearInput();  // Xóa dữ liệu trong TextBox
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (ValidateInput())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"UPDATE DANHMUCHANGHOA 
                                         SET TenHH = @TenHH, SoLuong = @SoLuong, DonGiaNhap = @DonGiaNhap, DonGiaBan = @DonGiaBan 
                                         WHERE MaHH = @MaHH";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaHH", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@TenHH", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(textBox3.Text.Trim()));
                        command.Parameters.AddWithValue("@DonGiaNhap", Convert.ToDecimal(textBox4.Text.Trim()));
                        command.Parameters.AddWithValue("@DonGiaBan", Convert.ToDecimal(textBox5.Text.Trim()));

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadProducts(); // Làm mới danh sách
                        ClearInput();  // Xóa dữ liệu trong TextBox
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM DANHMUCHANGHOA WHERE MaHH = @MaHH";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaHH", textBox1.Text.Trim());

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadProducts(); // Làm mới danh sách
                    ClearInput();  // Xóa dữ liệu trong TextBox
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Mã hàng"].Value.ToString();
                textBox2.Text = row.Cells["Tên hàng"].Value.ToString();
                textBox3.Text = row.Cells["Số lượng"].Value.ToString();
                textBox4.Text = row.Cells["Đơn giá nhập"].Value.ToString();
                textBox5.Text = row.Cells["Đơn giá bán"].Value.ToString();
            }
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(textBox3.Text, out _) || !decimal.TryParse(textBox4.Text, out _) || !decimal.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Số lượng và giá phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void ClearInput()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
