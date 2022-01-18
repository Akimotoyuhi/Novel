using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    [SerializeField] int m_spinFrame = 60;
    private bool _isRotate = false;

    private void Start()
    {
        //// IEnumerable: 繰り返し処理できるやつ（コレクション）
        //IEnumerable ary = new int[] { 1, 2, 3, 4, 5 };

        //// IEnumerator: 反復処理そのもの（イテレーター）
        //IEnumerator e = ary.GetEnumerator();

        //// MoveNext() で次の要素を取り出す
        //// 続きの要素があるなら true。なければ false。
        //while (e.MoveNext())
        //{
        //    // Current から現在指している値を取得できる
        //    Debug.Log($"Current={e.Current}");
        //}

        //IEnumerator e = DoAsync();

        //e.MoveNext(); // true
        //Debug.Log($"Current={e.Current}"); // 1

        //e.MoveNext(); // true
        //Debug.Log($"Current={e.Current}"); // 2

        //e.MoveNext(); // true
        //Debug.Log($"Current={e.Current}"); // 3

        //e.MoveNext(); // false

        // IEnumerable なら foreach できる
        //foreach (var i in DoAsync())
        //{
        //    Debug.Log(i);
        //}
        StartCoroutine(AsyncA());
    }

    // yield return 文が使えるのは
    // 戻り値が IEnumerable または IEnumerator のメソッド
    private IEnumerator DoAsync()
    {
        Debug.Log("DoAsync return 1");
        yield return 1;

        Debug.Log("DoAsync return 2");
        yield return 2;

        Debug.Log("DoAsync return 3");
        yield return 3;
    }

    private IEnumerator AsyncA()
    {
        while (true)
        {
            //for (int i = 0; i < 60; i++)
            //{
            //    transform.Rotate(1, 0, 0);
            //    yield return null;
            //}
            //yield return new WaitForSeconds(1f);
            //for (int i = 0; i < 60; i++)
            //{
            //    transform.Rotate(0, 1, 0);
            //    yield return null;
            //}
            //yield return new WaitForSeconds(1f);
            //for (int i = 0; i < 60; i++)
            //{
            //    transform.Rotate(0, 0, 1);
            //    yield return null;
            //}
            //yield return new WaitForSeconds(1f);

            // コルーチンの階層化
            // コルーチンを返すと、そのコルーチンに制御が移る
            yield return Rotate(Vector3.right);
            yield return new WaitForSeconds(1f);
            yield return Rotate(Vector3.up);
            yield return new WaitForSeconds(1f);
            yield return Rotate(Vector3.forward);
            yield return new WaitForSeconds(1f);
        }
    }
    
    private IEnumerator Rotate(Vector3 v)
    {
        for (int i = 0; i < 60; i++)
        {
            transform.Rotate(v);
            yield return null;
        }
    }
}