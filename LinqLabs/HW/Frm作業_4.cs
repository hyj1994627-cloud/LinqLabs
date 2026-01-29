using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    group f by MyLength(f.Length) into f
                    orderby f.Count() descending
                    select new { 大小 = f.Key, Count = f.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }
        string MyLength(long f)
        {
            if (f < 16) { return "小"; }
            else if (f < 1024) { return "中"; }
            else { return "大"; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    group f by f.CreationTime.Year into f
                    orderby f.Key descending
                    select new { 年 = f.Key, Count = f.Count() };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var q = from n in nums
                    group n by MySize(n) into g
                    select g;

            this.dataGridView1.DataSource = q.Select(g => new {
                Size = g.Key,
                Count = g.Count()
            }).ToList();

            this.treeView1.Nodes.Clear();

            foreach (var group in q)
            {
                TreeNode parentNode = this.treeView1.Nodes.Add(group.Key);

                foreach (int number in group)
                {
                    parentNode.Nodes.Add(number.ToString());
                }
            }
        }
        string MySize(int n)
        {
            if (n <= 10) { return "小"; }
            else if (n <= 15) { return "中"; }
            else { return "大"; }
        }
        NorthwindEntities NWD = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            var q = from od in this.NWD.Order_Details.ToList()
                    group od by new { od.Order.Employee.EmployeeID, od.Order.Employee.LastName } into g
                    select new
                    {
                        Emp = g.Key,
                        Sales = g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))
                    };
            this.dataGridView1.DataSource = q.OrderByDescending(od => od.Sales).ToList();
            this.dataGridView2.DataSource = q.OrderByDescending(od=>od.Sales).Take(5).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //總銷售
            var totalSales = this.NWD.Order_Details.ToList()
                         .Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount));

            this.dataGridView1.DataSource = new List<object> { new  { TotalSales = totalSales } };


        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var q = this.NWD.Order_Details.ToList()
            //    .OrderByDescending(o => o.UnitPrice).Select(o => new {o.Product.ProductName,o.Product.Category.CategoryName,o.UnitPrice}).Distinct().Take(5).ToList();

            var q = this.NWD.Products.ToList()
                .OrderByDescending(p=>p.UnitPrice).Select(p=> new {p.ProductName,p.Category.CategoryName,p.UnitPrice}).Distinct().Take(5).ToList();
            this.dataGridView1.DataSource = q;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = this.NWD.Products.ToList()
                .Where(p => p.UnitPrice > 300).Select(p => p).ToList();
            if (q.Count == 0) { MessageBox.Show("No Product Price >300"); }
            else { }
            this.dataGridView1.DataSource = q;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Group低中高
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q = this.NWD.Orders.ToList()
                .GroupBy(o=>o.OrderDate.Value.Year).Select(o=> new {Year = o.Key,Count = o.Count()}).ToList();
            this.dataGridView1 .DataSource = q;
            foreach (var group in q) { 
            TreeNode
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
