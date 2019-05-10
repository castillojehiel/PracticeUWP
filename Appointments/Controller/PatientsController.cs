using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Appointments
{
    public class PatientsController : PatientsModel
    {

        public int registerNewPatient(PatientsModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_SaveNewPatient");
                cmd.Parameters.AddWithValue("@lname", data.lname);
                cmd.Parameters.AddWithValue("@fname", data.fname);
                cmd.Parameters.AddWithValue("@mname", data.mname);
                cmd.Parameters.AddWithValue("@gender", data.gender);
                cmd.Parameters.AddWithValue("@userAddress", data.userAddress);
                cmd.Parameters.AddWithValue("@bday", data.bday);
                cmd.Parameters.AddWithValue("@contactNum", data.contactNum);
                cmd.Parameters.AddWithValue("@email", data.emailAdd);
                return ExecuteScalar(cmd);
            }
            catch (Exception err)
            {
                message(err.ToString());
                return 0;
            }
        }

        public DataTable getAllPatients()
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM Patients");
                return ExecuteReaderNoParams(cmd);
            }
            catch (Exception err)
            {
                message(err.ToString());
                return null;
            }
        }

        public DataTable patientSearch(PatientsModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_SearchPatients");
                cmd.Parameters.AddWithValue("@keyWord", data.keyword);
                return ExecuteReader(cmd);
            }catch(Exception err)
            {
                message(err.ToString());
                return null;
            }
        }

        public Boolean patientUpdate(PatientsModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_UpdatePatient");
                cmd.Parameters.AddWithValue("@patientID", data.patientID);
                cmd.Parameters.AddWithValue("@fname ", data.fname);
                cmd.Parameters.AddWithValue("@lname", data.lname);
                cmd.Parameters.AddWithValue("@mname", data.mname);
                cmd.Parameters.AddWithValue("@gender", data.gender);
                cmd.Parameters.AddWithValue("@userAddress ", data.userAddress);
                cmd.Parameters.AddWithValue("@bday", data.bday);
                cmd.Parameters.AddWithValue("@email", data.emailAdd);
                cmd.Parameters.AddWithValue("@contactNum", data.contactNum);
                return ExecNonQuery(cmd);
            }catch(Exception err)
            {
                message(err.ToString());
                return false;
            }
        }

        public async void message(string err)
        {
            //displays a messagebox
            MessageDialog cd = new MessageDialog(err);
            await cd.ShowAsync();
        }

        //#end of class
    }
}
