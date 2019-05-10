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
using Appointments.Models;
using System.Collections.ObjectModel;
using System.Data;
using Windows.UI.Popups;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    public sealed partial class PatientSearch : ContentDialog
    {

        public PatientsController patCtrl = new PatientsController();
        public ObservableCollection<Patient> patientCollection = new ObservableCollection<Patient> { };

        public PatientSearch()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            var slctdItem = dgPatients.SelectedIndex;
            if (slctdItem != -1)
            {
                //get values from observable collection using returned index
                var itmsrc = patientCollection[slctdItem];
                patCtrl.patientID = itmsrc.PatientID;
                patCtrl.fname = itmsrc.Fname;
                patCtrl.lname = itmsrc.Lname;
                patCtrl.mname = itmsrc.Mname;
                patCtrl.gender = itmsrc.Gender;
                patCtrl.bday = itmsrc.BirthDate;
                patCtrl.userAddress = itmsrc.UserAddress;
                patCtrl.contactNum = itmsrc.ContactNumber;
            }
            else
            {
                args.Cancel = true;
                //notify user no patient selected
                message("Please select a patient.");
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public void loadAllPatients()
        {
            DataTable dtPatients = patCtrl.getAllPatients();
            //dgPatientList.ItemsSource = dtPatients.DefaultView;
            patientCollection.Clear(); //clear all items in the collection
            foreach (DataRow row in dtPatients.Rows)
            {
                addToCollection(
                    row["PatientID"].ToString(),
                    row["LastName"].ToString(),
                    row["FirstName"].ToString(),
                    row["MiddleName"].ToString(),
                    row["Gender"].ToString(),
                    row["BirthDate"].ToString(),
                    row["ContactNumber"].ToString(),
                    row["EmailAddress"].ToString(),
                    row["Address"].ToString()
                    );
            }
        }

        private void addToCollection(String PatientID,
                String Lname,
                String Fname,
                String Mname,
                String Gender,
                String BirthDate,
                String ContactNumber,
                String EmailAddress,
                String UserAddress)
        {
            patientCollection.Add(new Patient()
            {
                PatientID = PatientID,
                Lname = Lname,
                Fname = Fname,
                Mname = Mname,
                Gender = Gender,
                BirthDate = Convert.ToDateTime(BirthDate).ToShortDateString(),
                ContactNumber = ContactNumber,
                EmailAddress = EmailAddress,
                UserAddress = UserAddress
            });
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            loadAllPatients();
        }


        private async void message(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if(sender == btnSearch)
            {
                patCtrl.keyword = txtSearchKeyword.Text;
                DataTable dtSearchRes = patCtrl.patientSearch(patCtrl);
                if (dtSearchRes.Rows.Count != 0)
                {
                    patientCollection.Clear();
                    foreach (DataRow row in dtSearchRes.Rows)
                    {
                        addToCollection(
                            row["PatientID"].ToString(),
                            row["LastName"].ToString(),
                            row["FirstName"].ToString(),
                            row["MiddleName"].ToString(),
                            row["Gender"].ToString(),
                            row["BirthDate"].ToString(),
                            row["ContactNumber"].ToString(),
                            row["EmailAddress"].ToString(),
                            row["Address"].ToString()
                        );
                    }
                }
                else
                {
                    patientCollection.Clear();
                    message("No results found!");
                }
            }
        }

        //#end of class
    }
}
