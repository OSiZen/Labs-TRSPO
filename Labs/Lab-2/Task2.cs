using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TRSPO_Labs_WinForm_Shevchuk.Labs.Lab2
{
    public partial class Task2 : Form
    {
        private Random rand = new Random();
        private object obj = new object();
        private bool firstThread = false;
        private bool secondThread = false;
        private bool threadWait = false;
        private Color currentColor;

        public Task2()
        {
            InitializeComponent();
            richTextBox1.TextChanged += (s, e) =>
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            };
            panel1.Paint += (s, e) =>
            {
                using (var paint = new SolidBrush(currentColor))
                {
                    e.Graphics.FillRectangle(paint, 0, 0, panel1.Size.Width, panel1.Size.Height);
                }
            };
            var timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += (s, e) =>
            {
                lock (obj)
                {
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
            timer.Start();
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