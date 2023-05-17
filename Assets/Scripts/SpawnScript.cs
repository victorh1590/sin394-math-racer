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
    private float swarmerInterval = 3.5f;
    // [SerializeField]
    // private float bigSwarmerInterval = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        ItemsAndObstaclesPrefabScript itemsAndObstacles = swarmerPrefab.GetComponent<ItemsAndObstaclesPrefabScript>();
        List<GameObject> itemsAndObstaclesList = new(itemsAndObstacles.ObstacleList);
        itemsAndObstaclesList.AddRange(itemsAndObstacles.ItemList);
        StartCoroutine(SpawnObstacle(swarmerInterval, itemsAndObstaclesList));
    }

    private IEnumerator SpawnObstacle(float interval, List<GameObject> itemsAndObstaclesList) 
    {
        yield return new WaitForSeconds(interval);
        GameObject obstacle = itemsAndObstaclesList[Random.Range(0, itemsAndObstaclesList.Count)];
        GameObject newObstacle = Instantiate(obstacle, new Vector3(9.5f, (float)Random.Range(-1, 4), 0f), Quaternion.identity);
        StartCoroutine(SpawnObstacle(interval, itemsAndObstaclesList));
    }
}
