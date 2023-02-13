namespace MajesticPremForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ellipseButton1_Click(object sender, EventArgs e)
        {
            string[] data = { textBox1.Text, textBox2.Text };
            listView1.Items.Add(new ListViewItem(data));
        }

        private void ellipseButton2_Click(object sender, EventArgs e)
        {
            label4.Text = "Статус: Сохранение...";
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Results")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Results"));
            }
            
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    string text = $"{item.SubItems[0].Text};{item.SubItems[1].Text};Премия\n";
                    File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "Results", String.Concat(/*DateTime.Now.Day, ".", DateTime.Now.Month*/ DateTime.Now.ToString().Replace(':', ';'), ".txt")), text);
                }
                label4.Text = "Сохранено!";
            }
        }
    }
}
