using Appointments;
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
using Appointments.Models;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PatientHome : Page
    {

        public PatientsController patCtrl = new PatientsController();
        public ObservableCollection<Patient> patientCollection = new ObservableCollection<Patient> { };

        public PatientHome()
        {
            this.InitializeComponent();
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if(sender == btnNewPatient)
            {
                //open form to register new patient
                openRegForm();  

                async void openRegForm()
                {
                    //contentdialog mustrun on async method
                    PatientRegistrationForm patRegform = new PatientRegistrationForm();
                    ContentDialogResult result = await patRegform.ShowAsync();
                    if(result == ContentDialogResult.Primary)
                    {
                        //add to collection
                        addToCollection(
                            patRegform.patCtrl.patientID,
                            patRegform.patCtrl.lname,
                            patRegform.patCtrl.fname,
                            patRegform.patCtrl.mname,
                            patRegform.patCtrl.gender,
                            patRegform.patCtrl.bday,
                            patRegform.patCtrl.contactNum,
                            patRegform.patCtrl.emailAdd,
                            patRegform.patCtrl.userAddress
                            );
                    }
                }
            }
            else if(sender == btnSearch)
            {
                patCtrl.keyword = txtSearchKeyword.Text;
                DataTable dtSearchRes = patCtrl.patientSearch(patCtrl);
                if(dtSearchRes.Rows.Count != 0)
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
                String UserAddress )
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadAllPatients();
        }

        private async void message(string err)
        {
            //displays a messagebox
            MessageDialog cd = new MessageDialog(err);
            await cd.ShowAsync();
        }

        private void BtnViewRecords_Click(object sender, RoutedEventArgs e)
        {
            //<summary>
                //view patient records
            //</summary>
        }

        private void BtnViewPatient_Click(object sender, RoutedEventArgs e)
        {
            //<summary>
            //view patient details
            //</summary>
            this.Frame.Navigate(typeof(PatientConsultationHistory));

        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            //<summary>
            //edit patient details
            //</summary>
            var indx = dgPatientList.SelectedIndex;
            //send data to update forms
            patCtrl.patientID = patientCollection[indx].PatientID;
            patCtrl.fname = patientCollection[indx].Fname;
            patCtrl.mname = patientCollection[indx].Mname;
            patCtrl.lname = patientCollection[indx].Lname;
            patCtrl.gender = patientCollection[indx].Gender;
            patCtrl.bday = patientCollection[indx].BirthDate;
            patCtrl.contactNum = patientCollection[indx].ContactNumber;
            patCtrl.emailAdd = patientCollection[indx].EmailAddress;
            patCtrl.userAddress = patientCollection[indx].UserAddress;
            updateForm();

            async void updateForm()
            {
                PatientUpdateForm updtFrm = new PatientUpdateForm(patCtrl);
                if (await updtFrm.ShowAsync() == ContentDialogResult.Primary)
                {
                    //update Observable collection
                    var src = updtFrm.patCtrl;
                    patientCollection[indx].PatientID = patCtrl.patientID;
                    patientCollection[indx].Fname = src.fname;
                    patientCollection[indx].Mname = src.mname;
                    patientCollection[indx].Lname = src.lname;
                    patientCollection[indx].Gender = src.gender;
                    patientCollection[indx].BirthDate = src.bday;
                    patientCollection[indx].ContactNumber = src.contactNum;
                    patientCollection[indx].EmailAddress = src.emailAdd;
                    patientCollection[indx].UserAddress = src.userAddress;
                }
            }
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            //<summary>
                //view delete patient
            //</summary>
        }




        //#end of class
    }
}
