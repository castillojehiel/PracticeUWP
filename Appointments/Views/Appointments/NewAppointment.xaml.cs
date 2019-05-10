using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class NewAppointment : ContentDialog
    {
        public AppointmentController appCtrl = new AppointmentController();

        public NewAppointment()
        {
            this.InitializeComponent();
        }

        public NewAppointment(AppointmentModel data)
        {
            this.InitializeComponent();
            appCtrl.selectedDate = data.selectedDate;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //execute saving of new appointment
            //set values
            appCtrl.appType = ((ComboBoxItem)cboAppType.SelectedItem).Content.ToString();
            appCtrl.dateStart = pckDteDateStart.Date.ToString();
            appCtrl.dateEnd = pckDteDateEnd.Date.ToString();
            appCtrl.timeStart = pckTmeTimeStart.Time.ToString();
            appCtrl.timeEnd = pckTmeTimeEnd.Time.ToString();
            appCtrl.appNote = txtNote.Text;
            appCtrl.appLocation = txtLocation.Text;
            if (appCtrl.appType.ToLower() == "consultation")
            {
                //use patient id only
                appCtrl.patientName = appCtrl.patientID;
            }
            else
            {
                appCtrl.patientName = txtName.Text;
            }

            var res = appCtrl.saveNewAppointment(appCtrl);
            if (res > 0)
            {
                message("New Appointment Successfully Saved!");
                appCtrl.appID = res.ToString();
                appCtrl.status = "active";
                appCtrl.patientID = "0";
            }
            else
            {
                args.Cancel = true;
                message("Failed to save new Appointment.");
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            pckDteDateStart.Date = Convert.ToDateTime(appCtrl.selectedDate);
            pckDteDateEnd.Date = Convert.ToDateTime(appCtrl.selectedDate);
        }

        private void CboAppType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = ((ComboBoxItem)cboAppType.SelectedItem).Content.ToString();
            if (type.ToLower() == "consultation")
            {
                toggleFormState("show");
            }
            else
            {
                toggleFormState("hidden");
            }
        }

        private void toggleFormState(String action)
        {
            if (action.ToLower() == "show")
            {
                //show add and search button
                //shorten textbox
                txtName.SetValue(Grid.ColumnSpanProperty, 1);
                btnNewPatient.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;
            }
            else if (action.ToLower() == "hide")
            {
                txtName.SetValue(Grid.ColumnSpanProperty, 3);
                btnNewPatient.Visibility = Visibility.Collapsed;
                btnSearch.Visibility = Visibility.Collapsed;
            }

        }


        //act as a message box
        private async void message(string err)
        {
            MessageDialog md = new MessageDialog(err);
            await md.ShowAsync();
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if (sender == btnSearch)
            {
                //open patient search
                this.Hide();
                searchForm();

                async void searchForm(){
                    PatientSearch search = new PatientSearch();
                    ContentDialogResult result = await search.ShowAsync();
                    if (result == ContentDialogResult.Primary || result == ContentDialogResult.Secondary)
                    {
                        var datasrc = search.patCtrl;
                        txtName.Text = datasrc.fname + " " + datasrc.lname;
                        appCtrl.patientID = datasrc.patientID;
                        await this.ShowAsync();
                    }
                }
            }
            else if(sender == btnNewPatient)
            {
                this.Hide();
                newPatient();

                async void newPatient()
                {
                    PatientRegistrationForm patRegForm = new PatientRegistrationForm();
                    ContentDialogResult result =  await patRegForm.ShowAsync();
                    if(result == ContentDialogResult.Primary)
                    {
                        var datasrc = patRegForm.patCtrl;
                        txtName.Text = datasrc.fname + " " + datasrc.lname;
                        appCtrl.patientName = datasrc.patientID;
                        await this.ShowAsync(); 
                    }
                }
            }
        }

        //end of class
    }
}
