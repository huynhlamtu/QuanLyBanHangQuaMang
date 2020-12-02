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
    public partial class ImportPhieuGiaoHang : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public ImportPhieuGiaoHang()
        {
            InitializeComponent();
        }

        private void ImportPhieuGiaoHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet5.PhieuGiaoHang' table. You can move, or remove it, as needed.
            this.phieuGiaoHangTableAdapter1.Fill(this.qLY_BANHANGDataSet5.PhieuGiaoHang);
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet4.PhieuGiaoHang' table. You can move, or remove it, as needed.
            //this.phieuGiaoHangTableAdapter.Fill(this.qLY_BANHANGDataSet4.PhieuGiaoHang);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportHoaDon form = new ImportHoaDon();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (txtMaNV.Text.Length != 0)
                {
                    string getTenNhanVien = "select HoTen from NhanVien where MaNV = " + txtMaNV.Text;

                    SqlCommand geTenNhanViencmd = new SqlCommand(getTenNhanVien, con);

                    try
                    {
                        SqlDataReader tenNhanVienData = geTenNhanViencmd.ExecuteReader();

                        DataTable tenNhanVienTable = new DataTable();

                        tenNhanVienTable.Load(tenNhanVienData);

                        txtTenNV.Text = tenNhanVienTable.Rows[0]["HoTen"].ToString();
                    }
                    catch (Exception ex)
                    {
                        txtTenNV.Text = "Không có nhân viên này";
                        //MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    txtTenNV.Text = "Mã nhân viên trống!";
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
                string exeProccreate_DonGiaoHang = "create_DonGiaoHang";
                SqlCommand cmd = new SqlCommand(exeProccreate_DonGiaoHang, con);
                cmd.CommandType = CommandType.StoredProcedure;    

                // set parameter values
                cmd.Parameters.Add("@MaHoaDon", SqlDbType.Decimal, 32767).Value = txtMaHoaDon.Text;
                cmd.Parameters.Add("@MaNV", SqlDbType.Decimal, 32767).Value = txtMaNV.Text;

                cmd.Parameters.Add("@MaDonGiao", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    // open connection and execute stored procedure
                    cmd.ExecuteNonQuery();

                    // read output value from @NewId
                    int maPhieuDat = Convert.ToInt32(cmd.Parameters["@MaDonGiao"].Value);

                    MessageBox.Show("Tạo phiếu giao hàng thành công!\nMã đơn giao hàng vừa tạo là " + maPhieuDat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    // MessageBox.Show("Đã xảy ra lỗi khi tạo phiếu Đặt hàng!");
                }
            }
        }
    }
}
