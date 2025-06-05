using System.Windows;
using System.Windows.Controls;
using Lab_4.Classes;
using Lab_4.Enum; 

namespace Lab_4.Views
{
    public partial class ViewBatches : Window
    {
        private Composition _composition;

        public ViewBatches(Composition composition)
        {
            InitializeComponent();
            _composition = composition;
            this.DataContext = _composition;

            productBatchesListView.ItemsSource = _composition.Info;
        }

        private void AddBatch_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddConsignmentWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newBatch = addWindow.NewConsignment;
                if (newBatch != null)
                {
                    _composition.AcceptProductBatch(newBatch);
                }
            }
        }

        private void EditBatch_Click(object sender, RoutedEventArgs e)
        {
            if (productBatchesListView.SelectedItem is ConsignmentOfGoods selectedBatch)
            {
                var editWindow = new EditConsignmentWindow(selectedBatch);
                if (editWindow.ShowDialog() == true)
                {
                }
            }
        }

        private void RemoveBatch_Click(object sender, RoutedEventArgs e)
        {
            if (productBatchesListView.SelectedItem is ConsignmentOfGoods selectedBatch)
            {
                var result = MessageBox.Show("Are you sure you want to remove this batch?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _composition.WriteOffProductBatch(selectedBatch);
                }
            }
        }

        private void EditComposition_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCompositionWindow(_composition);
            if (editWindow.ShowDialog() == true)
            {
            }
        }

        private void ViewContents_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}