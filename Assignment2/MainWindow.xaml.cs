using Assignment2.FileManagers;
using Assignment2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment2
{
    

    public partial class MainWindow : Window //sparar ner till json och säger vart vi vill spara den och namn
    {
        private FileManager _fileManager = new FileManager();
        private ObservableCollection<Contact> _contacts;
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\WPF.json";



        public MainWindow()// Här skapar vi något i listan.
        {
            InitializeComponent();
            try { _contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(_fileManager.Read(_filePath));}
            catch { }
            _contacts = new ObservableCollection<Contact>();
            lv_CONTACTS.ItemsSource = _contacts;    
        }


        public void btn_ADD_Click(object sender, RoutedEventArgs e) //Läggs direkt in i listan, vi gör alltså inga variabler (Knappen uppdaterar listan) 
        {

            var Contact = _contacts.FirstOrDefault(x=> x.PhoneNumber == tb_PhoneNumber.Text); // kollar om det redan finns en kontakt med samma telefonnummer och lägger annars till en
            if (Contact == null)
            {
                _contacts.Add(new Contact
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    StreetAddress = tb_StreetAddress.Text,
                    PhoneNumber = tb_PhoneNumber.Text,
                });
                _fileManager.save(_filePath, JsonConvert.SerializeObject(_contacts));
            }
            else
            {
                MessageBox.Show("Contact already exists");
            }

            // Rensar fälten vi skriver inom, går även att göra en Method och tillkalla på detta. 
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_StreetAddress.Text = "";
            tb_PhoneNumber.Text = "";
        }


        public void btn_Remove_Click(object sender, RoutedEventArgs e) // tar bort en kontakt men en egen knapp
        {

            var button = sender as Button;
            var contact = (Contact)button!.DataContext;
            _contacts.Remove(contact);

            _fileManager.save(_filePath, JsonConvert.SerializeObject(_contacts));
        }

        public void lv_CONTACTS_SelectionChanged(object sender, SelectionChangedEventArgs e) // Fyller i fälten igen och listar upp den personen du klickar på
        {
            try 
            { 
             if (lv_CONTACTS.SelectedItem != null){ 
                

              var contact = (Contact)lv_CONTACTS.SelectedItems[0]!;
              tb_FirstName.Text = contact.FirstName;
              tb_LastName.Text = contact.LastName;
              tb_StreetAddress.Text = contact.StreetAddress;
              tb_PhoneNumber.Text = contact.PhoneNumber;
             }

            } catch { }
        }

        public void btn_Update_Click(object sender, RoutedEventArgs e) // Updaterar en kontakt med Index
        {

            var contact = (Contact)lv_CONTACTS.SelectedItems[0]!;
            var index = _contacts.IndexOf(contact);
            _contacts[index].FirstName = tb_FirstName.Text;
            _contacts[index].LastName = tb_LastName.Text;
            _contacts[index].StreetAddress = tb_StreetAddress.Text;
            _contacts[index].PhoneNumber = tb_PhoneNumber.Text;
            lv_CONTACTS.Items.Refresh();

            _fileManager.save(_filePath, JsonConvert.SerializeObject(_contacts));
        }
    }
}
