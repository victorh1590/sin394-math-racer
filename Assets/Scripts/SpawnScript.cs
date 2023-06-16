using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnScript : MonoBehaviour
{
  [SerializeField]
  private GameObject swarmerPrefab;
  // [SerializeField]
  // private GameObject bigSwarmerPrefab;
  [SerializeField]
  public float minRange = 0.75f;

  [SerializeField]
  public float maxRange = 1.0f;

  private float swarmerInterval = 7.5f;
  // [SerializeField]
  // private float bigSwarmerInterval = 10.0f;

  private List<GameObject> itemsAndObstaclesList = new();

  private Coroutine spawnCoroutine = null;

  private ItemsAndObstaclesPrefabScript itemsAndObstacles;

  private GameObject heart;

  private GameObject fuel;

  // Start is called before the first frame update
  void Start()
  {
    ItemsAndObstaclesPrefabScript itemsAndObstacles = swarmerPrefab.GetComponent<ItemsAndObstaclesPrefabScript>();
    heart = itemsAndObstacles.ItemList.Where(obj => obj.tag == "Health").First();
    fuel = itemsAndObstacles.ItemList.Where(obj => obj.tag == "Fuel").First();
    itemsAndObstaclesList = new(itemsAndObstacles.ObstacleList);
    itemsAndObstaclesList.AddRange(itemsAndObstacles.ObstacleList);
    // itemsAndObstaclesList.AddRange(itemsAndObstacles.ObstacleList);
    // itemsAndObstaclesList.AddRange(itemsAndObstacles.ItemList);
    spawnCoroutine = StartCoroutine(SpawnObstacle(swarmerInterval, itemsAndObstaclesList));
  }

  private IEnumerator SpawnObstacle(float interval, List<GameObject> itemsAndObstaclesList)
  {
    yield return new WaitForSeconds(interval);
    interval = Random.Range(0.75f, 1.0f);
    GameObject obstacle = itemsAndObstaclesList[Random.Range(0, itemsAndObstaclesList.Count)];
    GameObject newObstacle = Instantiate(obstacle, new Vector3(9.5f, (float)Random.Range(-1, 3) + 0.5f, 20f), Quaternion.identity);
    StartCoroutine(SpawnObstacle(interval, itemsAndObstaclesList));
  }

  public void DisableHealingItems()
  {
    itemsAndObstaclesList = itemsAndObstaclesList.Except(itemsAndObstacles.ItemList).ToList();
  }

  public void EnableHealingItems()
  {
    itemsAndObstaclesList.AddRange(itemsAndObstacles.ItemList);
  }

  public void AddHeart()
  {
    if(!itemsAndObstaclesList.Contains(heart)) 
    {
      itemsAndObstaclesList.Add(heart);
    }
  }

  public void RemoveHeart()
  {
    itemsAndObstaclesList.RemoveAll(obj => obj.Equals(heart));
  }

  public void AddFuel()
  {
    if(!itemsAndObstaclesList.Contains(fuel)) 
    {
      itemsAndObstaclesList.Add(fuel);
    }
  }

  public void RemoveFuel()
  {
    itemsAndObstaclesList.RemoveAll(obj => obj.Equals(fuel));
  }

  public void IncreaseObstacles()
  {
    itemsAndObstaclesList.AddRange(itemsAndObstacles.ObstacleList);
  }

  public void DecreaseObstacles()
  {
    foreach(var obstacle in itemsAndObstacles.ObstacleList)
    {
        itemsAndObstaclesList.Remove(obstacle);
    }
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
    spawnCoroutine = StartCoroutine(SpawnObstacle(swarmerInterval - 5f, itemsAndObstaclesList)); // -5f is the cutscene time.
  }
}
