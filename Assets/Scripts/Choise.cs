using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choise
{
    /// <summary>選択肢の文言</summary>
    public string[] choiseText;
    /// <summary>０番の選択肢後のテキスト</summary>
    [SerializeField, TextArea(1, 3)] string[] m_choise0;
    /// <summary>１番の選択肢後のテキスト</summary>
    [SerializeField, TextArea(1, 3)] string[] m_choise1;
    /// <summary>２番の選択肢後のテキスト</summary>
    [SerializeField, TextArea(1, 3)] string[] m_choise2;

    public string GetChoiseText(int index)
    {
        return choiseText[index];
    }

    public string[] GetTexts(int num)
    {
        Debug.Log(num);
        if (num == 0)
        {
            return m_choise0;
        }
        else if (num == 1)
        {
            return m_choise1;
        }
        else if (num == 2)
        {
            return m_choise2;
        }
        else
        {
            Debug.LogError($"選択肢領域外 与えられた値:{num}");
            return new string[0];
        }
    }

    public int GetLength(int num)
    {
        if (num == 0)
        {
            return m_choise0.Length;
        }
        else if (num == 1)
        {
            return m_choise1.Length;
        }
        else if (num == 2)
        {
            return m_choise2.Length;
        }
        else
        {
            Debug.LogError($"選択肢領域外 与えられた値:{num}");
            return 0;
        }
    }
}