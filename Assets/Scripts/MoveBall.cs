using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveBall : MonoBehaviour
{

    private const bool RotateMode = true;
    private const bool MoveMode = false;

    private Rigidbody rb;
    private bool fired;
    private Vector3 fireSpeed;
    private Vector3 testAccel;
    private bool mode;
    public TextMeshPro modeText;
    private GameObject moveArrow;
    public GameObject pinsPrefab;
    public GameObject ballPrefab;
    private PlayerInput playerInput;
    public DefaultInputActions help;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fired = false;
        testAccel = new Vector3(0, 0, 5f);
        mode = MoveMode;
        moveArrow = transform.GetChild(0).gameObject;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            modeText = GameObject.Find("mode text").GetComponent<TextMeshPro>();
        ballPrefab = (GameObject)Resources.Load("Prefabs/ball");
        playerInput = this.gameObject.GetComponent<PlayerInput>();
        playerInput.actions = ballPrefab.GetComponent<PlayerInput>().actions;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(fired)
        {
            // Uncomment to add constant impulse to the ball.
            fireSpeed = new Vector3(rb.position.x, rb.position.y, 150f);
            rb.AddForce(fireSpeed, ForceMode.Impulse);

        }
    }

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        DraggedDirection whichWay = GetDragDirection(dragVectorDirection);
        
        if (!fired)
        {
            if (whichWay == DraggedDirection.Right)
            {
                if (!(rb.position.x > 86))
                {
                    Vector3 newPosition = new Vector3(rb.position.x + 5, rb.position.y, rb.position.z);
                    rb.MovePosition(newPosition);
                }
            }
            else if(whichWay == DraggedDirection.Left)
            {
                if (!(rb.position.x < -36))
                {
                    Vector3 newPosition = new Vector3(rb.position.x - 5, rb.position.y, rb.position.z);
                    rb.MovePosition(newPosition);
                }
            }

        }

    }

    void OnMove(InputValue movementValue)
    {
        if (mode == MoveMode)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            Vector3 leftRightMovement = new Vector3(0, 0, rb.position.z);
            leftRightMovement.x = movementVector.x;
            Debug.Log("Movement Value values are : " + movementVector.x + " for x, and " + movementVector.y);

            if (!fired)
            {
                if (leftRightMovement.x == 1)
                {
                    if (!(rb.position.x > 86))
                    {
                        Vector3 newPosition = new Vector3(rb.position.x + 5, rb.position.y, rb.position.z);
                        rb.MovePosition(newPosition);
                    }
                }
                else
                {
                    if (!(rb.position.x < -36))
                    {
                        Vector3 newPosition = new Vector3(rb.position.x - 5, rb.position.y, rb.position.z);
                        rb.MovePosition(newPosition);
                    }
                }

            }

        }
    }

    void OnRotate(InputValue movementValue)
    {
        if (mode == RotateMode)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            if (!fired)
            {
                Debug.Log("The rotation of the ball at present is " + rb.rotation.y);
                if (movementVector.x == 1)
                {
                    if (!(rb.rotation.y > .39f))
                    {
                        Quaternion newRotation = new Quaternion(rb.rotation.x, rb.rotation.y + 2, rb.rotation.z, 0);
                        Quaternion eulerTransform = Quaternion.Euler(0, 2, 0);
                        //newRotation = newRotation.normalized;
                        rb.MoveRotation(rb.rotation * eulerTransform);
                        //rb.transform.localRotation.Set(rb.rotation.x, rb.rotation.y + 2, rb.rotation.z, 0);

                    }
                }
                else
                {
                    if (!(rb.rotation.y < -.35f))
                    {
                        Quaternion newRotation = new Quaternion(rb.rotation.x, rb.rotation.y - 2, rb.rotation.z, 0);
                        Quaternion eulerTransform = Quaternion.Euler(0, -2, 0);
                        //newRotation = newRotation.normalized;
                        rb.MoveRotation(rb.rotation * eulerTransform);
                        // rb.MoveRotation(newRotation);
                        //rb.transform.localRotation.Set(rb.rotation.x, rb.rotation.y - 2, rb.rotation.z, 0);

                    }
                }
            }

        }
    }

    void OnFire(InputValue fireValue)
    {
        if(!fired)
        {
            Vector3 goBallGo = new Vector3(0, 0, 300.0f);



            /* Uncomment to alter initial impulse. 
             */ goBallGo = new Vector3(0, 0, 850.0f);
             
            Quaternion angle = rb.rotation;
            float angleThing = 0.0f;
            Vector3 axis = new Vector3(0,0,0);
            angle.ToAngleAxis(out angleThing, out axis);
            fireSpeed = Quaternion.AngleAxis(angleThing, axis) * goBallGo;
            //fireSpeed = Quaternion.AngleAxis(angle,) * goBallGo;
            rb.AddRelativeForce(goBallGo, ForceMode.VelocityChange);
            fired = true;
            moveArrow.SetActive(false);
        }
    }

    void OnReset(InputValue resetValue)
    {
        GameObject pins = GameObject.Find("Pins");
        if(pins == null)
        {
            pins = GameObject.Find("Pins(Clone)");
        }
        GameObject.Destroy(pins);
        GameObject newPins = GameObject.Instantiate(pinsPrefab);
        GameObject newBall = GameObject.Instantiate(ballPrefab);
        GameObject.Destroy(this.gameObject);
    }

    void OnChangeMode(InputValue modeSwap)
    {
        
        Debug.Log("changing mode");
        
        if(mode == MoveMode)
        {
            mode = RotateMode;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                modeText.text = "MODE: ROTATE";
                modeText.color = new Color(255, 108, 214);
            }

        }
        else
        {
            mode = MoveMode;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                modeText.text = "MODE: MOVEMENT";
                modeText.color = new Color(107, 208, 255);
            }


        }
    }
}
