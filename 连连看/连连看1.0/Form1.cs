using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 连连看1._0
{
    public partial class Form1 : Form
    {
        private int n, m;
        private int count = 0,seconds,score;
        private int shffles; 
        private int x1, y1, x2, y2; 
        private int[,] judgeKey;
        private aButton[,] buttons;
        public Form1()
        {
            InitializeComponent();
        }
        private void setGame()
        {
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = n;
            for (int i = 0; i < n;i++ )
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320/n));
            }
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = m;
            for (int i = 0; i < m;i++ )
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320/m));
            }
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 320);
            this.tableLayoutPanel1.TabIndex = 1;

            count = 0;
            score = 0;

            this.buttons = new aButton[n,m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m;j++ )
                {
                    buttons[i, j] = new aButton(i,j);
                    this.buttons[i,j].Dock = System.Windows.Forms.DockStyle.Fill;
                    this.buttons[i,j].Location = new System.Drawing.Point(0, 0);
                    this.buttons[i,j].Margin = new System.Windows.Forms.Padding(0);
                    this.buttons[i,j].Name = "buttons"; 
                    this.buttons[i,j].Size = new System.Drawing.Size(320/n, 320/m);
                    this.buttons[i,j].TabIndex = 0;
                    this.buttons[i,j].UseVisualStyleBackColor = true;
                    this.buttons[i,j].Click += new System.EventHandler(this.buttons_Click);
                    this.tableLayoutPanel1.Controls.Add(this.buttons[i,j], j, i);
                }
            setKey();

            button2.Enabled = true;
            label3.Text = "shuffles : " + shffles.ToString();
        }
        private void setKey()
        {
            int temp;
            int [,] key = new int [n,m];
            for (int i = 0; i < n*m/4; i++)
                for (int j = 0; j < 4; j++)
                    key[(4 * i + j) / n, (4 * i + j) % n] = i + 1;
            Random rand = new Random();
            for (int l = 0; l < n * m; l++)
            {
                int k = rand.Next(n * m);
                temp = key[l / n, l % n];
                key[l / n, l % n] = key[k / n, k % n];
                key[k / n, k % n] = temp;
            }
            judgeKey = new int[n + 2, m + 2];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    judgeKey[i+1, j+1] = key[i, j];
            for (int i = 0; i < m + 2; i++)
            {
                judgeKey[0, i] = 0;
                judgeKey[n + 1, i] = 0;
            }
            for (int i = 0; i < n + 2; i++)
            {
                judgeKey[i, 0] = 0;
                judgeKey[i, m + 1] = 0;
            }
            if (n == 8 && m == 8)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        this.buttons[i, j].BackgroundImage = imageList2.Images[judgeKey[i + 1, j + 1] - 1];
                    }
            }
            else
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        this.buttons[i, j].BackgroundImage = imageList1.Images[judgeKey[i + 1, j + 1] - 1];
                    }
            }
        }
        private void resetKey()
        {
            int temp,k;
            Random rand = new Random();
            for (int l = 0; l < n * m ; l++)
            {
                if (judgeKey[(l / n) + 1, (l % n) + 1] != 0)
                {
                    do
                    {
                        k = rand.Next(n * m);
                    } while (judgeKey[(k / n) + 1, (k % n) + 1] == 0);
                    temp = judgeKey[(l / n) + 1, (l % n) + 1];
                    judgeKey[(l / n) + 1, (l % n) + 1] = judgeKey[(k / n) + 1, (k % n) + 1];
                    judgeKey[(k / n) + 1, (k % n) + 1] = temp;
                }
                
            }
            if (n == 8 && m == 8)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        if (judgeKey[i + 1, j + 1]!=0)
                            this.buttons[i, j].BackgroundImage = imageList2.Images[judgeKey[i + 1, j + 1] - 1];
                    }
            }
            else
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        if (judgeKey[i + 1, j + 1] != 0)
                            this.buttons[i, j].BackgroundImage = imageList1.Images[judgeKey[i + 1, j + 1] - 1];
                    }
            }
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            aButton b = (aButton)sender;
            count++;
			if(count%2==1)
			{
                x1 = b.x;
                y1 = b.y;
                buttons[x1, y1].Enabled = false;
			}
			else
			{
                x2 = b.x;
                y2 = b.y;
                judge j = new judge(x1+1, y1+1, x2+1, y2+1, n + 2, m + 2, judgeKey);
				if(j.Judge())
				{
					buttons[x1,y1].Visible = false;
					buttons[x2,y2].Enabled = false;
					buttons[x2,y2].Visible = false;
                    judgeKey[x1 + 1, y1 + 1] = 0;
                    judgeKey[x2 + 1, y2 + 1] = 0;
                    score += 20;
				}
				else
				{
					buttons[x1,y1].Enabled = true;
                    count -= 2;
                    score -= 5;
				}
			}
        }
        private void button1_Click(object sender, EventArgs e)
        {
            n = 8;
            m = 8;
            shffles = 1;
            button2.Enabled = true;
            label3.Text = "shuffles : " + shffles.ToString();
            setGame();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            n = 10;
            m = 10;
            shffles = 2;
            setGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            shffles--;
            if(shffles ==0)
            {
                button2.Enabled = false;
            }
            label3.Text = "shuffles : " + shffles.ToString();
            resetKey(); 
            score -= 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count == 0)
                seconds = 0;
            else if (count < n * m)
            {
                seconds++;
                score -= 1;
            }
            else
                button2.Enabled = false;
            label1.Text = seconds.ToString();
            label2.Text = "score : " + score.ToString();
        }

    }
}
