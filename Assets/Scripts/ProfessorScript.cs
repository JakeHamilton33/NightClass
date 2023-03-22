using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorScript : MonoBehaviour
{
    public float timeIntervalMin;
    public float timeIntervalMax;
    public float phoneGracePeriod;
    public float cheatingGracePeriod;

    private Vector3 boardPostion = new Vector3(-2.8f, 1f, 14.5f);
    private Vector3 boardRotation = new Vector3(0f, -180f, 0f);
    private Vector3 midPostion = new Vector3(-1.5f, 1f, 11f);
    private Vector3 closePostion = new Vector3(-1.37f, 1f, 3.8f);

    private bool watching;
    private float phoneTimer;
    private float cheatTimer;

    enum ProfessorPosition { Board, Watching, Classroom, Desk }
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
        phoneTimer = phoneGracePeriod;
        cheatTimer = cheatingGracePeriod;
        StartCoroutine("WaitAtBoard");
    }

    // Update is called once per frame
    void Update()
    {
        WatchForPhone();
        WatchForCheating();
    }

    IEnumerator WaitAtBoard()
    {
        print("Board");
        StopCoroutine("Watching");
        watching = false;

        //Move prof to board
        transform.position = boardPostion;
        transform.eulerAngles = boardRotation;

        //Wait
        float waitTime = Random.Range(timeIntervalMin, timeIntervalMax);
        yield return new WaitForSeconds(waitTime);

        //Wait for player to put their head down
        while(HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Victimize
        StartCoroutine("ImGonnaGetYa");
    }

    IEnumerator ImGonnaGetYa()
    {
        transform.eulerAngles = Vector3.zero;

        //Start Watching
        print("Far");
        StartCoroutine("Watching");

        //Wait
        yield return new WaitForSeconds(4f);

        //Wait for player to stop moving
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Move forward
        print("Mid");
        transform.position = midPostion;

        //Wait
        yield return new WaitForSeconds(4f);

        //Wait for player to look at phone
        while (HeadMovement._position != HeadMovement.Position.Phone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Move forward
        print("Close");
        transform.position = closePostion;

        //Wait
        yield return new WaitForSeconds(Random.Range(4f, 7f));

        print("Game Over from not watching professor");
    }

    IEnumerator Watching()
    {
        watching = true;
        if (HeadMovement._position == HeadMovement.Position.Professor)
        {
            //Stop advancing toward player
            StopCoroutine("ImGonnaGetYa");

            //wait for player to put head down
            while (HeadMovement._position == HeadMovement.Position.Moving || HeadMovement._position == HeadMovement.Position.Professor)
            {
                yield return new WaitForSeconds(0.1f);
            }

            //Start Over
            StartCoroutine("WaitAtBoard");
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine("Watching");
    }

    private void WatchForPhone()
    {
        if(watching && HeadMovement._position == HeadMovement.Position.Phone)
        {
            phoneTimer -= Time.deltaTime;
        }
        else
        {
            phoneTimer = phoneGracePeriod;
        }

        if(phoneTimer <= 0)
        {
            print("Game Over from Phone");
        }
    }

    private void WatchForCheating()
    {
        if (watching && HeadMovement._position == HeadMovement.Position.Classmate)
        {
            cheatTimer -= Time.deltaTime;
        }
        else
        {
            cheatTimer = cheatingGracePeriod;
        }

        if (cheatTimer <= 0)
        {
            print("Game Over from cheating");
        }
    }
}
