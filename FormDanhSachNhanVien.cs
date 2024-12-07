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

namespace BaiTapLon
{
    public partial class FormDanhSachNhanVien : Form
    {
		ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
		Hashtable congViec = new Hashtable();
		public FormDanhSachNhanVien()
        {
            InitializeComponent();
        }

        //ADD dữ liệu vào combobox
        private void addComboBox()
        {
            DataTable dataTable = new DataTable();
            //dữ liệu chức vụ

            dataTable = dataProcessor.DataReader("Select MaCV,TenCV From CONGVIEC");
            foreach (DataRow row in dataTable.Rows)
            {

                int i = 0;
                String tam = "";
                foreach (var item in row.ItemArray)
                {
                    if (i == 0)
                    {
                        tam = item.ToString();
                        i++;
                    }
                    else
                    {
                        comboBoxCongViec.Items.Add(item.ToString());
                        congViec.Add(tam, item.ToString());
                    }

                }
            }
        }
		//Load dữ liệu 
		private void loadDuLieu()
		{
			textBoxMaNhanVien.Enabled = false ;
			textBoxHoTen.Enabled = false;
			textBoxGioiTinh.Enabled=false;
			textBoxDiaChi.Enabled = false;
			dateTimePickerNgaySinh.Enabled=false;
			textBoxDienThoai.Enabled=false;
			textBoxMatKhau.Enabled=false;
			textBoxMatKhau.PasswordChar = '*';
	
            dataGridViewDanhSachNhanVien.Columns.Clear();
            dataGridViewDanhSachNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewDanhSachNhanVien.Columns.Add("MaNV", "Mã nhân viên");
            dataGridViewDanhSachNhanVien.Columns.Add("TenNV", "Tên nhân viên");
            dataGridViewDanhSachNhanVien.Columns.Add("GioiTinh", "Giới tính");
            dataGridViewDanhSachNhanVien.Columns.Add("NgaySinh", "Ngày sinh");
            dataGridViewDanhSachNhanVien.Columns.Add("CongViec", "Công Việc");
            dataGridViewDanhSachNhanVien.Columns.Add("DiaChi", "Địa chỉ");
            dataGridViewDanhSachNhanVien.Columns.Add("DienThoai", "Điện thoại");
            dataGridViewDanhSachNhanVien.Columns.Add("MatKhau", "Mật khẩu");

			DataTable dataTable = new DataTable();
			string sqlSelect = "select MaNV, TenNV, GioiTinh, NgaySinh, TenCV, DiaChi, DienThoai, MatKhau" +
				" from NHANVIEN join CONGVIEC on NHANVIEN.MaCV = CONGVIEC.MaCV";

			dataTable = dataProcessor.DataReader(sqlSelect);
			foreach (DataRow row in dataTable.Rows) {
                dataGridViewDanhSachNhanVien.Rows.Add(
					row["MaNV"],
					row["TenNV"],
					row["GioiTinh"],
					row["NgaySinh"],
					row["TenCV"],
					row["DiaChi"],
					row["DienThoai"],
					row["MatKhau"]
				);
            }

        }
		//Load form
		private void FormDanhSachNhanVien_Load(object sender, EventArgs e)
		{
            dateTimePickerNgaySinh.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.addComboBox();
			this.loadDuLieu();
		}
		
