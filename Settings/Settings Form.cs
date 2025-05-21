namespace Settings
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        void button1_Click(object sender, EventArgs e)
        {
            SQL.Default.addressSetting = ServerAddressBox.Text;
            SQL.Default.portSetting = PortBox.Text;
            SQL.Default.Save();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}