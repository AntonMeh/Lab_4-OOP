using Lab_4.DTO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public Vegetables() 
        {

        }
        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));     
            }
        }
        [Required(ErrorMessage = "Country of origin is required.")]
        public string CountryOfOrigin
        {
            get => _countryOfOrigin;
            set
            {
                _countryOfOrigin = value;
                OnPropertyChanged(nameof(CountryOfOrigin));
            }
        }

        [Required(ErrorMessage = "City is required.")]
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        
        [Required(ErrorMessage = "Season is required.")]
        [Range(1990, 2025, ErrorMessage = "Season must be between 1990 and 2025.")]
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
        public VegetablesDTO ToDTO()
        {
            return new VegetablesDTO
            {
                Name = this.Name,
                CountryOfOrigin = this.CountryOfOrigin,
                City = this.City,
                Season = this.Season
            };
        }

        public static Vegetables FromDTO(VegetablesDTO dto)
        {
            if (dto == null) return null;
            return new Vegetables
            {
                Name = dto.Name,
                CountryOfOrigin = dto.CountryOfOrigin,
                City = dto.City,
                Season = dto.Season
            };
        }
    }
}
