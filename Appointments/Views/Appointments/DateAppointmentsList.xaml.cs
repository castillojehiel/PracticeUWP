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
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{

    public sealed partial class DateAppointmentsList : Page
    {
        public AppointmentController appCtrl = new AppointmentController();
        public ObservableCollection<Appointment> appointmentCollection = new ObservableCollection<Appointment> { };
        public DataTable dtDateApp = new DataTable();

        public DateAppointmentsList()
        {
            this.InitializeComponent();
        }

        private void loadAppointmentsToday()
        { 
            //<summary>
              //function will load all appointments for the current date
            //</summary>
          //query appointments from db
            dtDateApp.Clear(); //remove all data in the datatable
            dtDateApp = appCtrl.getDateAppointments(appCtrl);
            //convert datatable to observableCollection
            appointmentCollection.Clear();

            foreach (DataRow row in dtDateApp.Rows)
            {
                //add new item to observablecollection<Appointment>
                addToCollection(
                    row["AppType"].ToString().ToUpperInvariant(),
                    row["DateStart"].ToString(),
                    row["DateEnd"].ToString(),
                    Convert.ToDateTime(row["TimeStart"].ToString()).ToShortTimeString(),
                    Convert.ToDateTime(row["TimeEnd"].ToString()).ToShortTimeString(),
                    row["AppointmentID"].ToString(),
                    row["AppLocation"].ToString(),
                    row["AppNote"].ToString(),
                    row["name"].ToString(),
                    row["patientID"].ToString(),
                    row["AppointmentStatus"].ToString());

            }
        }

        private void addToCollection(
            String Type, String DateStart, String DateEnd,
            String TimeStart, String TimeEnd, String AppID ,
            String AppLocation, String AppNote, String AppName,
             String AppNameID ,String AppStatus 
        )
        {
            appointmentCollection.Add(new Appointment()
            {
                Type = Type.ToUpper(),
                DateStart = Convert.ToDateTime(DateStart).ToShortDateString(),
                DateEnd = Convert.ToDateTime(DateEnd).ToShortDateString(),
                TimeStart = Convert.ToDateTime(TimeStart).ToShortTimeString(),
                TimeEnd = Convert.ToDateTime(TimeEnd).ToShortTimeString(),
                AppID = AppID,
                AppLocation = AppLocation,
                AppNote = AppNote,
                AppName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AppName.ToLower()),
                AppNameID = AppNameID,
                AppStatus = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AppStatus.ToLower())
            }); ;
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if (sender == btnNewApp)
            {
                //open new window
                //default date is current date
                //add ending date field in case appointment exceed 1 day
                NewAppointment newApp = new NewAppointment(appCtrl);
                NewAppForm();
                async void NewAppForm()
                {
                    if (await newApp.ShowAsync() == ContentDialogResult.Primary)
                    {
                        //means save button is clicked and new record is saved successfully
                        //update observableCollection
                        addToCollection(
                            newApp.appCtrl.appType.ToString(),
                            newApp.appCtrl.dateStart.ToString(),
                            newApp.appCtrl.dateEnd.ToString(),
                            newApp.appCtrl.timeStart.ToString(),
                            newApp.appCtrl.timeEnd.ToString(),
                            newApp.appCtrl.appID.ToString(),
                            newApp.appCtrl.appLocation.ToString(),
                            newApp.appCtrl.appNote.ToString(),
                            newApp.appCtrl.patientName.ToString(),
                            newApp.appCtrl.patientID.ToString(),
                            newApp.appCtrl.status.ToString()
                            ) ;
                    }
                    
                }
            }
            else if (sender == btnEdit)
            {
                AppointmentUpdateForm updateFrm = new AppointmentUpdateForm(appCtrl);
                frmShow();

                async void frmShow()
                {
                    if (await updateFrm.ShowAsync() == ContentDialogResult.Primary)
                    {
                        //update observable collection
                        var itmsrc = updateFrm.appCtrl;
                        var indx = lstAppointments.SelectedIndex;
                        appointmentCollection[indx].Type = itmsrc.appType;
                        appointmentCollection[indx].DateStart = itmsrc.dateStart;
                        appointmentCollection[indx].TimeStart = itmsrc.timeStart;
                        appointmentCollection[indx].DateEnd = itmsrc.dateEnd;
                        appointmentCollection[indx].TimeEnd = itmsrc.timeEnd;
                        appointmentCollection[indx].AppLocation = itmsrc.appLocation;
                        appointmentCollection[indx].AppNote = itmsrc.appNote;
                        appointmentCollection[indx].AppName = itmsrc.patientName;
                        appointmentCollection[indx].AppNameID = itmsrc.patientID;
                    }
                }

            }
            else if (sender == btnDelete)
            {
                //delete curren selected appointment form list
                deleteApp();

                async void deleteApp()
                {
                    ContentDialog confirmDelete = new ContentDialog()
                    {
                        Title = "Confirm Delete?",
                        Content = "Are you sure you want to delete this record? " +
                                    "\nData could not be recoevered after deletion",
                        PrimaryButtonText = "Yes",
                        SecondaryButtonText = "Cancel"
                    };
                    if (await confirmDelete.ShowAsync() == ContentDialogResult.Primary)
                    {
                        if (appCtrl.deleteAppointment(appCtrl))
                        {
                            appCtrl.message("Record deleted successfully");
                            //update UI and remove record from removable collection
                            appointmentCollection.RemoveAt(lstAppointments.SelectedIndex);
                            grdAppDetailsView.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            appCtrl.message("An error has occured while deleting record.");
                        }
                        
                    }
                }
            }



        }

        private void LstAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = lstAppointments.SelectedIndex;

            //this searches the dataTable
            //if (index != -1)
            //{
            //    var row = dtDateApp.Rows[index];
            //    appCtrl.appType = row["AppType"].ToString();
            //    appCtrl.dateStart = row["DateStart"].ToString();
            //    appCtrl.dateEnd = row["DateEnd"].ToString();
            //    appCtrl.timeStart = row["TimeStart"].ToString();
            //    appCtrl.timeEnd = row["TimeEnd"].ToString();
            //    appCtrl.appNote = row["AppNote"].ToString();
            //    appCtrl.patientName = row["Appointee"].ToString();
            //    appCtrl.status = row["AppointmentStatus"].ToString();
            //    appCtrl.appLocation = row["AppLocation"].ToString();
            //    appCtrl.appID = row["AppointmentID"].ToString();

            //    // navigate to page, pass datatable
            //    frmAppointmentDetails.Navigate(typeof(AppointmentDetailsPage), appCtrl);
            //}

            //this searches the observablecollection
            if(index != -1)
            {
                //when using frame/page to display data
                //search collection
                //var item = AppointmentList[index];
                //appCtrl.appType = item.Type.ToString();
                //appCtrl.dateStart = item.DateStart.ToString();
                //appCtrl.dateEnd = item.DateEnd.ToString();
                //appCtrl.timeStart = item.TimeStart.ToString();
                //appCtrl.timeEnd = item.TimeEnd.ToString();
                //appCtrl.appNote = item.AppNote.ToString();
                //appCtrl.patientName = item.AppName.ToString();
                //appCtrl.status = item.AppStatus.ToString();
                //appCtrl.appLocation = item.AppLocation.ToString();
                //appCtrl.appID = item.AppID.ToString();
                //navigate to page
                //frmAppointmentDetails.Navigate(typeof(AppointmentDetailsPage), appCtrl);

                //set grid to visible
                grdAppDetailsView.Visibility = Visibility.Visible;
                //display data on grid
                var item = appointmentCollection[index];
                txtType.Text = appCtrl.appType = item.Type.ToString();
                txtDateStart.Text = appCtrl.dateStart = item.DateStart.ToString();
                txtDateEnd.Text = appCtrl.dateEnd = item.DateEnd.ToString();
                txtTimeStart.Text = appCtrl.timeStart = item.TimeStart.ToString();
                txtTimeEnd.Text = appCtrl.timeEnd = item.TimeEnd.ToString();
                txtNote.Text = appCtrl.appNote = item.AppNote.ToString();
                txtName.Text = appCtrl.patientName = item.AppName.ToString();
                txtStatus.Text = appCtrl.status = item.AppStatus.ToString();
                txtLocation.Text = appCtrl.appLocation = item.AppLocation.ToString();
                appCtrl.appID = item.AppID.ToString();
                appCtrl.patientID = item.AppNameID;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //this executes on succesful navigation to page
            //Recievs value form parent form/window
            base.OnNavigatedTo(e);
            var parameters = (AppointmentController)e.Parameter;
            //sets default selected date to date selected on the calendar
            appCtrl.selectedDate = parameters.selectedDate;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txbDate.Text = Convert.ToDateTime(appCtrl.selectedDate).ToLongDateString();
            loadAppointmentsToday();
        }

        //#end of class
    }
}
