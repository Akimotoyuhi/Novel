using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katasuironn : MonoBehaviour
{
    [SerializeField] GameObject _item;

    void Start()
    {
        // 型キャスト演算子、失敗するとエラーになる
        //var rt = (RectTransform)_item.transform;

        // is 演算子。指定の型かどうかを調べる。
        //if (_item.transform is RectTransform)
        //{
        //    // このキャストなら安全だけど、型チェックが2重なのが無駄
        //    var rt = (RectTransform)_item.transform;
        //}

        // as 演算子。変換できなければ null になる。
        // 型チェックの最も無難な文法。昔から使える。
        //var rt = _item.transform as RectTransform;
        //if (rt != null) { }

        // パターンマッチング(is パターン式)
        // 型チェックと型変換を同時に行える。モダンな書き方。
        if (_item.transform is RectTransform rt) { }
    }
}
