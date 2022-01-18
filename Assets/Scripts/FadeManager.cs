using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager
{
    public IEnumerator Fade(Image image, Color fadeColor)
    {
        while (true)
        {
            image.color -= fadeColor;
        }
    }
}