using StudentInfo1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace studentdb1
{
    public  class studentdb
    {
        
        public string _connectionString = ConfigurationManager.ConnectionStrings["studentsRecord.Properties.Settings.StudentRecordConnectionString"].ConnectionString;
        public  void addstudent(Student studentsall)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_insertstudent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@Name", studentsall.name);
                cmd.Parameters.AddWithValue("@Roll_no", studentsall.rollno);
                cmd.Parameters.AddWithValue("@Class", studentsall.sclass);
                cmd.Parameters.AddWithValue("@State", studentsall.state);
                cmd.Parameters.AddWithValue("@City", studentsall.city);
                cmd.Parameters.AddWithValue("@Campus", studentsall.campus);
                cmd.Parameters.AddWithValue("@stateid", studentsall.stateid);
                cmd.Parameters.AddWithValue("@cityid", studentsall.cityid);
                cmd.Parameters.AddWithValue("@campusid", studentsall.campusid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully", "Record Added", MessageBoxButtons.OK);
            }


        }
        public DataTable getgrid()
        {   
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_datagrid", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;
        }
        public DataTable getstates()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_states", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;
        }
        public DataTable getcity(int state)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_city", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@state", state);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;
        }
        public DataTable getcampus(int city)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_campus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@city", city);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;
        }
        public DataTable getback(int studentid)
        {   
            string studentids=studentid.ToString();
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("usp_getback", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@studentid", studentids);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;
        }
    }
}
