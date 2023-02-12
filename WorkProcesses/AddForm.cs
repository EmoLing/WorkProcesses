using System.Collections.Concurrent;
using WorkProcesses.Model;

namespace WorkProcesses
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        public User AddUser { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser = new User
            {
                Name = boxName.Text,
                Login = boxLogin.Text
            };

            Close();
        }

    }
}
