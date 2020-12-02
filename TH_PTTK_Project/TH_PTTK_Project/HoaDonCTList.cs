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
    public partial class HoaDonCTList : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public HoaDonCTList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string geHoaDonDetail = "select MaHD_CT as N'Mã hóa đơn chi tiết', MaMH as N'Mã mặt hàng', SoLuong as N'Số lượng', ThanhTien as N'Thành tiền' from HoaDonChiTiet where MaHoaDon = " + txtMaHoaDon.Text;

                SqlCommand getHoaDonDetail = new SqlCommand(geHoaDonDetail, con);

                SqlDataReader roleListData = getHoaDonDetail.ExecuteReader();

                DataTable tableRoleList = new DataTable();

                tableRoleList.Load(roleListData);

                dataGridView1.DataSource = tableRoleList;

                this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
