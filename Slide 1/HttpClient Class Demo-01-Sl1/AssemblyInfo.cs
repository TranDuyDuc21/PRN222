using System.Net.Http;
using System.Windows;

namespace HttpClient_Class_Demo_01_Sl1
{
    public partial class MainWindow : Window
    {
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        // HttpClient instance
        readonly HttpClient client = new HttpClient();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtContent.Text = string.Empty;
        }

        private async void btnViewHTML_Click(object sender, RoutedEventArgs e)
        {
            string uri = txtURL.Text;

            try
            {
                // Fetch HTML content from the provided URL
                string responseBody = await client.GetStringAsync(uri);

                // Display the content in txtContent
                txtContent.Text = responseBody.Trim();
            }
            catch (HttpRequestException ex)
            {
                // Show error message if the request fails
                MessageBox.Show($"Message: {ex.Message}");
            }
        }
    }
}
