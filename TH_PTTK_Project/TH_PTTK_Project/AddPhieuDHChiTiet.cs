using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TH_PTTK_Project
{
    public partial class AddPhieuDHChiTiet : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public AddPhieuDHChiTiet()
        {
            InitializeComponent();
        }

        private void AddPhieuDHChiTiet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet1.PhieuDatHang_CT' table. You can move, or remove it, as needed.
            this.phieuDatHang_CTTableAdapter.Fill(this.qLY_BANHANGDataSet1.PhieuDatHang_CT);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMaDonDH.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa điền mã đơn đặt hàng!");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string getMaDonDH = "select * from PhieuDatHang where MaDonDH = " + txtMaDonDH.Text;

                    SqlCommand getMaDonDHcmd = new SqlCommand(getMaDonDH, con);

                    SqlDataReader maDonDHData = getMaDonDHcmd.ExecuteReader();

                    DataTable tableMaDonDH = new DataTable();

                    tableMaDonDH.Load(maDonDHData);

                    con.Close();

                    if (tableMaDonDH.Rows.Count == 0)
                    {
                        MessageBox.Show("Đơn đặt hàng không tồn tại!");
                    }
                    else
                    {
                        con.Open();

                        string getDH_CTDetail = "select MaDon_CT as N'Mã đơn chi tiết', MH.MaMH as N'Mã mặt hàng', MH.TenMH as N'Tên mặt hàng', PCT.Soluong as N'Số lượng đặt mua' from PhieuDatHang_CT PCT, MatHang MH where PCT.MaMH = MH.MaMH and PCT.MaDonDH = " + txtMaDonDH.Text;

                        SqlCommand geDonDHDetail = new SqlCommand(getDH_CTDetail, con);

                        SqlDataReader roleListData = geDonDHDetail.ExecuteReader();

                        DataTable tableRoleList = new DataTable();

                        tableRoleList.Load(roleListData);

                        dataGridView1.DataSource = tableRoleList;

                        this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (tableRoleList.Rows.Count == 0)
                        {
                            MessageBox.Show("Đơn đặt hàng này trống");
                        }

                        string getDH_Owner = "select KH.MaKH, KH.HoTen, PDH.SDT, PDH.NguoiNhan, PDH.DiaChi from PhieuDatHang PDH, KhachHang KH where KH.MaKH = PDH.MaKH and PDH.MaDonDH = " + txtMaDonDH.Text;

                        SqlCommand geDonDHOwner = new SqlCommand(getDH_Owner, con);

                        SqlDataReader donDHOwnerData = geDonDHOwner.ExecuteReader();

                        DataTable donDHOwnerTable = new DataTable();

                        donDHOwnerTable.Load(donDHOwnerData);

                        MaKhachHang.Text = donDHOwnerTable.Rows[0]["MaKH"].ToString();
                        TenKhachHang.Text = donDHOwnerTable.Rows[0]["HoTen"].ToString();
                        SoDienThoai.Text = donDHOwnerTable.Rows[0]["SDT"].ToString();
                        TenNguoiNhan.Text = donDHOwnerTable.Rows[0]["NguoiNhan"].ToString();
                        DiaChi.Text = donDHOwnerTable.Rows[0]["DiaChi"].ToString();

                        con.Close();
                    }
                } 
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi!");
                }
            }     
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (MaMatHang.Text.Length != 0)
                {
                    string getTenMatHang = "select TenMH from MatHang where MaMH = " + MaMatHang.Text;

                    SqlCommand geTenMatHangcmd = new SqlCommand(getTenMatHang, con);

                    try
                    {
                        SqlDataReader tenMatHangData = geTenMatHangcmd.ExecuteReader();

                        DataTable tenMatHanTable = new DataTable();

                        tenMatHanTable.Load(tenMatHangData);

                        TenMatHang.Text = tenMatHanTable.Rows[0]["TenMH"].ToString();
                    }
                    catch (Exception ex)
                    {
                        TenMatHang.Text = "Không có mặt hàng này";
                    }
                }
                else
                {
                    TenMatHang.Text = "Mã mặt hàng trống!";
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getTenMatHang = "select SoLuong from MatHang where MaMH = " + MaMatHang.Text;

                SqlCommand geTenMatHangcmd = new SqlCommand(getTenMatHang, con);

                try
                {
                    SqlDataReader tenMatHangData = geTenMatHangcmd.ExecuteReader();

                    DataTable tenMatHanTable = new DataTable();

                    tenMatHanTable.Load(tenMatHangData);

                    int SoLuong = Int32.Parse(tenMatHanTable.Rows[0]["SoLuong"].ToString());

                    if (SoLuong < Int32.Parse(txtSoLuong.Text))
                    {
                        MessageBox.Show("Trong kho chỉ có sẵn " + SoLuong + " mặt hàng này");
                    }
                    else
                    {
                        string exeProcAdd_PhieuDatHang_CT = "Add_PhieuDatHang_CT";
                        SqlCommand cmd = new SqlCommand(exeProcAdd_PhieuDatHang_CT, con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // set parameter values
                        cmd.Parameters.Add("@MaPhieu", SqlDbType.Decimal, 32767).Value = txtMaDonDH.Text;
                        cmd.Parameters.Add("@MaMH", SqlDbType.Decimal, 32767).Value = MaMatHang.Text;
                        cmd.Parameters.Add("@SoLuong", SqlDbType.Decimal, 32767).Value = txtSoLuong.Text;

                        try
                        {
                            // open connection and execute stored procedure
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Tạo phiếu đặt hàng chi tiết thành công!");

                            string getDH_CTDetail = "select MaDon_CT as N'Mã đơn chi tiết', MH.MaMH as N'Mã mặt hàng', MH.TenMH as N'Tên mặt hàng', PCT.Soluong as N'Số lượng đặt mua' from PhieuDatHang_CT PCT, MatHang MH where PCT.MaMH = MH.MaMH and PCT.MaDonDH = " + txtMaDonDH.Text;

                            SqlCommand geDonDHDetail = new SqlCommand(getDH_CTDetail, con);

                            SqlDataReader roleListData = geDonDHDetail.ExecuteReader();

                            DataTable tableRoleList = new DataTable();

                            tableRoleList.Load(roleListData);

                            dataGridView1.DataSource = tableRoleList;

                            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            // MessageBox.Show("Đã xảy ra lỗi khi tạo phiếu Đặt hàng!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }
            con.Close();
        }

        private void MaMatHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void TenMatHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
