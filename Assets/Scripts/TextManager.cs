using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    #region Fields
    [SerializeField] TextDataBase m_database;
    [SerializeField] TextDataBase m_text;
    /// <summary>表示間隔</summary>
    [SerializeField] float m_time;
    /// <summary>会話テキスト</summary>
    [SerializeField] Text m_viewText;
    /// <summary>名前テキスト</summary>
    [SerializeField] Text m_nameText;
    /// <summary>背景</summary>
    [SerializeField] Image m_backGround;
    [SerializeField] Image m_fadePanel;
    /// <summary>選択肢</summary>
    [SerializeField] GameObject[] m_buttons;
    /// <summary>アニメーション</summary>
    [SerializeField] Animator m_anim;
    /// <summary>現在表示中のテキスト番号</summary>
    private int m_nowText = 0;
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
    //static private string m_folderPath = "C:/Users/vantan/Desktop/unity games/EnsyuuKadaiNovel/Assets/Animation/Clip";
    //private string[] clips = System.IO.Directory.GetFiles(@m_folderPath, "*", System.IO.SearchOption.TopDirectoryOnly);
    #endregion
    public static TextManager Instance { get; private set; }
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
        if (m_stop) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_isSpeak)
            {
                m_viewText.text = ""; //テキストリセット

                if (!m_isSelect)
                {
                    if (m_nowText < m_database.Data.Count)
                    {
                        var sequence = DOTween.Sequence();
                        for (int i = 0; i < m_database.Data[m_nowText].ScenarioLength; i++)
                        {
                            if (m_database.Data[m_nowText].IsAppend)
                            {
                                sequence.Append(SelectTween(m_database.Data[m_nowText], i));
                            }
                            else
                            {
                                sequence.Join(SelectTween(m_database.Data[m_nowText], i));
                            }
                        }
                        m_nowText++;
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
                m_isSpeak = false;
            }
        }
    }

    private Tween SelectTween(DataBase database, int index)
    {
        switch (database.ScenarioSettings(index).ScenarioSelectType)
        {
            case ScenarioSelectType.Text:
                string[] data = (string[])m_database.Data[m_nowText].ScenarioSettings(index).Execute();
                m_nameText.text = data[0];
                return m_viewText.DOText(data[1], float.Parse(data[2]));
                //StartCoroutine(TextAsync(data[1], 30));
                //break;
            case ScenarioSelectType.Fade:
                int[] num = (int[])m_database.Data[m_nowText].ScenarioSettings(index).Execute();
                return m_fadePanel.DOFade(num[0], num[1]);
            //break;
            default:
                return null;
        }
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
