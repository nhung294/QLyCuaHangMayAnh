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
    public partial class FormTaoDonHang : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        public FormTaoDonHang()
        {
            InitializeComponent();
        }

        //Sinh mã hóa đơn
        private String sinhMaHoaDon()
        {
            String maHDBNew = "HDB";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaHDB from HOADONBAN ORDER BY MaHDB ASC");
            if (dataTable.Rows.Count == 0) { return "HDB01"; }
            int[] maHDB = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    String[] tam = item.ToString().Split('B');
                    maHDB[i] = Convert.ToInt32(tam[1]);
                    i++;
                }
            }
            int max = maHDB.Max();
            return maHDBNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }

        //Sinh mã khách hàng
        private String sinhMaKhachHang()
        {
            String maKHNew = "KH";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaKH from KHACHHANG ORDER BY MaKH ASC");
            if (dataTable.Rows.Count == 0) { return "01"; }
            int[] maKH = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    String[] tam = item.ToString().Split('H');
                    maKH[i] = Convert.ToInt32(tam[1]);
                    i++;
                }
            }
            int max = maKH.Max();
            return maKHNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }
        //Tổng tiền
        private string epTongTien()
        {
            string tongTienTemp = labelTongTien.Text;
            string[] temp = tongTienTemp.Split(' ');
            return temp[0];
        }
        //Làm mới
        private void lamMoi()
        {
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select TenNV from NHANVIEN where MaNV = '" +  PhanQuyen.instance.MaNV + "'");
            textBoxSoHD.Text = sinhMaHoaDon();
            textBoxMaKH.Text = sinhMaKhachHang();
            dateTimePickerNgayBan.Value = DateTime.Now;
            textBoxMaNV.Text = PhanQuyen.instance.MaNV;
            textBoxTenNV.Text = dataTable.Rows[0][0].ToString();
            labelTongTien.Text = "0 VND";

            textBoxTenKH.Text = string.Empty;
            textBoxDiaChi.Text = string.Empty;
            textBoxDienThoai.Text = string.Empty;
            textBoxMaHang.Text = string.Empty;
            textBoxSoLuong.Text = string.Empty;
            textBoxTenHang.Text = string.Empty;
            textBoxGiamGia.Text = string.Empty;
            textBoxDonGia.Text = string.Empty;
            textBoxThanhTien.Text = string.Empty;
            buttonThemHoaDon.Enabled = true;
            buttonInHoaDon.Enabled = false;
            textBoxTimKiem.Text = string.Empty;

            textBoxDiaChi.Enabled = true;
            textBoxDienThoai.Enabled = true;
            textBoxTenKH.Enabled = true;
            textBoxMaHang.Enabled = true;
            buttonThemHoaDon.Enabled = true;
            buttonThem.Enabled = true;

            // Xóa các cột cũ nếu có
            dataGridViewDSMatHang.Columns.Clear();
            dataGridViewDSMatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tạo và thêm cột
            dataGridViewDSMatHang.Columns.Add("MaHang", "Mã hàng");
            dataGridViewDSMatHang.Columns.Add("TenHang", "Tên hàng");
            dataGridViewDSMatHang.Columns.Add("DonGia", "Đơn giá");
            dataGridViewDSMatHang.Columns.Add("SoLuong", "Số lượng");
            dataGridViewDSMatHang.Columns.Add("GiamGia", "Giảm giá");
            dataGridViewDSMatHang.Columns.Add("ThanhTien", "Thành tiền");


        }
        //Làm mới tìm kiếm
        private void lamMoiTimKiem()
        {

            // Xóa các cột cũ nếu có
            dataGridViewDSMatHang.Columns.Clear();
            dataGridViewDSMatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tạo và thêm cột
            dataGridViewDSMatHang.Columns.Add("MaHang", "Mã hàng");
            dataGridViewDSMatHang.Columns.Add("TenHang", "Tên hàng");
            dataGridViewDSMatHang.Columns.Add("DonGia", "Đơn giá");
            dataGridViewDSMatHang.Columns.Add("SoLuong", "Số lượng");
            dataGridViewDSMatHang.Columns.Add("GiamGia", "Giảm giá");
            dataGridViewDSMatHang.Columns.Add("ThanhTien", "Thành tiền");

            textBoxDiaChi.Enabled = false;
            textBoxDienThoai.Enabled = false;
            textBoxTenKH.Enabled = false;
            textBoxMaHang.Enabled = false;
            buttonThemHoaDon.Enabled = false;
            buttonThem.Enabled = false;
        }
        //Làm mới danh mục mua hàng
        private void lamMoiMuaHang()
        {
            textBoxMaHang.Text = string.Empty;
            textBoxGiamGia.Text= string.Empty;
            textBoxSoLuong.Text = string.Empty;
            textBoxTenHang.Text = string.Empty;
            textBoxThanhTien.Text= string.Empty;
            textBoxDonGia.Text = string.Empty;

            textBoxMaHang.Focus();
            textBoxGiamGia.Enabled = false;
            textBoxSoLuong.Enabled = false;
        }
        //Điền thông tin khi người dùng tìm kiếm
        private void dienThongTin()
        {
            textBoxSoHD.Text = string.Empty;
            dateTimePickerNgayBan.Value = DateTime.Now;
            textBoxMaNV.Text = string.Empty;
            textBoxTenNV.Text = string.Empty;
            textBoxMaKH.Text = string.Empty;
            textBoxTenKH.Text = string.Empty;
            textBoxDiaChi.Text = string.Empty;
            textBoxDienThoai.Text = string.Empty;
            textBoxMaHang.Text = string.Empty;
            textBoxSoLuong.Text = string.Empty;
            textBoxTenHang.Text = string.Empty;
            textBoxGiamGia.Text = string.Empty;
            textBoxDonGia.Text = string.Empty;
            textBoxThanhTien.Text = string.Empty;
            buttonThemHoaDon.Enabled = false;
            buttonInHoaDon.Enabled = true;

        }
        //Kiểm tra hàng hóa có trong datagridview 
        private bool IsDgvHangHoa(string maHang)
        {
            foreach (DataGridViewRow row in dataGridViewDSMatHang.Rows)
            {
                // Kiểm tra cột Mã hàng, tránh trường hợp null
                if (row.Cells["MaHang"].Value != null && row.Cells["MaHang"].Value.ToString() == maHang)
                {
                    return true; // Mặt hàng đã tồn tại
                }
            }
            return false; // Mặt hàng chưa tồn tại
        }

        //Kiểm tra số điện thoại
        private bool IsSDT()
        {
            if (textBoxDienThoai.Text.Length != 10)
            {
                return false;
            }
            else
            {
                string sdt = textBoxDienThoai.Text;
                if (sdt[0] == 0)
                {
                    return false ;
                }
            }
            return true;
        }
        //Load form
        private void FormTaoDonHang_Load(object sender, EventArgs e)
        {
            dateTimePickerNgayBan.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgayBan.CustomFormat = "dd/MM/yyyy";
            this.lamMoi();
        }
        //Thêm đơn hàng
        private void buttonThemHoaDon_Click(object sender   , EventArgs e)
        {
            if(textBoxTenKH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng!");
                textBoxTenKH.Focus();
                return;
            }
            if(textBoxDiaChi.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                textBoxDiaChi.Focus();
                return;
            }  
            if(textBoxDienThoai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                textBoxDienThoai.Focus();
                return;
            }
            if (dataGridViewDSMatHang.Rows.Count == 1)
            {
                MessageBox.Show("Bạn phải thêm sản phẩm");
                return;
            }
            //Thêm khách hàng
            string sqlKH = "Select * from KhachHang where MaKH = '" + textBoxMaKH.Text + "'";

            if(dataProcessor.DataReader(sqlKH).Rows.Count == 0)
            {

                string sqlThemKH = "INSERT INTO KHACHHANG(MaKH, TenKH, DiaChi, DienThoai) VALUES ('"
                + textBoxMaKH.Text.Trim() + "', N'" + textBoxTenKH.Text.Trim() + "', N'" + textBoxDiaChi.Text.Trim() + "', '" + textBoxDienThoai.Text.Trim() + "')";
                dataProcessor.DataChange(sqlThemKH);
            }
            //Thêm hóa đơn
            string sqlThemHD = "INSERT INTO HOADONBAN (MaHDB, NgayBan, TongTien, MaNV, MaKH) VALUES ('"
                + textBoxSoHD.Text.Trim() + "', '" + dateTimePickerNgayBan.Text.ToString() + "', " + Convert.ToInt64(epTongTien()) + ", '"
                + textBoxMaNV.Text.Trim() + "', '" +textBoxMaKH.Text.Trim() + "')" ;
            dataProcessor.DataChange(sqlThemHD);
            //Thêm chi tiết hóa đơn
            foreach (DataGridViewRow row in dataGridViewDSMatHang.Rows)
            {
                string sqlThemChiTietHD = "INSERT INTO CHITIETHDB (SoLuong, GiamGia, ThanhTien, MaHDB, MaHH) VALUES (";
                string sqlUpdate = "UPDATE DANHMUCHANGHOA SET SoLuong = ";
                if (!row.IsNewRow)
                {
                    // Lấy giá trị từng cột trong dòng hiện tại
                    string maHang = row.Cells["MaHang"].Value.ToString();
                    string tenHang = row.Cells["TenHang"].Value.ToString();
                    decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value); 
                    int soLuongMua = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal giamGia = Convert.ToDecimal(row.Cells["GiamGia"].Value);
                    decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                    sqlThemChiTietHD += soLuongMua + ", ";
                    sqlThemChiTietHD += giamGia + ", ";
                    sqlThemChiTietHD += thanhTien + ", '";
                    sqlThemChiTietHD += textBoxSoHD.Text.Trim() + "',  '";
                    sqlThemChiTietHD += maHang + "')";
                    //Lấy số lượng hàng còn
                    string sqlSelectSoLuong = "Select SoLuong from DANHMUCHANGHOA where MaHH = '" + maHang + "'";
                    DataTable dataTable = new DataTable();
                    dataTable = dataProcessor.DataReader(sqlSelectSoLuong);
                    int soLuongCon = Convert.ToInt16(dataTable.Rows[0][0].ToString());
                    //Update lại số lượng
                    sqlUpdate = sqlUpdate + Convert.ToString(soLuongCon - soLuongMua);
                    sqlUpdate = sqlUpdate + " where MaHH = '" + maHang + "'";
                    dataProcessor.DataChange(sqlUpdate);
                    dataProcessor.DataChange(sqlThemChiTietHD);
                }

            }
            MessageBox.Show("Bạn đã thêm thành công");
            this.lamMoi();
        }
        //In hóa đơn
        private void buttonInHoaDon_Click(object sender, EventArgs e)
        {

        }
        //Làm mới
        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            this.lamMoi();
        }
        //Thêm mặt hàng
        private void buttonThem_Click(object sender, EventArgs e)
        {
            if(textBoxMaHang.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập mã hàng");
                textBoxMaHang.Focus();
                return;
            }
            else if(textBoxSoLuong.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập số lượng");
                textBoxSoLuong.Focus();
                return;
            }
            else
            {
                string maHang = textBoxMaHang.Text.Trim();
                string tenHang = textBoxTenHang.Text.Trim();
                string donGia = textBoxDonGia.Text.Trim();
                string soLuong = textBoxSoLuong.Text.Trim();
                string giamGia = textBoxGiamGia.Text.Trim();
                string thanhTien = textBoxThanhTien.Text.Trim();

                dataGridViewDSMatHang.Rows.Add(maHang, tenHang, donGia, soLuong, giamGia, thanhTien);
                this.lamMoiMuaHang();

                labelTongTien.Text = Convert.ToString(Convert.ToInt64(epTongTien()) + Convert.ToInt64(thanhTien)) + " VND";
            }
        }
        //Khi người dùng nhập xong mã hàng
        private void textBoxMaHang_Leave(object sender, EventArgs e)
        {
            if (textBoxMaHang.Text.Length > 0)
            {
                if(IsDgvHangHoa(textBoxMaHang.Text.Trim()) == true)
                {
                    MessageBox.Show("Bạn đã thêm hàng hóa này rồi");
                    textBoxMaHang.Text = string.Empty;
                    return;
                }   
                //Kiểm tra xem hàng hóa có trong database không
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader("Select TenHH, DonGiaBan" +
                                                    " from DANHMUCHANGHOA" +
                                                    " where MaHH ='" + textBoxMaHang.Text.Trim().ToString() + "'");
                if(dataTable.Rows.Count > 0)
                {
                    textBoxTenHang.Text = dataTable.Rows[0][0].ToString();
                    textBoxDonGia.Text = dataTable.Rows[0][1].ToString();
                    textBoxSoLuong.Text = string.Empty;
                    textBoxSoLuong.Enabled = true;
                    textBoxGiamGia.Text = string.Empty;
                    textBoxGiamGia.Enabled = false;
                    textBoxThanhTien.Text = string.Empty ;
                }
                else
                {
                    MessageBox.Show("Mã hàng bạn nhập không có trong hệ thống!");
                    textBoxMaHang.Text = string.Empty;
                    textBoxSoLuong.Enabled = false;
                    textBoxGiamGia.Enabled = false;
                    return;
                }
            }
        }
        //Khi người dùng nhập xong số lượng
        private void textBoxSoLuong_Leave(object sender, EventArgs e)
        {
            if(textBoxSoLuong.Text.Trim().Length == 0 || Convert.ToInt16(textBoxSoLuong.Text.ToString()) == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng và số lượng phải lớn hơn 0 !");
                textBoxSoLuong.Text=string.Empty;
                textBoxSoLuong.Focus();
                textBoxGiamGia.Enabled = false;
                return;
            }
            else
            {
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader("Select SoLuong" +
                                                    " from DANHMUCHANGHOA" +
                                                    " where MaHH ='" + textBoxMaHang.Text.Trim().ToString() + "'");
                int SoLuongHH = Convert.ToInt16(dataTable.Rows[0][0].ToString());
                if( SoLuongHH - Convert.ToInt16(textBoxSoLuong.Text.ToString()) < 0)
                {
                    MessageBox.Show("Số lượng hàng hóa hiện không đủ");
                    textBoxSoLuong.Text = string.Empty;
                    textBoxSoLuong.Focus();
                    textBoxGiamGia.Enabled = false;
                    return;
                }
                else
                {
                    if (textBoxGiamGia.Text.Length > 0)
                    {
                        long donGia = Convert.ToInt64(textBoxDonGia.Text.Trim());
                        long giamGia = Convert.ToInt64(textBoxGiamGia.Text.Trim());
                        long soLuong = Convert.ToInt64(textBoxSoLuong.Text.Trim());
                        long thanhTien = donGia * soLuong * (100 - giamGia) / 100;
                        textBoxThanhTien.Text = thanhTien.ToString();
                    }
                    textBoxGiamGia.Enabled = true;
                }
            }
        }
        //Khi người dùng nhập xong giảm giá
        private void textBoxGiamGia_Leave(object sender, EventArgs e)
        {
            if (textBoxGiamGia.Text.Length > 0 || Convert.ToInt16(textBoxGiamGia.Text.ToString())<100 )
            {
                long donGia = Convert.ToInt64(textBoxDonGia.Text.Trim());
                long giamGia = Convert.ToInt64(textBoxGiamGia.Text.Trim());
                long soLuong = Convert.ToInt64(textBoxSoLuong.Text.Trim());

                long thanhTien = donGia * soLuong * (100 - giamGia) / 100;
                textBoxThanhTien.Text = thanhTien.ToString();
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập giảm giả hoặc giảm giá bạn nhập lớn hơn 100%");
                return;
            }
        }
        //Số điện thoại chỉ được nhập số
        private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
        //Số lượng chỉ được nhập số
        private void textBoxSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
        //Giảm giá chỉ được nhập số
        private void textBoxGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
        //Nhập số điện thoại thì hiện khách hàng
        private void textBoxDienThoai_Leave(object sender, EventArgs e)
        {
            if (IsSDT() == false)
            {
                MessageBox.Show("Bạn nhập số điện thoại sai");
                textBoxDienThoai.Focus();
                return;
            }
            else
            {
                string sqlKH = "select MaKH,TenKH,DiaCHi from KHACHHANG where DienThoai ='" + textBoxDienThoai.Text + "'";
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader(sqlKH);
                if (dataTable.Rows.Count > 0)
                {
                    textBoxMaKH.Text = dataTable.Rows[0][0].ToString();
                    textBoxTenKH.Text = dataTable.Rows[0][1].ToString();
                    textBoxDiaChi.Text = dataTable.Rows[0][2].ToString();
                }
                else
                {
                    textBoxMaKH.Text = this.sinhMaKhachHang();
                    textBoxTenKH.Text = string.Empty;
                    textBoxDiaChi.Text = string.Empty;
                }
            }
        }
        //Tìm kiếm hóa đơn
        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            this.lamMoiTimKiem();
            if(textBoxTimKiem.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn để tìm kiếm");
                textBoxTimKiem.Focus();
                return;
            }
            else
            {
                string sqlHDB = "Select HoaDonBan.MaHDB, NgayBan, HOADONBAN.MaNV, TenNV, HOADONBAN.MaKH, TenKH, KhachHang.DiaChi, KhachHang.DienThoai, HOADONBAN.TongTien" +
                    " from HOADONBAN join KHACHHANG on HOADONBAN.MaKH = KHACHHANG.MaKH" +
                    " join NHANVIEN on HOADONBAN.MaNV = NHANVIEN.MaNV" +
                    " where HoaDonBan.MaHDB = '"
                    + textBoxTimKiem.Text.Trim() + "'";
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader(sqlHDB);
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không có hóa đơn bạn tìm kiếm");
                }
                else
                {
                    textBoxSoHD.Text = dataTable.Rows[0][0].ToString();
                    dateTimePickerNgayBan.Text = dataTable.Rows[0][1].ToString();
                    textBoxMaNV.Text = dataTable.Rows[0][2].ToString();
                    textBoxTenNV.Text = dataTable.Rows[0][3].ToString();
                    textBoxMaKH.Text = dataTable.Rows[0][4].ToString();
                    textBoxTenKH.Text = dataTable.Rows[0][5].ToString();
                    textBoxDiaChi.Text = dataTable.Rows[0][6].ToString();
                    textBoxDienThoai.Text = dataTable.Rows[0][7].ToString();
                    labelTongTien.Text = dataTable.Rows[0][8].ToString() + " VND";

                    string sqlCTHDB = "SELECT CHITIETHDB.MaHH," +
                        " DANHMUCHANGHOA.TenHH," +
                        " CHITIETHDB.SoLuong," +
                        " CHITIETHDB.GiamGia," +
                        " DANHMUCHANGHOA.DonGiaBan," +
                        " (DANHMUCHANGHOA.DonGiaBan * CHITIETHDB.SoLuong * (100 - CHITIETHDB.GiamGia) / 100) AS ThanhTien" +
                        " FROM CHITIETHDB JOIN DANHMUCHANGHOA" +
                        " ON  CHITIETHDB.MaHH = DANHMUCHANGHOA.MaHH" +
                        " WHERE MaHDB = '" + textBoxSoHD.Text + "'";
                    DataTable dt = new DataTable();
                    dt = dataProcessor.DataReader(sqlCTHDB);
                 
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            // Lấy giá trị từ DataTable
                            string maHang = row["MaHH"].ToString();
                            string tenHang = row["TenHH"].ToString();
                            string donGia = row["DonGiaBan"].ToString();
                            string soLuong = row["SoLuong"].ToString();
                            string giamGia = row["GiamGia"].ToString();
                            string thanhTien = row["ThanhTien"].ToString();

                            // Thêm dòng vào DataGridView
                            dataGridViewDSMatHang.Rows.Add(maHang, tenHang, donGia, soLuong, giamGia, thanhTien);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để hiển thị.");
                    }

                }
            }
        }
        //Thoát
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                FormTrangChu formTrangChu = new FormTrangChu();
                formTrangChu.Show();
            }

        }
    }
}
