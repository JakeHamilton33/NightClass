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

    [Header("Sprites for answers")]
    public Sprite optionA;
    public Sprite optionASelected, optionB, optionBSelected, optionC, optionCSelected, optionD, optionDSelected;

    #endregion

    #region Unity Methods
    private void Awake()
    {

        //Grab generated test questions and answers
        questions = GenerateTest.instance.questions;

        GenerateTest.instance.SetTestText(gameObject, questions[currentQuestion-1], currentQuestion);
        PlayerPrefs.SetInt(currentQuestionString, 5);
    }
    #endregion

    #region Button Handling
    public void AnswerA()
    {
        Debug.Log("Pressed A");
        //Update Sprites
        /*
        answerButtonA.image.sprite = optionASelected;
        answerButtonB.image.sprite = optionB;
        answerButtonC.image.sprite = optionC;
        answerButtonD.image.sprite = optionD;
        */
        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 0);
    }
    public void AnswerB()
    {
        Debug.Log("Pressed B");
        //Update Sprites
        //answerButtonA.image.sprite = optionA;
        //answerButtonB.image.sprite = optionBSelected;
        //answerButtonC.image.sprite = optionC;
        //answerButtonD.image.sprite = optionD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void AnswerC()
    {
        Debug.Log("Pressed C");
        //Update Sprites
        //answerButtonA.image.sprite = optionA;
        //answerButtonB.image.sprite = optionB;
        //answerButtonC.image.sprite = optionCSelected;
        //answerButtonD.image.sprite = optionD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 2);
    }
    public void AnswerD()
    {
        Debug.Log("Pressed D");
        //Update Sprites
        //answerButtonA.image.sprite = optionA;
        //answerButtonB.image.sprite = optionB;
        //answerButtonC.image.sprite = optionC;
        //answerButtonD.image.sprite = optionDSelected;

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
                //answerButtonA.image.sprite = optionA;
                //answerButtonB.image.sprite = optionB;
                //answerButtonC.image.sprite = optionC;
                //answerButtonD.image.sprite = optionD;

                Debug.Log(currentQuestionString + ": " + PlayerPrefs.GetInt(currentQuestionString));
                currentQuestion++;
                currentQuestionString = "Question" + currentQuestion;
                PlayerPrefs.SetInt(currentQuestionString, 5);

                GenerateTest.instance.SetTestText(gameObject, questions[currentQuestion - 1], currentQuestion);
            }
            else if(currentQuestion == 10)
            {
                Debug.Log(currentQuestionString + ": " + PlayerPrefs.GetInt(currentQuestionString));
                PlayerPrefs.SetInt("Caught", 0);
                PhoneScript.instance.EndGame();

            }
        }
        
    }
    #endregion
}
