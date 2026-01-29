using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();

            Console.WriteLine("xxx");
            this.dbContext.Database.Log = Console.WriteLine;
        }
NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            
            var q  =from p in dbContext.Products
                    where p.UnitPrice >30
                    select p;
           this.dataGridView1.DataSource= q.ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    orderby p.UnitsInStock descending,p.ProductID
                    select p;
            this.dataGridView1.DataSource = q.ToList();

            var q2 = this.dbContext.Products.OrderByDescending(p=>p.UnitsInStock).ThenBy(p =>p.ProductID);
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
                    };
            this.dataGridView1.DataSource =q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //
            bool? result;
            result = null;
            if (result.HasValue) { }

            var q = from o in this.dbContext.Orders
                    select new { o.OrderID, o.OrderDate.Value.Year, o.OrderDate.Value.Month };
            this.dataGridView1.DataSource=q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();

            //MessageBox.Show(dbContext.Products.First().Category.CategoryName);
            MessageBox.Show(dbContext.Orders.First().Employee.FirstName);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var q = from c in dbContext.Categories
                    join p in dbContext.Products
                    on c.CategoryID equals p.CategoryID
                    select new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock
                    };
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new { p.CategoryID, p.Category.CategoryName, p.ProductID, p.ProductName, p.UnitPrice, p.UnitsInStock };
            this.dataGridView2.DataSource = q.ToList();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    group p by p.Category.CategoryName into g
                    select new { CategoryName = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };
            this.dataGridView1.DataSource = q.ToList();
                
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g
                    orderby g.Key
                    select new { g.Key, Count = g.Count() };
            this.dataGridView1.DataSource = q.ToList();

            //===========================
            var q2 = from o in this.dbContext.Orders
                    group o by new {o.OrderDate.Value.Year,o.OrderDate.Value.Month} into g
                    orderby g.Key.Year , g.Key.Month
                    select new { g.Key, Count = g.Count() };
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Product product = new Product { ProductName ="Test", Discontinued = true };
            this.dbContext.Products.Add(product);
            this.dbContext.SaveChanges();
        }
        private void RefreshDG()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = this.dbContext.Products.ToList();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            var product = this.dbContext.Products.FirstOrDefault(p => p.ProductName.Contains("Test"));
            if (product == null) return;
                    product.ProductName += "Test";
            this.dbContext.SaveChanges();
            RefreshDG();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            RefreshDG();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            var product = this.dbContext.Products.FirstOrDefault(p => p.ProductName.Contains("Test"));
            if (product == null) return;
            this.dbContext.Products.Remove(product);
            this.dbContext.SaveChanges();
            RefreshDG();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            var products = this.dbContext.Products.Where(p => p.ProductName.Contains("Test"));
            if (products == null) return;
            this.dbContext.Products.RemoveRange(products);
            this.dbContext.SaveChanges();
            RefreshDG();
        }
    }
}
