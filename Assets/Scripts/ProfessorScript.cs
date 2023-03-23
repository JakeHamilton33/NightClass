using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorScript : MonoBehaviour
{
    public Vector2 boardWaitRange;
    public Vector2 farToMidRange;
    public Vector2 midToCloseRange;
    public Vector2 closeToDeathRange;

    private Vector3 boardPostion = new Vector3(-2.8f, 1f, 14.5f);
    private Vector3 boardRotation = new Vector3(0f, -180f, 0f);
    private Vector3 midPostion = new Vector3(-1.5f, 1f, 11f);
    private Vector3 closePostion = new Vector3(-1.37f, 1f, 3.8f);

    private bool moving;
    private float timerMax;
    public float moveTimer;

    //enum ProfessorPosition { Board, Far, Mid, Close }
    //private ProfessorPosition _professorPosition;
    /*

    Hey I had an idea on how to use this enum field to keep track of his movements easier than all the booleans and float values

    You can use them together for example if( _professorState == Watching && _Position == Phone ) to streamline the checks

    You can also use this to keep track of which timer is going which can cut back on the different coroutines you need
        EX: use one coroutine to keep track of all time and have the different values based on which state you and the professor are in and compare the two states

    Obviously I know you're smart and I trust your judgement so whatever we end up with is all good
    Feel free to look through the other script to see how I navigated states and hit me up if you have any questions bc I really enjoy using Enum states

     */

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitAtBoard");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    IEnumerator WaitAtBoard()
    {
        print("Board");
        StopCoroutine("WatchForReset");
        moving = false;

        //Move prof to board
        transform.position = boardPostion;
        transform.eulerAngles = boardRotation;

        //Wait
        float waitTime = Random.Range(boardWaitRange.x, boardWaitRange.y);
        yield return new WaitForSeconds(waitTime);

        //Wait for player to put their head down
        while(HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return null;
        }

        //Victimize
        StartCoroutine("ImGonnaGetYa");
    }

    IEnumerator ImGonnaGetYa()
    {
        //Move Prof
        transform.eulerAngles = Vector3.zero;
        print("Far");

        //Set timer
        timerMax = Random.Range(farToMidRange.x, farToMidRange.y);
        moveTimer = timerMax;

        //Start Moving (start timer)
        moving = true;
        StartCoroutine("WatchForReset");

        //Wait for timer
        while (moveTimer > 0)
        {
            yield return null;
        }

        //Wait if player is looking 
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return null;
        }

        //Move forward
        print("Mid");
        transform.position = midPostion;

        //Set timer
        timerMax = Random.Range(midToCloseRange.x, midToCloseRange.y);
        moveTimer = timerMax;

        //Wait for timer
        while (moveTimer > 0)
        {
            yield return null;
        }

        //Wait if player is looking 
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return null;
        }

        //Move forward
        print("Close");
        transform.position = closePostion;

        //Set timer
        timerMax = Random.Range(midToCloseRange.x, midToCloseRange.y);
        moveTimer = timerMax;

        //Wait for timer
        while (moveTimer > 0)
        {
            yield return null;
        }

        //Wait if player is looking 
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return null;
        }

        print("Game Over");
    }

    IEnumerator WatchForReset()
    {
        if(HeadMovement._position == HeadMovement.Position.Professor)
        {
            moving = false;
            StopCoroutine("ImGonnaGetYa");

            //Wait if player is looking 
            while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
            {
                yield return null;
            }

            StartCoroutine("WaitAtBoard");
        }

        yield return null;
        StartCoroutine("WatchForReset");
    }

    private void Timer()
    {
        if(moving && (HeadMovement._position == HeadMovement.Position.Phone || HeadMovement._position == HeadMovement.Position.Classmate))
        {
            moveTimer -= Time.deltaTime;
        }
        else if(moving && HeadMovement._position == HeadMovement.Position.Paper && moveTimer < timerMax)
        {
            moveTimer += Time.deltaTime * 0.5f;
        }
    }
}
