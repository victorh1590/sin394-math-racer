using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelectScript : MonoBehaviour
{
    IEnumerator Menu1()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MenuScene");
    }

    public void Menu()
    {
        Time.timeScale = 1;
        StartCoroutine(Menu1());
    }

    IEnumerator Level1()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level");
    }

    public void Fase1()
    {
        Time.timeScale = 1;
        StartCoroutine(Level1());
    }

    IEnumerator Level2()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level2");
    }

    public void Fase2()
    {
        Time.timeScale = 1;
        StartCoroutine(Level2());
    }

    IEnumerator Level3()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level3");
    }

    public void Fase3()
    {
        Time.timeScale = 1;
        StartCoroutine(Level3());
    }

    IEnumerator Tutorial1()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Tutorial");
    }

    public void Tutorial()
    {
        Time.timeScale = 1;
        StartCoroutine(Tutorial1());
    }


}
