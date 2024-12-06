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

namespace BaiTapLon.Forms
{
    public partial class FormNhaCungCap : Form
    {
        ThaoTacVoiCSDL dataProcessor = new ThaoTacVoiCSDL();
        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        //Sinh mã nhà cung cấp
        private String sinhMaNhaCungCap()
        {
            String maNCCNew = "NCC";
            DataTable dataTable = new DataTable();
            dataTable = dataProcessor.DataReader("Select MaNCC from NHACUNGCAP ORDER BY MaNCC ASC");
            if (dataTable.Rows.Count == 0) { return "NCC01"; }
            int[] maNCC = new int[dataTable.Rows.Count + 1];
            //
            foreach (DataRow row in dataTable.Rows)
            {
                int i = 0;
                foreach (var item in row.ItemArray)
                {
                    // Cắt bỏ phần chữ "NCC" và chỉ lấy phần số
                    string soPhanTu = item.ToString().Substring(3); // Lấy từ ký tự thứ 3 trở đi
                    maNCC[i] = Convert.ToInt32(soPhanTu);
                    i++;
                }
            }

            int max = maNCC.Max();
            return maNCCNew + (max + 1 < 10 ? "0" + (max + 1).ToString() : (max + 1).ToString());
        }
        private void lamMoi()
        {
            DataTable dataTable = new DataTable();
            textBoxMaNCC.Text = sinhMaNhaCungCap();

            textBoxTenNCC.Text = string.Empty;
            textBoxDiaChi.Text = string.Empty;
            textBoxDienThoai.Text = string.Empty;

            textBoxDiaChi.Enabled = true;
            textBoxDienThoai.Enabled = true;
            textBoxTenNCC.Enabled = true;
            textBoxMaNCC.Enabled = false;
            buttonThemNCC.Enabled = true;
            buttonSuaNCC.Enabled = false;

            string sqlGetNhaCungCap = "SELECT * FROM NHACUNGCAP";
            DataTable dataTableDSNhaCungCap = dataProcessor.DataReader(sqlGetNhaCungCap);

            // Xóa các cột cũ nếu có
            dataGridViewDSNhaCungCap.Columns.Clear();
            dataGridViewDSNhaCungCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tạo và thêm cột
            dataGridViewDSNhaCungCap.Columns.Add("MaNCC", "Mã nhà cung cấp");
            dataGridViewDSNhaCungCap.Columns.Add("TenNCC", "Tên NCC");
            dataGridViewDSNhaCungCap.Columns.Add("DiaChi", "Địa chỉ");
            dataGridViewDSNhaCungCap.Columns.Add("DienThoai", "Điện thoại");

            // Thêm dữ liệu vào DataGridView
            foreach (DataRow row in dataTableDSNhaCungCap.Rows)
            {
                dataGridViewDSNhaCungCap.Rows.Add(
                    row["MaNCC"],
                    row["TenNCC"],
                    row["DiaChi"],
                    row["DienThoai"]
                );
            }
        }

        //Kiểm tra nhà cung cấp có trong datagridview 
        private bool IsDgvNCC(string maNCC)
        {
            foreach (DataGridViewRow row in dataGridViewDSNhaCungCap.Rows)
            {
                // Kiểm tra cột Mã nhà cung cấp, tránh trường hợp null
                if (row.Cells["MaNCC"].Value != null && row.Cells["MaNCC"].Value.ToString() == maNCC)
                {
                    return true; // Nhà cung cấp đã tồn tại
                }
            }
            return false; // Nhà cung cấp chưa tồn tại
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
                if (sdt[0] != '0')
                {
                    return false;
                }

            }
            return true;
        }


