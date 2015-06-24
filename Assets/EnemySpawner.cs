using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public float MinimumX;
    public float MaximumX;
    public float Interval = 5f;
    public GameObject EnemyPrefab;

	void Start ()
    {
        StartCoroutine(spawnEnemy());
	}

    IEnumerator spawnEnemy()
    {

        while(true)
        {
            Instantiate(EnemyPrefab, transform.position + Vector3.right * Random.Range(MinimumX, MaximumX), Quaternion.identity);

            yield return new WaitForSeconds(Interval);
        }

        yield return null;
    }
}
