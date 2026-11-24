using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public SusBar SM;
    public GameObject rileyScene;
    public Rigidbody2D Player;
    public BoxCollider2D playerBox;
    public Animator anim;
    public AudioSource dash;
    public AudioSource walking;

    //The bar to indicate stamina
    public UnityEngine.UI.Slider Stamina; //Rhys changed from Slider to UnityEngine.UI.Slider

    //lets player to change keybind if they want
    public string KeyUp;
    public string KeyLeft;
    public string KeyDown;
    public string KeyRight;
    string direction;

    bool movingUp;
    bool movingLeft;
    bool movingDown;
    bool movingRight;
    bool isCrawling;
    bool isRunning;
    bool canRun;

    //energy and cooldown for rolls
    public double energy;
    public double rollCooldown;
    bool cooldownStart;

    //stuff to help roll
    bool canRoll;
    public bool rolling;
    //makes sure that you cant change directions while rolling
    string LastLastMove;

    //Vector for moving Left/Right
    Vector2 horizontalMovement;
    //Vector for moving Up/Down
    Vector2 verticalMovement;
    //Players speed
    public float movementSpeed;
    //Vector for rolling Left/Right
    Vector2 XRoll;
    //Vector for rolling Up/Down
    Vector2 YRoll;
    //roll speed
    public float rollSpeed;




    //Little prompt for accessing task terminal
    public TMP_Text taskPrompt;
    //percentage meter
    public TMP_Text taskPercentage;
    //the terminal that gets cloned
    public GameObject terminal;
    //Checks player is close enough to Terminal
    bool inTaskRange;
    //The console the player moves to
    Vector2 consoleLocation;
    //Makes it so player cant move during task
    bool movementLock;
    //console position
    Transform console;
    //console object
    public GameObject consoleItself;

    public bool doingTask;
    bool isMoving;

  //  Vector2 boxSize;
    bool canGetUp;


    public bool inExit;

    //TEMPORARY!!!vvvvvv
    Vector2 characterSize;
    // Start is called before the first frame update
    void Start()
    {
        KeyUp = "w";
        KeyLeft = "a";
        KeyDown = "s";
        KeyRight = "d";

        movingUp = false;
        movingLeft = false;
        movingDown = false;
        movingRight = false;
        direction = "Right";
        isCrawling = false;
        isRunning = false;
        canRun = true;
        canRoll = true;
        rollCooldown = 5;
        walking.Stop();
        dash.Stop();
        SceneHandle.player = Player.GetComponent<PlayerMovement>();

        horizontalMovement = new Vector2(movementSpeed, 0);
        verticalMovement = new Vector2(0, movementSpeed);


        inTaskRange = false;
        movementLock = false;
        doingTask = false;

        inExit = false;

        //Gets the hitbox of the player so it can be adjusted when crawling
/*        playerBox = GetComponent<BoxCollider2D>();
        boxSize = new Vector2(0.5697545f, 0.5638605f);
        playerBox.size = boxSize;
        canGetUp = true;*/

        //TEMPORARY!!!!vvvvvvvv
        characterSize = new Vector2(1, 1);
        Player.transform.localScale = characterSize;

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = new Vector2(movementSpeed, 0);
        verticalMovement = new Vector2(0, movementSpeed);

       if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
           // print("ismoving");
        } else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            isMoving = false;
        }
       if (isMoving)
        {
           /* if (!walking.isPlaying)
            {
                walking.Play();
            }*/

        }
         



        if (Input.GetKeyUp(KeyCode.W))
        {
            movingUp = false;
            isMoving = false;

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
            isMoving = false;


        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            movingDown = false;
            isMoving = false;


        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
            isMoving = false;

        }

        //e key puts or takes player out of task
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pressed e");
            StartCoroutine(doTask());
        }

       /* //c key alternates player between crawling and standing
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(switchCrawl());
        }*/

        //animation


        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 1);

        } else if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 4);

        }
        else if (Input.GetKey(KeyCode.S))
        {

            anim.SetInteger("State", 3);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }
        else 
        {
            anim.SetInteger("State", 0);
           // Debug.Log("state 0");
        }



        //stamina value for running
        Stamina.value = (float)energy;
        if (isRunning)
        {
            energy -= 0.1;
        }
        if (energy < 5)
        {
            canRun = false;
        }
        else
        {
            canRun = true;
        }
        //ticks up stamina
        if (energy != 200 && !isRunning && !rolling)
        {
            //energy going up
            energy += 0.1;
        }
        if (energy > 200)
        {
            //making sure it doesnt get higher than 200
            energy = 200;
        }
        if (rollCooldown >= 4)
        {
            //the roll cooldown
            canRoll = true;
        }
        if (rollCooldown < 4)
        {
            rollCooldown += 0.01;
            //roll cooldown
            canRoll = false;
        }
        //input for running
        if (Input.GetKeyUp(KeyCode.LeftShift) || !canRun)
        {
            isRunning = false;
            movementSpeed = 10;
        }



        //flips hitbox 90 degrees and slows player down
        if (isCrawling && (movingLeft || movingRight))
        {
            //boxSize = new Vector2(1.75f, 0.571f);
            //TEMPORARY!!!vvvvvvv
            characterSize = new Vector2(1, 1);
            movementSpeed = 5;
            //Debug.Log(isCrawling);
        }

        //slightly changes player height based on crawling vertically
        if (isCrawling && (movingUp || movingDown))
        {
            //boxSize = new Vector2(1f, 0.75f);
            //TEMPORARY!!!vvvvvvv
            characterSize = new Vector2(1, 1.25f);
            movementSpeed = 7;
            //Debug.Log(isCrawling);
        }

        //Set hitbox and movement speed back to normal
        if (!isCrawling && !isRunning && !rolling)
        {
            //boxSize = new Vector2(1, 1);
            //TEMPORARY!!!vvvvvvv
            characterSize = new Vector2(1, 1);
            movementSpeed = 10;
            //Debug.Log(isCrawling);
        }

        //makes sure you can walk if youre rolling
        if (!rolling)
        {
            if (!isRunning && !isCrawling && canRun)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRunning = true;
                    movementSpeed = 20;
                    horizontalMovement = new Vector2(movementSpeed, 0);
                    verticalMovement = new Vector2(0, movementSpeed);
                }
            }


            //sets size of hitbox to current stance
