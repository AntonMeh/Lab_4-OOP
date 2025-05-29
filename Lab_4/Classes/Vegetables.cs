using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_4.Classes
{
    public class Vegetables : INotifyPropertyChanged
    {
        private string _name;
        private string _countryOfOrigin;
        private string _city;
        private int _season;

        public Vegetables(string name, string countryOfOrigin, string city, int season)
        {
            Name = name;
            CountryOfOrigin = countryOfOrigin;
            City = city;
            Season = season;
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));     
            }
        }
        public string CountryOfOrigin
        {
            get => _countryOfOrigin;
            set
            {
                _countryOfOrigin = value;
                OnPropertyChanged(nameof(CountryOfOrigin));
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public int Season
        {
            get => _season;
            set
            {
                _season = value;
                OnPropertyChanged(nameof(Season));
            }
        }
        public override string ToString()
        {
            return $"Name: {Name}, Country: {CountryOfOrigin}, Season: {Season}";
        }

        public string DisplayString => ToString();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToString)));
        }
    }
}
