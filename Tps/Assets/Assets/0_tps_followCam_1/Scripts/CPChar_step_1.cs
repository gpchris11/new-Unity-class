using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPChar_step_1 : MonoBehaviour
{
    //캐릭터의 움직임(이동)을 담당하는 컴포넌트는 이것을 항상 사용할 것이라고 가정
    //<--그래서 유니티 에디터에서 참조하도록 하겠다 라고 결정
    [SerializeField]
    CharacterController mCharController = null;
    // Rigidbody 물리가 적용되지 않는 경우 사용


    //속력
    [SerializeField]
    float mSpeed = 0f;

    //속도
    [SerializeField]
    Vector3 mVelocity = Vector3.zero;

    //중력가속도  y성분, 즉 스칼라
    [SerializeField]
    float GRAVITY = -9.8f;

    [SerializeField]
    float mJumpPower = 0f;      //점프 힘의 크기( 속도의 y성분에 대응되는 스칼라)



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //지표면에 닿아있다면 
        if (mCharController.isGrounded)
        {
            //축입력 
            float tV = Input.GetAxis("Vertical");//[-1, +1]        
            float tH = Input.GetAxis("Horizontal");

            //동작을 관찰해보면 Move는 월드좌표계 기준으로 작동한다.
            //그러므로 주인공캐릭터 전방이라는 표현이 필요하다.
            //입력값을 기반으로, zx평면에서의 속도 결정
            Vector3 tVelocity = new Vector3(tH, 0f, tV);    //local좌표계 상에서 구한 방향        
            tVelocity = mCharController.transform.TransformDirection(tVelocity);    //local --> world '변환'한 전방
            mVelocity = tVelocity.normalized*mSpeed;//임의의 속도 지정
        }
        else//지표면에 닿아있지 않다면 ( 즉, 공중이라면 )
        {
            //현재 속도 = 이전 속도 + 가속도*시간간격
            //현재 위치 = 이전 위치 + 속도*시간간격

            //오일러 방법에 의한 속도 결정 식
            mVelocity.y = mVelocity.y + GRAVITY * Time.deltaTime;
        }


        if(Input.GetKeyUp(KeyCode.Space))            
        {
            mVelocity.y = mJumpPower;
        }

        //CharacterController에서 제공하는  Move함수를 이용한다.
        //<-- 오일러 수치해석 방법에 의해 작동하는 함수이다.
        mCharController.Move(mVelocity*Time.deltaTime);//<--시간기반 진행




        //카메라가 바라보는 방향으로 선회
        Vector3 tDir = Camera.main.transform.forward;
        tDir.y = 0f;
        //카메라 전방 방향의 임의의 지점을 구한다.
        Vector3 tLookAtPosition = this.transform.position + tDir;
        //카메라 전방 방향의 임의의 지점을 바라보게 한다.
        this.transform.LookAt(tLookAtPosition);


    }
}
