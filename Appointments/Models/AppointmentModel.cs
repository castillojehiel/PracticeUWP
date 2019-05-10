using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments
{
    public class AppointmentModel : dbconnnection
    {

        public SqlCommand cmd;

        #region object properties
        public DataTable dtAppointmentDetails { get; set; }
        public String title { get; set; }
        public String date { get; set; }
        public String time { get; set; }
        public String dateStart { get; set; }
        public String dateEnd { get; set; }
        public String timeStart { get; set; }
        public String timeEnd { get; set; }
        public String appNote { get; set; }
        public String appLocation { get; set; }
        public String patientName { get; set; }
        public String patientID { get; set; }
        public String appType { get; set; }
        public string status { get; set; }
        public string appID { get; set; }

        public String selectedDate { get; set; }
        #endregion

    }
}
