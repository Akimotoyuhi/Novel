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
    //private Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
    //public Dictionary<string, Sprite> GetKeyValuePairs => dic;
    public void Setup()
    {
        Debug.LogWarning("Setup‚É‚È‚ñ‚à‚È‚¢‚·");
        //dic.Add(m_data.);
    }
}

public enum FadeType
{
    Fadein,
    Fadeout
}

[Serializable]
public class DataBase
{
    public string m_label;
    [SerializeField] bool m_isAppend = false;
    [SerializeReference, SubclassSelector]
    private List<IScenarioSetting> m_scenarioSettings;
    public bool IsAppend => m_isAppend;
    public int ScenarioLength => m_scenarioSettings.Count;
    public IScenarioSetting ScenarioSettings(int index)
    {
        return m_scenarioSettings[index];
    }
}

public enum ScenarioSelectType
{
    Text,
    Fade,
    CharaJoin,
    CharaMove,
}

public interface IScenarioSetting
{
    /// <summary>
    /// ğŒ•ªŠò—p
    /// </summary>
    public ScenarioSelectType ScenarioSelectType { get; }
    /// <summary>
    /// î•ñ‚Ìó‚¯“n‚µ
    /// </summary>
    /// <returns></returns>
    string[] Execute();
}
public class SetText : IScenarioSetting
{
    [SerializeField]
    string m_name;
    [SerializeField, TextArea(0, 3)]
    string m_text;
    [SerializeField]
    float m_duration;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Text;
    public string[] Execute()
    {
        return new string[] { m_name, m_text, m_duration.ToString() };
    }
}
public class SetFade : IScenarioSetting
{

    [SerializeField] FadeType m_fadeType;
    [SerializeField] float m_duration;
    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Fade;
    public string[] Execute()
    {
        string[] ret = new string[2];
        if (m_fadeType == FadeType.Fadein) ret[0] = "1";
        else ret[0] = "0";
        ret[1] = m_duration.ToString();
        return ret;
    }
}
public class SetCharaFade : IScenarioSetting
{
    [SerializeField] CharactorList m_fadeChara;
    [SerializeField] FadeType m_fadeType;
    [SerializeField] float m_duration;

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
}
public class SetCharaMove : IScenarioSetting
{
    [SerializeField] CharactorList m_fadeChara;
    [SerializeField] Vector2 m_endPosition;
    [SerializeField] float m_duration;

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
}