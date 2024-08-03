
using studentdb1;
using StudentInfo1;
using studentsRecord.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace studentsRecord
{
    public partial class studentrecord : Form
    {
        public studentrecord()
        {
            InitializeComponent();
        }
        public int state_id;
        public int city_id;
        private DataTable data = new DataTable();
        
        private void Form1_Load(object sender, EventArgs e)
        {

            Enable_button(false);
            reloaddatagrid();
            getstatescombo();
            if (statecomboBox.SelectedValue != null)
            {
                textBox1.Text = statecomboBox.SelectedValue.ToString();
            }
            else
            {
                textBox1.Text = "NULL HAI";
            }

            if (citycomboBox.SelectedValue != null)
            {
                textBox2.Text = citycomboBox.SelectedValue.ToString();
            }
            else
            {
                textBox2.Text = "NULL HAI";
            }

            if (CampuscomboBox.SelectedValue != null)
            {
                textBox3.Text = CampuscomboBox.SelectedValue.ToString();
            }
            else
            {
                textBox3.Text = "NULL HAI";
            }



        }
        private DataTable getdatasource()
        {
            studentdb db = new studentdb();
            DataTable dt = new DataTable();
            dt = db.getgrid();
            return dt;
        }
        private void reloaddatagrid()
        {
            data=getdatasource();
            studentsdataGridView.DataSource = data;
            studentsdataGridView.MultiSelect = false;
            studentsdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            studentsdataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Enable_button(bool value)
        {
            nametextBox.Enabled = value;
            nametextBox.Enabled = value;
            RollnotextBox.Enabled = value;
            classcomboBox.Enabled = value;
            statecomboBox.Enabled = value;
            citycomboBox.Enabled = value;
            CampuscomboBox.Enabled = value;
        }

        private void newStudentbutton_Click(object sender, EventArgs e)
        {
            Enable_button(true);
            getstatescombo();

            nametextBox.Clear();
            nametextBox.Focus();
            RollnotextBox.Clear();
            classcomboBox.SelectedIndex = 0;
            statecomboBox.SelectedIndex = 0;
            citycomboBox.SelectedIndex = 0;
            CampuscomboBox.SelectedIndex = 0;
            pictureBox1.Image = Resources.noimage;

        }

        private void getstatescombo()
        {
            studentdb db = new studentdb();

            DataTable dt = new DataTable();
            dt = db.getstates();
            DataRow dr2 = dt.NewRow();
            dr2[0] = -1;
            dr2[1] = "--Select State--";
            dt.Rows.InsertAt(dr2, 0);

            statecomboBox.ValueMember = "id";
            statecomboBox.DisplayMember = "name";
            statecomboBox.DataSource = dt;
            statecomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void insertbutton_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                Student stu = new Student()
                {
                    name = nametextBox.Text,
                    rollno = int.Parse(RollnotextBox.Text),
                    sclass = classcomboBox.Text,
                    state = statecomboBox.Text,
                    city = citycomboBox.Text,
                    campus = CampuscomboBox.Text,
                    stateid = Convert.ToInt32(statecomboBox.SelectedIndex),
                    cityid= Convert.ToInt32(citycomboBox.SelectedIndex),
                    campusid= Convert.ToInt32(CampuscomboBox.SelectedIndex)
                };

                studentdb db= new studentdb();
                db.addstudent(stu);

                reloaddatagrid();
                nametextBox.Clear();
                RollnotextBox.Clear();
                classcomboBox.SelectedIndex = 0;
                statecomboBox.SelectedIndex = 0;
                citycomboBox.SelectedIndex = 0;
                CampuscomboBox.SelectedIndex = 0;
                Enable_button(false);
                idtextbox.Focus();
            }
        }
        private bool Isvalid()
        {
            bool valid = true;
            if (nametextBox.Text == "")
            {
                nametextBox.BackColor = Color.Yellow;
                messageshow("Name");
                valid = false;
                
            }
            else if (RollnotextBox.Text == "")
            {
                
                RollnotextBox.BackColor = Color.Yellow;
                messageshow("Roll No");
                valid = false;
            }
            else if (classcomboBox.Text == "" || classcomboBox.Text == "--Select Class--")
            {
                classcomboBox.BackColor = Color.Yellow;
                messageshow("Class");
                valid = false;
            }
            else if (statecomboBox.Text == "" || statecomboBox.Text== "--Select State--")
            {
                statecomboBox.BackColor = Color.Yellow;
                messageshow("State");
                valid = false;
            }
            else if (citycomboBox.Text == "" || citycomboBox.Text== "--Select City--")
            {
                citycomboBox.BackColor = Color.Yellow;
                messageshow("City");
                valid = false;
            }
            else if (CampuscomboBox.Text == "" || CampuscomboBox.Text == "--Select Campus--")
            {
                CampuscomboBox.BackColor = Color.Yellow;
                messageshow("Campus");
                valid = false;
            }
            

            return valid;

        }

        private void messageshow(string message)
        {
            MessageBox.Show("Enter the "+message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       

        private void nametextBox_TextChanged(object sender, EventArgs e)
        {
            nametextBox.BackColor = Color.White;
        }
        
        

        private void statecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            statecomboBox.BackColor = Color.White;
            try
            {
                state_id = int.Parse(statecomboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("No VAlue" + ex);
            }


            studentdb db = new studentdb();

            DataTable dt = new DataTable();
            dt = db.getcity(state_id);
            DataRow dr2 = dt.NewRow();
            dr2[0] = -1;
            dr2[1] = "--Select City--";
            dt.Rows.InsertAt(dr2, 0);
            citycomboBox.ValueMember = "id";
            citycomboBox.DisplayMember = "name";
            citycomboBox.DataSource = dt;
            citycomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox1.Text= statecomboBox.SelectedValue.ToString();
            
        }

        private void citycomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            citycomboBox.BackColor = Color.White;
            try
            {
                city_id = int.Parse(citycomboBox.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }


            studentdb db = new studentdb();

            DataTable dt = new DataTable();
            dt = db.getcampus(city_id);
            DataRow dr2 = dt.NewRow();
            dr2[0] = -1;
            dr2[1] = "--Select Campus--";
            dt.Rows.InsertAt(dr2, 0);
            CampuscomboBox.ValueMember = "ID";
            CampuscomboBox.DisplayMember = "name";
            CampuscomboBox.DataSource = dt;
            CampuscomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox2.Text = citycomboBox.SelectedValue.ToString();
        }

        private void CampuscomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CampuscomboBox.BackColor = Color.White;
            textBox3.Text = CampuscomboBox.SelectedValue.ToString();
        }

        private void RollnotextBox_TextChanged(object sender, EventArgs e)
        {
            RollnotextBox.BackColor = Color.White;
        }

        

        private void classcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            classcomboBox.BackColor = Color.White;

        }

        private void studentsdataGridView_DoubleClick(object sender, EventArgs e)
        {
            
            int rowindex = studentsdataGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int studentid = (studentsdataGridView.Rows[rowindex].Cells["CNIC"].Value) is DBNull ? 0 : Convert.ToInt32(studentsdataGridView.Rows[rowindex].Cells["CNIC"].Value);
            studentdb db = new studentdb();
            DataTable dt = new DataTable();
            dt = db.getback(studentid);
            DataRow row = dt.Rows[0];
            Enable_button(true);
            nametextBox.Focus();
            nametextBox.Text = row["name"].ToString();
            RollnotextBox.Text = row["rollno"].ToString();
            classcomboBox.SelectedItem = row["class"].ToString();
            statecomboBox.SelectedValue = 3176;
            citycomboBox.SelectedValue = 85331;
            CampuscomboBox.SelectedValue = 7;
            //statecomboBox.SelectedIndex =(row["stateid"]) is DBNull? 0 : Convert.ToInt32(row["stateid"]);
            //citycomboBox.SelectedIndex = (row["cityid"]) is DBNull ? 0 : Convert.ToInt32( row["cityid"]);
            //CampuscomboBox.SelectedIndex = (row["campusid"]) is DBNull ? 0 : Convert.ToInt32( row["campusid"]);
            //insertbutton.Enabled = false;
            


        }

        private void idtextbox_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            
            dv.RowFilter = "CNIC like '%" + idtextbox.Text + "%'";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
    }
}