		//Tìm kiếm nhân viên
		private void buttonTimKiem_Click(object sender, EventArgs e)
		{
            dataGridViewDanhSachNhanVien.Columns.Clear();
            dataGridViewDanhSachNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewDanhSachNhanVien.Columns.Add("MaNV", "Mã nhân viên");
            dataGridViewDanhSachNhanVien.Columns.Add("TenNV", "Tên nhân viên");
            dataGridViewDanhSachNhanVien.Columns.Add("GioiTinh", "Giới tính");
            dataGridViewDanhSachNhanVien.Columns.Add("NgaySinh", "Ngày sinh");
            dataGridViewDanhSachNhanVien.Columns.Add("CongViec", "Công Việc");
            dataGridViewDanhSachNhanVien.Columns.Add("DiaChi", "Địa chỉ");
            dataGridViewDanhSachNhanVien.Columns.Add("DienThoai", "Điện thoại");
            dataGridViewDanhSachNhanVien.Columns.Add("MatKhau", "Mật khẩu");

            DataTable dataTable = new DataTable();
            string sqlSelect = "select MaNV, TenNV, GioiTinh, NgaySinh, TenCV, DiaChi, DienThoai, MatKhau" +
                " from NHANVIEN join CONGVIEC on NHANVIEN.MaCV = CONGVIEC.MaCV" +
                " Where TenNV LIKE '%"+textBoxTimKiem.Text.Trim()+"%' or TenCV LIKE '%" + textBoxTimKiem.Text.Trim()+"%'";

            dataTable = dataProcessor.DataReader(sqlSelect);
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridViewDanhSachNhanVien.Rows.Add(
                    row["MaNV"],
                    row["TenNV"],
                    row["GioiTinh"],
                    row["NgaySinh"],
                    row["TenCV"],
                    row["DiaChi"],
                    row["DienThoai"],
                    row["MatKhau"]
                );
            }

        }
		//Thêm một nhân viên
		private void buttonThem_Click(object sender, EventArgs e)
		{
			FormThemNhanVien formThemNhanVien = new FormThemNhanVien();
			formThemNhanVien.Show();
		}
		//Nhấn vào dữ liệu của dataGridView
		private void dataGridViewDanhSachNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
		{
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDanhSachNhanVien.Rows[e.RowIndex];

                // Gán giá trị từ các ô vào các TextBox
                textBoxMaNhanVien.Text = Convert.ToString(row.Cells["MaNV"].Value);
                textBoxHoTen.Text = Convert.ToString(row.Cells["TenNV"].Value);
                textBoxGioiTinh.Text = Convert.ToString(row.Cells["GioiTinh"].Value);
                dateTimePickerNgaySinh.Text = Convert.ToString(row.Cells["NgaySinh"].Value);
                comboBoxCongViec.Text = Convert.ToString(row.Cells["CongViec"].Value);
                textBoxDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
                textBoxDienThoai.Text = Convert.ToString(row.Cells["DienThoai"].Value);
                textBoxMatKhau.Text = Convert.ToString(row.Cells["MatKhau"].Value);
                if(textBoxMaNhanVien.Text.Length > 0)
				{
                    tabControlBody.SelectedTab = tabPage2;
                }

            }
        }
		//Reset form
	

		//Hàm clear data
		private void clear_data()
		{
			textBoxDiaChi.Text = "";
			textBoxMaNhanVien.Text = "";
			textBoxHoTen.Text = "";
			textBoxGioiTinh.Text = "";
			dateTimePickerNgaySinh.Text = "";
			comboBoxCongViec.SelectedIndex = -1;
			textBoxDienThoai.Text = "";
			textBoxMatKhau.Text = "";
		}

        private void buttonDoiMK_Click(object sender, EventArgs e)
        {
			string sqlResetMK = "Update NhanVien set MatKhau ='" + dateTimePickerNgaySinh.Text + "' where MaNV = '" + textBoxMaNhanVien.Text + "'";
			if (textBoxMaNhanVien.Text.Length > 0)
			{
				if (MessageBox.Show("Bạn có muốn reset mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					dataProcessor.DataChange(sqlResetMK);
                    MessageBox.Show("Thành công");
                }
			}
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.loadDuLieu();
        }

        private void dataGridViewDanhSachNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewDanhSachNhanVien.Columns[e.ColumnIndex].Name == "MatKhau" && e.Value != null)
            {
                // Chuyển giá trị thành chuỗi ký tự '*'
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }

        private void buttonCapNhat_Click_1(object sender, EventArgs e)
        {
            if(PhanQuyen.instance.MaNV == textBoxMaNhanVien.Text.ToString() || textBoxMaNhanVien.Text.ToString() == "Quản lý")
            {
                MessageBox.Show("Bạn không thể cập nhật chức vụ này!");
            }
            else
            {
                if(comboBoxCongViec.Text=="Quản lý")
                {
                    MessageBox.Show("Bạn không thể cập nhật chức vụ quản lý");
                }
                else
                {
                    string sqlUpdate = "Update NhanVien set MaCV='";
                    foreach (DictionaryEntry item in congViec)
                    {
                        if (item.Value.ToString() == comboBoxCongViec.Text.Trim().ToString())
                        {
                            sqlUpdate += item.Key + "'";
                        }
                    }
                    sqlUpdate += " where MaNV ='" + textBoxMaNhanVien.Text + "'";
                    if (textBoxMaNhanVien != null)
                    {
                        if (MessageBox.Show("Bạn có muốn cập nhật lại thông tin nhân viên không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            dataProcessor.DataChange(sqlUpdate);
                            MessageBox.Show("Thành công");
                        }
                    }
                }
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