        //Load form
        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            this.lamMoi();

        }
        // Phương thức để tải dữ liệu từ cơ sở dữ liệu vào DataGridView
        private void buttonThemNCC_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường hợp chưa nhập thông tin
            if (textBoxTenNCC.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp!");
                textBoxTenNCC.Focus();
                return;
            }
            if (textBoxDiaChi.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                textBoxDiaChi.Focus();
                return;
            }
            if (textBoxDienThoai.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                textBoxDienThoai.Focus();
                return;
            }

            // Kiểm tra định dạng số điện thoại
            if (!IsSDT())
            {
                MessageBox.Show("Bạn nhập sai số điện thoại");
                textBoxDienThoai.Focus();
                return;
            }


            // Kiểm tra xem nhà cung cấp đã tồn tại theo số điện thoại
            string sqlNCC = "SELECT * FROM NHACUNGCAP WHERE DienThoai = '" + textBoxDienThoai.Text.Trim() + "'";
            if (dataProcessor.DataReader(sqlNCC).Rows.Count > 0)
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng bởi nhà cung cấp khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDienThoai.Focus();
                return;
            }
            // Thêm mới nhà cung cấp
            string sqlThemNCC = "INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, DienThoai) VALUES ('"
                + textBoxMaNCC.Text.Trim() + "', N'" + textBoxTenNCC.Text.Trim() + "', N'" + textBoxDiaChi.Text.Trim() + "', '"
                + textBoxDienThoai.Text.Trim() + "')";

            dataProcessor.DataChange(sqlThemNCC);
            MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới form nhập liệu
            this.lamMoi();
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.lamMoi();
        }

        //Số điện thoại chỉ được nhập số
        private void textBoxDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }
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
                    textBoxMaNCC.Text = dataTable.Rows[0][0].ToString();
                    textBoxTenNCC.Text = dataTable.Rows[0][1].ToString();
                    textBoxDiaChi.Text = dataTable.Rows[0][2].ToString();
                }
                else
                {
                    textBoxMaNCC.Text = this.sinhMaNhaCungCap();
                    textBoxTenNCC.Text = string.Empty;
                    textBoxDiaChi.Text = string.Empty;
                }
            }
        }    
        //Thoát
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                //FormTrangChu formTrangChu = new FormTrangChu();
                //formTrangChu.Show();
            }

        }
        //khi Nguoi dung chon mot nhacc
        private void dataGridViewDSNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDSNhaCungCap.Rows[e.RowIndex];

                // Gán giá trị từ các ô vào các TextBox
                textBoxMaNCC.Text = Convert.ToString(row.Cells["MaNCC"].Value);
                textBoxTenNCC.Text = Convert.ToString(row.Cells["TenNCC"].Value);
                textBoxDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
                textBoxDienThoai.Text = Convert.ToString(row.Cells["DienThoai"].Value);
                buttonThemNCC.Enabled = false;
                buttonSuaNCC.Enabled = true;
            }
        }

        private void buttonSuaNCC_Click(object sender, EventArgs e)
        {
            if (textBoxMaNCC.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải chọn NCC");
                return;
            }
            else
            {
                if (textBoxTenNCC.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập tên nhà cung cấp!");
                    textBoxTenNCC.Focus();
                    return;
                }
                if (textBoxDiaChi.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập địa chỉ");
                    textBoxDiaChi.Focus();
                    return;
                }
                if (textBoxDienThoai.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập số điện thoại");
                    textBoxDienThoai.Focus();
                    return;
                }

                // Kiểm tra định dạng số điện thoại
                if (!IsSDT())
                {
                    MessageBox.Show("Bạn nhập sai số điện thoại");
                    textBoxDienThoai.Focus();
                    return;
                }
                string sqlNCC = "SELECT * FROM NHACUNGCAP WHERE DienThoai = '" + textBoxDienThoai.Text.Trim() + "'";
                if (dataProcessor.DataReader(sqlNCC).Rows.Count > 1)
                {
                    MessageBox.Show("Số điện thoại này đã được sử dụng bởi nhà cung cấp khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxDienThoai.Focus();
                    return;
                }

                string sqlUpdate = "UPDATE NHACUNGCAP SET TenNCC = N'";
                sqlUpdate = sqlUpdate + textBoxTenNCC.Text.Trim() + "', DiaChi = N'";
                sqlUpdate = sqlUpdate + textBoxDiaChi.Text.Trim() + "', DienThoai = '";
                sqlUpdate = sqlUpdate + textBoxDienThoai.Text.Trim()+ "' where MaNCC = '";
                sqlUpdate = sqlUpdate + textBoxMaNCC.Text.Trim()+"'";

                if(MessageBox.Show("Bạn có muốn sửa thông tin NCC này không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataProcessor.DataChange(sqlUpdate);
                    this.lamMoi();
                    MessageBox.Show("Bạn đã sửa thành công");
                }

            }
        }
    }
}
