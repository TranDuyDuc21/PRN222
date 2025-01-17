using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 8080;  // Cổng mà server sẽ lắng nghe

            Console.WriteLine("Starting server...");  // In ra thông báo khi server bắt đầu
            TcpListener listener = new TcpListener(IPAddress.Any, port);  // Khởi tạo listener để lắng nghe kết nối

            try
            {
                listener.Start();  // Bắt đầu lắng nghe kết nối
                Console.WriteLine($"Server is listening on port {port}...");  // In ra thông báo server đang lắng nghe

                while (true)
                {
                    Console.WriteLine("Waiting for a client to connect...");  // In ra thông báo khi server đang chờ kết nối
                    TcpClient client = listener.AcceptTcpClient();  // Chấp nhận kết nối từ client
                    Console.WriteLine("Client connected!");  // Thông báo khi client kết nối thành công

                    NetworkStream stream = client.GetStream();  // Lấy stream kết nối từ client

                    // Đọc dữ liệu từ client
                    byte[] buffer = new byte[1024];  // Tạo bộ đệm để đọc dữ liệu
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);  // Đọc dữ liệu từ stream
                    string receivedText = Encoding.UTF8.GetString(buffer, 0, bytesRead);  // Chuyển đổi mảng byte thành chuỗi
                    Console.WriteLine($"Received from client: {receivedText}");  // In ra dữ liệu nhận được từ client

                    // Xử lý dữ liệu (chuyển đổi thành chữ hoa)
                    string responseText = receivedText.ToUpper();  // Chuyển văn bản thành chữ hoa
                    byte[] responseData = Encoding.UTF8.GetBytes(responseText);  // Chuyển đổi văn bản thành mảng byte

                    // Gửi phản hồi tới client
                    stream.Write(responseData, 0, responseData.Length);  // Gửi dữ liệu tới client
                    Console.WriteLine($"Sent to client: {responseText}");  // In ra thông báo đã gửi phản hồi

                    // Đóng kết nối
                    client.Close();  // Đóng kết nối với client
                    Console.WriteLine("Client disconnected.\n");  // Thông báo khi client ngắt kết nối
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");  // In ra thông báo lỗi nếu có
            }
            finally
            {
                listener.Stop();  // Dừng listener khi server kết thúc
                Console.WriteLine("Server stopped.");  // Thông báo khi server dừng
            }
        }
    }
}
