using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/*
 *아주 간단한 멀티 스레드 프로그램 예시이다.
 *
 *프로셋스 1개에 스레드 1개는 반드시 동작한다.
 *이를 주 스레드 라고 한다.(MainThread)
 *
 *주 스레드가 동작하고 있는 와중에 별도로 만들어 동작시키는 스레드를
 *부스레드라고 한다(SubThread)
 * 
 * 
 * 
 * 
 * 
 */ 



public class Cexamthread : MonoBehaviour
{
    Thread mThread = null;

    bool mThreadLoop = false;
    void BeginThread()
    {
        mThreadLoop = true;

        mThread = new Thread(new ThreadStart(DoDisPatch));
        mThread.Name = "chun";

        mThread.Start();
    }


    void DoDisPatch()
    {
        //별도의 실행흐름을 유지하기 위해 반복제어구조를 배치했다
        while(mThreadLoop)
        {
            Debug.Log($"DoDispatch ThreadFunction Running. name: {mThread.Name}, id: {mThread.ManagedThreadId.ToString()}");

            //스레드를 잠시 대기 상태로 놓는다(이 스레드의 실행흐름을 잠깐 쉰다 5/1000 초)
            Thread.Sleep(5);
        }

        Debug.Log("DoDispatch ThreadFunction END");
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Thread"))
        {
            //스레드 강제 종료
            //.NET Framework레서 스레드 강제 종료 기능이다.(종료를 반드시 보장하지 않음, 개발용으로만 쓴다)
            mThread.Abort();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginThread", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("MainThread. Update");
    }
}
