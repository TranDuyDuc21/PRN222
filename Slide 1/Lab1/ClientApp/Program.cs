using System;
using System.Net.Sockets;
using System.Text;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string server = "localhost";  // Địa chỉ máy chủ
            const int port = 8080;  // Cổng kết nối

            Console.WriteLine("Client started...");  // In ra thông báo khi client bắt đầu
            try
            {
                using (TcpClient client = new TcpClient(server, port))  // Kết nối đến máy chủ
                {
                    Console.WriteLine($"Connected to server {server}:{port}");  // In ra thông báo khi kết nối thành công

                    NetworkStream stream = client.GetStream();  // Lấy stream kết nối từ client

                    // Gửi dữ liệu đến máy chủ
                    Console.Write("Enter text to send to server: ");
                    string input = Console.ReadLine() ?? string.Empty;  // Nhập dữ liệu từ người dùng
                    byte[] data = Encoding.UTF8.GetBytes(input);  // Chuyển đổi dữ liệu thành mảng byte
                    stream.Write(data, 0, data.Length);  // Gửi mảng byte đến máy chủ
                    Console.WriteLine($"Sent to server: {input}");  // In ra thông báo đã gửi

                    // Nhận phản hồi từ máy chủ
                    byte[] buffer = new byte[1024];  // Tạo bộ đệm để nhận dữ liệu
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);  // Đọc dữ liệu trả về từ máy chủ
                    string responseText = Encoding.UTF8.GetString(buffer, 0, bytesRead);  // Chuyển đổi mảng byte thành chuỗi
                    Console.WriteLine($"Received from server: {responseText}");  // In ra phản hồi từ máy chủ
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error: {ex.Message}");  // In ra lỗi khi kết nối gặp vấn đề
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");  // In ra lỗi chung
            }
            Console.WriteLine("Client terminated.");  // Thông báo khi client kết thúc
        }
    }
}
