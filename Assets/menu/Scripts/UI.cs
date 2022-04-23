using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;

    public AudioSource Music;
    public AudioSource audioEffects;
    public AudioClip click;

    public ScrollRect scrollviewShop;
    public Transform ContentMailBox;
    public GameObject ViewportMailbox;
    public GameObject NotifMainMenu;
    public TextMeshProUGUI NbrNotifMainMenu;

    public GameObject BottomMenu;
    public GameObject WindowStats;

    public GameObject eventObj;
    public GameObject[] bottom_btn;
    public GameObject[] Panels;
    public Color colorDisableTextTabFriends;
    public Button AddGems;
    public Button AddCoins;

    public Animator animator;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //animBottom_Button(2);
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.eventObj);
        StartCoroutine("SelectButtonStart");
    }

    void Update()
    {

    }

    public void animBottom_Button(int idButton)
    {
        for (int i = 0; i < bottom_btn.Length; i++)
        {
            if (i != idButton)
            {
                bottom_btn[i].GetComponent<Animator>().SetBool("open", false);
                Panels[i].SetActive(false);
            }
            else
            {
                bottom_btn[idButton].GetComponent<Animator>().SetBool("open", true);
                Panels[idButton].SetActive(true);
            }

            bottom_btn[i].GetComponent<Button>().interactable = false;
        }
        PlayAudioEffects();
        StartCoroutine(ActiveInteract(0.2f, idButton));
    }

    IEnumerator ActiveInteract(float time, int idBtn)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < bottom_btn.Length; i++)
        {
            if (i != idBtn)
            {
                bottom_btn[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    IEnumerator SelectButtonStart()
    {
        yield return new WaitForSeconds(0.1f);
        animBottom_Button(2);
        StopCoroutine("SelectButtonStart");
    }


    public void ShowHideOptionsPanel()
    {
        if (Panels[0].activeSelf)
        {
            Panels[0].SetActive(false);
            Panels[0].SetActive(true);
            BottomMenu.SetActive(true);
            StartCoroutine("SelectButtonStart");
            AddCoins.gameObject.SetActive(true);
        }
        else
        {
            Panels[0].SetActive(true);
            Panels[0].SetActive(false);
            BottomMenu.SetActive(false);
            AddCoins.gameObject.SetActive(false);
        }
    }
    public void ShowlevelSelectPanel()
    {
        if(Panels[1].activeSelf)
        {
            Panels[1].SetActive(false);
            Panels[2].SetActive(true);
            BottomMenu.SetActive(true);
            StartCoroutine("SelectButtonStart");
        }
        else
        {
            Panels[1].SetActive(true);
            Panels[2].SetActive(false);
            BottomMenu.SetActive(true);
            AddCoins.gameObject.SetActive(false);
        }
    }
    public void SelectShopItemMoneyGems()
    {
        for(int i = 0; i < Panels.Length; i++)
        {
            if(i > 0)
            {
                Panels[i].SetActive(false);
            }
        }

        animBottom_Button(0);
        StartCoroutine(ScrollVerticalposition(0.2f));
    }

    public void SelectShopItemMoneyCoins()
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i > 0)
            {
                Panels[i].SetActive(false);
            }
        }

        animBottom_Button(0);
        StartCoroutine(ScrollVerticalposition(0f));
    }

    IEnumerator ScrollVerticalposition(float pos)
    {
        yield return new WaitForSeconds(0.1f);
        scrollviewShop.verticalNormalizedPosition = pos;
        StopCoroutine("ScrollVerticalposition");
    }

    public void ShowStatsRank(ListMembers Lm, int id)
    {
        PlayAudioEffects();
        WindowStats.SetActive(true);
        WindowStats.GetComponent<InitStats>().LoginText.text = Lm.membersList[id].Login;
        WindowStats.GetComponent<InitStats>().LevelText.text = Lm.membersList[id].Level.ToString("00");
        WindowStats.GetComponent<InitStats>().WinsText.text = Lm.membersList[id].totalWins.ToString("00");
        WindowStats.GetComponent<InitStats>().GainText.text = Lm.membersList[id].Gain;
        WindowStats.GetComponent<InitStats>().Avatar.sprite = Lm.membersList[id].avatar;

        AddGems.gameObject.SetActive(false);
        AddCoins.gameObject.SetActive(false);
    }

    public void HideWindowStats()
    {
        PlayAudioEffects();
        if (WindowStats.activeSelf)
        {
            WindowStats.SetActive(false);
        }

        AddGems.gameObject.SetActive(true);
        AddCoins.gameObject.SetActive(true);
    }

    public void PlayAudioEffects()
    {
        if (!audioEffects.isPlaying)
        {
            audioEffects.PlayOneShot(click);
        }
    }
    public void LoadScene(int scene)
    {
        StartCoroutine(loadScene(scene));
    }

    IEnumerator loadScene(int index)
    {
        //animator.SetBool("fadeIn", true);
        //animator.SetBool("fadeout", false);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;
    }
    public void OnLoadedScene(AsyncOperation obj)
    {
        //animator.SetBool("fadeIn", false);
       // animator.SetBool("fadeout", true);
    }
}
