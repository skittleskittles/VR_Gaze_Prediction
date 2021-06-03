
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UnityCallPython : MonoBehaviour
{
    private Vector3 currentRotation;
    private int frameCount = 0;
    private int flag = 0;
    private List<float> buffer = new List<float>(5);

   /* private void Start()
    {
        string basePath = @"C:\Users\19752\Downloads\UnityProject\LSTM\";
        CallPythonPred(basePath + "conn_to_unity.py", 1, 1, 1, 1, 1);
    }*/

    void CallPythonPred(string pyScriptPath, float a, float b, float c, float d, float e)
    {
        CallPythonBase(pyScriptPath, a.ToString(), b.ToString(), c.ToString(), d.ToString(), e.ToString());
    }

    public void CallPythonBase(string pyScriptPath, params string[] argvs)
    {
        Process process = new Process();
        process.StartInfo.FileName = @"C:\Users\19752\Anaconda3\envs\Unity\python.exe";

        foreach (string item in argvs)
        {
            pyScriptPath += " " + item;
        }
        
        //UnityEngine.Debug.Log(pyScriptPath);

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = pyScriptPath;     // 路径+参数
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;        // 不显示执行窗口

        // 开始执行，获取执行输出，添加结果输出委托
        process.Start();
        process.BeginOutputReadLine();
        process.OutputDataReceived += new DataReceivedEventHandler(GetData);
        process.WaitForExit();
    }

    void GetData(object sender, DataReceivedEventArgs e)
    {
        // 结果不为空才打印（后期可以自己添加类型不同的处理委托）
        if (string.IsNullOrEmpty(e.Data) == false)
        {
            //UnityEngine.Debug.Log(e.Data);
        }
    }


    void Update()
    {
        frameCount++;
        if (frameCount != 50) return;
        currentRotation = this.transform.rotation.eulerAngles;
        //每30帧写入一次csv文件
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

            string basePath = @"C:\Users\19752\Downloads\UnityProject\LSTM\";
            CallPythonPred(basePath + "conn_to_unity.py", buffer[0], buffer[1], buffer[2], buffer[3], buffer[4]);
        }
        if (flag < 5)
        {
            frameCount = 0;
            buffer.Add(currentRotation.y);
            ++flag;
        }
    }
}