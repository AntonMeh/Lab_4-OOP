using Lab_4.Classes;
using Lab_4.Enum;
using System;
using System.ComponentModel; 
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Views
{
    public partial class EditConsignmentWindow : Window
    {
        private ConsignmentOfGoods _originalConsignment;
        public ConsignmentOfGoods _consignmentToEdit;

        public EditConsignmentWindow(ConsignmentOfGoods consignment)
        {
            InitializeComponent();
            _originalConsignment = consignment ?? throw new ArgumentNullException(nameof(consignment));
            _consignmentToEdit = _originalConsignment.Clone();
            this.DataContext = _consignmentToEdit;

            deliveryTypeComboBox.ItemsSource = System.Enum.GetValues(typeof(Delivery));

            deliveryTypeComboBox.SelectedItem = _consignmentToEdit.TypeOfDelivery;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidation())
            {
                _originalConsignment.Vegetables.Name = _consignmentToEdit.Vegetables.Name;
                _originalConsignment.Vegetables.CountryOfOrigin = _consignmentToEdit.Vegetables.CountryOfOrigin;
                _originalConsignment.Vegetables.City = _consignmentToEdit.Vegetables.City;
                _originalConsignment.Vegetables.Season = _consignmentToEdit.Vegetables.Season;

                _originalConsignment.TypeOfDelivery = _consignmentToEdit.TypeOfDelivery;
                _originalConsignment.Quantity = _consignmentToEdit.Quantity;
                _originalConsignment.PriceForOne = _consignmentToEdit.PriceForOne;
                _originalConsignment.PriceForTransport = _consignmentToEdit.PriceForTransport;
                _originalConsignment.DateOfDelivery = _consignmentToEdit.DateOfDelivery;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("The entered data is invalid. Please correct the errors..", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool CheckValidation()
        {
            bool uiHasErrors =
                Validation.GetHasError(vegetableNameTextBox) ||
                Validation.GetHasError(countryOfOriginTextBox) ||
                Validation.GetHasError(cityTextBox) ||
                Validation.GetHasError(seasonTextBox) ||
                Validation.GetHasError(quantityTextBox) ||
                Validation.GetHasError(pricePerOneTextBox) ||
                Validation.GetHasError(priceForTransportTextBox) ||
                Validation.GetHasError(deliveryDatePicker);

            bool modelHasErrors = !string.IsNullOrEmpty(((IDataErrorInfo)_consignmentToEdit).Error);

            return !uiHasErrors && !modelHasErrors;
        }
    }
}