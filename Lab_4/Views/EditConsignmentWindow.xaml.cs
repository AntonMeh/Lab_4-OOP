using Lab_4.Classes;
using Lab_4.Enum; 
using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Views
{
    public partial class EditConsignmentWindow : Window
    {
        private ConsignmentOfGoods _consignment;

        public EditConsignmentWindow(ConsignmentOfGoods consignment)
        {
            InitializeComponent();
            _consignment = consignment;

            deliveryTypeComboBox.ItemsSource = System.Enum.GetValues(typeof(Delivery));

            PopulateFields();
        }

        private void PopulateFields()
        {
            vegetableNameTextBox.Text = _consignment.Vegetables.Name;
            countryOfOriginTextBox.Text = _consignment.Vegetables.CountryOfOrigin;
            cityTextBox.Text = _consignment.Vegetables.City;
            seasonTextBox.Text = _consignment.Vegetables.Season.ToString();

            deliveryTypeComboBox.SelectedItem = _consignment.TypeOfDelivery;

            quantityTextBox.Text = _consignment.Quantity.ToString();
            pricePerOneTextBox.Text = _consignment.PriceForOne.ToString();
            priceForTransportTextBox.Text = _consignment.PriceForTransport.ToString();
            deliveryDatePicker.SelectedDate = _consignment.DateOfDelivery;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _consignment.Vegetables.Name = vegetableNameTextBox.Text;
                _consignment.Vegetables.CountryOfOrigin = countryOfOriginTextBox.Text;
                _consignment.Vegetables.City = cityTextBox.Text;
                _consignment.Vegetables.Season = int.Parse(seasonTextBox.Text); 

                _consignment.TypeOfDelivery = (Delivery)deliveryTypeComboBox.SelectedItem;
                _consignment.Quantity = int.Parse(quantityTextBox.Text);
                _consignment.PriceForOne = int.Parse(pricePerOneTextBox.Text);
                _consignment.PriceForTransport = int.Parse(priceForTransportTextBox.Text);
                _consignment.DateOfDelivery = deliveryDatePicker.SelectedDate ?? DateTime.Today;

                this.DialogResult = true; 
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers in numeric fields (Quantity, Price, Transport, Season).", "Format Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Validation Error: {ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}