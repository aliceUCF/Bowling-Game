using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBehavior : MonoBehaviour
{

    bool pinHit;
    public GameState controller;
    // Start is called before the first frame update

    private void Start()
    {
        if(controller == null)
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
    void OnEnable()
    {
        pinHit = false;
        //controller.pinExists(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.name == "Collision Checker")
      //  {
           // Debug.Log("collision with " + collision.collider.name);
         //   if (collision.collider.name == "Lane")
         //   {
         //       Debug.Log("Pin hit!");
          //  }
      // // }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pinHit)
        {
            //Debug.Log("collision with " + other.GetComponent<Collider>().name);
            if (other.GetComponent<Collider>().name == "Lane" && this.gameObject.name == "Collision Checker")
            {
                Debug.Log("Pin " + transform.parent.gameObject.name + " hit!");
                pinHit = true;
                controller.pinHit(transform.parent.gameObject.name);
            }
        }
    }
}
