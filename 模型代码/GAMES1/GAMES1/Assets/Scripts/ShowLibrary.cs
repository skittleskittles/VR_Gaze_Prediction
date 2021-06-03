using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLibrary : MonoBehaviour
{
    GameObject lib;
    bool flag = false;
    Vector3 Pos = new Vector3((float)54.6, (float)1.37, (float)75.5);

    // Start is called before the first frame update
    void Start()
    {
        lib = Resources.Load("Library") as GameObject;
    }

    public bool IsInView(Vector3 worldPos)
      {
          Transform camTransform = Camera.main.transform;
          Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
          Vector3 dir = (worldPos - camTransform.position).normalized;
          float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面
          if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
              return true;
         else
             return false;
     }

    public bool PredInView(Vector3 worldPos, float y)
    {
        Transform camTransform = Camera.main.transform;
        camTransform.localEulerAngles = new Vector3(camTransform.eulerAngles.x, y, camTransform.eulerAngles.z);
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);//判断物体是否在相机前面

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

    public void check_lib(float y)
    {
        if (PredInView(Pos, y) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(lib, Pos, Quaternion.Euler(0, 0, 0));
        }
    }


// Update is called once per frame
void Update()
    {
        /*if (IsInView(Pos) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(lib, Pos, Quaternion.Euler(0, 0, 0));
        }*/
    }
}
