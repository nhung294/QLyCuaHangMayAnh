using BaiTapLon.KetNoiCSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.Forms
{
    public partial class FormKhachHang : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        public FormKhachHang()
        {
            InitializeComponent();
        }
        private void loadDuLieu()
        {
            string sqlGetNhaCungCap = "SELECT * FROM KHACHHANG";
            DataTable dataTableDSNhaCungCap = dataProcessor.DataReader(sqlGetNhaCungCap);

            // Xóa các cột cũ nếu có
            dataGridViewDSKhachHang.Columns.Clear();
            dataGridViewDSKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tạo và thêm cột
            dataGridViewDSKhachHang.Columns.Add("MaKH", "Mã khách hàng");
            dataGridViewDSKhachHang.Columns.Add("TenKH", "Tên khách hàng");
            dataGridViewDSKhachHang.Columns.Add("DiaChi", "Địa chỉ");
            dataGridViewDSKhachHang.Columns.Add("DienThoai", "Điện thoại");

            // Thêm dữ liệu vào DataGridView
            foreach (DataRow row in dataTableDSNhaCungCap.Rows)
            {
                dataGridViewDSKhachHang.Rows.Add(
                    row["MaKH"],
                    row["TenKH"],
                    row["DiaChi"],
                    row["DienThoai"]
                );
            }
        }
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
           this.loadDuLieu();
        }
        private bool IsSDT()
        {
            if (textBoxDienThoai.Text.Length != 10)
            {
                return false;
            }
            else
            {
                string sdt = textBoxDienThoai.Text;
                if (sdt[0] != '0')
                {
                    return false;
                }

            }
            return true;
        }
        private void buttonSuaKH_Click(object sender, EventArgs e)
        {
            if (textBoxMaKH.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải chọn khách hàng");
                return;
            }
            if (textBoxTenKH.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng");
                return;
            }
            if(textBoxDiaChi.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ");
                return;
            }
            if (textBoxDienThoai.Text.Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập SDT");
                return ;
            }
            if (IsSDT() == false)
            {
                MessageBox.Show("Số điện thoại sai định dạng");
                return;
            }
            string sqlKH = "SELECT * FROM KHACHHANG WHERE DienThoai ='" + textBoxDienThoai.Text.Trim() + "'";
            if (dataProcessor.DataReader(sqlKH).Rows.Count > 1)
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng bởi khách hàng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDienThoai.Focus();
                return;
            }

            string sqlUpdate = "UPDATE KHACHHANG SET TenKH = N'";
            sqlUpdate = sqlUpdate + textBoxTenKH.Text.Trim() + "', DiaChi = N'";
            sqlUpdate = sqlUpdate + textBoxDiaChi.Text.Trim() + "', DienThoai = '";
            sqlUpdate = sqlUpdate + textBoxDienThoai.Text.Trim() + "' where MaKH = '";
            sqlUpdate = sqlUpdate + textBoxMaKH.Text.Trim() + "'";

            if (MessageBox.Show("Bạn có muốn sửa thông tin khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataProcessor.DataChange(sqlUpdate);
                this.loadDuLieu();
                MessageBox.Show("Bạn đã sửa thành công");
            }

        }

        private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void dataGridViewDSKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDSKhachHang.Rows[e.RowIndex];

                // Gán giá trị từ các ô vào các TextBox
                textBoxMaKH.Text = Convert.ToString(row.Cells["MaKH"].Value);
                textBoxTenKH.Text = Convert.ToString(row.Cells["TenKH"].Value);
                textBoxDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
                textBoxDienThoai.Text = Convert.ToString(row.Cells["DienThoai"].Value);
                buttonSuaKH.Enabled = true;
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                //FormTrangChu formTrangChu = new FormTrangChu();
                //formTrangChu.Show();
            }
        }
    }
}
