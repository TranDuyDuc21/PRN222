using System.IO;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    // Hàm xử lý tin nhắn từ client
    static void ProcessMessage(object parm)
    {
        string data; // Biến lưu dữ liệu nhận được từ client
        int count; // Biến lưu số byte đọc được
        try
        {
            TcpClient client = parm as TcpClient; // Lấy đối tượng client từ tham số đầu vào
            Byte[] bytes = new Byte[256]; // Bộ đệm lưu dữ liệu nhận được
            NetworkStream stream = client.GetStream(); // Lấy luồng mạng từ client

            // Vòng lặp nhận dữ liệu từ client
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Chuyển dữ liệu từ byte sang chuỗi
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now:t}"); // Hiển thị dữ liệu nhận được và thời gian

                // Xử lý dữ liệu (chuyển sang chữ hoa)
                data = $"{data.ToUpper()}";

                // Chuyển dữ liệu xử lý thành byte và gửi lại client
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length); // Gửi dữ liệu
                Console.WriteLine($"Sent: {data}"); // Hiển thị dữ liệu đã gửi
            }
            client.Close(); // Đóng kết nối với client
        }
        catch (Exception ex)
        {
            // Hiển thị lỗi nếu có
            Console.WriteLine("{0}", ex.Message);
            Console.WriteLine("Waiting Message.......");
        }
    }

    // Hàm khởi động server
    static void ExecuteServer(string host, int port)
    {
        int Count = 0; // Biến đếm số lượng client đã kết nối
        TcpListener server = null; // Biến lưu server listener
        try
        {
            Console.Title = "Server Application"; // Đặt tiêu đề cửa sổ console
            IPAddress localAddr = IPAddress.Parse(host); // Chuyển đổi địa chỉ IP từ chuỗi
            server = new TcpListener(localAddr, port); // Tạo đối tượng TcpListener để lắng nghe kết nối
            server.Start(); // Bắt đầu server
            Console.WriteLine(new string('*', 40)); // Hiển thị dòng phân cách
            Console.WriteLine("Waiting for a connection......."); // Chờ client kết nối

            // Vòng lặp chính để xử lý nhiều client
            while (true)
            {
                TcpClient client = server.AcceptTcpClient(); // Chấp nhận kết nối từ client
                Console.WriteLine($"Number of client connected: {+Count}"); // Hiển thị số lượng client đã kết nối
                Console.WriteLine(new string('*', 40)); // Hiển thị dòng phân cách

                // Tạo một luồng (thread) mới để xử lý tin nhắn từ client
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                thread.Start(client); // Bắt đầu luồng và truyền client làm tham số
            }
        }
        catch (Exception ex)
        {
            // Hiển thị lỗi nếu có
            Console.WriteLine("{0}", ex.Message);
        }
        finally
        {
            // Dừng server khi kết thúc
            server.Stop();
            Console.WriteLine("Server stop. Press any key to exit !");
        }
        Console.Read(); // Đợi người dùng nhấn phím trước khi thoát
    }

    // Hàm chính của chương trình
    public static void Main()
    {
        string host = "127.0.0.1"; // Địa chỉ IP server
        int port = 13000; // Cổng lắng nghe
        ExecuteServer(host, port); // Gọi hàm khởi động server
    }
}
