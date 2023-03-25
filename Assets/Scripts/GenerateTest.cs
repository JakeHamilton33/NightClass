using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenerateTest : MonoBehaviour
{
    public static GenerateTest instance;

    public List<Question> questions = new List<Question>();
    public TextAsset peopleFile;
    public TextAsset placesFile;
    public TextAsset DateQsFile;
    public TextAsset PeopleQsFile;
    public TextAsset PlaceQsFile;
    public TextAsset DateAsFile;
    public TextAsset PeopleAsFile;
    public TextAsset PlaceAsFile;

    public TMP_Text[] phoneTexts;

    [SerializeField] private Image[] buttons;
    private GameObject[] phones;
    private string[] names;
    private string[] places;
    private string[] DateQsArray;
    private string[] PeopleQsArray;
    private string[] PlaceQsArray;
    private string[] DateAsArray;
    private string[] PeopleAsArray;
    private string[] PlaceAsArray;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SetArrays();
        GenerateQuestions();
    }

    private void Start()
    {
        phones = PhoneScript.instance.websites;
        SetText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetArrays()
    {
        names = peopleFile.text.Split('\n');
        places = placesFile.text.Split('\n');
        DateQsArray = DateQsFile.text.Split('\n');
        PeopleQsArray = PeopleQsFile.text.Split('\n');
        PlaceQsArray = PlaceQsFile.text.Split('\n');
        DateAsArray = DateAsFile.text.Split('\n');
        PeopleAsArray = PeopleAsFile.text.Split('\n');
        PlaceAsArray = PlaceAsFile.text.Split('\n');
    }

    public void GenerateQuestions()
    {
        for(int i = 1; i <= 10; i++)
        {
            int index;
            Question newQuestion;
            string newQuestionString = "Question" + i + "CorrectAnswer";

            //Choose random question type
            int type = Random.Range(0, 3);

            //record indexes
            List<int> yearIndexes = new List<int>();
            List<int> peopleIndexes = new List<int>();
            List<int> placeIndexes = new List<int>();

            //Create question
            if (type == 0)
            {
                //Choose random Date question
                index = Random.Range(0, DateQsArray.Length);
                while (yearIndexes.Contains(index))
                {
                    index = Random.Range(0, DateQsArray.Length);
                }
                //record index
                yearIndexes.Add(index);

                //Create new question
                newQuestion = new Question(DateQsArray[index], DateAsArray[2 * index], DateAsArray[2 * index + 1]);

                //Randomize answer options
                for(int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = Random.Range(1500, 2200).ToString();
                }

                //Store answer
                Debug.Log(newQuestion.answer);
                PlayerPrefs.SetInt(newQuestionString, newQuestion.answer);
            }
            else if( type == 1)
            {
                //Choose random People question
                index = Random.Range(0, PeopleQsArray.Length);
                while (peopleIndexes.Contains(index))
                {
                    index = Random.Range(0, PeopleQsArray.Length);
                }
                //record index
                peopleIndexes.Add(index);

                //Create new question
                newQuestion = new Question(PeopleQsArray[index], PeopleAsArray[2 * index], PeopleAsArray[2 * index + 1]);

                //Randomize answer options
                for (int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = names[Random.Range(0, names.Length)];
                }

                //Store answer
                PlayerPrefs.SetInt("Question" + i + "CorrectAnswer", newQuestion.answer);
            }
            else
            {
                //Choose random Place question
                index = Random.Range(0, PlaceQsArray.Length);
                while (placeIndexes.Contains(index))
                {
                    index = Random.Range(0, PlaceQsArray.Length);
                }
                //record index
                placeIndexes.Add(index);

                //Create new question
                newQuestion = new Question(PlaceQsArray[index], PlaceAsArray[2 * index], PlaceAsArray[2 * index + 1]);

                //Randomize answer options
                for (int j = 0; j < newQuestion.options.Length; j++)
                {
                    newQuestion.options[j] = places[Random.Range(0, places.Length)];
                }

                //Store answer
                PlayerPrefs.SetInt("Question" + i + "CorrectAnswer", newQuestion.answer);
            }

            questions.Add(newQuestion);
        } //end for
    }

    private void SetText()
    {
        //Generate which question will be on the neighbors test and set it
        int testidx = Random.Range(0, 10);
        SetTestText(phones[10], questions[testidx], testidx + 1);
        buttons[questions[testidx].answer].color = Color.yellow;

        //Generate a randomly shuffled array containing each index of the questions
        int[] randomArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        for(int i = 0; i < randomArray.Length; i++)
        {
            int num = Random.Range(0, randomArray.Length);
            int temp = randomArray[i];
            randomArray[i] = randomArray[num];
            randomArray[num] = temp;
        }

        //Set phone text in the order of the randomArray
        int phoneIdx = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            if (randomArray[i] == testidx)
            {
                continue;
            }
            phoneTexts[phoneIdx++].text = questions[randomArray[i]].GetArticle();
        }
    }

    public void SetTestText(GameObject test, Question question, int questionNum)
    {
        //Get and Set Quesiton text
        TMP_Text questionText = test.transform.Find("Question").GetComponent<TMP_Text>();
        questionText.text = "Question " + questionNum + ": " + question.Q;

        //Get and Set button text
        TMP_Text AText = test.transform.Find("ButtonA").Find("Text (TMP)").GetComponent<TMP_Text>();
        TMP_Text BText = test.transform.Find("ButtonB").Find("Text (TMP)").GetComponent<TMP_Text>();
        TMP_Text CText = test.transform.Find("ButtonC").Find("Text (TMP)").GetComponent<TMP_Text>();
        TMP_Text DText = test.transform.Find("ButtonD").Find("Text (TMP)").GetComponent<TMP_Text>();

        AText.text = "A: " + question.options[0];
        BText.text = "B: " + question.options[1];
        CText.text = "C: " + question.options[2];
        DText.text = "D: " + question.options[3];
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

    public string GetArticle()
    {
        string article = articlePreAnswer + options[answer] + articlePostAnswer;
        article = article.Replace("\r", "");
        return article;
    }
}
