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
        private bool _cancelled = false;

        public AddConsignmentWindow()
        {
            InitializeComponent();
            NewConsignment = new ConsignmentOfGoods();
            NewConsignment.Vegetables = new Vegetables();
            NewConsignment.DateOfDelivery = DateTime.Today;

            this.DataContext = this;

            deliveryTypeComboBox.ItemsSource = System.Enum.GetValues(typeof(Delivery));
            deliveryTypeComboBox.SelectedIndex = 0;
            deliveryDatePicker.SelectedDate = DateTime.Today;
        }

        private void AddBatchButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidation()) 
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Введені дані не валідні", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancelled = true; 
            this.DialogResult = false;
            this.Close();
        }
        private bool CheckValidation()
        {
            return !Validation.GetHasError(vegetableNameTextBox) &&
                   !Validation.GetHasError(countryOfOriginTextBox) &&
                   !Validation.GetHasError(cityTextBox) &&
                   !Validation.GetHasError(seasonTextBox) &&
                   !Validation.GetHasError(deliveryTypeComboBox) &&
                   !Validation.GetHasError(quantityTextBox) &&
                   !Validation.GetHasError(pricePerOneTextBox) &&
                   !Validation.GetHasError(priceForTransportTextBox) &&
                   !Validation.GetHasError(deliveryDatePicker);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}