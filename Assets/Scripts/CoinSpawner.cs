using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField] GameObject coinPrefab;
    [SerializeField] List<Transform> coinSpawnPositions = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnCoinsOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoinsOverTime()
    {
        while (true){
            yield return new WaitForSeconds(2f);

            int randomIndex = Random.Range(0, coinSpawnPositions.Count);
            Vector3 newPos = coinSpawnPositions[randomIndex].position;

            Instantiate(coinPrefab, newPos, Quaternion.identity);
        }
    }
}
