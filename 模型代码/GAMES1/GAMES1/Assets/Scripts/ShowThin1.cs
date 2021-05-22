using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowThin1 : MonoBehaviour
{
    GameObject Tree;
    bool flag = false;
    Vector3 Pos = new Vector3((float)26.1, 0, 85);
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
