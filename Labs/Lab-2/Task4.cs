using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TRSPO_Labs_WinForm_Shevchuk.Labs.Lab2
{
    public partial class Task4 : Form
    {
        private Random rand = new Random();
        private object obj = new object();
        private bool firstThread = false;
        private bool secondThread = false;
        private bool threadWait = false;
        private bool runBall = false;
        private Color currentColor;

        public Task4()
        {
            InitializeComponent();
            richTextBox1.TextChanged += (s, e) =>
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            };
            int sizeBall = 35;
            var ball = new Point();
            ball.X = 0;
            ball.Y = 0;
            var side = new Point();
            side.X = 1;
            side.Y = 1;
            panel1.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(currentColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(brush, ball.X, ball.Y, sizeBall, sizeBall);
                }
            };
            var timer1 = new Timer();
            timer1.Interval = 50;
            timer1.Tick += (s, e) =>
            {
                if (runBall == true)
                {
                    ball.X += side.X * 8;
                    ball.Y += side.Y * 8;
                    if (ball.X <= 0 || ball.X >= panel1.Width - 40)
                    {
                        side.X *= -1;
                    }
                    if (ball.Y <= 0 || ball.Y >= panel1.Height - 40)
                    {
                        side.Y *= -1;
                    }
                    panel1.Invalidate();
                }
            };
            timer1.Start();
            var timer2 = new Timer();
            timer2.Interval = 5000;
            timer2.Tick += (s, e) =>
            {
                lock (obj)
                {
                    runBall = true;
                    int currentThread = rand.Next(0, 2);
                    if (currentThread == 0)
                    {
                        firstThread = true;
                    }
                    else
                    {
                        secondThread = true;
                    }
                    if (threadWait == true)
                    {
                        threadWait = false;
                        Monitor.Pulse(obj);
                    }
                    richTextBox1.Text += $" {currentThread + 1}";
                }
            };
            timer2.Start();
            var thread1 = new Thread(() =>
            {
                while (true)
                {
                    if (firstThread == true)
                    {
                        lock (obj)
                        {
                            currentColor = Color.Red;
                            panel1.Invalidate();
                            firstThread = false;
                            threadWait = true;
                            Monitor.Wait(obj);
                        }
                    }
                }
            });
            var thread2 = new Thread(() =>
            {
                while (true)
                {
                    if (secondThread == true)
                    {
                        lock (obj)
                        {
                            currentColor = Color.Green;
                            panel1.Invalidate();
                            secondThread = false;
                            threadWait = true;
                            Monitor.Wait(obj);
                        }
                    }
                }
            });
            thread1.Start();
            thread2.Start();
        }
    }
}