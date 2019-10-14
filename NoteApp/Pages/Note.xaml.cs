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
        private ObservableCollection<NoteItem> listNotezzz { get; set; }
        public Note()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var files = storageFolder.GetFilesAsync().GetAwaiter().GetResult();
            if (files == null)
            {
                return;
            }
            this.listNotezzz = new ObservableCollection<NoteItem>();
            for (int i = 0; i < files.Count; i++)
            {
                //var a = new Button();
                //a.Content = files[i].Name;
                //a.Click +=  (s, e) => {
                //    var button = s as Button;
                //    var note = ReadNoteFromLocalStorage(button.Content.ToString());
                //    this.noteEdit.Text = note;
                //};
                //this.menu.Children.Add(a);
                this.listNotezzz.Add(new NoteItem()
                {
                    nameFile = files[i].Name

                });
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
            this.listNotezzz.Add(new NoteItem()
            {
                nameFile = name+".txt"

            });
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
                Windows.Storage.StorageFile noteFile = storageFolder.GetFileAsync(fileName).GetAwaiter().GetResult();
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
            var selectItem = sender as TextBox;
            var selectI = listNote.SelectedItem as NoteItem;
            var note = ReadNoteFromLocalStorage(selectI.nameFile);
            this.noteEdit.Text = note;
        }
    }
}
