using System;
using System.Collections.Generic;
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
    public sealed partial class AppointmentDetails : ContentDialog
    {
        public DataTable dtDetails = new DataTable();

        public AppointmentDetails()
        {
            this.InitializeComponent();
        }

        public AppointmentDetails(AppointmentModel data)
        {
            this.InitializeComponent();
            txbTitle.Text = data.title;
            txbDate.Text = data.date;
            txbTime.Text = data.time;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void btnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
