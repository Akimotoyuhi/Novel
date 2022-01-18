using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelect
{
    string GetChoiseText { get; }
    string GetViewText();
}

/// <summary>
/// �I�������P�̏ꍇ
/// </summary>
public class Choices1 : ISelect
{
    /// <summary>�I����</summary>
    [SerializeField] string choiseText;
    /// <summary>�I������̉�b</summary>
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
    /// <summary>�I����</summary>
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
    /// <summary>�I����</summary>
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