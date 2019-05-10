using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Appointments
{
    public class AppointmentController : AppointmentModel
    {

        public DataTable getAppointments()
        {
            try
            {
                cmd = new SqlCommand("usp_LoadAppointments");
                return ExecuteReader(cmd);
            }catch(Exception err)
            {
                message(err.ToString());
                return null;
            }
        }

        public DataTable getDateAppointments(AppointmentModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_LoadDateAppointments");
                cmd.Parameters.AddWithValue("@date", data.selectedDate);
                return ExecuteReader(cmd);

            }catch(Exception err)
            {
                message(err.ToString());
                return null;
            }
        }

        public int saveNewAppointment(AppointmentModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_InsertNewAppointment");
                cmd.Parameters.AddWithValue("@dateStart", data.dateStart);
                cmd.Parameters.AddWithValue("@dateEnd", data.dateEnd);
                cmd.Parameters.AddWithValue("@timeStart", data.timeStart);
                cmd.Parameters.AddWithValue("@timeEnd", data.timeEnd);
                cmd.Parameters.AddWithValue("@note", data.appNote);
                cmd.Parameters.AddWithValue("@location", data.appLocation);
                cmd.Parameters.AddWithValue("@type", data.appType);
                cmd.Parameters.AddWithValue("@name", data.patientName);
                return ExecuteScalar(cmd);
            }
            catch(Exception err)
            {
                message(err.ToString());
                return 0;
            }
        }
        
        public Boolean updateAppointment(AppointmentModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_UpdateAppointment");
                cmd.Parameters.AddWithValue("@id", data.appID);
                cmd.Parameters.AddWithValue("@type", data.appType);
                cmd.Parameters.AddWithValue("@dateStart", data.dateStart);
                cmd.Parameters.AddWithValue("@dateEnd", data.dateEnd);
                cmd.Parameters.AddWithValue("@timeStart", data.timeStart);
                cmd.Parameters.AddWithValue("@timeEnd", data.timeEnd);
                cmd.Parameters.AddWithValue("@note", data.appNote);
                cmd.Parameters.AddWithValue("@location", data.appLocation);
                cmd.Parameters.AddWithValue("@name", data.patientName); //patientName contains patientID or appointment requestor name
                return ExecNonQuery(cmd);   

            }
            catch(Exception err)
            {
                message(err.ToString());
                return false;
            }
        }

        public Boolean deleteAppointment(AppointmentModel data)
        {
            try
            {
                cmd = new SqlCommand("usp_deleteAppointment");
                cmd.Parameters.AddWithValue("@appID", data.appID);
                return ExecNonQuery(cmd);
            }
            catch (Exception err)
            {
                message(err.ToString());
                return false;
            }
        }

        public async void message(string err)
        {
            MessageDialog cd = new MessageDialog(err);
            await cd.ShowAsync();
        }

        

    }
}
