using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreText != null)
        {
            scoreText.text = "Pontuação máxima\n" + PlayerPrefs.GetInt("Score");
        }
    }

    public void Close()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitClose());
    }

    IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }

    public void Play()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitPlay());
    }

    IEnumerator WaitPlay()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MenuSelect");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitReturnToMenu());
    }

    IEnumerator WaitReturnToMenu()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadProfessorArea()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitProfessorArea());
    }

    IEnumerator WaitProfessorArea()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("AreaProf");
    }
}
