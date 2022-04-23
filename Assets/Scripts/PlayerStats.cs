using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{


    public static PlayerStats StatKeeper;

    int totalBallCount;

    public string[,] ballsOwned;

    public int playerScore;

    Object[] ballOptions;

    // Start is called before the first frame update
    void Awake()
    {
        if (StatKeeper == null)
        {
            DontDestroyOnLoad(gameObject);
            StatKeeper = this;
        }
        else if(StatKeeper != this)
        {
            Destroy(this.gameObject);
        }

        Load();


        ballOptions = Resources.LoadAll("Images");

        ballsOwned = new string[totalBallCount, 2];

        if (!PlayerPrefs.HasKey("BallDataExists"))
        {
            Debug.Log("data does not exist");
            for (int i = 0; i < totalBallCount; i++)
            {
                ballsOwned[i, 0] = ballOptions[i].ToString().Split(' ')[0];
                if (ballsOwned[i, 0] == "ball_pic" || ballsOwned[i, 0] == "blackball_pic")
                {
                    ballsOwned[i, 1] = "yes";
                }
                else
                {
                    ballsOwned[i, 1] = "no";
                }
            }

            PlayerPrefs.SetString("ball_pic", "yes");
            PlayerPrefs.SetInt("BallDataExists", 1);

            Save();
        }

    }

    void Start()
    {
        PlayerPrefs.SetString("ball_pic", "yes");

        for (int i = 0; i < totalBallCount; i++)
        {
            if(ballsOwned[i,0] == "ball_pic" || ballsOwned[i,0] == "blackball_pic")
            {
                ballsOwned[i, 1] = "yes";
            }
        }
        PlayerPrefs.SetInt("BallDataExists", 1);
        Load();
    }

    void Save()
    {
        for(int i = 0; i < totalBallCount; i++)
        {
            PlayerPrefs.SetString(ballsOwned[i, 0], ballsOwned[i, 1]);
        }

        PlayerPrefs.SetInt("Score", playerScore);

    }

    void Load()
    {
        if(PlayerPrefs.HasKey("BallDataExists"))
        {
            for (int i = 0; i < totalBallCount; i++)
            {
                ballsOwned[i, 1] = PlayerPrefs.GetString(ballsOwned[i, 0]);
            }
            playerScore = PlayerPrefs.GetInt("Score");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
