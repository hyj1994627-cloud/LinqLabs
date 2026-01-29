using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList arrlist = new System.Collections.ArrayList();
            arrlist.Add(484);
            arrlist.Add(87);

            //IEnumerable<int> q = from n in arrlist.Cast<int>() //DataSet ds = new DataSet();
            //        where n>2
            //        select n;


            var q = from n in arrlist.Cast<int>() //DataSet ds = new DataSet();
                    where n > 2
                    select  new { n };

            foreach (var n in q)
            {
                this.listBox1.Items.Add(n);
            }
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(nwDataSet1.Products);

            var q = (from p in this.nwDataSet1.Products
                     orderby p.UnitsInStock descending
                     select p).Take(5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7 ,8,9,10,101};
            this.listBox1.Items.Add("Max = " + nums.Max());
            this.listBox1.Items.Add("Min = " + nums.Min());
            this.listBox1.Items.Add("Avg = " + nums.Average());
            this.listBox1.Items.Add("Sum = " + nums.Sum());
            this.listBox1.Items.Add("Count = " + nums.Count());

            //===================================

            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add("Max UnitsInstock = " + this.nwDataSet1.Products.Max(p => p.UnitsInStock));
            this.listBox1.Items.Add("Min UnitsInstock = " + this.nwDataSet1.Products.Min(p => p.UnitsInStock));
            this.listBox1.Items.Add("Avg UnitsInstock = " + this.nwDataSet1.Products.Average(p => p.UnitsInStock));
            this.listBox1.Items.Add("Sum UnitsInstock = " + this.nwDataSet1.Products.Sum(p => p.UnitsInStock));
            this.listBox1.Items.Add("Count = " + this.nwDataSet1.Products.Count());
        }
    }
}