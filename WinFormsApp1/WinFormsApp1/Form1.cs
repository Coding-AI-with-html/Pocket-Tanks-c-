namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        public static Form1 instance;
        public TextBox text;
        public TextBox text2;
        public Form1()
        {
            InitializeComponent();
            instance = this;
            text = textBox1;
            text2 = textBox2;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //open new window form

            Form2 newForm2 = new Form2();
            Form2.instance.lab1.Text = textBox1.Text;
            Form2.instance.lab2.Text = textBox2.Text;
            newForm2.Show();
            this.Hide();

            


        }
    }
}
