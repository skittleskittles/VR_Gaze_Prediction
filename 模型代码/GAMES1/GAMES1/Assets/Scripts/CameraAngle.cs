using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CameraAngle : MonoBehaviour
{
    private Vector3 currentRotation;
    public string file_path = "C:\\Users\\19752\\Downloads\\UnityProject\\LSTM\\CameraData.csv";
    private int frameCount = 0;
    private string newLine = "";
    // Start is called before the first frame update
    void Start()
    {
        newLine = "Rotation_x , Rotation_y";
        using (StreamWriter sw = new StreamWriter(file_path))
        {
            sw.WriteLine(newLine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        currentRotation = this.transform.rotation.eulerAngles;
        //每30帧写入一次csv文件
        if (frameCount % 10 == 0)
        {
            frameCount = 0;
            newLine =  currentRotation.x.ToString() + "," + currentRotation.y.ToString();
            using (StreamWriter sw = new StreamWriter(file_path, append: true))
            {
                sw.WriteLine(newLine);
            }
        }

    }
}
