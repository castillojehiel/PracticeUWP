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
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(sender.SelectedItem == navAppointments)
            {
                frmMain.Navigate(typeof(MainPage));
            }
            else if (sender.SelectedItem == navPatients)
            {
                frmMain.Navigate(typeof(PatientHome));
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //var control = args.InvokedItem.ToString();
            //if (control == "Appointments")
            //{
            //    frmMain.Navigate(typeof(MainPage));

            //}
            //else if(control == "Patients")
            //{
            //    frmMain.Navigate(typeof(PatientHome));
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navViewMainNav.SelectedItem = navPatients;
            frmMain.Navigate(typeof(PatientHome));
        }
    }
}
