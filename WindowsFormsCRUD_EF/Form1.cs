using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace WindowsFormsCRUD_EF
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg" })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    Employee obj = employeeBindingSource.Current as Employee;
                    if(obj != null)
                    {
                        obj.ImageUrl = ofd.FileName;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var db = new ModelContext())
            {
                employeeBindingSource.DataSource = db.Employees.ToList();
            }
            metroPanel1.Enabled = false;
            Employee obj = employeeBindingSource.Current as Employee;
            if (obj != null)
            {
                pictureBox1.Image = Image.FromFile(obj.ImageUrl);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            metroPanel1.Enabled = true;
            employeeBindingSource.Add(new Employee());
            employeeBindingSource.MoveLast();
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            metroPanel1.Enabled = true;
            txtName.Focus();
            Employee obj = employeeBindingSource.Current as Employee;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            metroPanel1.Enabled = false;
            employeeBindingSource.ResetBindings(false);
            Form1_Load(sender, e);
        }

        private void metroGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var obj = employeeBindingSource.Current as Employee;
            if (obj != null)
            {
                pictureBox1.Image = Image.FromFile(obj.ImageUrl);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, $"Are you sure, do you want to delete {txtName.Text} record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using(var db = new ModelContext())
                {
                    Employee obj = employeeBindingSource.Current as Employee;
                    if (obj != null)
                    {
                        if (db.Entry<Employee>(obj).State == EntityState.Detached)
                            db.Set<Employee>().Attach(obj);
                        db.Entry<Employee>(obj).State = EntityState.Deleted;
                        db.SaveChanges();
                        MetroMessageBox.Show(this, "Deleted Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        employeeBindingSource.RemoveCurrent();
                        metroPanel1.Enabled = false;
                        pictureBox1.Image = null;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var db = new ModelContext())
            {
                Employee obj = employeeBindingSource.Current as Employee;
                if (obj != null)
                {
                    if (db.Entry<Employee>(obj).State == EntityState.Detached)
                        db.Set<Employee>().Attach(obj);
                    if(obj.EmpID == 0)
                    {
                        if(pictureBox1.Image == null)
                        {
                            MetroMessageBox.Show(this, "Please select an image","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        db.Entry(obj).State = EntityState.Added;
                    }                      
                    else
                        db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    MetroMessageBox.Show(this, "Saved Successfully","Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroGrid1.Refresh();
                    metroPanel1.Enabled = true;
                }
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool isValid = regex.IsMatch(txtEmail.Text.Trim());
            if (!isValid)
            {
                MetroMessageBox.Show(this, "Invalid Email, Check Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Clear();
            }               
        }
    }
}
