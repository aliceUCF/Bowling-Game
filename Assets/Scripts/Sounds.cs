using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{

    public AudioClip spare;
    public AudioClip strike;
    public AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string name)
    {
        Debug.Log("Strike playing");
        AudioClip whichone;
        if(name == "spare")
        {
            whichone = spare;
        }
        else
        {
            whichone = strike;
        }

        player.clip = whichone;
        player.Play();
    }
}
