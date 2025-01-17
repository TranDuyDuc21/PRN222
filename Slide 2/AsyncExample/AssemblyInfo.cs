using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncExample
{
    public partial class MainWindow : Window
    {
        // HttpClient sử dụng để gửi yêu cầu
        private readonly HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        // Danh sách URL
        private readonly IEnumerable<string> UrlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/windows"
        };

      
        // Sự kiện khi nhấn nút Start
        private async void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            btnStartButton.IsEnabled = false; // Vô hiệu hóa nút để tránh nhấn nhiều lần
            txtResults.Clear(); // Xóa nội dung cũ trong TextBox

            await SumPageSizesAsync(); // Thực hiện tính tổng dung lượng trang

            txtResults.Text += $"\nControl returned to {nameof(OnStartButtonClick)}."; // Thông báo hoàn tất
            btnStartButton.IsEnabled = true; // Bật lại nút
        }

        // Tổng hợp kích thước của tất cả các trang
        private async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            int total = 0;

            foreach (string url in UrlList)
            {
                int contentLength = await ProcessUrlAsync(url, client);
                total += contentLength;
            }

            stopwatch.Stop();
            txtResults.Text += $"\nTotal bytes returned: {total:#,#}";
            txtResults.Text += $"\nElapsed time: {stopwatch.Elapsed}\n";
        }

        // Xử lý tải nội dung của một URL
        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            try
            {
                byte[] content = await client.GetByteArrayAsync(url); // Gửi yêu cầu
                DisplayResults(url, content); // Hiển thị kết quả
                return content.Length; // Trả về kích thước nội dung
            }
            catch (Exception ex)
            {
                DisplayError(url, ex.Message);
                return 0;
            }
        }

        // Hiển thị kết quả lên TextBox
        private void DisplayResults(string url, byte[] content)
        {
            Dispatcher.Invoke(() =>
            {
                txtResults.Text += $"{url,-60} {content.Length,10:#,#}\n";
            });
        }

        // Hiển thị lỗi nếu không tải được URL
        private void DisplayError(string url, string errorMessage)
        {
            Dispatcher.Invoke(() =>
            {
                txtResults.Text += $"{url,-60} Error: {errorMessage}\n";
            });
        }

        // Hủy HttpClient khi ứng dụng đóng
        protected override void OnClosed(EventArgs e)
        {
            client.Dispose();
            base.OnClosed(e);
        }
    }
}
