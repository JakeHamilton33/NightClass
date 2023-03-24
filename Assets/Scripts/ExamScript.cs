using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamScript : MonoBehaviour
{
    private int currentQuestion = 1;
    private string currentQuestionString = "Question1";

    public Button answerButtonA, answerButtonB, answerButtonC, answerButtonD;

    public Sprite answerA, answerACircled, answerB, answerBCircled, answerC, answerCCircled, answerD, answerDCircled;

    private void Awake()
    {
        //Delete all answers
        PlayerPrefs.DeleteAll();

        /*
        Grab generated test questions and answers
        
        string[] questions = new string[10];

        //answers are stored as int values 1-4
        int[] answers = new int[10];

        questions = GenerateTest.instance.GetQuestions();
        answers = GenerateTest.instance.GetAnswers(); 
        */
    }
    public void AnswerA()
    {
        //Update Sprites
        answerButtonA.image.sprite = answerACircled;
        answerButtonB.image.sprite = answerB;
        answerButtonC.image.sprite = answerC;
        answerButtonD.image.sprite = answerD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void AnswerB()
    {
        //Update Sprites
        answerButtonA.image.sprite = answerACircled;
        answerButtonB.image.sprite = answerB;
        answerButtonC.image.sprite = answerC;
        answerButtonD.image.sprite = answerD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void AnswerC()
    {
        //Update Sprites
        answerButtonA.image.sprite = answerACircled;
        answerButtonB.image.sprite = answerB;
        answerButtonC.image.sprite = answerC;
        answerButtonD.image.sprite = answerD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void AnswerD()
    {
        //Update Sprites
        answerButtonA.image.sprite = answerACircled;
        answerButtonB.image.sprite = answerB;
        answerButtonC.image.sprite = answerC;
        answerButtonD.image.sprite = answerD;

        //Store Answer
        PlayerPrefs.SetInt(currentQuestionString, 1);
    }
    public void NextQuestion()
    {
        //Set all buttons to uncircled sprites
        answerButtonA.image.sprite = answerA;
        answerButtonB.image.sprite = answerB;
        answerButtonC.image.sprite = answerC;
        answerButtonD.image.sprite = answerD;

        Debug.Log(currentQuestionString + ": " + PlayerPrefs.GetInt(currentQuestionString));
        currentQuestion++;
        currentQuestionString = "Question" + currentQuestion;

        //Load Next Question
    }
}