/*            playerBox.size = boxSize;
            Player.transform.localScale = characterSize;*/
        }

        if (doingTask && consoleItself.GetComponent<TaskDoing>().taskDone == true)
        {
            taskPercentage.color = Color.green;
            taskPercentage.SetText("Complete!");
        } else
        {
            taskPercentage.SetText("");
        }
    }

    void FixedUpdate()
    {
        //if movement isnt locked, basically if able to move
        if (!movementLock)
        {
            if (Input.GetKey(KeyCode.W) && !movingLeft && !movingDown && !movingRight)
            {
                //moving up
                Player.AddForce(verticalMovement);
                movingUp = true;
                direction = "Up";
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.A) && !movingUp && !movingDown && !movingRight)
            {
                //moving left
                Player.AddForce(-horizontalMovement);
                movingLeft = true;
                direction = "Left";
                isMoving = true;

            }
            if (Input.GetKey(KeyCode.S) && !movingLeft && !movingUp && !movingRight)
            {
                //moving down
                Player.AddForce(-verticalMovement);
                movingDown = true;
                direction = "Down";
                isMoving = true;

            }
            if (Input.GetKey(KeyCode.D) && !movingLeft && !movingDown && !movingUp)
            {
                //moving right
                Player.AddForce(horizontalMovement);
                movingRight = true;
                direction = "Right";
                isMoving = true;

            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || !canRun)
            {
                isRunning = false;
                movementSpeed = 10;
            }
            if (Input.GetKey(KeyCode.Space) && energy > 50 && canRoll)
            {
                //the roll and energy subtraction
                LastLastMove = direction;
                characterSize = new Vector2(1f, 0.75f);
                rolling = true;
                rollCooldown = 0;
                energy -= 50;
                SM.sus = SM.sus + 10;
                if (SM.sus > 100)
                {
                    SM.sus = 100;
                }
                
            }
        }
        //makes sure you cant walk if youre rolling
        if (rolling == true)
        {
            if (!dash.isPlaying)
            {
                dash.Play();
                walking.Stop();
            }

            //the actual roll
            //Debug.Log(rollSpeed);
            movementLock = true;
            XRoll = new Vector2(rollSpeed, 0);
            YRoll = new Vector2(0, rollSpeed);
            rollSpeed += 3.5f;

            if (LastLastMove == "Right")
            {
                //roll right
                Player.AddForce(XRoll);
            }
            if (LastLastMove == "Left")
            {
                //roll left
                Player.AddForce(-XRoll);
            }
            if (LastLastMove == "Up")
            {
                //roll up
                Player.AddForce(YRoll);
            }
            if (LastLastMove == "Down")
            {
                //roll down
                Player.AddForce(-YRoll);
            }
            if (rollSpeed >= 100)
            {
                //stops the roll
                rollSpeed = 0;
                rolling = false;
                movementLock = false;
                rollSpeed = 50;
                //  boxSize = new Vector2(1, 1);
            }
        }
    }

    IEnumerator doTask()
    {
        if (!movementLock && inTaskRange && !doingTask)
            //if not already in a task but is in range go to the front of console and start doing that task
        {
            TaskStatus(true, true, true, "Press 'E' to leave");
            Player.MovePosition(consoleLocation);
            yield return new WaitForSecondsRealtime(0.25f);
        } else if (movementLock && inTaskRange || doingTask)
            //if in a task already and in range, take player off task
        {
            TaskStatus(false, false, false, "Press 'E' to use");
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

   private void TaskStatus(bool mL, bool dT, bool doingT, string prompt)
    {
        consoleItself = console.gameObject;
        movementLock = mL;
        doingTask = dT;
        consoleItself.GetComponent<TaskDoing>().doingTask = doingT;
        taskPrompt.SetText(prompt);
    }


    //lets player switch from standing to crawling and vice versa
    IEnumerator switchCrawl()
    {
        //can get up ensures player doesnt stand up while in a crawl space
        if (isCrawling && canGetUp)
        {
            isCrawling = false;
        }
        else if (!isCrawling)
        {
            isCrawling = true;
        }
        yield return new WaitForSecondsRealtime(0.25f);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        //when player enters radius of the terminal
        if (collider.gameObject.tag == "Terminal")
        {
            inTaskRange = true;
            Debug.Log(inTaskRange);


            terminal = collider.gameObject;
            console = terminal.transform.Find("Terminal Console");
            consoleLocation = console.position;
        }
        //if in crawl space, you cant stand up
        if (collider.gameObject.tag == "CrawlSpace")
        {
            canGetUp = false;
            //Debug.Log(canGetUp);
        }

        if(collider.gameObject.tag == "ExitCircle")
        {
            inExit = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terminal")
        {
            consoleItself = console.gameObject;
            if (!doingTask)
            {
                taskPrompt.SetText("Press 'E' to use");
            }
            if (doingTask)
            {
                taskPrompt.SetText("Press 'E' to leave");
            }
        }

    }
    public void OnTriggerExit2D(Collider2D collider)
    {

        //when player exits radius of the terminal
        if (collider.gameObject.tag == "Terminal")
        {
            inTaskRange = false;
            Debug.Log(inTaskRange);

            taskPrompt.SetText("");
        }
        //exit crawl space and can get up
        if (collider.gameObject.tag == "CrawlSpace")
        {
            canGetUp = true;
        }
    }

    //various getter methods for different scripts to check player status
    public bool getDoingTask()
    {
        return doingTask;
    }

    public bool getCrawling()
    {
        return isCrawling;
    }

    public string getDirection()
    {
        return direction;
    }

    public bool getMovementLock()
    {
        return movementLock;
    }
    public GameObject getTerminal()
    {
        return terminal;
    }

}
