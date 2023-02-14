namespace MajesticPremForm
{
    using System;
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
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private static int Sum = 0;
        private void ellipseButton1_Click(object sender, EventArgs e)
        {
            string sum = textBox2.Text.Contains("$") ? textBox2.Text.Replace("$", string.Empty) : textBox2.Text;

            string[] data = { textBox1.Text, sum };
            Sum += int.Parse(sum); 
            listView1.Items.Add(new ListViewItem(data));
            label5.Text = String.Concat("Общая сумма: ", Sum.ToString(), "$");
        }

        private void ellipseButton2_Click(object sender, EventArgs e)
        {
            label4.Text = "Статус: Сохранение...";
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Results")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Results"));
            }
            
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Results", String.Concat(DateTime.Now.ToString().Replace(':', ';'), ".txt"));
                File.AppendAllText(path, "staticId;amount;comment\n");

                foreach (ListViewItem item in listView1.Items)
                {
                    
                    string text = $"{item.SubItems[0].Text};{item.SubItems[1].Text};Премия\n";
                    File.AppendAllText(path, text);
                }
                label4.Text = "Сохранено!";
            }
        }
    }
}
