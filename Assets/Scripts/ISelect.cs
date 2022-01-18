using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelect
{
    string GetChoiseText { get; }
    string GetViewText();
}

/// <summary>
/// 選択肢が１つの場合
/// </summary>
public class Choices1 : ISelect
{
    /// <summary>選択肢</summary>
    [SerializeField] string choiseText;
    /// <summary>選択肢語の会話</summary>
    [SerializeField] string[] m_text;

    public string GetChoiseText { get { return choiseText; } }
    public string GetViewText()
    {
        string ret = "";
        return ret;
    }
}

public class Choices2 : ISelect
{
    /// <summary>選択肢</summary>
    [SerializeField] string choiseText;
    [SerializeField] string[] m_texts1;
    [SerializeField] string[] m_texts2;

    public string GetChoiseText { get { return choiseText; } }
    public string GetViewText()
    {
        string ret = "";
        return ret;
    }
}
public class Choices3 : ISelect
{
    /// <summary>選択肢</summary>
    [SerializeField] string choiseText;
    [SerializeField] string[] m_texts1;
    [SerializeField] string[] m_texts2;
    [SerializeField] string[] m_texts3;

    public string GetChoiseText { get { return choiseText; } }
    public string GetViewText()
    {
        string ret = "";
        return ret;
    }
}