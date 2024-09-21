using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private List<Steve> steves; 

    [SerializeField] private List<Transform> spawnPositions;

    private void Start()
    {
        Transform firstPlayerSpawn = spawnPositions[Random.Range(0, spawnPositions.Count)];
        
        steves[0].transform.position = firstPlayerSpawn.position;

        for (int i = 1; i < steves.Count; i++)
        {
            steves[i].transform.position = FarthestSpawnPosition(steves.Where(x=> steves.IndexOf(x) < i).Select(x => x.transform.position).ToList()).position;
        }

        foreach (var steve in steves)
        {
            steve.Died += () => { StartCoroutine(RespawnSteve(steve)); };
        }
    }
    
    IEnumerator RespawnSteve(Steve steve)
    {
        yield return new WaitForSeconds(3);
        steve.Respawn();
        steve.transform.position = FarthestSpawnPosition(steves.Where(x => steves.IndexOf(x) != steves.IndexOf(steve)).Select(x => x.transform.position).ToList()).position;
        yield return 0;
    }

    private Transform FarthestSpawnPosition(List<Vector3> stevesPositioning)
    {
        float sqrMagnitude = 0;
        int index = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            float distance = 0;
            foreach (var pos in stevesPositioning)
            {
                distance += Vector3.SqrMagnitude(pos - spawnPositions[i].position);
            }

            if (distance > sqrMagnitude)
            {
                sqrMagnitude = distance;
                index = i;
            }
        }
        return spawnPositions[index];
    }
}
