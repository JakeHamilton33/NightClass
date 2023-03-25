using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessorScript : MonoBehaviour
{
    #region Declarations

    //Timer ranges initialized in editor
    public Vector2 boardWaitRange;
    public Vector2 farToMidRange;
    public Vector2 midToCloseRange;
    public Vector2 closeToDeathRange;

    //Position Coordinates
    private Vector3 boardPostion = new Vector3(-2.8f, 1f, 14.5f);
    private Vector3 boardRotation = new Vector3(0f, -180f, 0f);
    private Vector3 midPostion = new Vector3(-1.5f, 1f, 11f);
    private Vector3 closePostion = new Vector3(-1.37f, 1f, 3.8f);

    //Sound Array
    public AudioClip[] throatSounds;
    public AudioClip death;
    public AudioSource audioSource;

    private bool moving;
    private float timerMax;
    [SerializeField] private float moveTimer;

    public GameObject head;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToBoard());
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    #endregion

    #region Coroutines
    IEnumerator GoToBoard()
    {
        Debug.Log("Board");
        StopCoroutine(ResetPosition());
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
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer()
    {
        #region Far
        //Face Player
        transform.eulerAngles = Vector3.zero;
        Debug.Log("Far");

        //Set timer
        timerMax = Random.Range(farToMidRange.x, farToMidRange.y);
        moveTimer = timerMax;
        

        //Start Moving (start timer)
        moving = true;
        StartCoroutine(ResetPosition());

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
        #endregion

        #region Mid
        //Move forward
        Debug.Log("Mid");
        transform.position = midPostion;

        //Play sound
        audioSource.clip = throatSounds[Random.Range(0, throatSounds.Length - 1)];
        audioSource.Play();

        //transform.rotation = midRotation;

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
        #endregion

        #region Close
        //Move forward
        print("Close");
        transform.position = closePostion;

        //Set timer
        timerMax = Random.Range(closeToDeathRange.x, closeToDeathRange.y);
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
        #endregion

        //End Game
        PlayerPrefs.SetInt("Caught", 1);
        head.GetComponent<HeadMovement>().Caught();
        audioSource.clip = death;
        audioSource.Play();
    }

    IEnumerator ResetPosition()
    {
        if(HeadMovement._position == HeadMovement.Position.Professor)
        {
            moving = false;
            StopCoroutine(AttackPlayer());

            //Wait if player is looking 
            while (HeadMovement._position == HeadMovement.Position.Professor || HeadMovement._position == HeadMovement.Position.Moving)
            {
                yield return null;
            }

            StartCoroutine(GoToBoard());
        }

        yield return null;
        StartCoroutine(ResetPosition());
    }

#endregion

    #region Methods
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

    #endregion
}
