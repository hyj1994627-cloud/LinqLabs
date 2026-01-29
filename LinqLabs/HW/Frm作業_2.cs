using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
        }
        
        private void button11_Click(object sender, EventArgs e)
        {
            this.productTableAdapter1.Fill(this.awDataSet1.Product);
            this.dataGridView1.DataSource = this.awDataSet1.Product;
            
            List<int> Years = new List<int>();
            foreach (DataRow row in this.awDataSet1.Product.Rows)
            {
                DateTime dateValue = Convert.ToDateTime(row["SellStartDate"]);
                Years.Add(dateValue.Year);
            }
            var q = Years.Distinct();
            this.comboBox3.DataSource = q.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in awDataSet1.Product
                    where p.SellStartDate >= this.dateTimePicker1.Value && p.SellStartDate <= this.dateTimePicker2.Value 
                    select new {p.Name,p.ProductNumber,p.MakeFlag,p.FinishedGoodsFlag,p.SafetyStockLevel,p.ReorderPoint,p.SellStartDate,p.ModifiedDate};
            
            this.dataGridView1.DataSource= q.ToList();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = from p in awDataSet1.Product
                    where p.SellStartDate.Year == int.Parse( comboBox3.Text)
                    select new { p.Name, p.ProductNumber, p.MakeFlag, p.FinishedGoodsFlag, p.SafetyStockLevel, p.ReorderPoint, p.SellStartDate, p.ModifiedDate };

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            if (comboBox2.SelectedIndex == 0)
            {
                var q = from p in awDataSet1.Product
                        where p.SellStartDate.Month < 4
                        select new { p.Name, p.ProductNumber, p.MakeFlag, p.FinishedGoodsFlag, p.SafetyStockLevel, p.ReorderPoint, p.SellStartDate, p.ModifiedDate };

                this.dataGridView1.DataSource = q.ToList();
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                var q = from p in awDataSet1.Product
                        where p.SellStartDate.Month < 7 && p.SellStartDate.Month >= 4
                        select new { p.Name, p.ProductNumber, p.MakeFlag, p.FinishedGoodsFlag, p.SafetyStockLevel, p.ReorderPoint, p.SellStartDate, p.ModifiedDate };

                this.dataGridView1.DataSource = q.ToList();
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                var q = from p in awDataSet1.Product
                        where p.SellStartDate.Month < 10 && p.SellStartDate.Month >= 7
                        select new { p.Name, p.ProductNumber, p.MakeFlag, p.FinishedGoodsFlag, p.SafetyStockLevel, p.ReorderPoint, p.SellStartDate, p.ModifiedDate };

                this.dataGridView1.DataSource = q.ToList();
            }
            else {
                var q = from p in awDataSet1.Product
                        where p.SellStartDate.Month >= 10
                        select new { p.Name, p.ProductNumber, p.MakeFlag, p.FinishedGoodsFlag, p.SafetyStockLevel, p.ReorderPoint, p.SellStartDate, p.ModifiedDate };

                this.dataGridView1.DataSource = q.ToList();
            }
            }
    }
}
