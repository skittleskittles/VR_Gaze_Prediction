using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ScreenPixel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //设置硬件的分辨率
        //Y键，设置分辨率为3840x1160
        if (Input.GetKey(KeyCode.Y))
        {
            Screen.SetResolution(3840, 1160, true);
        }

        //U键，设置分辨率为1920x1080
        if (Input.GetKey(KeyCode.U))
        {
            Screen.SetResolution(1920, 1080, true);
        }

        //I键，设置分辨率为1600x1024
        if (Input.GetKey(KeyCode.I))
        {
            Screen.SetResolution(1600, 1024, true);
        }

        //O键，设置分辨率为1600x900
        if (Input.GetKey(KeyCode.O))
        {
            Screen.SetResolution(1600, 900, true);
        }

        //P键，设置分辨率为1366x768
        if (Input.GetKey(KeyCode.P))
        {
            Screen.SetResolution(1366, 768, true);
        }

        //K键，设置分辨率为1280x960
        if (Input.GetKey(KeyCode.K))
        {
            Screen.SetResolution(1280, 960, true);
        }


        //L键，设置分辨率为1280x800
        if (Input.GetKey(KeyCode.L))
        {
            Screen.SetResolution(1280, 800, true);
        }

        //按下Esc键退出程序
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

}

