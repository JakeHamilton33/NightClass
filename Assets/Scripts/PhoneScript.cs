using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{

    #region Declarations
    public static PhoneScript instance;

    public enum WebsiteState { Selecting, Website1, Website2, Website3, Website4, Website5, Website6, Website7, Website8, Website9 }
    public static WebsiteState _website;

    //[SerializeField] private GameObject[] cameras;
    public GameObject[] websites;
    private int currentWebsite;
    [SerializeField] private GameObject HomeButton;

    #endregion

    #region Unity Methods
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _website = WebsiteState.Selecting;
        currentWebsite = 0;
    }
    #endregion

    #region State Change
    public void StateChange()
    {
        websites[currentWebsite].SetActive(false);
        HomeButton.SetActive(false);
        switch (_website)
        {
            case WebsiteState.Selecting:
                {
                    currentWebsite = 0;
                    websites[currentWebsite].SetActive(true);
                    break;
                }
            case WebsiteState.Website1:
                {
                    currentWebsite = 1;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website2:
                {
                    currentWebsite = 2;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website3:
                {
                    currentWebsite = 3;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website4:
                {
                    currentWebsite = 4;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website5:
                {
                    currentWebsite = 5;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website6:
                {
                    currentWebsite = 6;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website7:
                {
                    currentWebsite = 7;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website8:
                {
                    currentWebsite = 8;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
            case WebsiteState.Website9:
                {
                    currentWebsite = 9;
                    websites[currentWebsite].SetActive(true);
                    HomeButton.SetActive(true);
                    break;
                }
        }
    }
    #endregion
    public void SendBack()
    {
        Debug.Log("Back Button");
        _website = WebsiteState.Selecting;
        StateChange();
    }

    public void EndGame()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        if(PlayerPrefs.GetInt("Caught") == 1)
        {
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
        else
        {
            yield return null;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
        
    } 
}
