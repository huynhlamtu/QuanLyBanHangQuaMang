using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH_PTTK_Project
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddDonDatHang form = new AddDonDatHang();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPhieuDHChiTiet form = new AddPhieuDHChiTiet();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportHoaDon form = new ImportHoaDon();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HoaDonCTList form = new HoaDonCTList();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ImportPhieuGiaoHang form = new ImportPhieuGiaoHang();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PhieuGiaoHangDetail form = new PhieuGiaoHangDetail();
            form.Show();
        }
    }
}
