using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        List<Student> students_scores ;

        public class Student
        {
          public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						
            var q = students_scores.Count();
            // 找出 前面三個 的學員所有科目成績					
            var top3 = students_scores.Take(3);
            
            // 找出 後面兩個 的學員所有科目成績					
            var last2 = students_scores.Skip(students_scores.Count - 2).ToList();
            this.dataGridView1.DataSource = last2;
            // 找出 Name 'aaa','bbb','ccc' 的學員 國文英文 科目成績						
            var names = new[] { "aaa", "bbb", "ccc" };
            var stu = students_scores
          .Where(s => names.Contains(s.Name))
          .Select(s => new { s.Name, s.Chi, s.Eng }).ToList();

           
            // 找出學員 'bbb' 的成績	                          
            var bbb = from student in students_scores
                      where student.Name =="bbb"
                      select student;

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            var nobbb = from student in students_scores
                      where student.Name != "bbb"
                      select student;
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
           var p3cm = students_scores.Where(s=>names.Contains(s.Name)).Select (s=>new { s.Name, s.Chi,s.Math}).ToList();
            // 數學不及格 ... 是誰 
            var Mno60 = students_scores.Where(s => s.Math < 60).Select(s => new { s.Name, s.Math }).ToList();

            this.dataGridView1.DataSource = Mno60;
            #endregion

        }
  
        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            var person = students_scores.Select(s => { int[] scores = { s.Math, s.Chi, s.Eng };
                return new { s.Name, Sum = scores.Sum(), Min = scores.Min(), Max = scores.Max(), Avg = Math.Round(scores.Average(), 2) };
            }).ToList();
            //各科 sum, min, max, avg

            var subject = new List<object>
                        {
                new { 科目 = "國文", 總分 = students_scores.Sum(s => s.Chi), 最高 = students_scores.Max(s => s.Chi), 平均 = Math.Round(students_scores.Average(s => s.Chi), 2) },
                new { 科目 = "英文", 總分 = students_scores.Sum(s => s.Eng), 最高 = students_scores.Max(s => s.Eng), 平均 = Math.Round(students_scores.Average(s => s.Eng), 2) },
                new { 科目 = "數學", 總分 = students_scores.Sum(s => s.Math), 最高 = students_scores.Max(s => s.Math), 平均 = Math.Round(students_scores.Average(s => s.Math), 2) }
            };
            this.dataGridView1.DataSource = person;
        }
        private void button33_Click(object sender, EventArgs e)
        {
            var score60 = students_scores.GroupBy(s => s.Math >= 60 ? "及格" : "不及格")
                .Select(g => new
                {
                    群組 = g.Key,
                    人數 = g.Count(),
                    名單 = string.Join(", ", g.Select(st => st.Name))
                }).ToList();
            this.dataGridView1.DataSource = score60;
            // split=> 數學分成 及格和不及格的 兩群 有幾個
        }

        private void button35_Click(object sender, EventArgs e)
        {
           
        }

        

      
    }
}
