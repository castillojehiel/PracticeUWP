using System;
using System.Collections.Generic;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    public sealed partial class AppointmentUpdateForm : ContentDialog
    {

        public AppointmentController appCtrl = new AppointmentController();

        public AppointmentUpdateForm()
        {
            this.InitializeComponent();
        }

        public AppointmentUpdateForm(AppointmentModel data)
        {
            this.InitializeComponent();
            appCtrl.appID = data.appID;
            appCtrl.appType = data.appType;
            appCtrl.dateStart = data.dateStart;
            appCtrl.dateEnd = data.dateEnd;
            appCtrl.timeStart = data.timeStart;
            appCtrl.timeEnd = data.timeEnd;
            appCtrl.appLocation = data.appLocation;
            appCtrl.appNote = data.appNote;
            appCtrl.patientName = data.patientName;
            appCtrl.patientID = data.patientID;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //<summary>
            //save changes to record
            //</summary>
            appCtrl.appType = ((ComboBoxItem)cboAppType.SelectedItem).Content.ToString(); ;
            appCtrl.dateStart = pckDteDateStart.Date.ToString();
            appCtrl.dateEnd = pckDteDateEnd.Date.ToString();
            appCtrl.timeStart = pckTmeTimeStart.Time.ToString();
            appCtrl.timeEnd = pckTmeTimeEnd.Time.ToString();
            appCtrl.appNote = txtNote.Text;
            appCtrl.appLocation = txtLocation.Text;
            if(appCtrl.patientID != null)
            {
                appCtrl.patientName = appCtrl.patientID;
            }
            else
            {
                appCtrl.patientName = txtName.Text;
            }

            if (appCtrl.updateAppointment(appCtrl))
            {
                appCtrl.message("Changes were successfully saved.");
            }
            else
            {
                args.Cancel = true; //prevent content dialog from closing
                appCtrl.message("Error saving changes.");
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            //set values to fields
            txtName.Text = appCtrl.patientName;
            //gets static values of combobox
            List<String> lstCboItems = cboAppType.Items
                                        .Cast<ComboBoxItem>()
                                        .Select(item => item.Content.ToString())
                                        .ToList();
            String cboStrValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(appCtrl.appType.ToLower());
            cboAppType.SelectedIndex = lstCboItems.FindIndex(a => a.Equals(cboStrValue));
            pckDteDateStart.Date = Convert.ToDateTime(appCtrl.dateStart);
            pckDteDateEnd.Date = Convert.ToDateTime(appCtrl.dateStart);
            pckTmeTimeStart.Time = Convert.ToDateTime(appCtrl.timeStart).TimeOfDay;
            pckTmeTimeEnd.Time = Convert.ToDateTime(appCtrl.timeEnd).TimeOfDay;
            txtNote.Text = appCtrl.appNote;
            txtLocation.Text = appCtrl.appLocation;
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if (sender == btnSearch)
            {
                //open patient search
                this.Hide();
                searchForm();

                async void searchForm()
                {
                    PatientSearch search = new PatientSearch();
                    ContentDialogResult result = await search.ShowAsync();
                    if (result == ContentDialogResult.Primary || result == ContentDialogResult.Secondary)
                    {
                        var datasrc = search.patCtrl;
                        txtName.Text = datasrc.fname + " " + datasrc.lname;
                        appCtrl.patientName = datasrc.patientID;
                        await this.ShowAsync();
                    }
                }
            }
            else if (sender == btnNewPatient)
            {
                this.Hide();
                newPatient();

                async void newPatient()
                {
                    PatientRegistrationForm patRegForm = new PatientRegistrationForm();
                    ContentDialogResult result = await patRegForm.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        var datasrc = patRegForm.patCtrl;
                        txtName.Text = datasrc.fname + " " + datasrc.lname;
                        appCtrl.patientName = datasrc.patientID;
                        await this.ShowAsync();
                    }
                }
            }
        }

        //#end of class
    }
}
