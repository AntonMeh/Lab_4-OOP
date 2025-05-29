using System;
using System.ComponentModel; 
using System.Windows;
using System.Windows.Controls;
using Lab_4.Classes; 
using Lab_4.Enum;    

namespace Lab_4
{
    public partial class AddConsignmentWindow : Window, INotifyPropertyChanged
    {
        public ConsignmentOfGoods NewConsignment { get; private set; }

        public AddConsignmentWindow()
        {
            InitializeComponent();
            this.DataContext = this; 

            deliveryDatePicker.SelectedDate = DateTime.Today;
            deliveryTypeComboBox.SelectedIndex = 0; 
        }

        private void AddBatchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = vegetableNameTextBox.Text;
                string country = countryOfOriginTextBox.Text;
                string city = cityTextBox.Text;
                int season = int.Parse(seasonTextBox.Text);

                Vegetables newVegetable = new Vegetables(name, country, city, season);

                Delivery deliveryType = (Delivery)deliveryTypeComboBox.SelectedItem;
                int quantity = int.Parse(quantityTextBox.Text);
                int pricePerOne = int.Parse(pricePerOneTextBox.Text);
                int priceForTransport = int.Parse(priceForTransportTextBox.Text);
                DateTime deliveryDate = deliveryDatePicker.SelectedDate ?? DateTime.Today; 

                NewConsignment = new ConsignmentOfGoods(newVegetable, deliveryType, quantity, pricePerOne, priceForTransport, deliveryDate);

                this.DialogResult = true;
                this.Close(); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Quantity, Price Per Unit, Transport Cost, and Season.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}