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
            Compositions = App.LoadAllCompositions();

            this.DataContext = this;
            this.Closed += MainWindow_Closed;

            UpdateButtonsBasedOnSelection();

        }
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            App.SaveAllCompositions(Compositions);
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