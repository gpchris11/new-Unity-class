using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/*
 *���� ������ ��Ƽ ������ ���α׷� �����̴�.
 *
 *���μ½� 1���� ������ 1���� �ݵ�� �����Ѵ�.
 *�̸� �� ������ ��� �Ѵ�.(MainThread)
 *
 *�� �����尡 �����ϰ� �ִ� ���߿� ������ ����� ���۽�Ű�� �����带
 *�ν������� �Ѵ�(SubThread)
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
        //������ �����帧�� �����ϱ� ���� �ݺ�������� ��ġ�ߴ�
        while(mThreadLoop)
        {
            Debug.Log($"DoDispatch ThreadFunction Running. name: {mThread.Name}, id: {mThread.ManagedThreadId.ToString()}");

            //�����带 ��� ��� ���·� ���´�(�� �������� �����帧�� ��� ���� 5/1000 ��)
            Thread.Sleep(5);
        }

        Debug.Log("DoDispatch ThreadFunction END");
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Thread"))
        {
            //������ ���� ����
            //.NET Framework���� ������ ���� ���� ����̴�.(���Ḧ �ݵ�� �������� ����, ���߿����θ� ����)
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
