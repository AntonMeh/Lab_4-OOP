using Lab_4.Classes;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab_4.Views
{
    public partial class EditCompositionWindow : Window
    {
        private Composition _originalComposition;
        public Composition _compositionToEdit;

        public EditCompositionWindow(Composition composition)
        {
            InitializeComponent();
            _originalComposition = composition ?? throw new ArgumentNullException(nameof(composition));
            _compositionToEdit = _originalComposition.Clone(); 
            this.DataContext = _compositionToEdit; 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression beNum = roomNumTextBox.GetBindingExpression(TextBox.TextProperty);
            beNum?.UpdateSource();

            BindingExpression bePrice = roomPriceTextBox.GetBindingExpression(TextBox.TextProperty);
            bePrice?.UpdateSource();

            if (CheckValidation()) 
            {
                _originalComposition.NumOfRoom = _compositionToEdit.NumOfRoom;
                _originalComposition.RoomPrice = _compositionToEdit.RoomPrice;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("The entered data is invalid. Please correct the errors.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool CheckValidation()
        {
            bool uiHasErrors = Validation.GetHasError(roomNumTextBox) ||
                               Validation.GetHasError(roomPriceTextBox);

            bool modelHasErrors = !string.IsNullOrEmpty(((IDataErrorInfo)_compositionToEdit).Error);

            return !uiHasErrors && !modelHasErrors;
        }
    }
}