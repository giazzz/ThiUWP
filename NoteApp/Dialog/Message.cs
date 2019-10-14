using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NoteApp.Dialog
{
    class Message
    {
        public static async void SuccessfullDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Successful!",
                Content = "Saved.",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        public static async void FalseDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Sorry!",
                Content = "Empty",
                CloseButtonText = "OK"
            };
            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
