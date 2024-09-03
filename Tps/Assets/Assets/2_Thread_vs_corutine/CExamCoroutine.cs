using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExamCoroutine : MonoBehaviour
{

    IEnumerator mDoDispatch = null;

    void BegineCoroutine()
    {
        mDoDispatch = DoDispatch();

        StartCoroutine(DoDispatch());
    }


    //코루틴 함수
    //<-- 리턴탑임을 IEnumerator 로 만든다.
    //<-- yiedl return~; 로 리턴한다.( 실행의 흐름을 양보하는 것이다 )
    IEnumerator DoDispatch()
    {
        Debug.Log("DoDispatch");

        for(; ;)
        {
            Debug.Log("DoDispatch 00");

            yield return null;

            Debug.Log("DoDispatch 01");
        }


    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Coroutine"))
        {
            StopCoroutine(mDoDispatch);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginCoroutine", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("MainThread. update");
    }
}
