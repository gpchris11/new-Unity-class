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


    //�ڷ�ƾ �Լ�
    //<-- ����ž���� IEnumerator �� �����.
    //<-- yiedl return~; �� �����Ѵ�.( ������ �帧�� �纸�ϴ� ���̴� )
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
