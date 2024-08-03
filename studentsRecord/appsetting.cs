using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentsRecord
{
    public class appsetting
    {
        public static string Getstring()
        {
            string cs = "";
            cs=ConfigurationManager.ConnectionStrings["studentsRecord.Properties.Settings.StudentRecordConnectionString"].ConnectionString;
            return cs;
        }
    }
}
