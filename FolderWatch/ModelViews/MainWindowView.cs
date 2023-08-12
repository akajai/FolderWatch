using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FolderWatch.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace FolderWatch.ModelViews
{
    public partial class MainWindowView: ObservableObject
    {
        //public IAsyncRelayCommand? LoadDataCommand { get; }
        [ObservableProperty]
        private ObservableCollection<Person> persons;
        [ObservableProperty]
        private ObservableCollection<string> sortCountryNames= new ObservableCollection<string>() ;
        [ObservableProperty]
        private ObservableCollection<Country> countries= new ObservableCollection<Country>();
        public MainWindowView()
        {
            sortCountryNames = new ObservableCollection<string>();
            sortCountryNames.Add("Country");
            sortCountryNames.Add("Name");
        }
        [RelayCommand]
        public void ComboBoxSelectionChanged()
        {

        }
        [RelayCommand]
        private async Task ConnectAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                var lines = await File.ReadAllLinesAsync(openFileDialog.FileName);
                var personsList = new List<Person>();

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    personsList.Add(new Person
                    {
                        Name = data[0],
                        Country = data[1],
                        PhoneNumber = data[2]
                    });
                }

                Persons = new ObservableCollection<Person>(personsList);
                var countryList=personsList.Select(person => new  { country = person.Country}).ToList().Distinct();
                countryList.ToList().ForEach(c => {
                    Country country1 = new Country() { Name = c.country, IsChecked = true };
                    countries.Add(country1);
                });
                OnPropertyChanged(nameof(Countries));
                OnPropertyChanged(nameof(Persons));
                OnPropertyChanged(nameof(SortCountryNames));
                
            }
        }
    }
}
