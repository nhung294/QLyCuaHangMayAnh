using BaiTapLon.KetNoiCSDL;
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
using System.Data.SqlClient;


namespace BaiTapLon.Forms
{
    public partial class FormThongKeDoanhThu : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();

        public FormThongKeDoanhThu()
        {
            InitializeComponent();
        }
        private string epTongTien()
        {
            string tongTienTemp = labelTongDoanhThu.Text;
            string[] temp = tongTienTemp.Split(' ');
            return temp[0];
        }
        private void FormThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            labelTongDoanhThu.Text = "0 VND";
            dateTimePickerTuNgay.Format = DateTimePickerFormat.Custom;
            dateTimePickerTuNgay.CustomFormat = "dd/MM/yyyy";

            dateTimePickerDenNgay.Format = DateTimePickerFormat.Custom;
            dateTimePickerDenNgay.CustomFormat = "dd/MM/yyyy";
            dataGridViewDSThongKe.Columns.Clear();
            dataGridViewDSThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Tạo và thêm cột
            dataGridViewDSThongKe.Columns.Add("MaHDB", "Mã hóa đơn bán");
            dataGridViewDSThongKe.Columns.Add("NgayBan", "Ngày bán");
            dataGridViewDSThongKe.Columns.Add("MaNV", "Tên nhân viên");
            dataGridViewDSThongKe.Columns.Add("TenKH", "Tên khách hàng");
            dataGridViewDSThongKe.Columns.Add("SoLuong", "Số lượng");
            dataGridViewDSThongKe.Columns.Add("GiamGia", "Giảm giá");
            dataGridViewDSThongKe.Columns.Add("ThanhTien", "Thành tiền");
            dataGridViewDSThongKe.Columns.Add("TenHang", "Tên hàng");
        }

        private void buttonThongKe_Click(object sender, EventArgs e)
        {
            if (dateTimePickerTuNgay.Value <= dateTimePickerDenNgay.Value)
            {
                string sqlSelect = "SELECT HoaDonBan.MaHDB, NgayBan,HOADONBAN.MaNV, TenKH,CHITIETHDB.SoLuong, GiamGia, ThanhTien,DANHMUCHANGHOA.TenHH" +
                " FROM HOADONBAN JOIN CHITIETHDB ON HOADONBAN.MaHDB = CHITIETHDB.MaHDB" +
                " join NHANVIEN on HOADONBAN.MaNV = NHANVIEN.MaNV" +
                " join KHACHHANG on HOADONBAN.MaKH = KHACHHANG.MaKH" +
                " join DANHMUCHANGHOA on CHITIETHDB.MaHH = DANHMUCHANGHOA.MaHH" +
                " WHERE NgayBan <= '";
                sqlSelect += dateTimePickerDenNgay.Text + "' and NgayBan >= '";
                sqlSelect += dateTimePickerTuNgay.Text + "'";
              
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader(sqlSelect);
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewDSThongKe.Rows.Add(
                        row["MaHDB"],       // Mã hóa đơn bán
                        row["NgayBan"],     // Ngày bán
                        row["MaNV"],        // Mã nhân viên
                        row["TenKH"],       // Tên khách hàng
                        row["SoLuong"],     // Số lượng
                        row["GiamGia"],     // Giảm giá
                        row["ThanhTien"],   // Thành tiền
                        row["TenHH"]        // Tên hàng hóa
                    );
                    labelTongDoanhThu.Text = Convert.ToString(Convert.ToInt64(epTongTien()) + Convert.ToInt64(row["ThanhTien"])) + " VND";
                }
            }
            else
            {
                MessageBox.Show("Từ ngày phải nhỏ hơn đến ngày");
            }

            

        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
