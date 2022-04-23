using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardControl : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> frameDisplays;
    // Start is called before the first frame update
    void Awake()
    {
        frameDisplays = new List<GameObject>(11);
        InitScoreboard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableScoreboard(BowlingFrame[] frames)
    {
        this.gameObject.transform.parent.gameObject.SetActive(true);
        Debug.Log("refresh scoreboard");
        refreshScoreboard(frames);
    }

    void InitScoreboard()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
                frameDisplays.Add(transform.GetChild(i).gameObject);
        }

        frameDisplays.Sort((GameObject t1, GameObject t2) => { return t1.name.CompareTo(t2.name); });
    }

    public void refreshScoreboard(BowlingFrame[] frames)
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("refreshing scoreboard");
            TextMeshProUGUI[] frameData = frameDisplays[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (frames[i].getScoreOne() == -1)
            {
                frameData[0].text = "-";
                frameData[1].text = "-";
                frameData[2].text = "-";
            }
            else
            {
                if(frames[i].getScoreOne() == 10)
                {
                    frameData[0].text = "X";
                }
                else
                {
                    frameData[0].text = frames[i].getScoreOne().ToString();
                }


                if (frames[i].getScoreTwo() == -1)
                {
                    frameData[1].text = "-";
                }
                else
                {
                    if(frames[i].getTotalScore() == 10)
                    {
                        frameData[1].text = "/";
                    }
                    else
                    {
                        frameData[1].text = frames[i].getScoreTwo().ToString();
                    }
                }

            }

            if (frames[i].getTotalScore() == -1)
            {
                frameData[2].text = "-";
            }
            else
            {
                frameData[2].text = frames[i].getTotalScore().ToString();
            }
        }

        frameDisplays[10].GetComponent<TextMeshProUGUI>().text = getTotalScore(frames).ToString();

    }

    public int getTotalScore(BowlingFrame[] frames)
    {
        int totalScore = 0;
        for(int i = 0; i < 10; i++)
        {
            if(frames[i].getTotalScore() != -1)
            {
                totalScore += frames[i].getTotalScore();
            }
        }

        Debug.Log("total score is " + totalScore);
        return totalScore;
    }


    public void closeScoreboard()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void goHome()
    {
        StartCoroutine(loadScene(0));
    }

    IEnumerator loadScene(int index)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;
    }

    public void OnLoadedScene(AsyncOperation obj)
    {
     
    }
}
