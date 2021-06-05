using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//服务器ip地址
    private int serverPort = 5000;//端口号

    private Vector3 currentRotation;
    private int frameCount = 0;
    private int flag = 0;
    private List<float> buffer = new List<float>(8);

    public ShowLibrary SL;
    public ShowBroad1 SB1;
    public ShowBroad2 SB2;
    public ShowRoad SR;
    public ShowThin1 ST1;
    public ShowThin2 ST2;
    public ShowThin3 ST3;

    public Camera main;
    public Camera pre;



    void Start()
    {
        //1、创建socket
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //2、建立一个连接请求
        IPAddress iPAddress = IPAddress.Parse(serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);
        tcpClient.Connect(endPoint);
        Debug.Log("请求服务器连接");
    }

    void Update()
    {
        frameCount++;
        if (frameCount != 30) return;
        currentRotation = main.transform.rotation.eulerAngles;
        if (flag == 5)
        {
            frameCount = 0;
            flag = 0;
            buffer[0] = buffer[1];
            buffer[1] = currentRotation.x;
            for (int i = 2; i < 4; ++i)
            {
                buffer[i] = buffer[i + 1];
            }
            buffer[4] = currentRotation.y;

            //发送消息
            string smessage = "";
            for(int i = 0; i < 5; ++i)
            {
                smessage += buffer[i].ToString();
                smessage += " ";
            }
            tcpClient.Send(Encoding.UTF8.GetBytes(smessage));
            Debug.Log("客户端向服务器发送消息" + smessage);

            //接受、发送消息
            byte[] data = new byte[1024];
            int length = tcpClient.Receive(data);
            var rmessage = Encoding.UTF8.GetString(data, 0, length);
            Debug.Log("客户端收到来自服务器发来的信息" + rmessage);
            //float cur_y = float.Parse(rmessage);
            string[] strArray = rmessage.Split(' ');
            float cur_x = float.Parse(strArray[0]);
            float cur_y = float.Parse(strArray[1]);


            pre.transform.eulerAngles = new Vector3(cur_x, cur_y, pre.transform.eulerAngles.z);
            
            SL.Check(pre);
            SB1.Check(pre);
            SB2.Check(pre);
            SR.Check(pre);
            ST1.Check(pre);
            ST2.Check(pre);
            ST3.Check(pre);
        }
        if (flag < 5)
        {
            frameCount = 0;
            buffer.Add(currentRotation.y);
            ++flag;

            buffer[0] = 0;
            buffer[1] = 0;
        }
    }
}