using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTest : MonoBehaviour
{
    public List<Question> questions = new List<Question>();
    public int[] answers = new int[10];
    public TextAsset peopleFile;
    public TextAsset placesFile;
    public TextAsset DateQsFile;
    public TextAsset PeopleQsFile;
    public TextAsset PlaceQsFile;
    public TextAsset DateAsFile;
    public TextAsset PeopleAsFile;
    public TextAsset PlaceAsFile;

    private GameObject[] phones;
    private string[] names;
    private string[] places;
    private string[] DateQsArray;
    private string[] PeopleQsArray;
    private string[] PlaceQsArray;
    private string[] DateAsArray;
    private string[] PeopleAsArray;
    private string[] PlaceAsArray;

    private int i = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //phones = PhoneScript.instance.websites;
        SetArrays();
        GenerateQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(questions[i].Q);
            print(questions[i].articlePreAnswer + questions[i].options[questions[i].answer] + questions[i++].articlePostAnswer);
        }
    }

    private void SetArrays()
    {
        names = peopleFile.text.Split('\n');
        places = placesFile.text.Split('\n');
        DateQsArray = DateQsFile.text.Split('\n');
        PeopleQsArray = DateQsFile.text.Split('\n');
        PlaceQsArray = DateQsFile.text.Split('\n');
        DateAsArray = DateAsFile.text.Split('\n');
        PeopleAsArray = PeopleAsFile.text.Split('\n');
        PlaceAsArray = PlaceAsFile.text.Split('\n');
    }

    public void GenerateQuestions()
    {
        for(int i = 0; i < 10; i++)
        {
            int index;
            Question newQuestion;

            //Choose random question type
            int type = Random.Range(0, 0);

            //Create question
            if(type == 0)
            {
                //Choose random Date question
                index = Random.Range(0, DateQsArray.Length - 1);

                //Create new question
                newQuestion = new Question(DateQsArray[index], DateAsArray[2 * index], DateAsArray[2 * index + 1]);

                //Randomize answer options
                for(int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = Random.Range(1500, 2200).ToString();
                }

                //Store answer
                answers[i] = newQuestion.answer;
            }
            else if( type == 1)
            {
                //Choose random People question
                index = Random.Range(0, PeopleQsArray.Length - 1);

                //Create new question
                newQuestion = new Question(PeopleQsArray[index], PeopleAsArray[2 * index], PeopleAsArray[2 * index + 1]);

                //Randomize answer options
                for (int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = names[Random.Range(0, names.Length)];
                }

                //Store answer
                answers[i] = newQuestion.answer;
            }
            else
            {
                //Choose random Place question
                index = Random.Range(0, PlaceQsArray.Length - 1);

                //Create new question
                newQuestion = new Question(PlaceQsArray[index], PlaceAsArray[2 * index], PlaceAsArray[2 * index + 1]);

                //Randomize answer options
                for (int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = places[Random.Range(0, names.Length)];
                }

                //Store answer
                answers[i] = newQuestion.answer;
            }

            questions.Add(newQuestion);
        } //end for
    }


}

public class Question
{
    public string Q;
    public string[] options = new string[4];
    public int answer; //value 0-3: a = 0, b = 1,...
    public string articlePreAnswer;
    public string articlePostAnswer;

    public Question(string question, string pre, string post)
    {
        Q = question;
        articlePreAnswer = pre;
        articlePostAnswer = post;
        answer = Random.Range(0, 4);
    }
}
