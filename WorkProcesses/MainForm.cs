using System.Collections.Concurrent;
using WorkProcesses.Model;

namespace WorkProcesses
{
    public partial class MainForm : Form
    {
        private readonly ConcurrentBag<User> _users;
        private readonly CancellationToken cancellationToken;

        public MainForm()
        {
            InitializeComponent();
            _users = new ConcurrentBag<User>();
            listBox1.DataSource = _users.ToList();
            listBox1.DisplayMember = "Name"; ;
            listBox1.ValueMember = "Login";

            Task.Run(() => UpdateList(), cancellationToken);
        }

        protected override void OnClosed(EventArgs e)
        {
            cancellationToken.ThrowIfCancellationRequested();
            base.OnClosed(e);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var addForm = new AddForm();

                addForm.ShowDialog();
                _users.Add(addForm.AddUser);

                return;
            }, cancellationToken);
        }

        private void UpdateList()
        {
            while (true)
            {
                if (listBox1.Items.Count == _users.Count)
                    continue;

                listBox1.Invoke(() => listBox1.DataSource = _users.ToList());
            }
        }
    }
}