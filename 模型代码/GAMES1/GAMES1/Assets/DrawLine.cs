
//�˽ű����ص���������ϣ�������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public float _farDistance = 100;//Զ�ӿھ���
    public float _nearDistance = 3;//���ӿھ���

    public enum Colorbar
    {
        red, green, white, yellow
    }
    public Colorbar linecolor;
    private Camera _camera;
    private Transform _camTrans;

    void OnDrawGizmos()
    {
        if (_camera == null)
        {
            _camera = this.GetComponent<Camera>();
            _camTrans = _camera.transform;
        }
        Color color;
        if (linecolor == Colorbar.red) color = Color.red;
        else if (linecolor == Colorbar.green) color = Color.green;
        else if (linecolor == Colorbar.white) color = Color.white;
        else color = Color.yellow;

        OnDrawFarView(color);
        OnDrawNearView(color);
        OnDrawCone(color);
    }
    void OnDrawFarView(Color color)
    {
        Vector3[] corners = GetCorners(_farDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], color); // UpperLeft -> UpperRight
        Debug.DrawLine(corners[1], corners[3], color); // UpperRight -> LowerRight
        Debug.DrawLine(corners[3], corners[2], color); // LowerRight -> LowerLeft
        Debug.DrawLine(corners[2], corners[0], color); // LowerLeft -> UpperLeft

        //������
        Vector3 vecStart = _camTrans.transform.position;
        Vector3 vecEnd = vecStart;
        vecEnd += _camTrans.forward * _farDistance;
        Debug.DrawLine(vecStart, vecEnd, color);
    }

    void OnDrawNearView(Color color)
    {
        Vector3[] corners = GetCorners(_nearDistance);

        // for debugging
        Debug.DrawLine(corners[0], corners[1], color);//����-����
        Debug.DrawLine(corners[1], corners[3], color);//����-����
        Debug.DrawLine(corners[3], corners[2], color);//����-����
        Debug.DrawLine(corners[2], corners[0], color);//����-����
    }

    void OnDrawCone(Color color)
    {
        Vector3[] corners = GetCorners(_farDistance);

        // for debugging
        Debug.DrawLine(_camTrans.position, corners[1], color); // UpperLeft -> UpperRight
        Debug.DrawLine(_camTrans.position, corners[3], color); // UpperRight -> LowerRight
        Debug.DrawLine(_camTrans.position, corners[2], color); // LowerRight -> LowerLeft
        Debug.DrawLine(_camTrans.position, corners[0], color); // LowerLeft -> UpperLeft
    }

    Vector3[] GetCorners(float distance)
    {
        Vector3[] corners = new Vector3[4];

        float halfFOV = (_camera.fieldOfView * 0.5f) * Mathf.Deg2Rad;//half of fov
        float aspect = _camera.aspect;

        float height = distance * Mathf.Tan(halfFOV);//distance����λ�ã�����ӿڸ߶ȵ�һ��
        float width = height * aspect;//����ӿڿ�ȵ�һ��

        //����
        corners[0] = _camTrans.position - (_camTrans.right * width);//������� - �ӿڿ��һ��
        corners[0] += _camTrans.up * height;//+�ӿڸߵ�һ��
        corners[0] += _camTrans.forward * distance;//+�ӿھ���

        // ����
        corners[1] = _camTrans.position + (_camTrans.right * width);//������� + �ӿڿ��һ��
        corners[1] += _camTrans.up * height;//+�ӿڸߵ�һ��
        corners[1] += _camTrans.forward * distance;//+�ӿھ���

        // ����
        corners[2] = _camTrans.position - (_camTrans.right * width);//������� - �ӿڿ��һ��
        corners[2] -= _camTrans.up * height;//-�ӿڸߵ�һ��
        corners[2] += _camTrans.forward * distance;//+�ӿھ���

        // ����
        corners[3] = _camTrans.position + (_camTrans.right * width);//������� + �ӿڿ��һ��
        corners[3] -= _camTrans.up * height;//-�ӿڸߵ�һ��
        corners[3] += _camTrans.forward * distance;//+�ӿھ���

        return corners;
    }
}

