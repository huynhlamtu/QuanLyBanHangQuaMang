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
    public partial class PhieuGiaoHangDetail : Form
    {
        public string conString = "Data Source=DESKTOP-II7U9L8;Initial Catalog=QLY_BANHANG;Integrated Security=True";
        public PhieuGiaoHangDetail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string getDonGiaoCTDetail = "select MaPhieuGiao_CT as N'Mã phiếu giao chi tiết', MaMH as N'Mã mặt hàng', SoLuong as N'Số lượng', TinhTrang as N'Tình trạng' from PhieuGiaoHang_CT where MaPhieuGiao = " + txtMaDonGiao.Text;

                SqlCommand getDonGiaoCTDetailcmd = new SqlCommand(getDonGiaoCTDetail, con);

                SqlDataReader DonGiaoCTtData = getDonGiaoCTDetailcmd.ExecuteReader();

                DataTable tableDonGiaoCT = new DataTable();

                tableDonGiaoCT.Load(DonGiaoCTtData);

                dataGridView1.DataSource = tableDonGiaoCT;

                this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            con.Close();
        }
    }
}
