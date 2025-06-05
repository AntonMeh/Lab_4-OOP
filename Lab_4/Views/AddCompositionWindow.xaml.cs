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
using Lab_4.Classes;

namespace Lab_4.Views
{
    public partial class AddCompositionWindow : Window
    {
        public Composition NewComposition { get; private set; }

        public AddCompositionWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(roomNumTextBox.Text, out int roomNum))
                {
                    throw new ArgumentException("Please enter a valid number for Room Number.");
                }
                if (!int.TryParse(roomPriceTextBox.Text, out int roomPrice))
                {
                    throw new ArgumentException("Please enter a valid number for Room Price.");
                }

                NewComposition = new Composition(roomNum, roomPrice);
                this.DialogResult = true; // Вказуємо, що дія виконана успішно
                this.Close();
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
            this.DialogResult = false; // Вказуємо, що дія скасована
            this.Close();
        }
    }
}
