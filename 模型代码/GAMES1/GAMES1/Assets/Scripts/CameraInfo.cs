using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CameraInfo : MonoBehaviour
{
    private Vector3 currentPosition;
    private Vector3 currentRotation;
    public string file_path = "C:\\Users\\19752\\Downloads\\UnityProject\\GAMES1\\CameraData.csv";
    private int frameCount = 0;
    private string newLine = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        currentPosition = this.transform.position;
        currentRotation = this.transform.rotation.eulerAngles;
        //每30帧写入一次csv文件
        if (frameCount % 50 == 0)
        {
            frameCount = 0;
            newLine = currentPosition.x.ToString() + "," + currentPosition.y.ToString() + "," +
                        currentPosition.z.ToString() + "," + currentRotation.x.ToString() + "," +
                        currentRotation.y.ToString();
            using (StreamWriter sw = new StreamWriter(file_path, append: true))
            {
                sw.WriteLine(newLine);
            }
        }

    }
}
