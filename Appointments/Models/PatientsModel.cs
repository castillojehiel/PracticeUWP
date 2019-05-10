using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments
{
    public class PatientsModel : dbconnnection
    {

        public SqlCommand cmd;

        #region object properties
        public String fname { get; set; }
        public String lname { get; set; }
        public String mname { get; set; }
        public String gender { get; set; }
        public String bday { get; set; }
        public String userAddress {get; set;}
        public String contactNum { get; set; }
        public String emailAdd { get; set; }
        public string patientID { get; set; }
        public String keyword { get; set; }
        #endregion

    }
}
