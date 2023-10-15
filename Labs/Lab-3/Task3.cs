using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace TRSPO_Labs_WinForm_Shevchuk.Labs.Lab3
{
    public partial class Task3 : Form
    {
        private Thread[] threads = new Thread[3];
        private Random rand = new Random();
        private ManualResetEvent pause = new ManualResetEvent(false);

        public Task3()
        {
            InitializeComponent();
            richTextBox1.TextChanged += (s, e) =>
            {
                richTextBox1.Invoke((MethodInvoker)delegate
                {
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                });
            };

            for (int i = 0; i < threads.Length; i++)
            {
                int indx = i;
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        pause.WaitOne(5000);
                        richTextBox1.Invoke((MethodInvoker)delegate
                        {
                            richTextBox1.Text += $"\n#{threads[indx].Name.Substring(6)} -> {threads[indx].Priority}";
                        });
                        threads[indx].Priority = (ThreadPriority)rand.Next(0, 5);
                        using (var graphics = panel1.CreateGraphics())
                        {
                            using (var paint = new SolidBrush(Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256))))
                            {
                                int paintX = (panel1.Width - 125) / 2;
                                int paintY = (panel1.Height - 150) / 2;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                if (threads[indx].Name == "thread1")
                                {
                                    graphics.DrawEllipse(new Pen(Color.Black, 2), paintX - 20, paintY, 120, 69);
                                    graphics.FillEllipse(paint, paintX - 20, paintY, 120, 69);
                                }
                                else if (threads[indx].Name == "thread2")
                                {
                                    graphics.DrawEllipse(new Pen(Color.Black, 2), paintX, paintY, 150, 150);
                                    graphics.FillEllipse(paint, paintX, paintY, 150, 150);
                                }
                                else
                                {
                                    graphics.DrawRectangle(new Pen(Color.Black, 2), paintX - 10, paintY, 150, 150);
                                    graphics.FillRectangle(paint, paintX - 10, paintY, 150, 150);
                                }
                            }
                        }
                    }
                });
                threads[i].Name = $"thread{i + 1}";
                threads[i].Start();
            }

            this.FormClosing += (s, e) =>
            {
                foreach(var t in threads)
                {
                    t.Abort();
                }
            };
        }
    }
}
