using congestion.calculator.Enum;
using congestion.calculator.Model;

namespace Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Please select dates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var calculator = new CongestionTaxCalculator(openFileDialog1.FileName);

                var dates = new List<DateTime>();
                foreach (var item in listBox1.Items)
                {
                    dates.Add(DateTime.Parse(item.ToString()));
                }
                var fee = calculator.GetTax((VehicleType)comboBox1.SelectedIndex, dates.ToArray());
                MessageBox.Show("Total fee: " + fee.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(dateTimePicker1.Value.ToString());

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            comboBox1.SelectedIndex = 0;
        }
    }
}