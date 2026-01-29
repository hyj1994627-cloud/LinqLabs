using LinqLabs;
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
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            this.listBox1.Items.Add("==================");

            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }

            int w = 100;
            //因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義，所以 foreach 陳述式無法在型別 'int' 的變數上運作
            //foreach (int n in w) {;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }
            //============================

            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //IEnumerable<int> q = from n in nums
            //                         where n % 2 == 0
            //                         select n;

            //foreach (int n in q)
            //{
            //    this.listBox1.Items.Add(n);
            //}


            IEnumerable<int> qn = from n in nums
                                  where n < 5 || n > 8
                                  select n;
            foreach (int n in qn)
            {
                this.listBox1.Items.Add(n);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "apple", "banana", "orange", "avocado", "grape", "kiwi" ,"pinapple","Applewatch" };
            IEnumerable<string> q = from w in words
                                        //where w.Contains("apple") && w.Length > 5
                                    where w.ToLower().Contains("apple") && w.Length > 5
                                    orderby w descending
                                    select w;
            foreach (string w in q)
            {
                this.listBox1.Items.Add(w);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n = 100;
            bool result = n > 20 && A();
        }
        bool A()
        {
            MessageBox.Show("A");
            return true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            
        }bool IsEven(int n)
            {
                return n % 2 == 0;
            }

        private void button8_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<Point> q = from n in nums
                                 where IsEven(n)
                                 select new Point(n,n*n);
            foreach (Point n in q)
            {
                this.listBox1.Items.Add(n.X +","+ n.Y);
            }
            List<Point> list = q.ToList();
            this.dataGridView1.DataSource = list;

            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "x";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series[0].Color = Color.Aqua;
            this.chart1.Series[0].BorderWidth = 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            IEnumerable<NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products
                                                   where p.UnitPrice > 30 && p.ProductName.StartsWith("P")
                                                   select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            IEnumerable<NWDataSet.OrdersRow> q = from r in this.nwDataSet1.Orders
                                                 where !r.IsShipRegionNull() && r.OrderDate.Year == 1997 
                                                 select r;
            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
