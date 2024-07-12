using System;
using System.Data;
using System.Windows.Forms;
using Kribo.Class;

namespace SQLiteFeladat1.Forms
{
    public partial class EditContact : Form
    {
        private int contactID;
        public EditContact(int contactID)
        {
            InitializeComponent();
            this.contactID = contactID;
            LoadContactData();
        }

        private void LoadContactData()
        {
            try
            {
                string connectionString = dBFunctions.ConnectionStringSQLite;
                dBHelper dbHelper = new dBHelper(connectionString);
                dbHelper.Load($"SELECT * FROM Contact WHERE contact_id = {contactID}");

                if (dbHelper.DataSet != null && dbHelper.DataSet.Tables.Count > 0)
                {
                    DataTable table = dbHelper.DataSet.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        txtID.Text = row["contact_id"].ToString();
                        txtFirst.Text = row["FirstName"].ToString();
                        txtLast.Text = row["LastName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int contact_id = Int32.Parse(txtID.Text);
            string firstName = txtFirst.Text;
            string lastName = txtLast.Text;

            string query = $"UPDATE Contact SET FirstName = '{firstName}', LastName = '{lastName}' WHERE contact_id = {contact_id}";

            try
            {
                string connectionString = dBFunctions.ConnectionStringSQLite;
                dBHelper dbHelper = new dBHelper(connectionString);
                dbHelper.Load(query);

                // Notify the parent form to refresh the data grid view
                if (Owner is Form1 parentForm)
                {
                    parentForm.RefreshDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBezar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
