using Lab_4.DTO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_4.Classes
{
    public class Vegetables : INotifyPropertyChanged, IDataErrorInfo
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
        public Vegetables() { }
        public Vegetables Clone()
        {
            return new Vegetables
            {
                Name = this.Name,
                CountryOfOrigin = this.CountryOfOrigin,
                City = this.City,
                Season = this.Season
            };
        }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [RegularExpression(@"[\p{Lu}][\p{Ll}]+([' \-][\p{Lu}][\p{Ll}]+)*", ErrorMessage = "The name must start with a capital letter and contain only letters, spaces, hyphens, and apostrophes.")]
        [Display(Name = "Vegetable Name")]
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
        [RegularExpression(@"[\p{Lu}][\p{Ll}]+([' \-][\p{Lu}][\p{Ll}]+)*", ErrorMessage = "The country must start with a capital letter and contain only letters, spaces, hyphens, and apostrophes.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 50 characters.")]
        [Display(Name = "Country of Origin")]
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
        [RegularExpression(@"[\p{Lu}][\p{Ll}]+([' \-][\p{Lu}][\p{Ll}]+)*", ErrorMessage = "The city must start with a capital letter and contain only letters, spaces, hyphens, and apostrophes.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "City must be between 2 and 50 characters.")]
        [Display(Name = "City")]
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
        [Display(Name = "Season Year")]
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

        public string Error
        {
            get
            {
                var context = new ValidationContext(this);
                var results = new List<ValidationResult>();

                Validator.TryValidateObject(this, context, results, true);

                string seasonError = this[nameof(Season)];
                if (!string.IsNullOrEmpty(seasonError))
                {
                    results.Add(new ValidationResult(seasonError, new[] { nameof(Season) }));
                }

                return string.Join("\n", results.Select(r => r.ErrorMessage));
            }
        }

        public string this[string columnName]
        {
            get
            {
                var prop = GetType().GetProperty(columnName);
                if (prop == null) return null;

                if (columnName == nameof(Name) || columnName == nameof(CountryOfOrigin) || columnName == nameof(City))
                {
                    var value = prop.GetValue(this);
                    var context = new ValidationContext(this) { MemberName = columnName };
                    var results = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateProperty(value, context, results);
                    return isValid ? string.Empty : results.First().ErrorMessage;
                }
                else if (columnName == nameof(Season))
                {
                    if (Season < 2010 || Season > 2026)
                    {
                        return "The season year must be between 2010 and 2026.";
                    }
                }
                return null;
            }
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
                Season = dto.Season,
            };
        }
    }
}