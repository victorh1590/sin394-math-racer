using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerScript : MonoBehaviour
{
  public float speed = 5;
  public Vector2 screenLimit;
  public float health = 100;
  public float fuel = 100;
  public float maxFuel = 100;
  public float maxHealth = 100;
  int score = 0;
  private float seconds = 0;
  private int minutes = 0;
  public Image lifeBar;
  public Image fuelBar;
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI healthText;
  public TextMeshProUGUI fuelText;
  public GameObject questionPanel;
  public GameObject questions;
  public GameObject spawn;
  // public TextMeshProUGUI scoreText;
  // public TextMeshProUGUI newScoreText;
  //   public GameObject explosion;
  // bool dead = false;
  public GameObject deathPanel;
  private bool isPaused;
  public GameObject pausePanel;
  public string cena;
  private float timeSinceLastScoreUpdate = 0f;
  private float lastHealthUpdate = 0;
  private float lastFuelUpdate = 0;
  private SpawnScript spawnScript;

  // Start is called before the first frame update


  private float xMin, xMax;
  private float yMin, yMax;
  private float spriteSize;

  [NonSerialized]
  public Coroutine fuelCoroutine = null;

  private Coroutine cutscene = null;
  public TextMeshProUGUI cutsceneText;
  public GameObject cutscenePanel;

  public GameObject victoryPanel;

  [NonSerialized]
  public bool timeStop = false;
  public bool P_tutorial = true;

  public void PPP(bool X)
  {
    P_tutorial = X;
  }
  private void Start()
  {
    P_tutorial = true;
    Time.timeScale = 1f;
    health = maxHealth;
    fuel = maxFuel;
    score = 0;
    spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f;
    enabled = false;
    spawnScript = spawn.GetComponent<SpawnScript>();
    if (cena != "Tutorial")
    {
      cutscene = StartCoroutine(Cutscene());
    }
    else
    {
      enabled = true;
    }

    fuelCoroutine = StartCoroutine(UpdateFuel());
    UpdateUI();
  }

  private void Update()
  {
    if (!P_tutorial)
    {
      return;
    }
    if (!isPaused)
    {
      UpdateItemRatio();
      UpdateUI();
      UpdateTimer();
      UpdateScore();
      Movement();
    }
    if (!victoryPanel.activeSelf)
    {
        UpdateUI();

    }

        if (Input.GetKeyDown(KeyCode.Escape) && cena != "Tutorial")
    {
      PauseScreen();

    }

    }

  private IEnumerator Cutscene()
  {
    cutscenePanel.SetActive(true);
    yield return new WaitForSeconds(1);
    cutsceneText.text = "3";
    yield return new WaitForSeconds(1);
    cutsceneText.text = "2";
    yield return new WaitForSeconds(1);
    cutsceneText.text = "1";
    yield return new WaitForSeconds(1);
    cutsceneText.text = "Let's Go!";
    yield return new WaitForSeconds(1);
    cutscenePanel.SetActive(false);
    // custceneTime = false;
    enabled = true;
  }

  private void UpdateItemRatio()
  {
    // SpawnScript script = spawn.GetComponent<SpawnScript>();
    if (lastFuelUpdate != fuel && lastHealthUpdate != health)
    {
      if (health == maxHealth)
      {
        spawnScript.RemoveHeart();
      }
      else
      {
        spawnScript.AddHeart();
      }
      if (fuel == maxFuel)
      {
        spawnScript.RemoveFuel();
      }
      else
      {
        spawnScript.AddFuel();
      }
      lastFuelUpdate = fuel;
      lastHealthUpdate = health;
    }
  }

  void PauseScreen()
  {
    if (!isPaused && health > 0 && !victoryPanel.gameObject.activeSelf)
    {
      isPaused = true;
      Time.timeScale = 0f;
      pausePanel.SetActive(true);
      this.GetComponent<AudioSource>().Pause();
      if (questions.GetComponent<QuestionScript>().questionOpen) questionPanel.SetActive(false);
    }
    else
    {
      isPaused = false;
      Time.timeScale = 1f;
      pausePanel.SetActive(false);
      this.GetComponent<AudioSource>().UnPause();
      if (questions.GetComponent<QuestionScript>().questionOpen) questionPanel.SetActive(true);
    }
  }

  public void BackToMenu()
  {
    SceneManager.LoadScene(cena);
  }

  private void Movement()
  {
    // var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f; // Working with a simple box here, adapt to you necessity

    var cam = Camera.main;// Camera component to get their size, if this change in runtime make sure to update values
    var camHeight = cam.orthographicSize;
    var camWidth = cam.orthographicSize * cam.aspect;

    yMin = (-camHeight + 3.5f) + spriteSize; // lower bound - y = -2
    yMax = (camHeight - 1.55f) - spriteSize; // upper bound

    xMin = -camWidth + spriteSize; // left bound
    xMax = -spriteSize; // right bound 

    // Get buttons
    var ver = Input.GetAxisRaw("Vertical");
    var hor = Input.GetAxisRaw("Horizontal");

    Vector2 move = Application.isMobilePlatform ? InputHandle.joystickAxis : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Calculate movement direction
        var direction = move;/* new Vector2(hor, ver).normalized; */
    direction *= speed * Time.deltaTime; // apply speed

    var xValidPosition = Mathf.Clamp(transform.position.x + direction.x, xMin, xMax);
    var yValidPosition = Mathf.Clamp(transform.position.y + direction.y, yMin, yMax);

    transform.position = new Vector3(xValidPosition, yValidPosition, 20f);
  }

  private void UpdateTimer()
  {
    if (!timeStop)
    {
      seconds += Time.deltaTime;
      if ((int)seconds >= 60)
      {
        seconds -= 60;
        minutes++;
      }
      if (minutes == 2)
      {
        minutes = 0;
        Victory();
      }
    }
  }

  private void Victory()
  {
    Time.timeScale = 0; 
    victoryPanel.SetActive(true);
  }

  public IEnumerator UpdateFuel()
  {
    while (fuel > 10)
    {
      yield return new WaitForSeconds(10);
      fuel -= 10;
      if (fuel == 0)
      {
        Die();
      }
    }
  }

  public void StopUpdateFuel()
  {
    if (fuelCoroutine != null)
    {
      StopCoroutine(fuelCoroutine);
      fuelCoroutine = null;
    }
  }

  public void RestartUpdateFuel()
  {
    StopUpdateFuel();
    fuelCoroutine = StartCoroutine(UpdateFuel());
  }

  private void UpdateUI()
  {
    lifeBar.fillAmount = health / 100;
    healthText.text = "" + health + "%";
    fuelBar.fillAmount = fuel / 100;
    fuelText.text = "" + fuel + "%";
    timeText.text = minutes + ":" + ((int)seconds).ToString("00");
    scoreText.text = score.ToString("0000");
  }

  public void TakeDamage(int damage = 20)
  {
    if (damage < 0) return;
    if (health - damage > 0)
    {
      health -= damage;
    }
    else
    {
      health = 0;
      Die();
    }
    UpdateUI();
  }

  public void Heal(int healingAmount = 20)
  {
    if (healingAmount <= 0) return;
    if (health + healingAmount <= 100)
    {
      health += healingAmount;
    }
    else
    {
      health = 100;
    }
    UpdateUI();
  }

  public void AddGas(int healingAmount = 20)
  {
    if (healingAmount <= 0) return;
    if (fuel + healingAmount <= 100)
    {
      fuel += healingAmount;
    }
    else
    {
      fuel = 100;
    }
    UpdateUI();
  }

  public void AddScore(int value = 10) => score += value;

  private void Die()
  {
    this.GetComponent<AudioSource>().Stop();
    Time.timeScale = 0;
    deathPanel.SetActive(true);
  }

  private void UpdateScore()
  {
    timeSinceLastScoreUpdate += Time.deltaTime;
    if (timeSinceLastScoreUpdate >= 10f)
    {
      AddScore(10);
      timeSinceLastScoreUpdate = 0f;
    }
  }
}