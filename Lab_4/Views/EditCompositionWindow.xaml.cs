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
    public partial class EditCompositionWindow : Window
    {
        private Composition _compositionToEdit;

        public EditCompositionWindow(Composition composition)
        {
            InitializeComponent();
            _compositionToEdit = composition;

            roomNumTextBox.Text = _compositionToEdit.NumOfRoom.ToString();
            roomPriceTextBox.Text = _compositionToEdit.RoomPrice.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(roomNumTextBox.Text) || !int.TryParse(roomNumTextBox.Text, out int roomNum))
                {
                    throw new ArgumentException("Please enter a valid number for Room Number.");
                }

                if (string.IsNullOrWhiteSpace(roomPriceTextBox.Text) || !int.TryParse(roomPriceTextBox.Text, out int roomPrice))
                {
                    throw new ArgumentException("Please enter a valid number for Room Price.");
                }

                _compositionToEdit.NumOfRoom = roomNum;
                _compositionToEdit.RoomPrice = roomPrice;

                DialogResult = true;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
