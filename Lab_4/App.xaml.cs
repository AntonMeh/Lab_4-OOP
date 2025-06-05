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
        private static readonly JsonSerializerOptions _serializeOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        private static readonly JsonSerializerOptions _deserializeOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };

        private const string DataFilePath = "compositions_data.json"; 

    }

}
