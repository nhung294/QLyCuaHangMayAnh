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
    public partial class FormHoaDonNhap : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        Hashtable NCC = new Hashtable();
        Hashtable MatHang= new Hashtable();
        Hashtable LoaiHang= new Hashtable();
        Hashtable DoPhanGiai= new Hashtable();
        Hashtable DungLuong= new Hashtable();
        Hashtable DoiMay= new Hashtable();
        Hashtable NuocSX= new Hashtable();
        Hashtable Mau= new Hashtable();
        Hashtable HangSX= new Hashtable();
        int maHangTemp = 0;
        public FormHoaDonNhap()
        {
            InitializeComponent();
        }

        //ADD dữ liệu vào combobox
        private void addComboBox()
        {
            DataTable dataTable = new DataTable();
            //Ten NCC
            dataTable = dataProcessor.DataReader("Select MaNCC,TenNCC From NhaCungCap");
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
                        comboBoxTenNCC.Items.Add(item.ToString());
                        NCC.Add(tam, item.ToString());
                    }

                }
            }

            //Mat hang
            dataTable = dataProcessor.DataReader("Select MaHH,TenHH From DanhMucHangHoa");
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
                        comboBoxTenHang.Items.Add(item.ToString());
                        MatHang.Add(tam, item.ToString());
                    }

                }
            }

            //Loai hang
            dataTable = dataProcessor.DataReader("Select MaLoai,TenLoai From LoaiHang");
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
                        comboBoxTenLoai.Items.Add(item.ToString());
                        LoaiHang.Add(tam, item.ToString());
                    }

                }
            }

            //Do phan giai
            dataTable = dataProcessor.DataReader("Select MaDPG,TenDPG From DoPhanGiai");
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
                        comboBoxDoPhanGiai.Items.Add(item.ToString());
                        DoPhanGiai.Add(tam, item.ToString());
                    }

                }
            }

            //Dung luong
            dataTable = dataProcessor.DataReader("Select MaDL,TenDL From DungLuong");
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
                        comboBoxDungLuong.Items.Add(item.ToString());
                        DungLuong.Add(tam, item.ToString());
                    }

                }
            }

            //Doi may
            dataTable = dataProcessor.DataReader("Select MaDoi,TenDoi From DoiMay");
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
                        comboBoxDoiMay.Items.Add(item.ToString());
                        DoiMay.Add(tam, item.ToString());
                    }

                }
            }

            //Mau sac
            dataTable = dataProcessor.DataReader("Select MaMau,TenMau From MauSac");
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
                        comboBoxMau.Items.Add(item.ToString());
                        Mau.Add(tam, item.ToString());
                    }

                }
            }

            //Nuoc SX
            dataTable = dataProcessor.DataReader("Select MaNuocSX,TenNuocSX From NuocSX");
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
                        comboBoxNuocSX.Items.Add(item.ToString());
                        NuocSX.Add(tam, item.ToString());
                    }

                }
            }

            // Hang SX
            dataTable = dataProcessor.DataReader("Select MaHangSX,TenHangSX From HangSX");
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
                        comboBoxHangSX.Items.Add(item.ToString());
                        HangSX.Add(tam, item.ToString());
                    }

                }
            }
        }

        //Sinh Ma HDN
        private String sinhMaHDN()
        {
            String maHDNNew = "HDN";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaHDN from HoaDonNhap ORDER BY MaHDN ASC");
            if (dataTable.Rows.Count == 0) { return "HDN01"; }
            int[] maHDN = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    String[] tam = item.ToString().Split('N');
                    maHDN[i] = Convert.ToInt32(tam[1]);
                    i++;
                }
            }
            int max = maHDN.Max();
            return maHDNNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }

        //Sinh Ma Hang
        private String sinhMaHang()
        {
            String maHHNew = "HH";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaHH from DanhMucHangHoa ORDER BY MaHH ASC");
            if (dataTable.Rows.Count == 0) { return "HH01"; }
            int[] maHH = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    // Cắt bỏ phần chữ "NCC" và chỉ lấy phần số
                    string soPhanTu = item.ToString().Substring(2); // Lấy từ ký tự thứ 3 trở đi
                    maHH[i] = Convert.ToInt32(soPhanTu);
                    i++;
                }
            }
            int max = maHH.Max();
            maHangTemp = maHH.Max();
            return maHHNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }

        //Tổng tiền
        private string epTongTien()
        {
            string tongTienTemp = labelTongTien.Text;
            string[] temp = tongTienTemp.Split(' ');
            return temp[0];
        }
        private void load()
        {
           
            dateTimePickerNgayNhap.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgayNhap.CustomFormat = "dd/MM/yyyy";

            dataGridViewDSMatHang.Columns.Clear();
            dataGridViewDSMatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Tạo và thêm cột
            dataGridViewDSMatHang.Columns.Add("MaHang", "Mã hàng");
            dataGridViewDSMatHang.Columns.Add("TenHang", "Tên hàng");
            dataGridViewDSMatHang.Columns.Add("SoLuong", "Số lượng");
            dataGridViewDSMatHang.Columns.Add("DonGiaNhap", "Đơn giá nhập");
            dataGridViewDSMatHang.Columns.Add("DonGiaBan", "Đơn giá bán");
            dataGridViewDSMatHang.Columns.Add("ThoiGianBH", "Thời gian bảo hành");
            dataGridViewDSMatHang.Columns.Add("TenLoai", "Tên loại");
            dataGridViewDSMatHang.Columns.Add("DoPhanGiai", "Độ phân giải");
            dataGridViewDSMatHang.Columns.Add("DungLuong", "Dung lượng");
            dataGridViewDSMatHang.Columns.Add("DoiMay", "Đời máy");
            dataGridViewDSMatHang.Columns.Add("NuocSX", "Nước sản xuất");
            dataGridViewDSMatHang.Columns.Add("Mau", "Màu");
            dataGridViewDSMatHang.Columns.Add("HangSX", "Hãng sản xuất");
            dataGridViewDSMatHang.Columns.Add("GiamGia", "Giảm giá");
            dataGridViewDSMatHang.Columns.Add("ThanhTien", "Thành tiền");
            labelTongTien.Text = "0 VND";
        }
        private void lamMoi()
        {
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select TenNV from NHANVIEN where MaNV = '" + PhanQuyen.instance.MaNV + "'");
            textBoxMaNV.Text = PhanQuyen.instance.MaNV;
            textBoxTenNV.Text = dataTable.Rows[0][0].ToString();
            
            dateTimePickerNgayNhap.Value = DateTime.Now;
            textBoxMaHD.Text =this.sinhMaHDN();
           
            textBoxDonGiaNhap.Enabled = true;
            textBoxDonGiaBan.Enabled = true;
            comboBoxTenLoai.Enabled = true;
            comboBoxDoiMay.Enabled = true;
            comboBoxDoPhanGiai.Enabled = true;
            comboBoxDungLuong.Enabled = true;
            comboBoxNuocSX.Enabled = true;
            comboBoxMau.Enabled = true;
            comboBoxHangSX.Enabled = true;
            textBoxThoiGianBH.Enabled = true;

            textBoxDonGiaNhap.Text = string.Empty;
            textBoxDonGiaBan.Text = string.Empty;
            comboBoxTenLoai.SelectedIndex = -1;
            comboBoxDoiMay.SelectedIndex = -1;
            comboBoxDoPhanGiai.SelectedIndex = -1;
            comboBoxDungLuong.SelectedIndex = -1;
            comboBoxNuocSX.SelectedIndex = -1;
            comboBoxMau.SelectedIndex = -1;
            comboBoxHangSX.SelectedIndex = -1;
            comboBoxTenHang.SelectedIndex = -1;
            comboBoxTenNCC.SelectedIndex = -1;
            textBoxMaNCC.Text = string.Empty;
            textBoxDiaChi.Text = string.Empty;
            textBoxDienThoai.Text = string.Empty;
            textBoxSoLuong.Text = string.Empty;
            textBoxGiamGia.Text = string.Empty;
            textBoxThoiGianBH.Text = string.Empty;
            //dataGridViewDSMatHang.Columns.Clear();


        }
        private void FormHoaDonNhap_Load(object sender, EventArgs e)
        {
            this.sinhMaHang();
            this.load();
            this.lamMoi();
            this.addComboBox();
        }

        private void MatHangTonTai()
        {
           textBoxDonGiaNhap.Enabled = false;
           textBoxDonGiaBan.Enabled = false;
           comboBoxTenLoai.Enabled = false;
           comboBoxDoiMay.Enabled = false;
           comboBoxDoPhanGiai.Enabled = false;
           comboBoxDungLuong.Enabled = false;
           comboBoxNuocSX.Enabled = false;
           comboBoxMau.Enabled = false;
           comboBoxHangSX.Enabled = false;
            textBoxThoiGianBH.Enabled = false;
            
        }
        private void buttonThemHoaDon_Click(object sender, EventArgs e)
        {
            if(textBoxMaNCC.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp");
                return;
            }
            if (dataGridViewDSMatHang.Rows.Count == 1)
            {
                MessageBox.Show("Ban chua them mat hang");
                return;
            }

            if (MessageBox.Show("Bạn có muốn thêm hóa đơn nhập không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridViewDSMatHang.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        string maHang = row.Cells["MaHang"].Value.ToString();
                        string tenHang = row.Cells["TenHang"].Value.ToString();
                        long donGiaNhap = Convert.ToInt64(row.Cells["DonGiaNhap"].Value.ToString());
                        long donGiaBan = Convert.ToInt64(row.Cells["DonGiaBan"].Value.ToString());
                        string thoiGianBH = row.Cells["ThoiGianBH"].Value.ToString();
                        string tenLoai = row.Cells["TenLoai"].Value.ToString();
                        string doPhanGiai = row.Cells["DoPhanGiai"].Value.ToString();
                        string dungLuong = row.Cells["DungLuong"].Value.ToString();
                        string doiMay = row.Cells["DoiMay"].Value.ToString();
                        string nuocSX = row.Cells["NuocSX"].Value.ToString();
                        string mau = row.Cells["Mau"].Value.ToString();
                        string hangSX = row.Cells["HangSX"].Value.ToString();
                        string giamGia = row.Cells["GiamGia"].Value.ToString();

                        string sqlselect = "Select * from DanhMucHangHoa where MaHH='" + maHang + "'";
                        if (dataProcessor.DataReader(sqlselect).Rows.Count == 0)
                        {
                            string sqlInsert = "INSERT INTO DANHMUCHANGHOA (MaHH, TenHH, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBH, MaLoai, MaDPG, MaDL, MaHangSX, MaDoi, MaNuocSX, MaMau) VALUES ('";
                            sqlInsert += maHang + "', N'";
                            sqlInsert += tenHang + "', 0, ";
                            sqlInsert += donGiaNhap + ", ";
                            sqlInsert += donGiaBan + ", ";
                            sqlInsert += thoiGianBH + ", '";

                            foreach (DictionaryEntry item in LoaiHang)
                            {
                                if (item.Value.ToString() == comboBoxTenLoai.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in DoPhanGiai)
                            {
                                if (item.Value.ToString() == comboBoxDoPhanGiai.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in DungLuong)
                            {
                                if (item.Value.ToString() == comboBoxDungLuong.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in HangSX)
                            {
                                if (item.Value.ToString() == comboBoxHangSX.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in DoiMay)
                            {
                                if (item.Value.ToString() == comboBoxDoiMay.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in NuocSX)
                            {
                                if (item.Value.ToString() == comboBoxNuocSX.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "', '";
                                    break;
                                }
                            }

                            foreach (DictionaryEntry item in Mau)
                            {
                                if (item.Value.ToString() == comboBoxMau.Text.Trim().ToString())
                                {
                                    sqlInsert += item.Key + "')";
                                    break;
                                }
                            }
                            //MessageBox.Show(sqlInsert);
                            dataProcessor.DataChange(sqlInsert);
                        }

                    }
                }

                //Insert HDN
                string sqlInsertHDN = "INSERT INTO HOADONNHAP (MaHDN, NgayNhap, TongTien, MaNCC, MaNV) VALUES ('";
                sqlInsertHDN += textBoxMaHD.Text + "', '";
                sqlInsertHDN += dateTimePickerNgayNhap.Text + "', ";
                sqlInsertHDN += Convert.ToInt64(epTongTien()) + ", '";
                sqlInsertHDN += textBoxMaNCC.Text + "' , '";
                sqlInsertHDN += textBoxMaNV.Text + "')";
                
                dataProcessor.DataChange(sqlInsertHDN);
                //MessageBox.Show(sqlInsertHDN);
                // Insert chi tiet nhap
                foreach (DataGridViewRow row in dataGridViewDSMatHang.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        string maHang = row.Cells["MaHang"].Value.ToString();
                        string tenHang = row.Cells["TenHang"].Value.ToString();
                        long donGiaNhap = Convert.ToInt64(row.Cells["DonGiaNhap"].Value.ToString());
                        long donGiaBan = Convert.ToInt64(row.Cells["DonGiaBan"].Value.ToString());
                        long soLuong = Convert.ToInt64(row.Cells["SoLuong"].Value.ToString());
                        string thoiGianBH = row.Cells["ThoiGianBH"].Value.ToString();
                        string tenLoai = row.Cells["TenLoai"].Value.ToString();
                        string doPhanGiai = row.Cells["DoPhanGiai"].Value.ToString();
                        string dungLuong = row.Cells["DungLuong"].Value.ToString();
                        string doiMay = row.Cells["DoiMay"].Value.ToString();
                        string nuocSX = row.Cells["NuocSX"].Value.ToString();
                        string mau = row.Cells["Mau"].Value.ToString();
                        string hangSX = row.Cells["HangSX"].Value.ToString();
                        string giamGia = row.Cells["GiamGia"].Value.ToString();
                        long thanhTien = Convert.ToInt64(row.Cells["ThanhTien"].Value.ToString());

                        string sqlInsertCHDN = "INSERT INTO CHITIETHDN (SoLuong, DonGia, GiamGia, ThanhTien, maHDN, MaHH) VALUES (";
                        sqlInsertCHDN += soLuong + ", ";
                        sqlInsertCHDN += donGiaNhap + ", ";
                        sqlInsertCHDN += giamGia + ", ";
                        sqlInsertCHDN+= thanhTien + ", '";
                        sqlInsertCHDN += textBoxMaHD.Text + "', '";
                        sqlInsertCHDN += maHang + "')";
                        //MessageBox.Show(sqlInsertCHDN);

                        dataProcessor.DataChange(sqlInsertCHDN);

                        string sqlUpdateSp = "Update DanhMucHangHoa Set SoLuong = Soluong +" + soLuong+ " where MaHH ='"+maHang+"'";

                        dataProcessor.DataChange(sqlUpdateSp);
                        //MessageBox.Show(sqlUpdateSp);

                    }
                }
                this.load();
                this.lamMoi();
            }

        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            this.sinhMaHang();
        }
        //Thoát
        private void buttonThoat_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                //FormTrangChu formTrangChu = new FormTrangChu();
                //formTrangChu.Show();
            }
        }

        private void comboBoxTenNCC_Leave(object sender, EventArgs e)
        {
            if(comboBoxTenNCC.Text.Trim() != string.Empty)
            {
                string sqlSelect = "select * from NHACUNGCAP" +
                    " where TenNCC = N'" + comboBoxTenNCC.Text + "'";
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader(sqlSelect);
                textBoxMaNCC.Text = dataTable.Rows[0][0].ToString();
                textBoxDiaChi.Text = dataTable.Rows [0][2].ToString();
                textBoxDienThoai.Text = dataTable.Rows[0][3].ToString();
            }
        }

        private void comboBoxTenHang_Leave(object sender, EventArgs e)
        {
            if(comboBoxTenHang.Text.Trim() != string.Empty)
            {
                string sqlSelect = "Select * from DanhMucHangHoa where TenHH = N'"+comboBoxTenHang.Text.Trim()+ "'";
                DataTable dataTable = new DataTable();
                dataTable = dataProcessor.DataReader(sqlSelect);
                if (dataTable.Rows.Count == 1)
                {
                    this.MatHangTonTai();
                    textBoxMaHang.Text = dataTable.Rows[0][0].ToString();
                    textBoxDonGiaNhap.Text = dataTable.Rows[0][3].ToString();
                    textBoxDonGiaBan.Text = dataTable.Rows[0][4].ToString();
                    textBoxThoiGianBH.Text = dataTable.Rows[0][5].ToString();
                    comboBoxTenLoai.Text = LoaiHang[dataTable.Rows[0][6].ToString()].ToString();
                    comboBoxDoPhanGiai.Text = DoPhanGiai[dataTable.Rows[0][7].ToString()].ToString();
                    comboBoxDungLuong.Text = DungLuong[dataTable.Rows[0][8].ToString()].ToString();
                    comboBoxHangSX.Text = HangSX[dataTable.Rows[0][9].ToString()].ToString();
                    comboBoxDoiMay.Text = DoiMay[dataTable.Rows[0][10].ToString()].ToString();
                    comboBoxNuocSX.Text = NuocSX[dataTable.Rows[0][11].ToString()].ToString();
                    comboBoxMau.Text = Mau[dataTable.Rows[0][12].ToString()].ToString();



                }
                if(dataTable.Rows.Count < 1)
                {
                    textBoxMaHang.Text = "HH" + (maHangTemp + 1 < 10 ? "0" + (maHangTemp + 1).ToString() : (maHangTemp + 1).ToString());
                    this.lamMoi();

                }
            }
            else
            {
                this.lamMoi();
            }
        }

        private void textBoxSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
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

        private void buttonThem_Click(object sender, EventArgs e)
        {   
            if (comboBoxTenHang.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn tên hàng ");
                return;
            }
            if (comboBoxTenLoai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn tên loại");
                return;
            }
            if (textBoxSoLuong.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số lượng");
                return;
            }
            if (textBoxDonGiaNhap.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập đơn giá nhập");
                return;
            }
            if (textBoxDonGiaBan.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập đơn giá bán");
                return;
            }
            if (textBoxThoiGianBH.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập thời gian bảo hành");
                return;
            }
            if (comboBoxDoPhanGiai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn độ phân giải");
                return;
            }
            if (comboBoxDungLuong.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn dung lượng");
                return;
            }
            if (comboBoxDoiMay.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn đời máy");
                return;
            }
            if (comboBoxMau.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn màu máy");
                return;
            }
            if (comboBoxNuocSX.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn nước sx");
                return;
            }
            if (comboBoxHangSX.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa chọn hãng sx");
                return;
            }
            if (textBoxGiamGia.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập giảm giá");
                return;
            }
            if (IsDgvHangHoa(textBoxMaHang.Text.Trim()) == true)
            {
                MessageBox.Show("Bạn đã thêm hàng hóa này rồi");
                textBoxMaHang.Text = string.Empty;
                return;
            }
            else
            {
                string maHang = textBoxMaHang.Text.Trim();
                string tenHang = comboBoxTenHang.Text.Trim();
                string soLuong = textBoxSoLuong.Text.Trim();
                string donGiaNhap = textBoxDonGiaNhap.Text.Trim();
                string donGiaBan = textBoxDonGiaBan.Text.Trim();
                string thoigianBH = textBoxThoiGianBH.Text.Trim();
                string tenLoai = comboBoxTenLoai.Text.Trim();
                string dophanGiai = comboBoxDoPhanGiai.Text.Trim();
                string dungLuong = comboBoxDungLuong.Text.Trim();
                string doiMay = comboBoxDoiMay.Text.Trim();
                string nuocSX = comboBoxNuocSX.Text.Trim();
                string mauMay = comboBoxMau.Text.Trim();
                string hangSX = comboBoxHangSX.Text.Trim();
                string giamGia = textBoxGiamGia.Text.Trim();
                long Tien = Convert.ToInt64(soLuong) * Convert.ToInt64(donGiaNhap) * (100 - Convert.ToInt64(giamGia))/100;
                string thanhTien = Convert.ToString(Tien);
                long tongTien = Convert.ToInt64(epTongTien()) + Tien;
                labelTongTien.Text = tongTien.ToString() +" VND";
                dataGridViewDSMatHang.Rows.Add(maHang, tenHang, soLuong, donGiaNhap,donGiaBan ,thoigianBH,tenLoai,dophanGiai,dungLuong,doiMay,nuocSX,mauMay,hangSX,giamGia, thanhTien);
                string temp = "HH"+ (maHangTemp + 1 < 10 ? "0" + (maHangTemp + 1).ToString() : (maHangTemp + 1).ToString());
                if( temp == maHang)
                {
                    maHangTemp += 1;
                }
                this.lamMoi();
            }
        }

        private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxThoiGianBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBoxGiamGia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
