namespace MajesticPremForm
{
    using System;
    using System.Text.RegularExpressions;
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
            string p = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + @"\client_secret.json";
            if (File.Exists(p)) 
            {
                File.Delete(p);
            }

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private static int Sum = 0;
        private void ellipseButton1_Click(object sender, EventArgs e)
        {
            GTabs gt = new GTabs("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", comboBox1.Text);
            int Sum = 0;
            foreach (string s in gt.Load())
            {
                string[] data = { s.Split(";")[0], Regex.Replace(s.Split(";")[1].Replace("$", string.Empty), @"\s", "") };
                if (int.Parse(data[1]) == 0 || int.Parse(data[1]) == null) { continue; }
                listView1.Items.Add(new ListViewItem(data));
                Sum += int.Parse(data[1]);
            }
            label5.Text = String.Concat("Общая сумма: ", Sum.ToString(), "$");
        }

        private void ellipseButton2_Click(object sender, EventArgs e)
        {
            label4.Text = "Статус: Сохранение...";
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Results")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Results"));
            }

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
