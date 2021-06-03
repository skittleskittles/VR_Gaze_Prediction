using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//������ip��ַ
    private int serverPort = 5000;//�˿ں�

    private Vector3 currentRotation;
    private int frameCount = 0;
    private int flag = 0;
    private List<float> buffer = new List<float>(5);

    public ShowLibrary SL;


    void Start()
    {
        //1������socket
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //2������һ����������
        IPAddress iPAddress = IPAddress.Parse(serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);
        tcpClient.Connect(endPoint);
        Debug.Log("�������������");

        /*//������Ϣ
        string smessage = "1.123 1.2345 1.5431 1.12";
        tcpClient.Send(Encoding.UTF8.GetBytes(smessage));
        Debug.Log("�ͻ����������������Ϣ" + smessage);

        //3�����ܡ�������Ϣ
        byte[] data = new byte[1024];
        int length = tcpClient.Receive(data);
        var rmessage = Encoding.UTF8.GetString(data, 0, length);
        Debug.Log("�ͻ����յ����Է�������������Ϣ" + rmessage);*/
    }

    void Update()
    {
        frameCount++;
        if (frameCount != 50) return;
        currentRotation = this.transform.rotation.eulerAngles;
        if (flag == 5)
        {
            frameCount = 0;
            flag = 0;
            //newLine = currentRotation.x.ToString() + "," + currentRotation.y.ToString();
            for (int i = 0; i < 4; ++i)
            {
                buffer[i] = buffer[i + 1];
            }
            buffer[4] = currentRotation.y;

            //������Ϣ
            string smessage = "";
            for(int i = 0; i < 5; ++i)
            {
                smessage += buffer[i].ToString();
                smessage += " ";
            }
            tcpClient.Send(Encoding.UTF8.GetBytes(smessage));
            Debug.Log("�ͻ����������������Ϣ" + smessage);

            //���ܡ�������Ϣ
            byte[] data = new byte[1024];
            int length = tcpClient.Receive(data);
            var rmessage = Encoding.UTF8.GetString(data, 0, length);
            Debug.Log("�ͻ����յ����Է�������������Ϣ" + rmessage);
            float cur_y = float.Parse(rmessage);
            SL.check_lib(cur_y);
        }
        if (flag < 5)
        {
            frameCount = 0;
            buffer.Add(currentRotation.y);
            ++flag;
        }
    }
}