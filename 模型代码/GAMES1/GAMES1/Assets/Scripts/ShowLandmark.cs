using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLandmark : MonoBehaviour
{
    GameObject lm;
    GameObject sp;
// public Vector3 offset;
    bool flag=false;
    public static int created = 0;

    // Start is called before the first frame update
    void Start()
    {
        lm = Resources.Load("Landmark") as GameObject;
        sp = Resources.Load("Sphere") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        int remain = (int)(transform.position[2] / 10) + 1;
        if (transform.position[2]%10 > 8 && transform.position[2] % 10 <9 && flag == false&&remain<=created+1)
        {
            flag = true;
            ++created;
            Vector3 Pos = new Vector3(12, 1, remain*10);
            MonoBehaviour.Instantiate(lm, Pos, Quaternion.Euler(0, 0, 0));
            Vector3 Pos2 = new Vector3(15, 1, remain * 10-3);
            MonoBehaviour.Instantiate(sp, Pos2, Quaternion.Euler(0, 0, 0));
        }
        if (transform.position[2] % 10 > 7 && transform.position[2] % 10 < 8 && flag == true)
        {
            flag = false;
        }
    }
}
