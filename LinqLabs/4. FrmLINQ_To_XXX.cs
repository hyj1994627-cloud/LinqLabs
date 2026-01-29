using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 101 };
            //var q = from n in nums
            //        group n by n % 2 == 0 ? "偶數" : "奇數" into g
            //        select new { Mykey = g.Key, MyCount = g.Count(), Myavg = g.Average(), MyGroup = g };

            var q = nums.GroupBy(n => n % 2 == 0 ? "偶數" : "奇數")
                .Select(g => new { Mykey = g.Key, MyCount = g.Count(), Myavg = g.Average(), MyGroup = g });
            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                TreeNode parentNode = this.treeView1.Nodes.Add(group.Mykey.ToString());

                foreach (var item in group.MyGroup)
                {
                    parentNode.Nodes.Add(item.ToString());
                }
            }
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "Mykey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "Mykey";
            this.chart1.Series[1].YValueMembers = "Myavg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.Mykey, s);

                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 101 };
            var q = from n in nums
                    group n by Mykey(n) into g
                    select new { Mykey = g.Key, MyCount = g.Count(), Myavg = g.Average(), MyGroup = g };

            

            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                TreeNode parentNode = this.treeView1.Nodes.Add(group.Mykey.ToString());

                this.dataGridView1.DataSource = q.ToList();
                foreach (var item in group.MyGroup)
                {
                    parentNode.Nodes.Add(item.ToString());
                }
            }
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "Mykey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "Mykey";
            this.chart1.Series[1].YValueMembers = "Myavg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            foreach (var group in q)
            {
                string s = $"{group.Mykey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.Mykey, s);

                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }
        string Mykey(int n)
        {
            if (n < 5)
            {
                return "小";
            }
            else if (n < 10)
            {
                return "中";
            }
            else {
                return "大";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files;
            var q = from f in files
                    group f by f.Extension into g
                    orderby g.Count()
                    select new { g.Key, Count = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            //this.dataGridView1.DataSource=nwDataSet1.Orders;
            var q = from o in nwDataSet1.Orders
                    group o by o.OrderDate.Year into g
                    select new { g.Key,Count = g.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "This is a pen . This is an apple . This is a pencil";
            char[] chars = { ',', ' ', '.', '?' };
            string[] words = s.Split(chars);
            int count = (from w in words
                         where w == "is"
                         select w).Count();
            MessageBox.Show("count = " + count);
            //=======================

            var q = from w in words
                    where string.IsNullOrWhiteSpace(w)
                    group w by w.ToLower() into g
                    select new {g.Key, Count = g.Count() };
            this.dataGridView1.DataSource= q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9,11,15,22,11,11,7};
            int[] nums2 = { 5, 10, 15, 20, 25 };

            IEnumerable<int> q = nums1.Intersect(nums2);
            q = nums1.Union(nums2);
            q =nums1.Distinct();

            bool b = nums1.Any(n => n > 10);

            int i =nums1.ElementAtOrDefault(13);

            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            var products = nwDataSet1.Products.FirstOrDefault(p => p.ProductName.Contains("C"));
            if (products != null)
            {
                MessageBox.Show(products.ProductName);
            }
            else
            {
                MessageBox.Show("Null ...");
            }
            string productName = nwDataSet1.Products
                .FirstOrDefault(p=> p.ProductName.Contains("gfhjklmnuy"))?.ProductName??"Null";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);

            var q = from c in nwDataSet1.Categories
                    join p in nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    select new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock
                    };
            this.dataGridView1.DataSource = q.ToList();

            var q2 = from c in nwDataSet1.Categories
                     join p in this.nwDataSet1.Products
                     on c.CategoryID equals p.CategoryID
                     group p by c.CategoryName into g
                     select new { categoryname = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q2.ToList();
        }
    }
}
