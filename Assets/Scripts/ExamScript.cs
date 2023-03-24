using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamScript : MonoBehaviour
{
    #region Declarations
    private string[] questions;
    private string[] answers;

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
        //Delete all answers
        PlayerPrefs.DeleteAll();

        /*
        Grab generated test questions and answers

        questions = GenerateTest.instance.GetQuestions();
        answers = GenerateTest.instance.GetAnswers(currentQuestion); 
        */
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
        PlayerPrefs.SetInt(currentQuestionString, 1);
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
        PlayerPrefs.SetInt(currentQuestionString, 2);
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
        PlayerPrefs.SetInt(currentQuestionString, 3);
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
        PlayerPrefs.SetInt(currentQuestionString, 4);
    }
    public void NextQuestion()
    {
        //Set all buttons to uncircled sprites
        //answerButtonA.image.sprite = optionA;
        //answerButtonB.image.sprite = optionB;
        //answerButtonC.image.sprite = optionC;
        //answerButtonD.image.sprite = optionD;

        Debug.Log(currentQuestionString + ": " + PlayerPrefs.GetInt(currentQuestionString));
        currentQuestion++;
        currentQuestionString = "Question" + currentQuestion;
        //Load Next Question
        /*
        questionField.text = questions[currentQuestion-1];
        answers = GenerateTest.instance.GetAnswers(currentQuestion);

        answerButtonA.text = answers[0];
        answerButtonB.text = answers[1];
        answerButtonC.text = answers[2];
        answerButtonD.text = answers[3];

        */
    }
    #endregion
}
