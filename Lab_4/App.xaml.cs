using Lab_4.Classes;
using Lab_4.DTO_s;
using Lab_4.Enum;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace Lab_4
{

    public partial class App : Application
    {
        private const string DataFilePath = "compositions_data.json";

        private static readonly JsonSerializerOptions _serializeOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        private static readonly JsonSerializerOptions _deserializeOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };

        public static void SaveAllCompositions(ObservableCollection<Composition> compositions)
        {
            try
            {
                List<CompositionDTO> compositionDtos = compositions.Select(c => c.ToDTO()).ToList();

                if (compositionDtos.Count > 0)
                {
                    string jsonString = JsonSerializer.Serialize(compositionDtos, _serializeOptions);
                    File.WriteAllText(DataFilePath, jsonString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static ObservableCollection<Composition> LoadAllCompositions()
        {
            var loadedCompositions = new ObservableCollection<Composition>();

            try
            {
                var compositionsJson = File.ReadAllText(DataFilePath);

                if (!string.IsNullOrEmpty(compositionsJson))
                {
                    var compositionDtos = JsonSerializer.Deserialize<List<CompositionDTO>>(compositionsJson, _deserializeOptions);

                    if (compositionDtos != null)
                    {
                        foreach (var dto in compositionDtos)
                        {
                            loadedCompositions.Add(Composition.FromDTO(dto));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from '{DataFilePath}': {ex.Message}\nInitializing with default data.", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (loadedCompositions.Count == 0)
            {
                MessageBox.Show("No data loaded. Initializing with a minimal default set.", "No Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                var defaultComposition = new Composition(1, 100);
                loadedCompositions.Add(defaultComposition);
            }

            return loadedCompositions;
        }

    }

}
