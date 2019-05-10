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
using System.Globalization;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    public sealed partial class PatientRegistrationForm : ContentDialog
    {

        public PatientsController patCtrl = new PatientsController();

        public PatientRegistrationForm()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //execute saving record
            if (isFormValid())
            {
                if (registerNewPatient())
                {
                    message("New Patient Registered Successfully");
                }
                else
                {
                    args.Cancel = true;
                    message("Patient Registration Failed!");
                }
            }
            else
            {
                //form is not valid
                args.Cancel = true;
            }
            

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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

        private Boolean registerNewPatient()
        {
            
            patCtrl.fname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFirstName.Text);
            patCtrl.lname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtLastName.Text);
            patCtrl.mname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMiddleName.Text); 
            patCtrl.bday = pckDateBirthDate.Date.ToString();
            patCtrl.emailAdd = txtEmail.Text;
            patCtrl.contactNum = txtContactNumber.Text;
            patCtrl.userAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtAddress.Text); 
            string gender = "";
            if(radMale.IsChecked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            patCtrl.gender = gender;

            var id = patCtrl.registerNewPatient(patCtrl);
            if (id >= 1)
            {
                patCtrl.patientID = id.ToString() ;
                return true;
            }
            else
            {
                return false;
            }


        }

        private async void message(string message)
        {
            //displays a messagebox
            MessageDialog cd = new MessageDialog(message);
            await cd.ShowAsync();
        }

        //#end of class
    }
}
