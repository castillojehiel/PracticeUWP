﻿#pragma checksum "C:\Users\CASTILLO\source\repos\Appointments\Appointments\Views\Appointments\AppointmentUpdateForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1136BE2A44133FCCD0A2D4D290B250C7"
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
    partial class AppointmentUpdateForm : 
        global::Windows.UI.Xaml.Controls.ContentDialog, 
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
            case 1: // Views\Appointments\AppointmentUpdateForm.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.ContentDialog element1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).PrimaryButtonClick += this.ContentDialog_PrimaryButtonClick;
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).SecondaryButtonClick += this.ContentDialog_SecondaryButtonClick;
                    ((global::Windows.UI.Xaml.Controls.ContentDialog)element1).Loaded += this.ContentDialog_Loaded;
                }
                break;
            case 2: // Views\Appointments\AppointmentUpdateForm.xaml line 36
                {
                    this.cboAppType = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cboAppType).SelectionChanged += this.CboAppType_SelectionChanged;
                }
                break;
            case 3: // Views\Appointments\AppointmentUpdateForm.xaml line 62
                {
                    this.pckDteDateStart = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 4: // Views\Appointments\AppointmentUpdateForm.xaml line 65
                {
                    this.pckDteDateEnd = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 5: // Views\Appointments\AppointmentUpdateForm.xaml line 68
                {
                    this.pckTmeTimeStart = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 6: // Views\Appointments\AppointmentUpdateForm.xaml line 71
                {
                    this.pckTmeTimeEnd = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 7: // Views\Appointments\AppointmentUpdateForm.xaml line 74
                {
                    this.txtLocation = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // Views\Appointments\AppointmentUpdateForm.xaml line 77
                {
                    this.txtNote = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // Views\Appointments\AppointmentUpdateForm.xaml line 48
                {
                    this.txtName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // Views\Appointments\AppointmentUpdateForm.xaml line 49
                {
                    this.btnSearch = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnSearch).Click += this.btnClick;
                }
                break;
            case 11: // Views\Appointments\AppointmentUpdateForm.xaml line 54
                {
                    this.btnNewPatient = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnNewPatient).Click += this.btnClick;
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

