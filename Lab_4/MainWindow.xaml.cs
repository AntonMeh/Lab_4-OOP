using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Classes;
using Lab_4.Enum;

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

        private Composition _selectedComposition;
        public Composition SelectedComposition
        {
            get { return _selectedComposition; }
            set
            {
                _selectedComposition = value;
                OnPropertyChanged(nameof(SelectedComposition));
                viewProductBatchesBtn.IsEnabled = value != null;
                editCompositionBtn.IsEnabled = value != null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Compositions = new ObservableCollection<Composition>();

            var carrot = new Vegetables("Carrot", "Ukraine", "Kyiv", 2024);
            var potato = new Vegetables("Potato", "Poland", "Warsaw", 2024);
            var tomato = new Vegetables("Tomato", "Turkey", "Antalya", 2025);

            var consignment1 = new ConsignmentOfGoods(carrot, Delivery.Supplier, 50, 0.75m, 15.00m, DateTime.Now.AddDays(-7)); 
            var consignment2 = new ConsignmentOfGoods(potato, Delivery.Intermediary, 250, 0.40m, 20.50m, DateTime.Now.AddDays(-10));
            var consignment3 = new ConsignmentOfGoods(tomato, Delivery.OwnFunds, 150, 1.10m, 5.00m, DateTime.Now.AddDays(-3));
            var consignment4 = new ConsignmentOfGoods(carrot, Delivery.Supplier, 75, 0.80m, 12.00m, DateTime.Now.AddDays(-1));

            var warehouse1 = new Composition(101, 500.00m);
            warehouse1.AcceptProductBatch(consignment1);
            warehouse1.AcceptProductBatch(consignment3);

            var warehouse2 = new Composition(202, 750.00m);
            warehouse2.AcceptProductBatch(consignment2);
            warehouse2.AcceptProductBatch(consignment4);

            var warehouse3 = new Composition(303, 300.00m);

            Compositions.Add(warehouse1);
            Compositions.Add(warehouse2);
            Compositions.Add(warehouse3);

            this.DataContext = this;
        }

        private void CompositionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedComposition = compositionListView.SelectedItem as Composition;
        }

        private void ShowProductBatches_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComposition != null)
            {
                MessageBox.Show($"Displaying product batches for Room No: {SelectedComposition.NumOfRoom}\n" +
                                $"Total batches: {SelectedComposition.Info.Count}\n" +
                                $"Summary: {SelectedComposition.ToShortString()}",
                                "Product Batches Details");
            }
            else
            {
                MessageBox.Show("Please select a warehouse composition to view its product batches.", "Selection Required");
            }
        }

        private void EditComposition_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedComposition != null)
            {
                MessageBox.Show($"Editing Composition for Room No: {SelectedComposition.NumOfRoom}", "Edit Composition");
            }
            else
            {
                MessageBox.Show("Please select a warehouse composition to edit.", "Selection Required");
            }
        }

        private void NewComposition_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Creating a new Composition", "New Composition");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}