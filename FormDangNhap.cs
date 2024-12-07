using BaiTapLon.HamPhuTro;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace BaiTapLon
{
    public partial class FormDangNhap : Form
    {
        PhanQuyen instance = PhanQuyen.Instance;

        ThaoTacVoiCSDL dataProcess = new ThaoTacVoiCSDL();
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            textBoxMatKhau.PasswordChar = '*';
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void checkBoxHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHienMatKhau.Checked == true) textBoxMatKhau.PasswordChar = '\0';
            else textBoxMatKhau.PasswordChar = '*';
        }

        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            
            labelThongBao.Text = "";
            if (textBoxTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập tài khoản");
                textBoxTaiKhoan.Focus();
                return;
            }
            if (textBoxMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mật khẩu");
                textBoxMatKhau.Focus();
                return;
            }
            String sqlSelect = "select MaNV, TenCV" +
                " from NHANVIEN join CONGVIEC on NHANVIEN.MaCV = CONGVIEC.MaCV" +
                " where MaNV='" + textBoxTaiKhoan.Text.Trim()+"' and MatKhau='"+textBoxMatKhau.Text.Trim()+"'";
            DataTable dataTable = new DataTable();
            dataTable = dataProcess.DataReader(sqlSelect);
            

            if (dataTable.Rows.Count > 0)
            {
                PhanQuyen.instance.MaNV = dataTable.Rows[0][0].ToString();
                PhanQuyen.instance.CongViec = dataTable.Rows[0][1].ToString();
                this.Hide();
                FormTrangChu formTrangChu = new FormTrangChu();
                formTrangChu.Show();
            }
            else
            {
                String sqlSelectAdmin = "select * from ADMIN where TenDangNhap='" + textBoxTaiKhoan.Text.Trim() + "' and MatKhau='" + textBoxMatKhau.Text.Trim() + "'";
                DataTable dataTableAdmin = new DataTable();
                dataTableAdmin = dataProcess.DataReader(sqlSelectAdmin);
                if (dataTableAdmin.Rows.Count > 0)
                {
                    PhanQuyen.Instance.MaNV = "Admin";
                    PhanQuyen.Instance.CongViec = "Admin";
                    this.Hide();
                    FormTrangChu formTrangChu = new FormTrangChu();
                    formTrangChu.Show();
                }
                else
                {
                    labelThongBao.Text = "Thông tin tài khoản mật khẩu không chính xác";
                    textBoxMatKhau.Text = "";
                    textBoxMatKhau.Focus();
                }
            }
        }
    }
}
