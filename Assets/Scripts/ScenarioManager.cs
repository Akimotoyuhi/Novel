using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public enum CharactorList
{
    Sasaki,
    Sasaki2,
    Sasaki3,
}
public enum BackGroundList
{
    Space,
}

public class ScenarioManager : MonoBehaviour
{
    #region メンバ
    [SerializeField] TextDataBase m_database;
    /// <summary>表示間隔</summary>
    [SerializeField] float m_time;
    /// <summary>会話テキスト</summary>
    [SerializeField] Text m_viewText;
    /// <summary>名前テキスト</summary>
    [SerializeField] Text m_nameText;
    /// <summary>背景</summary>
    [SerializeField] Image m_backGround;
    /// <summary>フェード用背景</summary>
    [SerializeField] Image m_fadeBackGround;
    /// <summary>フェードパネル</summary>
    [SerializeField] Image m_fadePanel;
    /// <summary>選択肢</summary>
    [SerializeField] GameObject[] m_buttons;
    /// <summary>キャラクター達</summary>
    [SerializeField] Image[] m_images;
    /// <summary>現在表示中のテキスト番号</summary>
    private int m_nowData = 0;
    /// <summary>現在のテキスト番号内のindex</summary>
    //private int m_nowIndex = 0;
    /// <summary>現在表示中のテキスト番号(選択肢中)</summary>
    private int m_nowSelectText = 0;
    /// <summary>喋ってる途中かどうか</summary>
    private bool m_isSpeak = false;
    /// <summary>Updateを止めたいときのフラグ</summary>
    private bool m_stop = false;
    /// <summary>選択肢中フラグ</summary>
    private bool m_isSelect = false;
    /// <summary>選ばれた番号保存用</summary>
    private int m_selectNum = 0;
    private Sequence m_sequence = default;
    //static private string m_folderPath = "C:/Users/vantan/Desktop/unity games/EnsyuuKadaiNovel/Assets/Animation/Clip";
    //private string[] clips = System.IO.Directory.GetFiles(@m_folderPath, "*", System.IO.SearchOption.TopDirectoryOnly);
    #endregion
    public static ScenarioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DisableButton(m_buttons.Length);
        m_nameText.text = "System";
        m_viewText.text = "[[[PleaseSpaceKey]]]";
    }

    void Update()
    {
        //if (m_stop) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_isSpeak)
            {
                m_isSpeak = true;
                m_viewText.text = ""; //テキストリセット
                if (!m_isSelect)
                {
                    if (m_nowData < m_database.Data.Count)
                    {
                        m_sequence = DOTween.Sequence();
                        //データを読み込む
                        for (int i = 0; i < m_database.Data[m_nowData].ScenarioLength; i++)
                        {
                            SequenceSelect(m_database.Data[m_nowData], i, m_sequence);
                        }
                        m_sequence.OnComplete(() => m_isSpeak = false);
                        m_nowData++;
                    }
                    else
                    {
                        m_nameText.text = "System";
                        m_viewText.text = "オワオワリ";
                    }
                }
                else
                {
                    //選択肢テキストの表示
                    //if (m_nowSelectText < m_database.m_data[m_nowText].choise.GetLength(m_selectNum))
                    //{
                    //    StartCoroutine(TextAsync(m_database.m_data[m_nowText].choise.GetTexts(m_selectNum)[m_nowSelectText]));
                    //    m_nowSelectText++;
                    //}
                    //else
                    //{
                    //    m_isSelect = false;
                    //    m_nowSelectText = 0;
                    //    m_nowText++;
                    //}
                }
            }
            else
            {
                m_sequence.Kill(true);
                m_isSpeak = false;
            }
        }
    }

    /// <summary>
    /// 非同期で行う処理達
    /// </summary>
    /// <param name="database"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private Tween SelectTween(DataBase database, int index)
    {
        List<string> list = ToStringList(database.ScenarioSettings(index).Execute());
        Image image;
        Tween ret = null;
        switch (database.ScenarioSettings(index).ScenarioSelectType)
        {
            case ScenarioSelectType.Text:
                m_nameText.text = list[0];
                return m_viewText.DOText(list[1], float.Parse(list[2]));
            case ScenarioSelectType.Fade:
                return m_fadePanel.DOFade(int.Parse(list[0]), float.Parse(list[1]));
            case ScenarioSelectType.CharaJoin:
                image = m_images[int.Parse(list[0])];
                return image.DOFade(int.Parse(list[1]), float.Parse(list[2]));
            case ScenarioSelectType.CharaMove:
                RectTransform rect = m_images[int.Parse(list[0])].GetComponent<RectTransform>();
                return rect.DOAnchorPos(new Vector2(float.Parse(list[1]), float.Parse(list[2])), float.Parse(list[3]));
            case ScenarioSelectType.ChangeBackGround:
                return null;
            default:
                Debug.LogError("予期しないパラメーター");
                break;
        }
        //foreach (var item in database.ScenarioSettings(index).HighlightImages)
        //{

        //}
        return ret;
    }

    /// <summary>
    /// AppendやJoinの制御
    /// </summary>
    /// <param name="database"></param>
    /// <param name="index"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    private Sequence SequenceSelect(DataBase database, int index, Sequence s)
    {
        if (database.ScenarioSettings(index).ScenarioSelectType == ScenarioSelectType.Interval)
        {
            return s.AppendInterval(float.Parse(database.ScenarioSettings(index).Execute()[0]));
        }
        switch (database.ScenarioSettings(index).SequenceType)
        {
            case SequenceType.Append:
                return s.Append(SelectTween(database, index));
            case SequenceType.Join:
                return s.Join(SelectTween(database, index));
            default:
                Debug.LogError("予期しないパラメーター");
                return null;
        }
    }

    public List<string> ToStringList(object obj)
    {
        List<string> list = new List<string>();
        string[] vs = (string[])obj;
        foreach (string s in vs)
        {
            list.Add(s);
        }
        return list;
    }

    /// <summary>
    /// テキストを１文字づつ表示する
    /// </summary>
    /// <param name="t">喋らせる文字列</param>
    /// <returns></returns>
    private IEnumerator TextAsync(string t, float time)
    {
        time *= 60;
        m_isSpeak = true;
        for (int i = 0; i < t.Length; i++)
        {
            for (int n = 0; n < time; n++)
            {
                if (!m_isSpeak)
                {
                    m_viewText.text += t;
                    yield return null;
                }
            }
            m_viewText.text += t[i];
            yield return null;
        }
        m_isSpeak = false;
    }

    private void ViewButton(int value)
    {
        m_stop = true; //選択肢中ではゲームを一旦止めておく
        for (int i = 0; i < value; i++)
        {
            m_buttons[i].SetActive(true);
            //m_buttons[i].transform.GetChild(0).GetComponent<Text>().text = m_database.m_data[m_nowText].choise.GetChoiseText(i);
        }
    }

    private void SelectionText(int index)
    {
        m_stop = false;
        m_isSelect = true;
        DisableButton(m_buttons.Length); //選択肢選んだらボタンを消す
        m_selectNum = index;
        m_viewText.text = "";
        //StartCoroutine(TextAsync(m_database.m_data[m_nowText].choise.GetTexts(m_selectNum)[m_nowSelectText]));
        m_nowSelectText++;
        //m_selectionTexts = m_database.m_data[m_nowText].choise.GetTexts(index);
    }

    private void DisableButton(int value)
    {
        for (int i = 0; i < value; i++)
        {
            m_buttons[i].SetActive(false);
        }
    }

    public void Select1()
    {
        Debug.Log("選択肢１");
        SelectionText(0);
    }

    public void Select2()
    {
        Debug.Log("選択肢２");
        SelectionText(1);
    }
    public void Select3()
    {
        Debug.Log("選択肢３");
        SelectionText(2);
    }
}
