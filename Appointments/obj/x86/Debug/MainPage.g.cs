﻿#pragma checksum "C:\Users\CASTILLO\source\repos\Appointments\Appointments\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DA7250CC32F3F5B1EC1B76845467EE4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Appointments
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // MainPage.xaml line 28
                {
                    this.calAppointments = (global::Windows.UI.Xaml.Controls.CalendarView)(target);
                    ((global::Windows.UI.Xaml.Controls.CalendarView)this.calAppointments).CalendarViewDayItemChanging += this.renderAppointments;
                    ((global::Windows.UI.Xaml.Controls.CalendarView)this.calAppointments).SelectedDatesChanged += this.CalendarView_SelectedDatesChanged;
                }
                break;
            case 4: // MainPage.xaml line 49
                {
                    global::Windows.UI.Xaml.Controls.ListView element4 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)element4).SelectionChanged += this.lstClick;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
