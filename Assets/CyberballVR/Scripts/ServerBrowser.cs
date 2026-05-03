using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ServerBrowser : MonoBehaviour
{
    private int currentLevel = -1;
    public GameManager gameManager;
    public GameObject startButton;
    public GameObject findingLobby;
    public GameObject foundLobby;
    public GameObject cancelButton;
    public GameObject tutorialButton;
    public GameObject usernameButton;
    public GameObject title;
    public GameObject usernameMenu;

    public FadeToBlack fadeScript;

    public AudioSource audioFoundLobby;

    public void StartButton()
    {
        StartCoroutine(start());
    }

    public void CancelButton()
    {
        Debug.Log("cancel");
        StopAllCoroutines();
        fadeScript.cancel();
        startButton.SetActive(true);
        findingLobby.SetActive(false);
        cancelButton.SetActive(false);
        currentLevel--;
    }

    public IEnumerator start()
    {
        currentLevel++;
        startButton.SetActive(false);
        findingLobby.SetActive(true);
        cancelButton.SetActive(true);
        yield return new WaitForSeconds(Random.Range(5, 10));

        findingLobby.SetActive(false);
        foundLobby.SetActive(true);
        int wait = Random.Range(4, 10);
        cancelButton.SetActive(false);
        fadeScript.fadeToBlack("Joining Lobby", wait);
        audioFoundLobby.PlayOneShot(audioFoundLobby.clip);
        yield return new WaitForSeconds(wait);

        gameManager.StartGame();
        foundLobby.SetActive(false);
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        yield return null;
    }

    public void loadTutorial()
    {
        //Debug.Log("Button CLicked");
        SceneManager.LoadScene("Tutorial");
    }

    public void UsernameButton()
    {
        startButton.SetActive(false);
        tutorialButton.SetActive(false);
        usernameButton.SetActive(false);
        title.SetActive(false);
        usernameMenu.SetActive(true);
    }

    public void BackButton()
    {
        startButton.SetActive(true);
        tutorialButton.SetActive(true);
        usernameButton.SetActive(true);
        title.SetActive(true);
        usernameMenu.SetActive(false);
    }
}
