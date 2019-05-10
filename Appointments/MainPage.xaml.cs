using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Appointments
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public AppointmentController appCtrl = new AppointmentController();

        public DataTable dtAppointments = new DataTable();
        public MainPage()
        {
            initializeRecords();
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        private void initializeRecords()
        {
            //loading values from sample db
            dtAppointments = appCtrl.getAppointments();
            var recCount = dtAppointments.Rows.Count;

            ////static values
            //dtAppointments.Columns.Add("AppType", typeof(string));
            //dtAppointments.Columns.Add("DateStart", typeof(string));
            //dtAppointments.Columns.Add("Time", typeof(string));

            ////add records
            //String[] titles = new string[] { "title1", "title2", "title3", "title4" };
            //String[] appDate = new string[] { "5-26-2019", "5-26-2019", "5-27-2019", "5-28-2019" };
            //String[] appTime = new string[] { "9:00 am", "10:00 am", "10:30 am", "10:35 am" };

            //for (int i = 0; i < 4; i++)
            //{
            //    DataRow newRow = dtAppointments.NewRow();
            //    newRow["AppType"] = titles[i];
            //    newRow["DateStart"] = appDate[i];
            //    newRow["Time"] = appTime[i];
            //    dtAppointments.Rows.Add(newRow);
            //}
        }

        private void renderAppointments(CalendarView sender,
                                   CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Phase == 0)
            {
                //<Render basic items of the calendar>
                // Register callback for next phase.
                args.RegisterUpdateCallback(renderAppointments);

            }
            else if(args.Phase == 1)
            {
                //Blackout dates past current date 
                if (args.Item.Date < DateTimeOffset.Now.AddDays(-1)) 
                {
                    args.Item.IsBlackout = true;
                }
                // Register callback for next phase.
                args.RegisterUpdateCallback(renderAppointments);
            }
            else if (args.Phase == 2)
            {
                //List<Windows.UI.Color> densityColors = new List<Windows.UI.Color>();
                
                if (args.Item.Date > DateTimeOffset.Now.AddDays(-1))
                {

                    var listView = FindFirstChildOfType<ListView>(args.Item);   //find the listview control inside the calendarViewDayItem
                    listView.Items.Clear(); 

                    foreach (DataRow row in dtAppointments.Rows)
                    {
                        string itemDate = Convert.ToDateTime(args.Item.Date.ToString()).ToShortDateString();
                        string recordDate = Convert.ToDateTime(row["DateStart"]).ToShortDateString();
                        if (itemDate == recordDate)
                        {
                            //densityColors.Add(Colors.Red);
                            
                            listView.Items.Add(row["AppType"].ToString());   //add appointment items to listview
                        }
                    }
                    //args.Item.SetDensityColors(densityColors);
                }

            }
        }


        //finds the first type of control inside a parent control\
        //this populates the listview inside the calendarViewDayItem
        private T FindFirstChildOfType<T>(DependencyObject control) where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(control);
            for (int childIndex = 0; childIndex < childrenCount; childIndex++)
            {
                var child = VisualTreeHelper.GetChild(control, childIndex);
                if (child is T typedChild)
                {
                    return typedChild;
                }
            }
            return null;
        }

        private void lstClick(object sender, SelectionChangedEventArgs e)
        {
            //<summary>
                //details view on item click on the date
            //</summary>
            //ListView lst = (ListView)sender as ListView;
            //var a = lst.SelectedItem.ToString();
            //foreach(DataRow row in dtAppointments.Rows)
            //{
            //    if(row["Title"].ToString() == a)
            //    {
            //        appCtrl.title = a;
            //        appCtrl.date = row["Date"].ToString();
            //        appCtrl.time = row["Time"].ToString();
            //        AppointmentDetails app = new AppointmentDetails(appCtrl);
            //        asd();
            //        async void asd()
            //        {
            //            ContentDialogResult result = await app.ShowAsync();
            //        }
                    
            //    }
            //}  
                
        }


        private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            try
            {
                var selectedDate = Convert.ToDateTime(calAppointments.SelectedDates[0].ToString()).ToShortDateString();
                openAppointmentOnDate(selectedDate);
            }
            catch (Exception err)
            {
                //if same date is selected again, an error will occur
                //passing the same value of selected date present in the model
                openAppointmentOnDate(appCtrl.selectedDate);
            }
            
            
        }


        private async void openAppointmentOnDate(String selectedDate)
        {
            //<summary>
                //creates a new window /multiwindow view for daily appointments
            //</summary>
            var viewId = 0;
            var newView = CoreApplication.CreateNewView();
            await newView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    appCtrl.selectedDate = selectedDate;
                    var frame = new Frame();
                    frame.Navigate(typeof(DateAppointmentsList), appCtrl);
                    Window.Current.Content = frame;
                    Window.Current.Activate();
                    viewId = ApplicationView.GetForCurrentView().Id;                    
                });

            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }
       
        //#end of class
    }
}
