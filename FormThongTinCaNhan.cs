using BaiTapLon.HamPhuTro;
using BaiTapLon.KetNoiCSDL;
using System;
using System.Collections;
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
	public partial class FormThongTinCaNhan : Form
	{
		ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
		public FormThongTinCaNhan()
		{
			InitializeComponent();
		}
		//Hàm load
		private void load()
		{
			textBoxMaNhanVien.Enabled= false;
			textBoxHoTen.Enabled = false;
			textBoxGioiTinh.Enabled = false;
			dateTimePickerNgaySinh.Enabled = false;

			string sqlSelect = "select MaNV, TenNV, GioiTinh, NgaySinh, TenCV, DiaChi, DienThoai, MatKhau" +
				" from NHANVIEN join CONGVIEC on NHANVIEN.MaCV = CONGVIEC.MaCV" +
				" where MaNV ='" + PhanQuyen.instance.MaNV+"'";
			DataTable dataTable = new DataTable();
			dataTable = dataProcessor.DataReader(sqlSelect);
			
			textBoxMaNhanVien.Text = dataTable.Rows[0][0].ToString();
            textBoxHoTen.Text = dataTable.Rows[0][1].ToString();
            textBoxGioiTinh.Text = dataTable.Rows[0][2].ToString();
            dateTimePickerNgaySinh.Text = dataTable.Rows[0][3].ToString();
            textBoxCongViec.Text = dataTable.Rows[0][4].ToString();
            textBoxDiaChi.Text = dataTable.Rows[0][5].ToString();
            textBoxDienThoai.Text = dataTable.Rows[0][6].ToString();
            textBoxMatKhau.Text = dataTable.Rows[0][7].ToString();
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
        //Load form
        private void FormChiTietNhanVien_Load(object sender, EventArgs e)
		{
            textBoxMatKhau.PasswordChar = '*';
            dateTimePickerNgaySinh.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.load();
			
		}
		//Mở hộp thoại Openfiledialog	
		//Sửa thông tin cá nhân
		private void buttonSua_Click(object sender, EventArgs e)
		{
            if (textBoxDiaChi.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                return;
            }
            if (textBoxDienThoai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                return;
            }
            else
            {
                if (IsSDT() == false)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    return;
                }
            }

            string sqlKH = "select * from NhanVien where DienThoai ='" + textBoxDienThoai.Text + "'";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader(sqlKH);
            if (dataTable.Rows.Count > 1)
            {
                MessageBox.Show("Số điện thoại đã tồn tại");
                return;
            }

            string sqlUpdate = "Update NhanVien set DiaChi= N'";
            sqlUpdate += textBoxDiaChi.Text.ToString() + "', DienThoai ='";
            sqlUpdate += textBoxDienThoai.Text.ToString() + "' ";
            sqlUpdate += " where MaNV ='" + textBoxMaNhanVien.Text + "'";
            if (textBoxMaNhanVien != null)
            {
                if (MessageBox.Show("Bạn có muốn cập nhật lại thông tin cá nhân không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataProcessor.DataChange(sqlUpdate);
                    MessageBox.Show("Thành công");
                }
            }
        }

		//Sự kiện chỉ được nhập số
		private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
		}

        private void buttonThoat_Click(object sender, EventArgs e)
        {
			this.Close();
        }

        private void buttonDoiMatKhau_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau formDoiMatKhau=new FormDoiMatKhau();
            formDoiMatKhau.Show();
        }

        private void textBoxDienThoai_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
    }
}
