using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using NoteApp.Dialog;
using System.Collections.ObjectModel;
using NoteApp.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NoteApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Note : Page
    {
        ObservableCollection<String> list;
        public Note()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var files = storageFolder.GetFilesAsync().GetAwaiter().GetResult();
            if (files == null)
            {
                return;
            }
            for (int i = 0; i < files.Count; i++)
            {
                this.list.Add(files[i].Name);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = DateTime.Now.ToString("yyyy-MM-dd-H-mm");
            if (this.noteEdit.Text == null)
            {
                Message.FalseDialog();
            }
            SaveNoteToLocalStorage(this.noteEdit.Text, name);
            Message.SuccessfullDialog();
        }
        private void SaveNoteToLocalStorage(string note, string fileName)
        {
            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile noteFile =
                 storageFolder.CreateFileAsync(fileName + ".txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
            Debug.WriteLine(noteFile.Path);
            Windows.Storage.FileIO.WriteTextAsync(noteFile, note).GetAwaiter().GetResult();
        }
        public string ReadNoteFromLocalStorage(string fileName)
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile noteFile = storageFolder.GetFileAsync(fileName+".txt").GetAwaiter().GetResult();
                Debug.WriteLine(noteFile.Path);
                var note = Windows.Storage.FileIO.ReadTextAsync(noteFile).GetAwaiter().GetResult();
                return note;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectItem = sender as StackPanel;
            
            //ReadNoteFromLocalStorage(selectItem.Children.ElementAt(0).GetValue(Text);
        }
    }
}
