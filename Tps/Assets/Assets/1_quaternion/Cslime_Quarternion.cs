using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//z x y ������ ���� 90�� degree ȸ��

//����� * ����� * �����


public class Cslime_Quarternion : MonoBehaviour
{
    Quaternion mStart = Quaternion.identity;  //������ �����(ȸ���� ���� ��� �ǹ�)�� �ʱ�ȭ
    Quaternion mEnd = Quaternion.identity;  //������ �����(ȸ���� ���� ��� �ǹ�)�� �ʱ�ȭ

    Matrix4x4 mMatrix = Matrix4x4.identity; //���� ��ķ� �ʱ�ȭ

    MeshFilter mMeshFilter = null; //���� �������� ��ȸ�ϱ� ���� �޽����� �����͵��� ����
    Vector3[] mOriginVertices; //�Ž��� ���� ������
    Vector3[] mNewVertices; //ȸ�� ��ȯ�� ����� ���ο� ������

    //IMGUI ���߿����� ���� ���̴� UI���� �ý����̴�.
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Quaternion ����"))
        {
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(0f, 90f, 0f);
        }

        this.transform.rotation = mEnd;


        if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "Transform Rotate ����"))
        {
            

            this.transform.Rotate(0f, 0f, 90f);
            this.transform.Rotate(90f, 0f, 0f);
            this.transform.Rotate(0f, 90f, 0f);
        }

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "Euler\nQuaternion.Euler"))
        {
            //zxy������ 90, 90, 90�� Quaternion.Euler�� ǥ��
            //�̰��� ���Ϸ� ���� ���� ȸ���� ������ ����� ������� ������̴�. ��, ���Ϸ� ���� ���� ȸ���̴�.
            mEnd = Quaternion.Euler(90f, 90f, 90f);

            this.transform.rotation = mEnd;
        }

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "Euler\nMatrix Rot"))
        {
            Matrix4x4 tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 90f, 90f), Vector3.one);
            mMatRot = tM * mMatRot;

            mMeshFilter = GetComponentInChildren<MeshFilter>();
            mOriginVertices = mMeshFilter.mesh.vertices; //������ ��ȸ�Ͽ� ��Ƶд�.(��Ÿ���̹Ƿ� ����Ǿ� ����)
        }
    }
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
