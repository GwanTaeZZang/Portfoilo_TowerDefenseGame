using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkTestTCP : MonoBehaviour
{
    //private TcpClient client;
    //private NetworkStream stream;

    //void Start()
    //{
    //    ConnectToServer("52.78.67.151", 9000);
    //}

    //void ConnectToServer(string server, int port)
    //{
    //    try
    //    {
    //        client = new TcpClient(server, port);
    //        stream = client.GetStream();

    //        Debug.Log("Connected to server");

    //        // 예시로 서버에 메시지 전송
    //        SendMessage("Hello from Unity!");
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Connection error: " + e.Message);
    //    }
    //}

    //void SendMessage(string message)
    //{
    //    if (stream == null)
    //    {
    //        Debug.LogError("NetworkStream is null");
    //        return;
    //    }

    //    byte[] data = Encoding.ASCII.GetBytes(message);
    //    stream.Write(data, 0, data.Length);

    //    Debug.Log("Message sent: " + message);
    //}

    //void OnApplicationQuit()
    //{
    //    stream?.Close();
    //    client?.Close();
    //}





    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        ConnectToServer("ec2-52-78-67-151.ap-northeast-2.compute.amazonaws.com", 9000);
    }

    void ConnectToServer(string server, int port)
    {
        try
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();

            Debug.Log("Connected to server");

            // 서버에 메시지 전송 예시
            SendMessage("Hello from Unity!");

            //client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Connection error: " + e.Message);
        }
    }

    void SendMessage(string message)
    {
        if (stream == null)
        {
            Debug.LogError("NetworkStream is null");
            return;
        }

        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);

        Debug.Log("Message sent: " + message);

        // 서버로부터 응답 받기
        byte[] receivedBytes = new byte[256];
        int bytesRead = stream.Read(receivedBytes, 0, receivedBytes.Length);
        string responseData = Encoding.ASCII.GetString(receivedBytes, 0, bytesRead);
        Debug.Log("Received: " + responseData);
    }

    void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}
