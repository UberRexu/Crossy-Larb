using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int min_distanceFromPlayer;
    [SerializeField] private List<TerrainData> terrainsDatas = new List<TerrainData>();
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private Transform terrainSpawned;
    [HideInInspector]private Vector3 currentPosition = new Vector3(0, 0, 0);
    private List<GameObject> currentTerrains = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrains(true, new Vector3(0, 0, 0));
        }
        maxTerrainCount = currentTerrains.Count;
    }

    public void SpawnTerrains(bool isStart, Vector3 playerPos)
    {
        if (currentPosition.x - playerPos.x < min_distanceFromPlayer || (isStart))
        {
            int whichTerrain = Random.Range(0, terrainsDatas.Count);
            int terrainMaxRange;
            if (whichTerrain == 1)
            {
                terrainsDatas[whichTerrain].maxRange = 2;
                terrainMaxRange = terrainsDatas[whichTerrain].maxRange;
            }
            else
            {
                terrainMaxRange = Random.Range(1, terrainsDatas[whichTerrain].maxRange);
            }
            for (int i = 0; i < terrainMaxRange; i++)
            {
                GameObject terrain = Instantiate(terrainsDatas[whichTerrain].terrain, currentPosition, Quaternion.identity);
                terrain.transform.SetParent(terrainSpawned);
                currentTerrains.Add(terrain);
                if (!isStart)
                {
                    if (currentTerrains.Count > maxTerrainCount)
                    {
                        Destroy(currentTerrains[0]);
                        currentTerrains.RemoveAt(0);
                    }
                }
                currentPosition.x++;
            }
        }
    }
}
