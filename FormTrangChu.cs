using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using BaiTapLon.Forms;
using BaiTapLon.HamPhuTro;
using BaiTapLon.KetNoiCSDL;
using FontAwesome.Sharp;
using Color = System.Drawing.Color;

namespace BaiTapLon
{
    public partial class FormTrangChu : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        //Fields
        private IconButton iconButtonMenu;
        private Panel panelLeft;
        private Form currentFormChild;// Biến dùng để lưu trữ form con hiện tại
        private void OpenChildForm(Form childForm) // phương thức để mở form con
        {
            if (currentFormChild != null) // trc khi mở kiểm tra xem có form con nào đang mở ko
            {
                currentFormChild.Close();// nếu có thì đóng form con đó lại
            }
            currentFormChild = childForm; // sau đó gán form cho biến currentFormChild
            childForm.TopLevel = false; // đặt để form con ko đc coi là một top-level form
            childForm.FormBorderStyle = FormBorderStyle.None;// loại bỏ viền xung quanh của form con, để form con ko có tiêu đề, nút đóng
            childForm.Dock = DockStyle.Fill;//để lấp đầy form cha
            panelBody.Controls.Add(childForm);// đưa form con vào Panel_Body
            panelBody.Tag = childForm;// lưu trữ form con hiện tại trong thuộc tính tag của panel_body
            childForm.BringToFront();//Bảo đảm rằng form con hiển thị trc hết (ở trc mọi control khác trên panel)
            childForm.Show();//hiển thị form con mới
        }
        //Hàm tạo
        public FormTrangChu()
        {
            InitializeComponent();
            panelLeft = new Panel();
            panelLeft.Size = new Size(7, 40);
            panelMenu.Controls.Add(panelLeft);
        }

        private struct RGBcolor
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        private void ActivateButton(object button, Color color)
        {
            DisableButton();
            if (button != null)
            {
                //Button
                iconButtonMenu = (IconButton)button;
                iconButtonMenu.BackColor = Color.FromArgb(37, 36, 81);
                iconButtonMenu.ForeColor = color;
                iconButtonMenu.TextAlign = ContentAlignment.MiddleCenter;
                iconButtonMenu.IconColor = color;
                iconButtonMenu.TextImageRelation = TextImageRelation.TextBeforeImage;
                iconButtonMenu.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                panelLeft.BackColor = color;
                panelLeft.Location = new Point(0, iconButtonMenu.Location.Y);
                panelLeft.Visible = true;
                panelLeft.BringToFront();
                //Icon Home
                iconHome.IconChar = iconButtonMenu.IconChar;
                iconHome.IconColor = color;
            }
            else
            {
                panelLeft.Visible = false;
            }
        }

        private void DisableButton()
        {
            if (iconButtonMenu != null)
            {
                iconButtonMenu.BackColor = Color.FromArgb(31, 30, 68);
                iconButtonMenu.ForeColor = Color.Gainsboro;
                iconButtonMenu.TextAlign = ContentAlignment.MiddleLeft;
                iconButtonMenu.IconColor = Color.Gainsboro;
                iconButtonMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
                iconButtonMenu.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void panelBieuDo_Paint(object sender, PaintEventArgs e)
        {

        }
        //From load trang chủ
        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            labelUser.Text = PhanQuyen.instance.CongViec;
        }
        //Hiện các form con

        //Quản lý khách hàng
        private void iconButtonKH_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonKH.Text;
                this.OpenChildForm(new FormKhachHang());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color3);
        }
        //Quản lý bán hàng
        private void iconButtonBanHang_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Nhân viên bán hàng" || PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonBanHang.Text;
                this.OpenChildForm(new FormTaoDonHang());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color1);
        }
        //Quản lý danh mục sản phẩm
        private void iconButtonDMSP_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonDMSP.Text;
                this.OpenChildForm(new FormDanhMucSP());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color4);

        }
        //Đăng xuất
        private void iconButtonDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormDangNhap formDangNhap = new FormDangNhap();
                formDangNhap.Show();
            }
        }

        private void iconButtonNCC_Click(object sender, EventArgs e)
        {

            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonNCC.Text;
                this.OpenChildForm(new FormNhaCungCap());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color2);
        }

        private void panelBieuDo_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentFormChild != null) // trc khi mở kiểm tra xem có form con nào đang mở ko
            {
                labelHome.Text = "Home";
                iconHome.IconChar = IconChar.Home;
                iconHome.IconColor = RGBcolor.color1;
                this.ActivateButton(null, RGBcolor.color1);


            }
        }

        private void iconButtonNhanVien_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonNhanVien.Text;
                this.OpenChildForm(new FormDanhSachNhanVien());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color3);
        }

        private void iconButtonThongTin_Click(object sender, EventArgs e)
        {
            labelHome.Text = iconButtonThongTin.Text;
            this.OpenChildForm(new FormThongTinCaNhan());
            this.ActivateButton(sender, RGBcolor.color5);
        }

        private void iconButtonKhoHang_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý" || PhanQuyen.instance.CongViec == "Nhân viên kho")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonKhoHang.Text;
                this.OpenChildForm(new FormHoaDonNhap());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color5);
        }

        private void iconButtonTKDT_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonTKDT.Text;
                this.OpenChildForm(new FormThongKeDoanhThu());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color6);
        }

        private void iconButtonSanPham_Click(object sender, EventArgs e)
        {
            if (PhanQuyen.instance.CongViec == "Quản lý")
            {
                //this.Hide();
                //FormTaoDonHang formTaoDonHang = new FormTaoDonHang();
                //formTaoDonHang.Show();
                labelHome.Text = iconButtonSanPham.Text;
                this.OpenChildForm(new FormThongKeDoanhThu());

            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào đây!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            this.ActivateButton(sender, RGBcolor.color6);
        }
    }
}
