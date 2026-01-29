using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            
        }
        int currentPageIndex;
        IEnumerable<NWDataSet.OrdersRow> currentData;
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            //this.bindingSource1.DataSource = this.nwDataSet1.Orders.Skip(int.Parse(this.textBox1.Text)).Take(int.Parse(this.textBox1.Text));
            int pageSize = int.Parse(this.textBox1.Text);
            currentPageIndex += pageSize;

            var q = this.currentData.Skip(currentPageIndex).Take(pageSize);
            this.bindingSource1.DataSource = q.ToList();
            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            //System.IO.FileInfo[] files =  dir.GetFiles();

            //this.dataGridView1.DataSource = files;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =(from p in dir.GetFiles()
                                         where p.Name.Contains("log")
                                         select p).ToArray();

            this.dataGridView1.DataSource = files;

            //files[0].Extension
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = (from p in dir.GetFiles()
                                         where p.CreationTime.Year == 2019
                                         select p).ToArray();

            this.dataGridView1.DataSource = files;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = (from p in dir.GetFiles()
                                          where p.Length > 1000000
                                          select p).ToArray();

            this.dataGridView1.DataSource = files;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            currentPageIndex = 0;
            this.bindingSource1.DataSource = this.nwDataSet1.Orders.Take(int.Parse(this.textBox1.Text));
            currentData = this.nwDataSet1.Orders;
            
            List<int> orderdates = new List<int>();
            foreach (DataRow row in this.nwDataSet1.Orders.Rows)
            {
                if (!row.IsNull("OrderDate"))
                {
                    DateTime dateValue = Convert.ToDateTime(row["OrderDate"]);
                    orderdates.Add(dateValue.Year);
                }
            }

            var q = orderdates.Distinct();
            this.comboBox1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            int pageSize = int.Parse(this.textBox1.Text);

            //bindingSource1.Filter = $"OrderDate >= '#{{comboBox1.Text}}-01-01#' AND OrderDate <= '#{{comboBox1.Text}}-12-31#'";
            // 做法 A：使用 DataView 篩選
            DataView dv = new DataView(this.nwDataSet1.Orders);
            dv.RowFilter = $"OrderDate >= '#{comboBox1.Text}-01-01#' AND OrderDate <= '#{comboBox1.Text}-12-31#'";
            this.bindingSource1.DataSource = dv; // 透過 BindingSource 切換

            // 做法 B：如果是 LINQ
            var q = from d in this.nwDataSet1.Orders
                    where !d.IsOrderDateNull() && d.OrderDate.Year == int.Parse(this.comboBox1.Text)
                    select d;

            this.currentPageIndex = 0;

            var PageF = q.Skip(currentPageIndex).Take(pageSize).ToList();
            this.bindingSource1.DataSource = PageF;
            currentData = q;

            //var q = from d in this.nwDataSet1.Orders
            //        where d.OrderDate.Year ==int.Parse(this.comboBox1.Text)
            //        select d;
            //this.dataGridView1.DataSource = q.ToList();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //上一頁
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            int pageSize = int.Parse(this.textBox1.Text);
            if (currentPageIndex >= pageSize)
            {
                currentPageIndex -= pageSize;

                var q = currentData.Skip(currentPageIndex).Take(pageSize);
                this.bindingSource1.DataSource = q.ToList();
            }
        }
    }
}
