using System.Threading.Tasks;
using System.Windows;

namespace SampleProvider.Views
{
    /// <summary>
    /// Interaction logic for SampleConfig.xaml
    /// </summary>
    public partial class SampleConfig 
    {
        public SampleConfig()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000); //represents a service call
            Close();
        }
    }
}
