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
            DataContext = _composition;

            compositionHeader.Text = $"Composition №{_composition.NumOfRoom} - Room Price: {_composition.RoomPrice} $";
        }

        private void ProductBatchesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection = productBatchesListView.SelectedItem != null;
            removeBatchBtn.IsEnabled = hasSelection;
            editBatchBtn.IsEnabled = hasSelection;
            viewContentsBtn.IsEnabled = hasSelection;
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
                    productBatchesListView.ItemsSource = null;
                    productBatchesListView.ItemsSource = _composition.Info;
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
                    productBatchesListView.ItemsSource = null;
                    productBatchesListView.ItemsSource = _composition.Info;
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

                    productBatchesListView.ItemsSource = null;
                    productBatchesListView.ItemsSource = _composition.Info;
                }
            }
        }

        private void ViewContents_Click(object sender, RoutedEventArgs e)
        {
            if (productBatchesListView.SelectedItem is ConsignmentOfGoods selectedBatch)
            {
                var viewWindow = new ViewVegetablesInConsignmentWindow(selectedBatch.Vegetables);
                viewWindow.ShowDialog();
            }
        }
    }
}