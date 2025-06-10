using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Classes;

namespace Lab_4.Views
{
    public partial class AddCompositionWindow : Window
    {
        public Composition NewComposition { get; private set; }
        private bool _cancelled = false; 

        public AddCompositionWindow()
        {
            InitializeComponent();

            NewComposition = new Composition();
            this.DataContext = NewComposition;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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
            return !Validation.GetHasError(roomNumTextBox) &&
                   !Validation.GetHasError(roomPriceTextBox);
        }
    }
}
