using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Lane { Left, Mid, Right }; // These are the lanes on the map.
public class CharacterMovement : MonoBehaviour
{
    #region Variables.
    CharacterStats character; 
    CharacterController controller; //reference to the Character Controller Compenent .
    Animator animator; // reference to the animator on the object.
    AudioSource audioclip; //reference to Music clip.

    Lane lane = Lane.Mid; //starting lane is mid.

    Vector3 boundary; //To clamp the player .
    int clampAtRight;
    int clampAtLeft;


    int xdisplacement; // To switch lanes one at a time.
    int newSidePosition; 

    public bool canMove; // If player hits an obstacle , controls are stop for 2 sec.


    //For swipe functions for mobile.
    Vector3 swipeStartPosition;
    Vector3 swipeCurrentPosition;
    Vector3 swipeEndPosition;
    bool swipingRange=false;
    float swipeRange=50;
    float tapRange=10;


    public Vector3 startingposition;

    //for keyboard controls.
    bool SwipeLeft=false, SwipeRight = false,SwipeDown=false;


    [SerializeField]
    private float speed; //vertical speed
    [SerializeField]
    private float sideSpeed; //horizontal speed
    #endregion
    private void Awake()
    {
        character = GetComponent<CharacterStats>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioclip = GetComponent<AudioSource>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        audioclip.Play();

        startingposition = transform.position; //Player starting position.

        clampAtLeft = -10;
        clampAtRight = 10;

        xdisplacement = 10;
         
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {

        BoundaryClamping(); 



        if (canMove)
        {
            KeyboardControls();
            Swipe();
            controller.Move((Vector3.forward * speed * Time.deltaTime) + (newSidePosition - transform.position.x) * Vector3.right * sideSpeed * Time.deltaTime);
        }

       
       

    }

    void BoundaryClamping()
    {
        boundary = transform.position;
        boundary.x = Mathf.Clamp(boundary.x, clampAtLeft, clampAtRight);
        transform.position = boundary;

    }
    void KeyboardControls()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A);
        SwipeRight = Input.GetKeyDown(KeyCode.D);
        SwipeDown = Input.GetKeyDown(KeyCode.S);


        //Below logic checks where the player is currently standing on the lane and when move command is given , moves the character by 1 lane if it is allowed.
        if (SwipeLeft)
        {
            if (lane == Lane.Mid) // Will move to left lane.
            {
                newSidePosition = -xdisplacement;
                lane = Lane.Left;
                animator.Play("Left");
            }
            else if (lane == Lane.Right)// Will move to mid lane.
            {
                newSidePosition = 0;
                lane = Lane.Mid;
                animator.Play("Left");
            }
        }
        if (SwipeRight)
        {
            if (lane == Lane.Mid)// Will move to mid lane.
            {
                newSidePosition = xdisplacement;
                lane = Lane.Right;
                animator.Play("Right");
            }
            else if (lane == Lane.Left)// Will move to left lane.
            {
                newSidePosition = 0;
                lane = Lane.Mid;
                animator.Play("Right");
            }
        }
        if (SwipeDown)
        {
            animator.Play("Slide");
        }
    }


    void Swipe() //Mobile Controls.
    {
        //When player touchs the screen , first touch is used to register movement.
        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began) 
        {
            swipeStartPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) //Finger movement.
        {
            swipeCurrentPosition = Input.GetTouch(0).position;
            Vector3 distanceMoved = swipeCurrentPosition - swipeStartPosition; //Distance from starting of finger touch to current.

            if(!swipingRange)
            {
                if(distanceMoved.x<-swipeRange) //Finger moving left.
                {
                    swipingRange = true;
                    SwipeLeftControl();
                }
                else if(distanceMoved.x > swipeRange) //Finger moving right.
                {
                    swipingRange = true;
                    SwipeRightControl();
                }
                else if(distanceMoved.y > swipeRange) //Finger moving up.
                {
                    swipingRange = true;
                    // Do nothing . No jump action.

                }
                else if(distanceMoved.y < -swipeRange) //Finger moving down.
                {
                    swipingRange = true;
                    SwipeDownControl();
                }
            }
        }
        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended)
        {
            swipingRange = false;
            swipeEndPosition = Input.GetTouch(0).position;
            Vector3 distanceMoved = swipeEndPosition - swipeStartPosition;
            
        }
    }
    void SwipeRightControl() //Mobile right movement.
    {
        if (lane == Lane.Mid)
        {
            newSidePosition = xdisplacement;
            lane = Lane.Right;
            animator.Play("Right");
        }
        else if (lane == Lane.Left)
        {
            newSidePosition = 0;
            lane = Lane.Mid;
            animator.Play("Right");
        }
    }
    void SwipeLeftControl()
    {
        if (lane == Lane.Mid)
        {
            newSidePosition = -xdisplacement;
            lane = Lane.Left;
            animator.Play("Left");
        }
        else if (lane == Lane.Right)
        {
            newSidePosition = 0;
            lane = Lane.Mid;
            animator.Play("Left");
        }
    }//Mobile left movement.
    void SwipeDownControl()
    {
        animator.Play("Slide");
    }//Mobile slide movement.


    public float GetSpeed()
    {
        return speed;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public void PlayIdleAnim()
    {
        animator.Play("Idle");
    }
}
