using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    // Hàm kết nối đến server
    static void ConnectServer(string server, int port)
    {
        string message, responseData; // Biến lưu tin nhắn và dữ liệu phản hồi từ server
        int bytes; // Biến lưu số byte nhận được
        try
        {
            // Tạo kết nối TCP đến server với địa chỉ IP và cổng chỉ định
            TcpClient client = new TcpClient(server, port);

            // Đặt tiêu đề cho cửa sổ console là "Client Application"
            Console.Title = "Client Application";

            NetworkStream stream = null; // Đối tượng để giao tiếp với server
            while (true)
            {
                // Hiển thị yêu cầu nhập tin nhắn từ người dùng
                Console.WriteLine("Input message <press Enter to exit>:");
                message = Console.ReadLine(); // Đọc tin nhắn từ người dùng

                // Nếu người dùng nhấn Enter (không nhập tin nhắn), thoát khỏi vòng lặp
                if (message == string.Empty)
                {
                    break;
                }

                // Chuyển tin nhắn sang dạng byte để gửi qua mạng
                Byte[] data = System.Text.Encoding.ASCII.GetBytes($"{message}");
                stream = client.GetStream(); // Lấy luồng mạng từ client
                stream.Write(data, 0, data.Length); // Gửi dữ liệu đến server
                Console.WriteLine("Sent: {0}", message); // Hiển thị tin nhắn đã gửi

                // Chuẩn bị mảng byte để nhận dữ liệu phản hồi từ server
                data = new Byte[256];
                bytes = stream.Read(data, 0, data.Length); // Đọc dữ liệu từ server
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes); // Chuyển byte sang chuỗi
                Console.WriteLine("Received: {0}", responseData); // Hiển thị dữ liệu nhận được
            }

            // Đóng kết nối với server
            client.Close();
        }
        catch (Exception ex)
        {
            // Hiển thị thông báo lỗi nếu xảy ra lỗi
            Console.WriteLine("Exception: {0}", ex.Message);
        }
    }

    // Hàm chính của chương trình
    static void Main(string[] args)
    {
        string server = "127.0.0.1"; // Địa chỉ IP server (localhost)
        int port = 13000; // Cổng server
        ConnectServer(server, port); // Gọi hàm kết nối đến server
    }
}
