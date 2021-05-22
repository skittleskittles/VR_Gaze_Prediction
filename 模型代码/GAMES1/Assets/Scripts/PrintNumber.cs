using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintNumber : MonoBehaviour
{
    GameObject lmNum;
    bool flag = false;
    int created = 0;

    // Start is called before the first frame update
    void Start()
    {
        lmNum = Resources.Load("LmNumber") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        int remain = (int)(transform.position[2] / 10) + 1;
        if (transform.position[2] % 10 > 8 && transform.position[2] % 10 < 9 && flag == false && remain <= created + 1)
        {
            flag = true;
            ++created;
            Vector3 Pos = new Vector3((float)10.5, (float) 2.5, remain * 10-(float)0.5);
            MonoBehaviour.Instantiate(lmNum, Pos, Quaternion.Euler(0, 0, 0));
            //lmNum.TextMesh.text = "You have arrive:" + (remain * 10).ToString() + '~';
        }
        if (transform.position[2] % 10 > 7 && transform.position[2] % 10 < 8 && flag == true)
        {
            flag = false;
        }
    }
}
