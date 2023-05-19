using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
  public float speed = 5;
  public Vector2 screenLimit;
  public int health = 100;
  public int fuel = 100;
  public int maxFuel = 100;
  public int maxHealth = 100;
  int score = 0;
  private float seconds = 0;
  private int minutes = 0;
  public Image lifeBar;
  public Image fuelBar;
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI healthText;
  public TextMeshProUGUI fuelText;
  // public TextMeshProUGUI scoreText;
  // public TextMeshProUGUI newScoreText;
  //   public GameObject explosion;
  //   public GameObject menu;
  //   bool pause = false, dead = false;
  //   public GameObject pauseMenu;

  // Start is called before the first frame update

  private float xMin, xMax;
  private float yMin, yMax;

  private Coroutine fuelCoroutine = null;

  private void Start()
  {
    health = maxHealth;
    fuel = maxFuel;
    // score = 0;
    fuelCoroutine = StartCoroutine(UpdateFuel());
    UpdateUI();
  }

  private void Update()
  {
    UpdateUI();
    UpdateTimer();
    Movement();
  }

  private void Movement()
  {
    var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f; // Working with a simple box here, adapt to you necessity

    var cam = Camera.main;// Camera component to get their size, if this change in runtime make sure to update values
    var camHeight = cam.orthographicSize;
    var camWidth = cam.orthographicSize * cam.aspect;

    yMin = (-camHeight + 3) + spriteSize; // lower bound - y = -2
    yMax = camHeight - spriteSize; // upper bound

    xMin = -camWidth + spriteSize; // left bound
    xMax = (camWidth - camWidth) - spriteSize; // right bound 

    // Get buttons
    var ver = Input.GetAxis("Vertical");
    var hor = Input.GetAxis("Horizontal");

    // Calculate movement direction
    var direction = new Vector2(hor, ver).normalized;
    direction *= speed * Time.deltaTime; // apply speed

    var xValidPosition = Mathf.Clamp(transform.position.x + direction.x, xMin, xMax);
    var yValidPosition = Mathf.Clamp(transform.position.y + direction.y, yMin, yMax);

    transform.position = new Vector3(xValidPosition, yValidPosition, 20f);
  }

  private void UpdateTimer()
  {
    seconds += Time.deltaTime;
    if ((int)seconds >= 60)
    {
      seconds -= 60;
      minutes++;
    }
  }

  public IEnumerator UpdateFuel()
  {
    while (fuel > 10)
    {
      yield return new WaitForSeconds(10);
      fuel -= 10;
    }
  }

  public void StopUpdateFuel()
  {
    if (fuelCoroutine != null)
    {
      StopCoroutine(fuelCoroutine);
      this.StopAllCoroutines();
      fuelCoroutine = null;
    }
  }

  public void RestartUpdateFuel()
  {
    if (fuelCoroutine != null)
    {
      StopUpdateFuel();
    }
    fuelCoroutine = StartCoroutine(UpdateFuel());
  }

  private void UpdateUI()
  {
    lifeBar.fillAmount = (float)health / maxHealth;
    healthText.text = "Vida: " + (int)(health / 20);
    fuelBar.fillAmount = (float)fuel / maxFuel;
    fuelText.text = "Gasolina: " + fuel + "%";
    timeText.text = minutes + ":" + ((int)seconds).ToString("00");
    // scoreText.text = "Score: " + ((int)gameTimer + score);
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
      // Die();
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

  private void AddScore(int value = 20) => score += value;

  private void Die()
  {
    // dead = true;
    // if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
    // health = maxHealth;
    // transform.position = Vector2.zero;
    // int oldScore = PlayerPrefs.GetInt("Score");
    // int newScore = (int)gameTimer + score;
    // if (newScore >= oldScore)
    // {
    //   PlayerPrefs.SetInt("Score", newScore);
    // }
    // if (newScoreText.text != null)
    // {
    //   newScoreText.text = "Sua Pontuação: " + newScore.ToString() + "\nPontuação Máxima: " + PlayerPrefs.GetInt("Score");
    // }
    // menu.SetActive(true);
    // Time.timeScale = 0;
  }
}