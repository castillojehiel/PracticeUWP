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
    public sealed partial class PatientUpdateForm : ContentDialog
    {

        public PatientsController patCtrl = new PatientsController();

        public PatientUpdateForm()
        {
            this.InitializeComponent();
        }
        public PatientUpdateForm(PatientsModel data)
        {
            this.InitializeComponent();
            patCtrl.patientID = data.patientID;
            patCtrl.fname = data.fname;
            patCtrl.lname = data.lname;
            patCtrl.mname = data.mname;
            patCtrl.gender = data.gender;
            patCtrl.userAddress = data.userAddress;
            patCtrl.bday = data.bday;
            patCtrl.contactNum = data.contactNum;
            patCtrl.emailAdd = data.emailAdd;

        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = patCtrl.fname;
            txtLastName.Text = patCtrl.lname;
            txtMiddleName.Text = patCtrl.mname;
            txtAddress.Text = patCtrl.userAddress;
            txtContactNumber.Text = patCtrl.contactNum;
            txtEmail.Text = patCtrl.emailAdd;
            pckDateBirthDate.Date = Convert.ToDateTime(patCtrl.bday);
            if(patCtrl.gender == "Male")
            {
                radMale.IsChecked = true;
            }
            else
            {
                radFemale.IsChecked = true;
            }
        }

        public Boolean patientUpdate()
        {
            patCtrl.fname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFirstName.Text.ToLower());
            patCtrl.lname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtLastName.Text.ToLower());
            patCtrl.mname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMiddleName.Text.ToLower());
            patCtrl.bday = pckDateBirthDate.Date.ToString();
            patCtrl.emailAdd = txtEmail.Text;
            patCtrl.contactNum = txtContactNumber.Text;
            patCtrl.userAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtAddress.Text.ToLower());
            string gender = "";
            if (radMale.IsChecked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            patCtrl.gender = gender;

            if (patCtrl.patientUpdate(patCtrl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean isFormValid()
        {
            //form validation
            if (txtFirstName.Text == "")
            {
                patCtrl.message("Please provide FirstName.");
                return false;

            }
            else if (txtLastName.Text == "")
            {
                patCtrl.message("Please provide LastName.");
                return false;
            }
            else if (txtAddress.Text == "")
            {
                patCtrl.message("Please provide Address.");
                return false;
            }
            else if (radMale.IsChecked == false && radFemale.IsChecked == false)
            {
                patCtrl.message("Please select gender.");
                return false;
            }
            else if (pckDateBirthDate.Date.ToString() == "" || pckDateBirthDate.Date == null)
            {
                patCtrl.message("Please provide brthdate.");
                return false;
            }
            else
            {
                //all fields is valid
                return true;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //<summary>
            //execute update
            //</summary>
            if (isFormValid()){
                if (patientUpdate()){
                    patCtrl.message("Patient Record updated successfully");

                }else{
                    patCtrl.message("Error updating patient record");
                    args.Cancel = true;
                }
            }else{
                args.Cancel = true;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        
    }
}
