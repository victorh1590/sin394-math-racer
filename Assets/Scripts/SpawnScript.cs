using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
  [SerializeField]
  private GameObject swarmerPrefab;
  // [SerializeField]
  // private GameObject bigSwarmerPrefab;

  [SerializeField]
  private float swarmerInterval = 2.5f;
  // [SerializeField]
  // private float bigSwarmerInterval = 10.0f;

  private List<GameObject> itemsAndObstaclesList = new();

  private Coroutine spawnCoroutine = null;

  // Start is called before the first frame update
  void Start()
  {
    ItemsAndObstaclesPrefabScript itemsAndObstacles = swarmerPrefab.GetComponent<ItemsAndObstaclesPrefabScript>();
    itemsAndObstaclesList = new(itemsAndObstacles.ObstacleList);
    itemsAndObstaclesList.AddRange(itemsAndObstacles.ItemList);
    spawnCoroutine = StartCoroutine(SpawnObstacle(swarmerInterval, itemsAndObstaclesList));
  }

  private IEnumerator SpawnObstacle(float interval, List<GameObject> itemsAndObstaclesList)
  {
    yield return new WaitForSeconds(interval);
    interval = Random.Range(2f, 4f);
    GameObject obstacle = itemsAndObstaclesList[Random.Range(0, itemsAndObstaclesList.Count)];
    GameObject newObstacle = Instantiate(obstacle, new Vector3(9.5f, (float)Random.Range(-1, 3) + 0.5f, 20f), Quaternion.identity);
    StartCoroutine(SpawnObstacle(interval, itemsAndObstaclesList));
  }

  public void StopSpawn()
  {
    if(spawnCoroutine != null)
    {
        StopCoroutine(spawnCoroutine);
        this.StopAllCoroutines();
        spawnCoroutine = null;
    }
    // StopCoroutine(this.SpawnObstacle(swarmerInterval, itemsAndObstaclesList));
    List<GameObject> itemsOnScreen = new();
    itemsOnScreen.AddRange(GameObject.FindGameObjectsWithTag("Obstacle"));
    itemsOnScreen.AddRange(GameObject.FindGameObjectsWithTag("Health"));
    itemsOnScreen.AddRange(GameObject.FindGameObjectsWithTag("Fuel"));

    itemsOnScreen.ForEach(obj => GameObject.Destroy(obj.gameObject));
  }

  public void RestartSpawn()
  {
    if(spawnCoroutine != null)
    {
        StopSpawn();
    }
    spawnCoroutine = StartCoroutine(SpawnObstacle(swarmerInterval, itemsAndObstaclesList));
  }
}
