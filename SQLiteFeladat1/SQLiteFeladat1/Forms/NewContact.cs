using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Kribo.Class;
using Microsoft.Data.Sqlite;

namespace SQLiteFeladat1.Forms
{
    public partial class NewContact : Form
    {
        public NewContact()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int contact_id = Int32.Parse(txtID.Text);
            string FirstName = txtFirst.Text;
            string LastName = txtLast.Text;

            string query = $"INSERT INTO Contact (contact_id, FirstName, LastName) VALUES ('{contact_id}', '{FirstName}', '{LastName}')";
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
