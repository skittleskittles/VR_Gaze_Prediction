using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoad : MonoBehaviour
{
    GameObject Road;
    bool flag = false;
    Vector3 Pos = new Vector3((float)43.5, (float)0.07, (float)46.8);
    // Start is called before the first frame update
    void Start()
    {
        Road = Resources.Load("Road") as GameObject;
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

    public bool PredInView(Vector3 worldPos, Camera pre)
    {
        int i = 0;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(pre);
        float offsetx = 20, offsetz = -20;
        Vector3 tmpPos = new Vector3(worldPos.x + offsetx, worldPos.y, worldPos.z + offsetz);
        for (i = 0; i < planes.Length; ++i)
        {
            if (!planes[i].GetSide(tmpPos)) break;
        }
        if (i == planes.Length) return true;
        return false;
    }


    public void Check(Camera pre)
    {
        if (PredInView(Pos, pre) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(Road, Pos, Quaternion.Euler(0, (float)56.022, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        if (IsInView(Pos) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(Road, Pos, Quaternion.Euler(0, (float)56.022, 0));
        }
    }
}
