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
    public partial class ImportHoaDon : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public ImportHoaDon()
        {
            InitializeComponent();
        }

        private void ImportHoaDon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet3.HoaDon' table. You can move, or remove it, as needed.
            this.hoaDonTableAdapter1.Fill(this.qLY_BANHANGDataSet3.HoaDon);
            // TODO: This line of code loads data into the 'qLY_BANHANGDataSet2.HoaDon' table. You can move, or remove it, as needed.
            // this.hoaDonTableAdapter.Fill(this.qLY_BANHANGDataSet2.HoaDon);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.hoaDonTableAdapter.FillBy(this.qLY_BANHANGDataSet2.HoaDon);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddDonDatHang form = new AddDonDatHang();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string exeProcCreate_HoaDon = "create_HoaDon";
                SqlCommand cmd = new SqlCommand(exeProcCreate_HoaDon, con);
                cmd.CommandType = CommandType.StoredProcedure;

                string FlagDaThuTien; 
                if (CheckDaThuTien.Checked)
                {
                    FlagDaThuTien = "1";
                }
                else
                {
                    FlagDaThuTien = "0";
                }

                // set parameter values
                cmd.Parameters.Add("@MaDonDH", SqlDbType.Decimal, 32767).Value = txtMaDonDH.Text;
                cmd.Parameters.Add("@ShipDate", SqlDbType.Date).Value = txtShipDate.Text;
                cmd.Parameters.Add("@Thue", SqlDbType.Decimal, 32767).Value = txtThue.Text;
                cmd.Parameters.Add("@TienShip", SqlDbType.Decimal, 32767).Value = txtTienShip.Text;
                cmd.Parameters.Add("@CreditCard", SqlDbType.VarChar).Value = txtCreditCard.Text;
                cmd.Parameters.Add("@FlagThanhToan", SqlDbType.VarChar, 32767).Value = FlagDaThuTien;


                cmd.Parameters.Add("@MaHoaDon", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    // open connection and execute stored procedure
                    cmd.ExecuteNonQuery();

                    // read output value from @NewId
                    int maPhieuDat = Convert.ToInt32(cmd.Parameters["@MaHoaDon"].Value);

                    MessageBox.Show("Tạo phiếu hóa đơn thành công!\nMã Hóa đơn vừa tạo là " + maPhieuDat);
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
