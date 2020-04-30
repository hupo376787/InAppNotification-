using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestInAppNotification
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static ObservableCollection<DataGridDataItem> _items;

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await GetDataAsync();
        }

        public async Task<IEnumerable<DataGridDataItem>> GetDataAsync()
        {
            var uri = new Uri($"ms-appx:///Assets/mtns.csv");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            IRandomAccessStreamWithContentType randomStream = await file.OpenReadAsync();
            _items = new ObservableCollection<DataGridDataItem>();

            using (StreamReader sr = new StreamReader(randomStream.AsStreamForRead()))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    _items.Add(
                        new DataGridDataItem()
                        {
                            Rank = uint.Parse(values[0]),
                            Mountain = values[1],
                            Height_m = uint.Parse(values[2]),
                            Range = values[3],
                            Coordinates = values[4],
                            Prominence = uint.Parse(values[5]),
                            Parent_mountain = values[6],
                            First_ascent = uint.Parse(values[7]),
                            Ascents = values[8]
                        });
                }
            }

            return _items;
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MessageForUser.Show("Finally", 3000);
        }
    }

    
}
