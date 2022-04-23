using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameState : MonoBehaviour
{
    float score;
    public BowlingFrame[] frames = new BowlingFrame[10];
    double frameStatus;
    public bool[] currentPins = new bool[10];
    public GameObject[] pinObjects = new GameObject[10];

    public bool ballRolling;
    public float timeSincePinHit;

    public GameObject ball;
    public GameObject pins;

    public GameObject pinsPrefab;
    public GameObject ballPrefab;

    InputAction fireAction;
    InputAction resumeAction;

    float timeOfRound = 2.75f;

    public GameObject scoreUI;
    public BallChoice ballChooser;

    public AudioSource soundPlayer;
    public Sounds strikeNoise;

    // Start is called before the first frame update
    void Start()
    {
        
        ball = GameObject.Find("ball");
        pins = GameObject.Find("Pins");
        frameStatus = 1.0;
        for(int i = 0; i < 10; i++)
        {
            BowlingFrame frame = new BowlingFrame(i+1);
            frames[i] = frame;
            currentPins[i] = false;
        }

        ballRolling = false;
        timeSincePinHit = 0.0f;

        pinsPrefab = (GameObject)Resources.Load("Prefabs/Pins");

        fireAction = this.GetComponent<PlayerInput>().actions.FindAction("Fire");
        resumeAction = this.GetComponent<PlayerInput>().actions.FindAction("Continue");

        fireAction.Disable();
        resumeAction.Disable();
        //this.GetComponent<PlayerInput>().currentActionMap = this.GetComponent<PlayerInput>().ac

    }

    public void OpenScore()
    {
        fireAction.Disable();
        resumeAction.Disable();
        scoreUI.SetActive(true);
        scoreUI.GetComponentInChildren<ScoreboardControl>().enableScoreboard(frames);
    }

    public void CloseScore()
    {
        scoreUI.SetActive(false);
        fireAction.Enable();
    }

    public void BeginGame(string ballChoice)
    {
        ballPrefab = (GameObject)Resources.Load("Prefabs/" + ballChoice);
        ball = GameObject.Instantiate(ballPrefab);
        initPins();

        ball.name = "ball";
        ball.SetActive(true);
        fireAction.Enable();
        GameObject.Find("Ball Choice UI").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(ballRolling)
        {
            timeSincePinHit += Time.fixedDeltaTime;
            if (timeSincePinHit > timeOfRound)
            {
                ballRolling = false;
                updateFrame();
                //fireAction.Enable();
            }
        }
        
    }

    void OnFire(InputValue fireValue)
    {
        timeSincePinHit = 0.0f;
        ballRolling = true;
        fireAction.Disable();
        resumeAction.Enable();
    }

    void OnContinue(InputValue contValue)
    {
        if (timeSincePinHit > timeOfRound)
        {
            fireAction.Enable();
            resumeAction.Disable();
        }
    }

    public void pinExists(GameObject pin)
    {
        int pinNo = int.Parse(pin.name);
        pinObjects[pinNo - 1] = pin;
    }

    bool strikeNoisePlayed = false;

    public void pinHit(string pinName)
    {
        timeSincePinHit = 0.0f;
        soundPlayer.Play();
        Debug.Log("Pin name is..??? :" + pinName);
        int pinHit = int.Parse(pinName);
        //BowlingFrame currentFrame = frames[Mathf.FloorToInt((float) frameStatus) - 1];
        currentPins[pinHit - 1] = true;

        int hitCount = 0;
        for(int i = 0; i < 10; i++)
        {
            if(currentPins[i] == true)
            {
                hitCount++;
            }
        }

        if(hitCount == 10)
        {
            if(frameStatus % 1 == 0 && strikeNoisePlayed == false)
            {
                strikeNoise.playSound("strike");
                strikeNoisePlayed = true;
            }
            else
            {
                strikeNoise.playSound("spare");
            }
        }

    }

    void updateFrame()
    {
        strikeNoisePlayed = false;
        int newPinsHit = 0;
        for(int i = 0; i < 10; i++)
        {
            if(currentPins[i] == true)
            {
                if(pinObjects[i].activeSelf)
                {
                    pinObjects[i].SetActive(false);
                    newPinsHit++;
                }    
            }
        }
        int whatever = Mathf.FloorToInt((float)frameStatus) - 1;
        Debug.Log("That frame was " + whatever + " and it ");
        frames[Mathf.FloorToInt((float)frameStatus) - 1].updateScore(newPinsHit);

        Debug.Log("Score of frame " + frameStatus + " is " + frames[Mathf.FloorToInt((float)frameStatus) - 1].getTotalScore());

        if(frames[Mathf.FloorToInt((float)frameStatus) - 1].getTotalScore() == 10 && frameStatus % 1 == 0)
        {
            frameStatus += 1;
        }
        else if (frameStatus < 10)
        {
            frameStatus += .5;
        }
        else
        {
            gameOver();
        }

        if (frameStatus % 1 == 0)
        {
            resetField();
        }


        // UnityEditor.PrefabUtility.RevertObjectOverride(ball, UnityEditor.InteractionMode.AutomatedAction);
        GameObject.Destroy(ball);
        ball = GameObject.Instantiate(ballPrefab);
        ball.name = "ball";
    }

    void gameOver()
    {
        OpenScore();
        scoreUI.transform.Find("Close").gameObject.SetActive(false);
        int newPlayerScore = PlayerPrefs.GetInt("Score") + scoreUI.GetComponentInChildren<ScoreboardControl>().getTotalScore(frames);
        PlayerPrefs.SetInt("Score", newPlayerScore);
    }
    void resetField()
    {
        GameObject.Destroy(pins);
        initPins();
    }

    void initPins()
    {
        pins = GameObject.Instantiate(pinsPrefab);
        pins.name = "Pins";
        for (int i = 0; i < 10; i++)
        {
            currentPins[i] = false;
            GameObject pin = pins.transform.GetChild(i).gameObject;
            pin.GetComponent<PinBehavior>().controller = this;
            int pinNo = int.Parse(pin.name);
            pinObjects[pinNo - 1] = pin;
            pin.transform.GetChild(1).GetComponent<PinBehavior>().controller = this;
        }

    }
}
