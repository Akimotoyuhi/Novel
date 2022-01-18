using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choise
{
    /// <summary>�I�����̕���</summary>
    public string[] choiseText;
    /// <summary>�O�Ԃ̑I������̃e�L�X�g</summary>
    [SerializeField, TextArea(1, 3)] string[] m_choise0;
    /// <summary>�P�Ԃ̑I������̃e�L�X�g</summary>
    [SerializeField, TextArea(1, 3)] string[] m_choise1;
    /// <summary>�Q�Ԃ̑I������̃e�L�X�g</summary>
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
            Debug.LogError($"�I�����̈�O �^����ꂽ�l:{num}");
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
            Debug.LogError($"�I�����̈�O �^����ꂽ�l:{num}");
            return 0;
        }
    }
}