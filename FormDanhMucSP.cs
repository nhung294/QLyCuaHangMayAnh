using BaiTapLon.HamPhuTro;
using BaiTapLon.KetNoiCSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.Forms
{
    public partial class FormDanhMucSP : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        public FormDanhMucSP()
        {
            InitializeComponent();
        }
        //Sinh mã loại  
        private String sinhMaLoai()
        {   //LH01
            String maLoaiNew = "LH";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaLoai from LOAIHANG ORDER BY MaLoai ASC");
            if (dataTable.Rows.Count == 0) { return "LH01"; }
            int[] maLoai = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    String[] tam = item.ToString().Split('H');
                    maLoai[i] = Convert.ToInt32(tam[1]);
                    i++;
                }
            }
            int max = maLoai.Max();
            return maLoaiNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }

        private void LamMoi()
        {
            // Làm mới các trường nhập liệu
            textBoxMaLoai.Text = sinhMaLoai();
            textBoxTenLoai.Text = string.Empty;
            buttonSuaLoai.Enabled = false;
            buttonThemLoai.Enabled = true;
            // Đặt con trỏ vào ô nhập tên loại
            textBoxTenLoai.Focus();

            // Lấy dữ liệu từ bảng LOAIHANG
            string sqlGetLoaiHang = "SELECT * FROM LOAIHANG";
            DataTable dataTableLoaiHang = dataProcessor.DataReader(sqlGetLoaiHang);

            // Đặt dữ liệu vào DataGridView
            dataGridViewLoaiHang.DataSource = dataTableLoaiHang;

            // Tùy chỉnh lại cột trong DataGridView
            dataGridViewLoaiHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLoaiHang.Columns["MaLoai"].HeaderText = "Mã Loại";
            dataGridViewLoaiHang.Columns["TenLoai"].HeaderText = "Tên Loại";
        }

        // Phương thức để tải dữ liệu từ cơ sở dữ liệu vào DataGridView

        private void buttonThemLoai_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tên loại đã nhập chưa
            if (textBoxTenLoai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTenLoai.Focus();
                return;
            }

            // Kiểm tra xem tên loại đã tồn tại chưa
            string sqlKiemTraTenLoai = "SELECT * FROM LOAIHANG WHERE TenLoai = N'" + textBoxTenLoai.Text.Trim() + "'";
            if (dataProcessor.DataReader(sqlKiemTraTenLoai).Rows.Count > 0)
            {
                MessageBox.Show("Tên loại này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTenLoai.Focus();
                return;
            }

            // Thêm loại hàng mới
            string sqlThemLoaiHang = "INSERT INTO LOAIHANG (MaLoai, TenLoai) VALUES ('"
                + textBoxMaLoai.Text.Trim() + "', N'" + textBoxTenLoai.Text.Trim() + "')";

            dataProcessor.DataChange(sqlThemLoaiHang);

            // Thông báo thêm thành công
            MessageBox.Show("Bạn đã thêm loại hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // Làm mới giao diện
            this.LamMoi();
        }

        private void FormDanhMucSP_Load(object sender, EventArgs e)
        {
            this.LamMoi();
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

        private void buttonSuaLoai_Click(object sender, EventArgs e)
        {
            if (textBoxMaLoai.Text.Length == 0) 
            {
                MessageBox.Show("Bạn phải chọn loại hàng cần sửa");
                return;
            }
            if(textBoxTenLoai.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại");
                return;
            }

            string sqlLoaihang = "SELECT * FROM LoaiHang WHERE TenLoai = N'" + textBoxTenLoai.Text.Trim() + "'";
            if (dataProcessor.DataReader(sqlLoaihang).Rows.Count > 1)
            {
                MessageBox.Show("loại hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlUpdate = "UPDATE LoaiHang SET TenLoai = N'";
            sqlUpdate = sqlUpdate + textBoxTenLoai.Text.Trim() + "' where MaLoai = '";
            sqlUpdate = sqlUpdate + textBoxMaLoai.Text.Trim() + "'";

            if (MessageBox.Show("Bạn có muốn sửa thông tin loại hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataProcessor.DataChange(sqlUpdate);
                this.LamMoi();
                MessageBox.Show("Bạn đã sửa thành công");
            }
        }

        private void dataGridViewLoaiHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewLoaiHang.Rows[e.RowIndex];

                // Gán giá trị từ các ô vào các TextBox
                textBoxMaLoai.Text = Convert.ToString(row.Cells["MaLoai"].Value);
                textBoxTenLoai.Text = Convert.ToString(row.Cells["TenLoai"].Value);
                buttonThemLoai.Enabled = false;
                buttonSuaLoai.Enabled = true;
            }
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            this.LamMoi();
        }
    }
}
