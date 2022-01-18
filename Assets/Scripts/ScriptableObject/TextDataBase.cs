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

public enum AnimState
{
    none,
    SasakiHop,
    SasakiSlide
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
    CharaPosition,
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
    object Execute();
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
    public object Execute()
    {
        return new string[] { m_name, m_text, m_duration.ToString() };
    }
}
public class SetFade : IScenarioSetting
{
    enum FadeType { Fadein, Fadeout }
    [SerializeField] FadeType m_type;
    [SerializeField] int m_duration;
    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.Fade;
    public object Execute()
    {
        int[] ret = new int[2];
        if (m_type == FadeType.Fadein) ret[0] = 1;
        else ret[0] = 0;
        ret[1] = m_duration;
        return ret;
    }
}
public class SetCharaJoin : IScenarioSetting
{
    [SerializeField] Image m_image;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.CharaJoin;
    public object Execute()
    {
        return m_image;
    }
}
public class SetCharaPosition : IScenarioSetting
{
    [SerializeField] Vector2 m_position;

    public ScenarioSelectType ScenarioSelectType => ScenarioSelectType.CharaPosition;
    public object Execute()
    {
        return m_position;
    }
}