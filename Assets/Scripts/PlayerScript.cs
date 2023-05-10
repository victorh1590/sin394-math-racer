using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
  public float speed = 5;
  public Vector2 screenLimit;
  //   float shootTimer = 0;
  //   public int health = 10;
  //   public int maxHealth = 10;
  //   int score = 0;
  //   private float gameTimer = 0;
  //   public Image lifeBar;
  //   public TextMeshProUGUI healthText, scoreText;
  //   public TextMeshProUGUI newScoreText;
  //   public GameObject explosion;
  //   public GameObject menu;
  //   bool pause = false, dead = false;
  //   public GameObject pauseMenu;

  // Start is called before the first frame update

  private float xMin, xMax;
  private float yMin, yMax;

  private void Start()
  {
    var spriteSize = GetComponent<SpriteRenderer>().bounds.size.x * .5f; // Working with a simple box here, adapt to you necessity

    var cam = Camera.main;// Camera component to get their size, if this change in runtime make sure to update values
    var camHeight = cam.orthographicSize;
    var camWidth = cam.orthographicSize * cam.aspect;

    yMin = (camHeight - camHeight) + spriteSize; // lower bound
    yMax = camHeight - spriteSize; // upper bound

    xMin = -camWidth + spriteSize; // left bound
    xMax = (camWidth - camWidth) - spriteSize; // right bound 
  }

  private void Update()
  {
    // Get buttons
    var ver = Input.GetAxis("Vertical");
    var hor = Input.GetAxis("Horizontal");

    // Calculate movement direction
    var direction = new Vector2(hor, ver).normalized;
    direction *= speed * Time.deltaTime; // apply speed

    var xValidPosition = Mathf.Clamp(transform.position.x + direction.x, xMin, xMax);
    var yValidPosition = Mathf.Clamp(transform.position.y + direction.y, yMin, yMax);

    transform.position = new Vector3(xValidPosition, yValidPosition, 0f);
  }
//   void Start()
//   {
//     // score = 0;
//     // health = maxHealth;
//     // UpdateUI();
//   }

  // Update is called once per frame
//   void Update()
//   {
//     gameTimer += Time.deltaTime;
//     // UpdateUI();
//     // shootTimer += Time.deltaTime;
//     Movement();

//     // Pause
//     // if (Input.GetButtonDown("Cancel"))
//     // {
//     //   pause = !pause;
//     //   if (pause && !dead)
//     //   {
//     //     pauseMenu.SetActive(true);
//     //     Time.timeScale = 0;
//     //   }
//     //   else if (!dead)
//     //   {
//     //     pauseMenu.SetActive(false);
//     //     Time.timeScale = 1;
//     //   }
//     // }
//   }

  //   Atualizar UI
  //   void UpdateUI()
  //   {
  //     lifeBar.fillAmount = (float)health / maxHealth;
  //     healthText.text = health + "/" + maxHealth;
  //     scoreText.text = "Score: " + ((int)gameTimer + score);
  //   }

  //   //Pontuação
  //   public void AddScore(int value = 20)
  //   {
  //     score += value;
  //   }

  //Movimento.
//   void Movement()
//   {
//     float xMove = Input.GetAxisRaw("Horizontal");
//     float yMove = Input.GetAxisRaw("Vertical");
//     transform.Translate(new Vector2(xMove, yMove).normalized * speed * Time.deltaTime);

//     //Limites do eixo X.
//     if (transform.position.x > ((screenLimit.x / 2) - 0.5f))
//     {
//       transform.position = new Vector3(transform.position.x, transform.position.y);
//     }

//     if (transform.position.x < (-screenLimit.x + 0.5f))
//     {
//       transform.position = new Vector3((-screenLimit.x + 0.5f), transform.position.y);
//     }

//     // Limites do eixo Y.
//     if (transform.position.y > (screenLimit.y - 0.5f))
//     {
//       transform.position = new Vector3(transform.position.x, transform.position.y);
//     }

//     if (transform.position.y < (-screenLimit.y + 0.5f))
//     {
//       transform.position = new Vector3(transform.position.x, transform.position.y);
//     }
//   }

  //   Tomando Dano.
  //   public void TakeDamage(int damage = 1)
  //   {
  //     if (damage < 0) return;
  //     if (health - damage > 0)
  //     {
  //       health -= damage;
  //     }
  //     else
  //     {
  //       health = 0;
  //       Die();
  //     }
  //     UpdateUI();
  //   }

  //   Morte
  //   void Die()
  //   {
  //     dead = true;
  //     if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
  //     health = maxHealth;
  //     transform.position = Vector2.zero;
  //     int oldScore = PlayerPrefs.GetInt("Score");
  //     int newScore = (int)gameTimer + score;
  //     if (newScore >= oldScore)
  //     {
  //       PlayerPrefs.SetInt("Score", newScore);
  //     }
  //     if (newScoreText.text != null)
  //     {
  //       newScoreText.text = "Sua Pontuação: " + newScore.ToString() + "\nPontuação Máxima: " + PlayerPrefs.GetInt("Score");
  //     }
  //     menu.SetActive(true);
  //     Time.timeScale = 0;
  //   }
}