using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu]
public class TextDataBase : ScriptableObject
{
    [SerializeField] List<DataBase> m_data = new List<DataBase>();
    public List<DataBase> Data => m_data;
}
public enum FadeType
{
    Fadein,
    Fadeout
}
public enum SequenceType
{
    Append,
    Join,
}
public enum ScenarioSelectType
{
    Text,
    Fade,
    CharaJoin,
    CharaMove,
    Interval,
    ChangeBackGround,
}
public enum HighlightImage
{
    Right,
    Center,
    Left,
}
[Serializable]
public class DataBase
{
    public string m_label;
    [SerializeReference, SubclassSelector]
    List<IScenarioSetting> m_scenarioSettings;
    public int ScenarioLength => m_scenarioSettings.Count;
    public IScenarioSetting ScenarioSettings(int index)
    {
        return m_scenarioSettings[index];
    }
}

public interface IScenarioSetting
{
    /// <summary>
    /// 条件分岐用
    /// </summary>
    ScenarioSelectType ScenarioSelectType { get; }
    /// <summary>
    /// 情報渡し用
    /// </summary>
    /// <returns></returns>
    string[] Execute();
    SequenceType SequenceType { get; }
    public List<HighlightImage> HighlightImages { get; }
}
public class SetText : IScenarioSetting
{
    [SerializeField] SequenceType m_sequenceType;
    [SerializeField] string m_name;
    [SerializeField, TextArea(0, 3)]
    　　　　　　　　 string m_text;
    [SerializeField] float m_duration;
    [SerializeField] List<HighlightImage> m_highlightImages;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Text;
    public string[] Execute()
    {
        return new string[] { m_name, m_text, m_duration.ToString() };
    }
    public SequenceType SequenceType => m_sequenceType;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}
public class SetFade : IScenarioSetting
{
    [SerializeField] SequenceType m_sequenceType;
    [SerializeField] FadeType m_fadeType;
    [SerializeField] float m_duration;
    [SerializeField] List<HighlightImage> m_highlightImages;
    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Fade;
    public string[] Execute()
    {
        string[] ret = new string[2];
        if (m_fadeType == FadeType.Fadein) ret[0] = "1";
        else ret[0] = "0";
        ret[1] = m_duration.ToString();
        return ret;
    }
    public SequenceType SequenceType => m_sequenceType;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}
public class SetCharaFade : IScenarioSetting
{
    [SerializeField] SequenceType m_sequenceType;
    [SerializeField] CharactorList m_fadeChara;
    [SerializeField] FadeType m_fadeType;
    [SerializeField] float m_duration;
    [SerializeField] List<HighlightImage> m_highlightImages;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.CharaJoin;
    public string[] Execute()
    {
        string[] ret = new string[3];
        int i = (int)m_fadeChara;
        ret[0] = i.ToString();
        if (m_fadeType == FadeType.Fadein) ret[1] = "1";
        else ret[1] = "0";
        ret[2] = m_duration.ToString();
        return ret;
    }
    public SequenceType SequenceType => m_sequenceType;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}
public class SetCharaMove : IScenarioSetting
{
    [SerializeField] SequenceType m_sequenceType;
    [SerializeField] CharactorList m_fadeChara;
    [SerializeField] Vector2 m_endPosition;
    [SerializeField] float m_duration;
    [SerializeField] List<HighlightImage> m_highlightImages;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.CharaMove;
    public string[] Execute()
    {
        string[] ret = new string[4];
        int i = (int)m_fadeChara;
        ret[0] = i.ToString();
        ret[1] = m_endPosition.x.ToString();
        ret[2] = m_endPosition.y.ToString();
        ret[3] = m_duration.ToString();
        return ret;
    }
    public SequenceType SequenceType => m_sequenceType;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}

public class Interval : IScenarioSetting
{
    [SerializeField] float m_intervalTime = 0;
    [SerializeField] List<HighlightImage> m_highlightImages;
    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Interval;
    public string[] Execute()
    {
        return new string[1] { m_intervalTime.ToString() };
    }
    public SequenceType SequenceType => SequenceType.Append;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}
public class ChangeBackGround : IScenarioSetting
{
    [SerializeField] Sprite m_backGroundSprite;
    [SerializeField] float m_fadeInterval;
    [SerializeField] SequenceType m_sequenceType;
    [SerializeField] List<HighlightImage> m_highlightImages;
    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.ChangeBackGround;
    public string[] Execute()
    {
        return new string[] { "" };
    }
    public SequenceType SequenceType => m_sequenceType;
    public List<HighlightImage> HighlightImages => m_highlightImages;
}