using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamScript : MonoBehaviour
{
    #region Declarations
    private List<Question> questions;
    private int currentQuestion = 1;
    private string currentQuestionString = "Question1";

    public TextMeshPro QuestionField;
    [Header("Buttons")]
    public Button answerButtonA;
    public Button answerButtonB, answerButtonC, answerButtonD;

    [Header("Circle Image")]
    public GameObject optionA;
    public GameObject optionB, optionC, optionD;

    public AudioClip[] pencilSounds;
    public AudioSource audioSource;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        //Grab generated test questions and answers
        PlayerPrefs.SetInt(currentQuestionString, 5);
    }
    private void Start()
    {
        questions = GenerateTest.instance.questions;
        GenerateTest.instance.SetTestText(gameObject, questions[currentQuestion - 1], currentQuestion);
    }
    #endregion

    #region Button Handling
    public void AnswerA()
    {
        //Update Sprites
        optionA.SetActive(true);
        optionB.SetActive(false);
        optionC.SetActive(false);
        optionD.SetActive(false);

        //Play sound
        audioSource.clip = pencilSounds[Random.Range(0, pencilSounds.Length)];
        audioSource.Play();

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 0);
    }
    public void AnswerB()
    {
        //Update Sprites
        optionA.SetActive(false);
        optionB.SetActive(true);
        optionC.SetActive(false);
        optionD.SetActive(false);

        //Play sound
        audioSource.clip = pencilSounds[Random.Range(0, pencilSounds.Length - 1)];
        audioSource.Play();

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void AnswerC()
    {
        //Update Sprites
        optionA.SetActive(false);
        optionB.SetActive(false);
        optionC.SetActive(true);
        optionD.SetActive(false);

        //Play sound
        audioSource.clip = pencilSounds[Random.Range(0, pencilSounds.Length)];
        audioSource.Play();

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 2);
    }
    public void AnswerD()
    {
        //Update Sprites
        optionA.SetActive(false);
        optionB.SetActive(false);
        optionC.SetActive(false);
        optionD.SetActive(true);

        //Play sound
        audioSource.clip = pencilSounds[Random.Range(0, pencilSounds.Length)];
        audioSource.Play();

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 3);
    }
    public void NextQuestion()
    {
        if(PlayerPrefs.GetInt(currentQuestionString) == 0 | PlayerPrefs.GetInt(currentQuestionString) == 1 | PlayerPrefs.GetInt(currentQuestionString) == 2 | PlayerPrefs.GetInt(currentQuestionString) == 3)
        {
            if(currentQuestion < 10)
            {
                //Set all buttons to uncircled sprites
                optionA.SetActive(false);
                optionB.SetActive(false);
                optionC.SetActive(false);
                optionD.SetActive(false);

                currentQuestion++;
                currentQuestionString = "Question" + currentQuestion;
                PlayerPrefs.SetInt(currentQuestionString, 5);

                GenerateTest.instance.SetTestText(gameObject, questions[currentQuestion - 1], currentQuestion);
            }
            else if(currentQuestion == 10)
            {
                PlayerPrefs.SetInt("Caught", 0);
                PhoneScript.instance.EndGame();

            }
        }
        
    }
    #endregion
}
