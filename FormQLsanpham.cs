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
    public partial class FormSanPham : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        public FormSanPham()
        {
            InitializeComponent();
        }
        private void load()
        {

            dataGridViewDSSanPham.Columns.Clear();
            dataGridViewDSSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Tạo và thêm cột
            dataGridViewDSSanPham.Columns.Add("MaHH", "Mã sản phẩm");
            dataGridViewDSSanPham.Columns.Add("TenHH", "Tên sản phẩm");
            dataGridViewDSSanPham.Columns.Add("SoLuong", "Số lượng");
            dataGridViewDSSanPham.Columns.Add("DonGiaNhap", "Đơn giá nhập");
            dataGridViewDSSanPham.Columns.Add("DonGiaBan", "Đơn giá bán");
            dataGridViewDSSanPham.Columns.Add("ThoiGianBH", "Thời gian bảo hành");

            DataTable dataTable = new DataTable();
            string sqlSelect = "select MaHH, TenHH, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBH\r\n from DANHMUCHANGHOA";
            dataTable = dataProcessor.DataReader(sqlSelect);
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridViewDSSanPham.Rows.Add(
                    row["MaHH"],
                    row["TenHH"],
                    row["SoLuong"],
                    row["DonGiaNhap"],
                    row["DonGiaBan"],
                    row["ThoiGianBH"]
                );
            }
        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {
            this.load();
        }

        private void dataGridViewDSSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDSSanPham.Rows[e.RowIndex];
                textBoxMaSP.Text = Convert.ToString(row.Cells["MaHH"].Value);         
                textBoxTenSP.Text = Convert.ToString(row.Cells["TenHH"].Value);       
                textBoxSoLuong.Text = Convert.ToString(row.Cells["SoLuong"].Value);   
                textBoxDonGiaNhap.Text = Convert.ToString(row.Cells["DonGiaNhap"].Value); 
                textBoxDonGiaBan.Text = Convert.ToString(row.Cells["DonGiaBan"].Value);   
                textBoxThoiGianBH.Text = Convert.ToString(row.Cells["ThoiGianBH"].Value);
            }
        }

        private void textBoxDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxThoiGianBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void but_Click(object sender, EventArgs e)
        {
            textBoxMaSP.Text = string.Empty;
            textBoxTenSP.Text = string.Empty;
            textBoxSoLuong.Text = string.Empty;
            textBoxDonGiaNhap.Text = string.Empty;
            textBoxDonGiaBan.Text = string.Empty;
            textBoxThoiGianBH.Text = string.Empty;
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            if (textBoxMaSP.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải chọn một mặt hàng");
                return;
            }
            if(textBoxDonGiaBan.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán");
                return;
            }
            if(textBoxThoiGianBH.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập thời gian bảo hành");
                return;
            }
            string sqlUpdate = "Update DanhMucHangHoa set DonGiaBan=";
            sqlUpdate += Convert.ToInt64(textBoxDonGiaBan.Text) + ", ThoiGianBH = ";
            sqlUpdate += Convert.ToInt64(textBoxThoiGianBH.Text) + " where MaHH ='";
            sqlUpdate += textBoxMaSP.Text + "'";
            if(MessageBox.Show("Bạn có muốn sửa không","Thông báo",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataProcessor.DataChange(sqlUpdate);
                MessageBox.Show("Thành công");
                this.load();
                textBoxMaSP.Text = string.Empty;
                textBoxTenSP.Text = string.Empty;
                textBoxSoLuong.Text = string.Empty;
                textBoxDonGiaNhap.Text = string.Empty;
                textBoxDonGiaBan.Text = string.Empty;
                textBoxThoiGianBH.Text = string.Empty;
            }
        }
    }
}
