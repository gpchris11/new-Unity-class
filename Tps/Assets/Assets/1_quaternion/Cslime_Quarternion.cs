using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//z x y 순으로 각각 90도 degree 회전

//사원수 * 사원수 * 사원수


public class Cslime_Quarternion : MonoBehaviour
{
    Quaternion mStart = Quaternion.identity;  //단위원 사원수(회전이 없다 라는 의미)로 초기화
    Quaternion mEnd = Quaternion.identity;  //단위원 사원수(회전이 없다 라는 의미)로 초기화

    Matrix4x4 mMatRot = Matrix4x4.identity; //단위 행렬로 초기화


    MeshFilter mMeshFilter = null; //정점 정보들을 조회하기 위한 메쉬필터 컴포넌드의 참조
    Vector3[] mOriginVertices; //매쉬의 원래 정점들
    Vector3[] mNewVertices; //회전 변환이 적용된 새로운 정점들

    //IMGUI 개발용으로 자주 쓰이는 UI제작 시스템이다.
    private void OnGUI()
    {
        //사원수의 보간(Interpolation: 근사치를 구한다)
        if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "Quaternion\n보간"))
        {
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(0f, 90f, 0f);

            //this.transform.rotation = mEnd;
            mWeight = 0f;

            //상태변경
            mState = STATE.WITH_INTERPOLATION;
        }

        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Quaternion 곱셈"))
        {
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(0f, 90f, 0f);
        
            this.transform.rotation = mEnd;
        }



        if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "Transform Rotate 곱셈"))
        {
            

            this.transform.Rotate(0f, 0f, 90f);
            this.transform.Rotate(90f, 0f, 0f);
            this.transform.Rotate(0f, 90f, 0f);
        }

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "Euler\nQuaternion.Euler"))
        {
            //zxy순으로 90, 90, 90을 Quaternion.Euler로 표현
            //이것은 오일러 각에 의한 회전을 적용한 결과를 사원수로 만든것이다. 즉, 오일러 각의 의한 회전이다.
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
            mOriginVertices = mMeshFilter.mesh.vertices; //정점을 조회하여 담아둔다.(값타입이므로 복사되어 담긴다)

            //변환된 위치 값들을 담을 새로운 원소들의 배열을 만들자.
            mNewVertices = new Vector3[mOriginVertices.Length];


            int ti = 0;
            while(ti < mOriginVertices.Length)
            {
                mNewVertices[ti] =  mMatRot.MultiplyPoint3x4(mOriginVertices[ti]);

                ++ti;
            }

            //메쉬필터 컴포넌트에 (회전 변환 행렬이 적용된) 새로운 메쉬를 적용한다.
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
        WITH_INTERPOLATION  //보간상태

    }

    STATE mState = STATE.WITH_NONE;

    float mWeight = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(STATE.WITH_INTERPOLATION == mState)
        {
            //사원수의 선형보간
            //this.transform.rotation = Quaternion.Lerp(mStart, mEnd, mWeight);
            this.transform.rotation = Quaternion.Slerp(mStart, mEnd, mWeight);
            //구면 선형보간 Slerp Sphere Linear Interpolation 구면 선형보간
            /*
              선형 보간은 일차함수를 이용하여 근사치를 구한다.
              구면 선형 보간은 두 점 사이의 호를 사용하여 근사치를 구한다.
            */
            mWeight += Time.deltaTime;

            



            //선형보간의 가중치는 0부터 시작
           
        }
    }
}
