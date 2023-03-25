using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfGameScript : MonoBehaviour
{
    [SerializeField] private GameObject Graded;
    [SerializeField] private GameObject Caught;
    [SerializeField] private TMP_Text grade;

    private bool caught;

    private int questionsCorrect = 0;
    private string gradeText;

    private void Awake()
    {
        caught = PlayerPrefs.GetInt("Caught") == 1;
        CheckGrade();
    }

    void CheckGrade()
    {
        if (!caught)
        {
            Debug.Log("GradeTest");
            for(int i = 1; i <= 10; i++)
            {
                Debug.Log("For loop");
                string questionAnswer = "Question" + i;
                string questionCorrectAnswer = "Question" + i + "CorrectAnswer";

                Debug.Log(questionAnswer);
                Debug.Log(questionCorrectAnswer);
                Debug.Log(PlayerPrefs.GetInt(questionAnswer));
                Debug.Log(PlayerPrefs.GetInt(questionCorrectAnswer));
                if (PlayerPrefs.GetInt(questionAnswer) == PlayerPrefs.GetInt(questionCorrectAnswer))
                {
                    questionsCorrect++;
                }
            }

            switch (questionsCorrect)
            {
                case 0:
                    {
                        gradeText = "0\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 1:
                    {
                        gradeText = "1\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 2:
                    {
                        gradeText = "2\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 3:
                    {
                        gradeText = "3\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 4:
                    {
                        gradeText = "4\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 5:
                    {
                        gradeText = "5\nF";
                        grade.text = gradeText;
                        break;
                    }
                case 6:
                    {
                        gradeText = "6\nD";
                        grade.text = gradeText;
                        break;
                    }
                case 7:
                    {
                        gradeText = "7\nC";
                        grade.text = gradeText;
                        break;
                    }
                case 8:
                    {
                        gradeText = "8\nB";
                        grade.text = gradeText;
                        break;
                    }
                case 9:
                    {
                        gradeText = "9\nA";
                        grade.text = gradeText;
                        break;
                    }
                case 10:
                    {
                        gradeText = "10\nA+";
                        grade.text = gradeText;
                        break;
                    }
            }
            Graded.SetActive(true);
        }
        else
        {
            Caught.SetActive(true);
        }
    }

}
