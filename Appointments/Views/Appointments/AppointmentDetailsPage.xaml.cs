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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Appointments
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppointmentDetailsPage : Page
    {
        
        public AppointmentController appCtrl = new AppointmentController();

        public AppointmentDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //<summary>
                //load data sent to page view frame.Navigate() for display or manipulation
            //</summary>
            base.OnNavigatedTo(e);
            var parameters = (AppointmentController)e.Parameter;
            //set values to respective fields
            appCtrl.appID = parameters.appID;
            txtType.Text = appCtrl.appType = parameters.appType;
            txtStatus.Text = appCtrl.status = parameters.status;
            txtDateStart.Text = appCtrl.dateStart = parameters.dateStart;
            txtDateEnd.Text = appCtrl.dateEnd = parameters.dateEnd;
            txtTimeStart.Text = appCtrl.timeStart = parameters.timeStart;
            txtTimeEnd.Text = appCtrl.timeEnd = parameters.timeEnd;
            txtLocation.Text = appCtrl.appLocation = parameters.appLocation;
            txtNote.Text = appCtrl.appNote = parameters.appNote;
            txtName.Text = appCtrl.patientName = parameters.patientName;
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            if(sender == btnEdit)
            {
                AppointmentUpdateForm updateFrm = new AppointmentUpdateForm(appCtrl);
                frmShow();

                async void frmShow()
                {
                    ContentDialogResult result = await updateFrm.ShowAsync();
                    if(result == ContentDialogResult.Primary)
                    {
                        
                    }
                }
            }
        }
    }
}
