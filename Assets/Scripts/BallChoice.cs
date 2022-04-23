using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallChoice : MonoBehaviour
{

    public UnityEngine.Object[] ballChoices;

    public RawImage ballOnePic;
    public RawImage ballTwoPic;
    public RawImage ballThreePic;
    public int currentPos;

    public TextMeshProUGUI ballStatus;
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        ballChoices = Resources.LoadAll("Images", typeof(Texture2D));
        ballOnePic.texture = (Texture2D)ballChoices[0];
        ballTwoPic.texture = (Texture2D)ballChoices[1];
        ballThreePic.texture = (Texture2D)ballChoices[2];
        UpdateBallChoiceVisuals(0);
        currentPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBallChoiceVisuals(int direction)
    {
        int ballOptioncount = ballChoices.Length;
        if (direction == 1)
            currentPos++;
        else if (direction == -1)
            currentPos--;

        if (currentPos >= ballOptioncount)
            currentPos = 0;

        if (currentPos < 0)
            currentPos = ballOptioncount - 1;


        if (currentPos == ballOptioncount - 2)
        {
            ballOnePic.texture = (Texture2D)ballChoices[currentPos];
            ballTwoPic.texture = (Texture2D)ballChoices[currentPos + 1];
            ballThreePic.texture = (Texture2D)ballChoices[ballChoices.Length - (currentPos + 2)];
        }
        else if (currentPos == ballOptioncount - 1)
        {
            ballOnePic.texture = (Texture2D)ballChoices[currentPos];
            ballTwoPic.texture = (Texture2D)ballChoices[ballChoices.Length - (currentPos + 1)];
            ballThreePic.texture = (Texture2D)ballChoices[ballChoices.Length - (currentPos)];
        }
        else
        {
            ballOnePic.texture = (Texture2D)ballChoices[currentPos];
            ballTwoPic.texture = (Texture2D)ballChoices[currentPos + 1];
            ballThreePic.texture = (Texture2D)ballChoices[currentPos + 2];
        }

        if (PlayerPrefs.GetString(ballOnePic.texture.name) != "yes")
        {
            ballOnePic.color = new Color(.45f, .7f, .7f, .35f);
        }
        else
        {
            ballOnePic.color = new Color(255, 255, 255, 255);
        }

        if (PlayerPrefs.GetString(ballTwoPic.texture.name) != "yes")
        {
            ballTwoPic.color = new Color(.45f, .7f, .7f, .35f);
            ballStatus.text = "LOCKED";
        }
        else
        {
            ballTwoPic.color = new Color(255, 255, 255, 255);
            ballStatus.text = "UNLOCKED";
        }

        if (PlayerPrefs.GetString(ballThreePic.texture.name) != "yes")
        {
            ballThreePic.color = new Color(.45f, .7f, .7f, .35f);
        }
        else
        {
            ballThreePic.color = new Color(255, 255, 255, 255);
        }

    }

    public void ChoiceMade(int direction)
    {
        if (direction == 0)
        {
            if (PlayerPrefs.GetString(ballTwoPic.texture.name) != "yes")
            {
                // Add no sound effect or screen shake or something.
            }
            else
            {
                Debug.Log("ball chosen is " + ballTwoPic.texture.name);
                gameState.BeginGame(ballTwoPic.texture.name.Split('_')[0]);
            }
        }
        else
        {
            UpdateBallChoiceVisuals(direction);
        }
    }
}
