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
    private Vector3 closePostion = new Vector3(-1.37f, 1f, 3.8f);

    private bool watching;
    private float phoneTimer;
    private float cheatTimer;

    enum ProfessorPosition { Board, Watching, Desk }
    // Start is called before the first frame update
    void Start()
    {
        watching = false;
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
        RollForIntimidation();
    }

    private void RollForIntimidation()
    {
        //If player is looking at their phone, there is a chance for professor to be close
        if(HeadMovement._position == HeadMovement.Position.Phone && Random.Range(1, 4) == 1)
        {
            print("up close");
            StartCoroutine("CloseWatch");
            return;
        }

        print("far");
        StartCoroutine("BoardWatch");
    }

    IEnumerator CloseWatch()
    {
        transform.position = closePostion;
        transform.eulerAngles = Vector3.zero;

        //Start Watching
        watching = true;

        //Wait
        float waitTime = Random.Range(4f, 10f);
        yield return new WaitForSeconds(waitTime);

        //Wait for player to put their head down
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Stop Watching
        watching = false;

        //Back to board
        StartCoroutine("WaitAtBoard");
    }

    IEnumerator BoardWatch()
    {
        transform.eulerAngles = Vector3.zero;

        //Start Watching
        watching = true;

        //Wait
        float waitTime = Random.Range(4f, 15f);
        yield return new WaitForSeconds(waitTime);

        //Wait for player to put their head down
        while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Stop Watching
        watching = false;

        //Back to board
        StartCoroutine("WaitAtBoard");
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
            print("Game Over");
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
            print("Game Over");
        }
    }
}
