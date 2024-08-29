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

    Matrix4x4 mMatRot = Matrix4x4.identity; //���� ��ķ� �ʱ�ȭ


    MeshFilter mMeshFilter = null; //���� �������� ��ȸ�ϱ� ���� �޽����� �����͵��� ����
    Vector3[] mOriginVertices; //�Ž��� ���� ������
    Vector3[] mNewVertices; //ȸ�� ��ȯ�� ����� ���ο� ������

    //IMGUI ���߿����� ���� ���̴� UI���� �ý����̴�.
    private void OnGUI()
    {
        //������� ����(Interpolation: �ٻ�ġ�� ���Ѵ�)
        if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "Quaternion\n����"))
        {
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(0f, 90f, 0f);

            //this.transform.rotation = mEnd;
            mWeight = 0f;

            //���º���
            mState = STATE.WITH_INTERPOLATION;
        }

        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Quaternion ����"))
        {
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(0f, 90f, 0f);
        
            this.transform.rotation = mEnd;
        }



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

        if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "Euler\nMatrix Rot"))
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

            //��ȯ�� ��ġ ������ ���� ���ο� ���ҵ��� �迭�� ������.
            mNewVertices = new Vector3[mOriginVertices.Length];


            int ti = 0;
            while(ti < mOriginVertices.Length)
            {
                mNewVertices[ti] =  mMatRot.MultiplyPoint3x4(mOriginVertices[ti]);

                ++ti;
            }

            //�޽����� ������Ʈ�� (ȸ�� ��ȯ ����� �����) ���ο� �޽��� �����Ѵ�.
            mMeshFilter.mesh.vertices = mNewVertices;



        }
    }
// Start is called before the first frame update
void Start()
    {
        
    }

    enum STATE
    {
        WITH_NONE = 0,
        WITH_INTERPOLATION  //��������

    }

    STATE mState = STATE.WITH_NONE;

    float mWeight = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(STATE.WITH_INTERPOLATION == mState)
        {
            //������� ��������
            //this.transform.rotation = Quaternion.Lerp(mStart, mEnd, mWeight);
            this.transform.rotation = Quaternion.Slerp(mStart, mEnd, mWeight);
            //���� �������� Slerp Sphere Linear Interpolation ���� ��������
            /*
              ���� ������ �����Լ��� �̿��Ͽ� �ٻ�ġ�� ���Ѵ�.
              ���� ���� ������ �� �� ������ ȣ�� ����Ͽ� �ٻ�ġ�� ���Ѵ�.
            */
            mWeight += Time.deltaTime;

            



            //���������� ����ġ�� 0���� ����
           
        }
    }
}
