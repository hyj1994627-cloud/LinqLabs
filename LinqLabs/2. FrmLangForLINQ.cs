using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            Swap(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);


            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        //傳址
        void Swap(ref int n1,ref int n2)
        {
            int n3 =n1;
            n1 = n2;
            n2 = n3;
        }

        void Swap(ref string n1, ref string n2)
        {
            string n3 = n1;
            n1 = n2;
            n2 = n3; 
        }

        void Swap(ref Point  n1, ref Point  n2)
        {
           Point  n3 = n1;
            n1 = n2;
            n2 = n3;
        }

        void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T n3 = n1;
            n1 = n2;
            n2 = n3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);
            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);


            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            SwapAnyType(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.0舊式
            this.buttonX.Click += new EventHandler(ButtonX_Click);
            this.buttonX.Click += aaa;
            this.buttonX.Click += bbb;

            //2.0泛型
            this.buttonX.Click +=delegate(object s, EventArgs ev)
            {
                MessageBox.Show("匿名方法");
            };

            //3.0 Lambda
            this.buttonX.Click += (s, ev) => { MessageBox.Show("Lambda"); };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX click");
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }

        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        bool isEven(int n)
        {
            return n % 2 == 0;
        }

        bool isOdd(int n)
        {
            return n % 2 != 0;
        }
        //方法
        bool Test(int n)
        {
            return n > 5;
        }
        //委派
        delegate bool MyDelegate(int n);

        
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(10);


            //1.0舊式委派物件(具名方法)
            MyDelegate delegateObj = Test;
            result = delegateObj(1);

            delegateObj = isOdd;
            result = delegateObj(5);


            //2.0匿名方法
            delegateObj = delegate (int n)
            {
                return n % 2 == 0;
            };
            result = delegateObj(3);


            //3.0 Lambda   => expression
            delegateObj = n => n % 2 == 0;
            result = delegateObj(4);
            MessageBox.Show("result =" + result); ;
        }

        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums) {
                if (delegateObj(n)) { list.Add(n);
                }
            }
            return list;
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            foreach (int n in nums)
            {
                if (delegateObj(n)) { yield return n; }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> LargeList = MyWhere(nums, Test);
            List<int> EvenList = MyWhere(nums, isEven);

            LargeList = MyWhere(nums, n => n > 8);
            EvenList = MyWhere(nums, n => n % 2 == 0);
            List<int> OddList = MyWhere(nums, n => n % 2 == 1);
            foreach (int n in EvenList) {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in OddList)
            {
                this.listBox2.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //IEnumerable<int> q = from n in nums where n>5 select n;
            IEnumerable<int> q = nums.Where<int>(n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

            string[] strings = { "aaa", "bbbb", "ccccc", "dddddd" };
            IEnumerable<string> q1 = strings.Where(w => w.Length >= 5);
            foreach(string s in q1)
            {
                this.listBox2.Items.Add(s);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums, n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            int i = 1;
            var j = 0;

            var s = "abcde";
            s = s.ToUpper();

            MessageBox.Show(s);

            var p = new Point(i, j);
            MessageBox.Show(p.X + "," + p.Y);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Point p = new Point(100, 200);//constructor 建構子方法
            this.Font = new Font("arial", 12,FontStyle.Bold);


            Point p1 = new Point { X= 1}; //object initialize 物件初始化
            MessageBox.Show(p1.X + "," + p1.Y);

            List<Point> list = new List<Point>();
                list.Add(p); 
            list.Add(p1);
            list.Add(new Point { X= 1,Y = 1111});
            list.Add(new Point { X = 2, Y = 2222 });
            list.Add(new Point { X = 3, Y = 3333 });
            this.dataGridView1.DataSource = list;
            
           //==============================
           List<Point> list2 = new List<Point>()
           {
               new Point {X=1,Y=1 },
               new Point {X=11,Y=1 },
               new Point {X=222 },
               new Point {X=1111,Y=1 },
           };
            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var pt1 = new {P1 = 100, P2 = 200, P3 = 300}; 
            var pt2 = new { P1 = 200, P2 = 200, P3 = 300 };

            var pt3 = new { X = 111, Y = 222 };

            MessageBox.Show("pt1.P1 = "+pt1.P1);

            int w = pt1.P1;//get
            //pt1.P1 = 100; //set 無法指派為屬性或索引子 '<anonymous type: int P1, int P2, int P3>.P1' -- 其為唯讀


            this.listBox1.Items.Add(pt1.GetType());
            this.listBox1.Items.Add(pt2.GetType());
            this.listBox1.Items.Add(pt3.GetType());


            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in nums
            //                     where n > 5
            //                     select new {N= n,Square = n*n,Cube = n*n*n};

            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });
            this.dataGridView1.DataSource = q.ToList();


            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            //var q2 = from p in this.nwDataSet1.Products
            //         where p.UnitPrice > 30
            //         select new
            //         {
            //             ID = p.ProductID,
            //             Name = p.ProductName,
            //             p.UnitPrice,
            //             p.UnitsInStock,
            //             TotalPrice = p.UnitPrice * p.UnitsInStock
            //         };

            var q2 = this.nwDataSet1.Products.Where(p => p.UnitPrice > 30).Select(p => new
            {
                ID = p.ProductID,
                Name = p.ProductName,
                p.UnitPrice,
                p.UnitsInStock,
                TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
            });
            this.dataGridView1.DataSource = q2.ToList();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "abcde";
            int n = s.WordCount();


            string s1 = "123456789";
            //n= s1.WordCount();

            n = Mystring.WordCount(s1);
            MessageBox.Show("s WordCount = " + n);

            //============================
            char ch = s1.chars(2);
            MessageBox.Show("ch = " + ch);
        }
    }
}
public static class Mystring
{
    public static int WordCount(this string s)
    {
        return s.Length;
    }

    public static char chars(this string s,int index)
    {
        return s[index];
    }
}
