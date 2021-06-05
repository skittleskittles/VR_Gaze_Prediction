using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowThin3 : MonoBehaviour
{
    GameObject Tree;
    bool flag = false;
    Vector3 Pos = new Vector3((float)71.8, 0, (float)19.5);
    // Start is called before the first frame update
    void Start()
    {
        Tree = Resources.Load("Thinleaf") as GameObject;
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
        for (i = 0; i < planes.Length; ++i)
        {
            if (!planes[i].GetSide(worldPos)) break;
        }
        if (i == planes.Length) return true;
        return false;
    }


    public void Check(Camera pre)
    {
        if (PredInView(Pos, pre) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(Tree, Pos, Quaternion.Euler(0, 0, 0));
        }
    }



    // Update is called once per frame
    void Update()
    {
        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        if (IsInView(Pos) && flag == false)
        {
            flag = true;
            MonoBehaviour.Instantiate(Tree, Pos, Quaternion.Euler(0, 0, 0));
        }
    }
}
