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
    public partial class FormThemNhanVien : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
		Hashtable congViec = new Hashtable();
		public FormThemNhanVien()
        {
            InitializeComponent();
        }
        //Sinh mã nhân viên
        private String sinhMaNV()
        {
            String maNVNew = "NV";
			DataTable dataTable = new DataTable();
			dataTable = dataProcessor.DataReader("Select MaNV from NHANVIEN ORDER BY MaNV ASC");
			if (dataTable.Rows.Count == 0) { return "NV01"; }
			int[] maNV = new int[dataTable.Rows.Count + 1];
			//
			foreach(DataRow row in dataTable.Rows)
			{
				int i = 0;
				foreach(var item in row.ItemArray)
				{
					String[] tam=item.ToString().Split('V');
					maNV[i] = Convert.ToInt32(tam[1]);
					i++;
				}
			}
			int max = maNV.Max();
			return maNVNew + (max+1<10?"0"+(max+1).ToString():(max+1).ToString());
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
		//ADD dữ liệu vào combobox
		private void addComboBox()
		{
			DataTable dataTable = new DataTable();
			//dữ liệu giới tính
			comboBoxGioiTinh.Items.Add("Nam");
			comboBoxGioiTinh.Items.Add("Nữ");
			comboBoxGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
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
			comboBoxCongViec.DropDownStyle = ComboBoxStyle.DropDownList;
		}

        //form load
        private void load()
        {
			textBoxMaNhanVien.Text = this.sinhMaNV();
			this.lamMoi();
        }
        //Load form
        private void ThemNhanVien_Load(object sender, EventArgs e)
        {
           
            this.addComboBox();
			this.load();
		}
        //Thêm nhân viên
		private void buttonThem_Click(object sender, EventArgs e)
		{
           if(textBoxHoTen.Text.Trim() == string.Empty)
			{
				MessageBox.Show("Bạn chưa nhập Tên nhân viên");
				return;
			}
		   if(comboBoxGioiTinh.Text.Trim() == string.Empty)
			{
				MessageBox.Show("Bạn chưa chọn giới tính");
				return;
			}
		   if(dateTimePickeNgaySinh.Text.Trim() == string.Empty)
			{
				MessageBox.Show("Bạn phải nhập ngày sinh");
				return;			
			}
			else
			{
                if (DateTime.Now.Year - dateTimePickeNgaySinh.Value.Year < 19 && DateTime.Now.Year - dateTimePickeNgaySinh.Value.Year > 50)
                {
                    MessageBox.Show("Năm sinh không hợp lệ");
					return;
                }
            }
		   if(comboBoxCongViec.Text.Trim() == string.Empty)
			{
				MessageBox.Show("Bạn phải chọn cong việc");
				return;
			}
		   if(textBoxDiaChi.Text.Trim() == string.Empty)
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
            if (dataTable.Rows.Count > 0)
            {
				MessageBox.Show("Số điện thoại đã tồn tại");
				return;
            }

			string sqlInsert = "INSERT INTO NHANVIEN (MaNV, TenNV, GioiTinh, DienThoai, DiaChi, NgaySinh, MaCV,MatKhau) VALUES('";
            sqlInsert += textBoxMaNhanVien.Text + "', N'";
            sqlInsert += textBoxHoTen.Text + "', N'";
            sqlInsert += comboBoxGioiTinh.Text + "', '";
            sqlInsert += textBoxDienThoai.Text + "', N'";
            sqlInsert += textBoxDiaChi.Text + "', '";
            sqlInsert += dateTimePickeNgaySinh.Text + "', '";
            foreach (DictionaryEntry item in congViec)
            {
                if (item.Value.ToString() == comboBoxCongViec.Text.Trim().ToString())
                {
                    sqlInsert += item.Key + "', '";
                }
            }
			sqlInsert += textBoxMatKhau.Text + "')";

			if(MessageBox.Show("Bạn có muốn thêm nhân viên này không?","Thông báo",MessageBoxButtons.YesNo)==DialogResult.Yes)
			{
				dataProcessor.DataChange(sqlInsert);
				MessageBox.Show("Thành công");
			}




        }
        //SDT chỉ được nhập số
        private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
		}
		//Làm lại dữ liệu
		private void lamMoi()
		{
			textBoxMaNhanVien.Text = this.sinhMaNV();
			textBoxHoTen.Text = "";
			textBoxDienThoai.Text = "";
			textBoxDiaChi.Text = "";
			textBoxMatKhau.Text = "";
			textBoxMatKhau.PasswordChar ='*';
			comboBoxCongViec.SelectedIndex = -1;
			comboBoxGioiTinh.SelectedIndex = -1;
		}
		//Nhấn nút làm mới
		private void buttonLamMoi_Click(object sender, EventArgs e)
		{
			this.lamMoi();
		}

        private void buttonThoat_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                //FormTrangChu formTrangChu = new FormTrangChu();
                //formTrangChu.Show();
            }
        }

        private void dateTimePickeNgaySinh_Leave(object sender, EventArgs e)
        {
			textBoxMatKhau.Text = dateTimePickeNgaySinh.Text;
        }

    }
}
