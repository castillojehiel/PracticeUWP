using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    public sealed partial class DailyAppointmentList : ContentDialog
    {
        public AppointmentController appCtrl = new AppointmentController();
        public ObservableCollection<Appointment> AppointmentList = new ObservableCollection<Appointment> { };
        public DataTable dtDateApp = new DataTable();

        public DailyAppointmentList(AppointmentModel data)
        {
            this.InitializeComponent();
            appCtrl.selectedDate = data.selectedDate;
        }

        public DailyAppointmentList()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //execute when close button is clicked
            //by default this closes the contentdialog window
            //and returns content dialog result primary
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txbDate.Text = Convert.ToDateTime(appCtrl.selectedDate).ToLongDateString();
            //query for the appointments for the current selected date
            loadAppointmentsToday();

            //load page to frame
            //frmAppointmentDetails.Navigate(typeof(AppointmentDetailsPage));

        }

        //<summary>
            //function will load all appointments for the current date
        //</summary>
        private void loadAppointmentsToday()
        {
            //query appointments from db
            dtDateApp.Clear(); //remove all data in the datatable
            dtDateApp = appCtrl.getDateAppointments(appCtrl);
            //convert datatable to observableCollection
            AppointmentList.Clear();

            foreach(DataRow row in dtDateApp.Rows)
            {
                AppointmentList.Add(new Appointment()
                {
                    Type = row["AppType"].ToString().ToUpperInvariant(),
                    DateStart = row["DateStart"].ToString(),
                    DateEnd = row["DateEnd"].ToString(),
                    TimeStart = Convert.ToDateTime(row["TimeStart"].ToString()).ToShortTimeString(),
                    TimeEnd = Convert.ToDateTime(row["TimeEnd"].ToString()).ToShortTimeString()
                });
                
            }
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if(sender == btnNewApp)
            {
                //open new window
                //default date is current date
                //add ending date field in case appointment exceed 1 day
                NewAppointment newApp = new NewAppointment(appCtrl);
                this.Hide();
                NewAppForm();
                async void NewAppForm()
                {
                    ContentDialogResult result = await newApp.ShowAsync();
                    if(result == ContentDialogResult.Secondary)
                    {
                        await this.ShowAsync();   
                    }
                }
            }
        }

        private void LstAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = lstAppointments.SelectedIndex;
            if(index != -1)
            {
                var row = dtDateApp.Rows[index];
                appCtrl.appType = row["AppType"].ToString();
                appCtrl.dateStart = row["DateStart"].ToString();
                appCtrl.dateEnd = row["DateEnd"].ToString();
                appCtrl.timeStart = row["TimeStart"].ToString();
                appCtrl.timeEnd = row["TimeEnd"].ToString();
                appCtrl.appNote = row["AppNote"].ToString();
                appCtrl.patientName = row["Appointee"].ToString();
                appCtrl.status = row["AppointmentStatus"].ToString();
                appCtrl.appLocation = row["Location"].ToString();
                appCtrl.appID = row["AppointmentID"].ToString();

                // navigate to page, pass datatable
                frmAppointmentDetails.Navigate(typeof(AppointmentDetailsPage), appCtrl);
            }
        }

    //end of class
    }
}
