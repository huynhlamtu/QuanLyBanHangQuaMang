using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TH_PTTK_Project
{
    public partial class AddDonDatHang : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public AddDonDatHang()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getMaKHList = "select MaKH from KhachHang";
                SqlCommand cmd = new SqlCommand(getMaKHList, con);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    MaKhachHang.Items.Add(DR[0]);
                }
            }
            con.Close();
        }

        private void AddDonDatHang_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet.PhieuDatHang' table. You can move, or remove it, as needed.
            this.phieuDatHangTableAdapter.Fill(this.qLY_BANHANGDataSet.PhieuDatHang);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string checkMaKH = "select MaKH from KhachHang where maKH = " + MaKhachHang.Text;
                SqlCommand cmdcheckMaKH = new SqlCommand(checkMaKH, con);
                SqlDataReader DR = cmdcheckMaKH.ExecuteReader();

                if (!DR.Read())
                {
                    MessageBox.Show("Mã khách hàng không tồn tại!");
                    con.Close();
                }
                else
                {
                    con.Close();
                    con.Open();
                    string exeProcAdd_PhieuDatHang = "Add_PhieuDatHang";
                    SqlCommand cmd = new SqlCommand(exeProcAdd_PhieuDatHang, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // set parameter values
                    cmd.Parameters.Add("@MaKH", SqlDbType.Decimal).Value = MaKhachHang.SelectedItem;
                    cmd.Parameters.Add("@NguoiNhan", SqlDbType.NVarChar, 32767).Value = TenNguoiNhan.Text;
                    cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 32767).Value = SoDienThoai.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 32767).Value = DiaChi.Text;

                    cmd.Parameters.Add("@MaPhieu", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        // open connection and execute stored procedure
                        cmd.ExecuteNonQuery();

                        // read output value from @NewId
                        int maPhieuDat = Convert.ToInt32(cmd.Parameters["@MaPhieu"].Value);

                        MessageBox.Show("Tạo phiếu đặt hàng thành công!\nMã phiếu đặt vừa tạo là " + maPhieuDat);

                        this.phieuDatHangTableAdapter.Fill(this.qLY_BANHANGDataSet.PhieuDatHang);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        // MessageBox.Show("Đã xảy ra lỗi khi tạo phiếu Đặt hàng!");
                    }
                }
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MaKhachHang.Text.Length == 0)
            {
                MessageBox.Show("Mã khách không được để trống!");
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string findTenKH = "select HoTen from KhachHang where MaKH = " + MaKhachHang.Text;
                    SqlCommand cmd = new SqlCommand(findTenKH, con);
                    SqlDataReader DR = cmd.ExecuteReader();

                    if (DR.Read())
                    {
                        TenKhachHang.Text = DR[0].ToString();
                    }
                    else
                        MessageBox.Show("Mã khách hàng không tồn tại!");
                }
                con.Close();
            }
        }
    }
}
