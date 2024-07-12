using Kribo.Class;
using SQLiteFeladat1.Forms;
using System;
using System.Data;
using System.Windows.Forms;

namespace SQLiteFeladat1
{
    public partial class Form1 : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;

        public Form1()
        {
            InitializeComponent();
            InitializeContextMenuStrip();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = dBFunctions.ConnectionStringSQLite;
                dBHelper dbHelper = new dBHelper(connectionString);

                // Lekérdezés futtatása a partnerek listájának lekérésére
                dbHelper.Load("SELECT * FROM Contact");

                // Ellenőrizzük, hogy a DataSet nem üres
                if (dbHelper.DataSet != null && dbHelper.DataSet.Tables.Count > 0)
                {
                    DataTable table = dbHelper.DataSet.Tables[0];

                    // DataGridView adatforrásának beállítása
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("No data found in the DataSet.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }

        private void InitializeContextMenuStrip()
        {
            contextMenuStrip1 = new ContextMenuStrip();

            
            editToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();


            newToolStripMenuItem.Text = "New Contact";
            newToolStripMenuItem.Click += new EventHandler(newToolStripMenuItem_Click);

            editToolStripMenuItem.Text = "Edit Contact";
            editToolStripMenuItem.Click += new EventHandler(editToolStripMenuItem_Click);

            deleteToolStripMenuItem.Text = "Delete Contact";
            deleteToolStripMenuItem.Click += new EventHandler(deleteToolStripMenuItem_Click);

            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, editToolStripMenuItem, deleteToolStripMenuItem });

            dataGridView1.ContextMenuStrip = contextMenuStrip1;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewContactForm();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedContact();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedContact();
        }

        private void EditSelectedContact()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int contactID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["contact_id"].Value);

                    EditContact editContactForm = new EditContact(contactID);
                    editContactForm.Owner = this;
                    editContactForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing contact: " + ex.Message);
            }
        }

        private void DeleteSelectedContact()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this contact?", "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int contactID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["contact_id"].Value);

                        string connectionString = dBFunctions.ConnectionStringSQLite;
                        dBHelper dbHelper = new dBHelper(connectionString);

                        // Töröljük a kiválasztott névjegyet az adatbázisból
                        string deleteQuery = "DELETE FROM Contact WHERE contact_id = " + contactID;
                        dbHelper.Load(deleteQuery);

                        // Frissítsük a DataGridView-t
                        RefreshDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void RefreshDataGridView()
        {
            // Újra betöltjük az adatokat a DataGridView-be
            try
            {
                string connectionString = dBFunctions.ConnectionStringSQLite;
                dBHelper dbHelper = new dBHelper(connectionString);

                dbHelper.Load("SELECT * FROM Contact");

                if (dbHelper.DataSet != null && dbHelper.DataSet.Tables.Count > 0)
                {
                    DataTable table = dbHelper.DataSet.Tables[0];
                    dataGridView1.DataSource = table;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing DataGridView: " + ex.Message);
            }
        }



        private void OpenNewContactForm()
        {
            try
            {
                NewContact newContactForm = new NewContact();
                newContactForm.Owner = this; 
                newContactForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening New Contact form: " + ex.Message);
            }
        }



        private void newStripButton_Click(object sender, EventArgs e)
        {
            OpenNewContactForm();
        }

        private void editStripButton_Click(object sender, EventArgs e)
        {
            EditSelectedContact();
        }

        private void deleteStripButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedContact();
        }
    }

}
