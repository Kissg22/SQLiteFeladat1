using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kribo.Class;

namespace SQLiteFeladat1.Forms
{
    public partial class BaseContact : Form
    {
        public BaseContact()
        {
            InitializeComponent();
        }
        protected void SaveData(string query)
        {
            try
            {
                string connectionString = dBFunctions.ConnectionStringSQLite;
                dBHelper dbHelper = new dBHelper(connectionString);
                dbHelper.Load(query);
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
