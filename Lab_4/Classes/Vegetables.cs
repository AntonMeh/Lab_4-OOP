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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Vegetable name cannot be empty or whitespace.", nameof(Name));
                }
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string CountryOfOrigin
        {
            get => _countryOfOrigin;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Country of origin cannot be empty or whitespace.", nameof(CountryOfOrigin));
                }
                if (_countryOfOrigin != value)
                {
                    _countryOfOrigin = value;
                    OnPropertyChanged(nameof(CountryOfOrigin));
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City cannot be empty or whitespace.", nameof(City));
                }
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }
        public int Season
        {
            get => _season;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Season must be a positive number.", nameof(Season));
                }
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged(nameof(Season));
                }
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
