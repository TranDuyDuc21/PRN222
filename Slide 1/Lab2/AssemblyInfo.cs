using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace HttpClientApp
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        // Constructor: Khởi tạo ứng dụng và HttpClient
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();  // Tạo đối tượng HttpClient để sử dụng cho các yêu cầu HTTP
        }

        // Sự kiện khi nhấn nút "Fetch Data"
        private async void FetchDataButton_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;  // Lấy URL người dùng nhập vào TextBox

            // Kiểm tra nếu URL không được nhập
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("Please enter a URL.");  // Hiển thị thông báo nếu URL rỗng
                return;
            }

            try
            {
                // Gọi phương thức FetchDataFromUrl để lấy dữ liệu từ URL
                string content = await FetchDataFromUrl(url);
                DisplayTextBox.Text = content;  // Hiển thị nội dung trả về trong TextBox kết quả
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có vấn đề khi fetch dữ liệu
                MessageBox.Show($"Error: {ex.Message}");  // Hiển thị thông báo lỗi
            }
        }

        // Phương thức lấy dữ liệu từ URL sử dụng HttpClient
        private async Task<string> FetchDataFromUrl(string url)
        {
            // Gửi yêu cầu GET đến URL
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();  // Kiểm tra nếu mã trạng thái HTTP không phải là lỗi (4xx, 5xx)

            // Đọc nội dung của phản hồi và trả về dưới dạng chuỗi
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        // Sự kiện khi nhấn nút "Clear"
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UrlTextBox.Clear();  // Xóa nội dung trong TextBox nhập URL
            DisplayTextBox.Clear();  // Xóa nội dung trong TextBox hiển thị kết quả
        }

        // Sự kiện khi nhấn nút "Close"
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Đóng ứng dụng
        }
    }
}
