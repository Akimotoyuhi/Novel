using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Domove : MonoBehaviour
{
    private RectTransform rect = default;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        Sequence s = DOTween.Sequence();
        s.Append(rect.DOAnchorPos(new Vector2(500, 0), 1))
            .AppendInterval(0.5f)
            .Append(rect.DOAnchorPos(new Vector2(-500, 0), 1));
    }

    void Update()
    {
        
    }
}
