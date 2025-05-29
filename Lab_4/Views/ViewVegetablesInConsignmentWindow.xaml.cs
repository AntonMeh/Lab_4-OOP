using Lab_4.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab_4.Views
{   
    public partial class ViewVegetablesInConsignmentWindow : Window
    {
        private Vegetables _vegetables;

        public ViewVegetablesInConsignmentWindow(Vegetables vegetables)
        {
            InitializeComponent();

            _vegetables = vegetables ?? throw new ArgumentNullException(nameof(vegetables));

            PopulateFields();
        }
        private void PopulateFields()
        {
            vegetableNameTextBlock.Text = _vegetables.Name;
            countryOfOriginTextBlock.Text = _vegetables.CountryOfOrigin;
            cityTextBlock.Text = _vegetables.City;
            seasonTextBlock.Text = _vegetables.Season.ToString();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
