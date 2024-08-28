using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPChar_step_0 : MonoBehaviour
{
    //미리 참조를 담아두어 보다 빠르게 접근
    Transform mTransform = null;

    //좌우 선회 속력
    [SerializeField]        //유니티 에디터에 통합시켜 반복개발을 수월하게 해준다.
    float mSpeedRotate = 0f;//private이므로 은닉화encapsulation 달성

    //전후 이동 속력
    [SerializeField]
    float mSpeedForward = 0f;


    // Start is called before the first frame update
    void Start()
    {
        mTransform = this.transform;//참조를 캐쉬해둔다.
    }

    // Update is called once per frame
    void Update()
    {
        //축입력 
        float tV = Input.GetAxis("Vertical");//[-1, +1]        
        float tH= Input.GetAxis("Horizontal");


        //좌우 선회
        //Rotate함수는 '사원수'를 기반으로 작동한다.<--사원수는 나중에 살펴보도록 하자.
        mTransform.Rotate(Vector3.up, tH * mSpeedRotate * Time.deltaTime);

        //속도 = 거리의 변화량/시간의 변화량.             벡터이다.
        //Vector3 tVelocity = Vector3.zero;//영벡터로 초기화
        //tVelocity = Vector3.forward * mSpeedForward*tV*Time.deltaTime;  //벡터의 스칼라곱셈
        ////프레임 시간을 곱하여 시간기반 진행을 한다.
        //mTransform.Translate(tVelocity, Space.Self);//local 좌표계 기준으로 속도 적용


        //속도 = 거리의 변화량/시간의 변화량.             벡터이다.
        Vector3 tVelocity = Vector3.zero;//영벡터로 초기화
        tVelocity = mTransform.forward * mSpeedForward * tV * Time.deltaTime;  //벡터의 스칼라곱셈

        //영벡터가 아니면 이동
        if (!tVelocity.Equals(Vector3.zero))
        {
            //프레임 시간을 곱하여 시간기반 진행을 한다.
            mTransform.Translate(tVelocity, Space.World);//World 좌표계 기준으로 속도 적용
        }

        //Transform.forward는 로컬좌표계 상의 전방벡터를 월드좌표계 기준의 수치로 나타낸 것
        

        /*
            게임엔진에서 이동을 수행하는 방법 요약
            i) 직접 좌표를 지정하는 방법

                현재 좌표 = 이전 좌표 + 속도*시간간격
                <--오일러 수치해석 방법에 의한 이동

                게임 프로그래밍에서는 조금 부정확해도 연산이 적은 것을 선택하여
                오일러 수치해석에 의한 이동 방법을 쓴다.

            ii) 게임엔진에서 제공하는 이동함수를 이용하는 방법
            iii) 물리엔진을 이용하는 방법

            iv) 애니메이션 안에 이동을 포함하는 방법
        */
    }
}
