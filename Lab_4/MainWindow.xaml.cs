using Lab_4.Classes;
using Lab_4.Enum;
using Lab_4.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Composition> _compositions;
        public ObservableCollection<Composition> Compositions
        {
            get { return _compositions; }
            set
            {
                _compositions = value;
                OnPropertyChanged(nameof(Compositions));
            }
        }

        public MainWindow()
        {            
            InitializeComponent();

            Compositions = new ObservableCollection<Composition>();

            var carrot = new Vegetables("Carrot", "Ukraine", "Kyiv", 2024);
            var potato = new Vegetables("Potato", "Poland", "Warsaw", 2024);
            var tomato = new Vegetables("Tomato", "Turkey", "Antalya", 2025);

            var consignment1 = new ConsignmentOfGoods(carrot, Delivery.Supplier, 50, 75, 15, DateTime.Now.AddDays(-7));
            var consignment2 = new ConsignmentOfGoods(potato, Delivery.Intermediary, 250, 4, 20, DateTime.Now.AddDays(-10));
            var consignment3 = new ConsignmentOfGoods(tomato, Delivery.OwnFunds, 150, 10, 5, DateTime.Now.AddDays(-3));
            var consignment4 = new ConsignmentOfGoods(carrot, Delivery.Supplier, 75, 2, 12, DateTime.Now.AddDays(-1));

            var warehouse1 = new Composition(101, 500);
            warehouse1.AcceptProductBatch(consignment1);
            warehouse1.AcceptProductBatch(consignment3);

            var warehouse2 = new Composition(202, 750);
            warehouse2.AcceptProductBatch(consignment2);
            warehouse2.AcceptProductBatch(consignment4);

            var warehouse3 = new Composition(303, 300);

            Compositions.Add(warehouse1);
            Compositions.Add(warehouse2);
            Compositions.Add(warehouse3);

            this.DataContext = this;

            UpdateButtonsBasedOnSelection();

        }

        private void CompositionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonsBasedOnSelection();
        }

        private void UpdateButtonsBasedOnSelection()
        {
            bool hasSelection = compositionListView.SelectedItem != null;

            if (viewProductBatchesBtn != null)
                viewProductBatchesBtn.IsEnabled = hasSelection;
            if (editCompositionBtn != null)
                editCompositionBtn.IsEnabled = hasSelection;
            if (deleteCompositionBtn != null) 
                deleteCompositionBtn.IsEnabled = hasSelection;
        }

        private void ShowProductBatches_Click(object sender, RoutedEventArgs e)
        {
            if (compositionListView.SelectedItem is Composition selectedComposition)
            {
                var viewBatchesWindow = new ViewBatches(selectedComposition);
                viewBatchesWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a composition to view the batches of goods.", "Selection required");
            }
        }

        private void EditComposition_Click(object sender, RoutedEventArgs e)
        {
            if (compositionListView.SelectedItem is Composition selectedComposition)
            {
                var editWindow = new EditCompositionWindow(selectedComposition);

                if (editWindow.ShowDialog() == true)
                {
                }
            }
            else
            {
                MessageBox.Show("Please select a composition to edit.", "Selection required");
            }
        }

        private void NewComposition_Click(object sender, RoutedEventArgs e)
        {
            var addCompositionWindow = new AddCompositionWindow();

            if (addCompositionWindow.ShowDialog() == true)
            {
                if (addCompositionWindow.NewComposition != null)
                {
                    Compositions.Add(addCompositionWindow.NewComposition);
                    compositionListView.SelectedItem = addCompositionWindow.NewComposition; 
                }
            }
        }
        private void DeleteComposition_Click(object sender, RoutedEventArgs e)
        {
            if (compositionListView.SelectedItem is Composition selectedComposition)
            {
                var result = MessageBox.Show($"Are you sure you want to delete Composition (Room No. {selectedComposition.NumOfRoom})?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Compositions.Remove(selectedComposition);
                }
            }
            else
            {
                MessageBox.Show("Please select a composition to delete.", "Selection required");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}