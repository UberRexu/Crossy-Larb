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
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject car2;
    [SerializeField] private GameObject plank;
    [SerializeField] private GameObject plank2;
    private int roadnum = 0; //For car left2right or right2left check
    private int plankside; //For plank left2right or right2left check || 0 = right || 1 = left
    private int previousRandomNumber = -1;
    void Start()
    {
        SpawnTerrains(true, true, new Vector3(0, 0, 0));
        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrains(true, false, new Vector3(0, 0, 0));
        }
        maxTerrainCount = currentTerrains.Count;
    }

    public void SpawnTerrains(bool isStart, bool isFirst, Vector3 playerPos)
    {
        if (currentPosition.x - playerPos.x < min_distanceFromPlayer || (isStart))
        {
            int whichTerrain;
            if (isFirst && isStart)
            {
                whichTerrain = 0;
            }
            else
            {
                whichTerrain = RandomTerrain(0, terrainsDatas.Count);
            }
            int terrainMaxRange;
            if (whichTerrain == 1)
            {
                terrainsDatas[whichTerrain].maxRange = 2;
                terrainMaxRange = terrainsDatas[whichTerrain].maxRange;
            }
            else
            {
                terrainMaxRange = Random.Range(2, terrainsDatas[whichTerrain].maxRange);
            }
            for (int i = 0; i < terrainMaxRange; i++)
            {
                Vector3 currentTerrainPosition = currentPosition;
                if (whichTerrain == 1)
                {
                    if (roadnum == 0)
                    {
                        Spawncar();
                        roadnum++;
                    }
                    else
                    {
                        Spawncar2();
                        roadnum = 0;
                    }
                }
                if (whichTerrain == 2)
                {
                    plankside = Random.Range(0,2);
                    Spawnplank(plankside);
                }
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
    void Spawncar()
    {
        Instantiate(car, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z - 12), Quaternion.identity);
    }
    void Spawncar2()
    {
        Instantiate(car2, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z + 12), Quaternion.Euler(0, 180, 0));
    }
    void Spawnplank(int plankside)
    {
        if (plankside == 0)
        {
            Instantiate(plank, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z - 12), Quaternion.identity);
        }
        if (plankside == 1)
        {
            Instantiate(plank2, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z + 12), Quaternion.Euler(0, 180, 0));
        }
    }
    int RandomTerrain(int min, int max)
    {
        int randomNumber;

        // Generate a random number
        randomNumber = Random.Range(min, max);

        // Check if it's the same as the previous one
        while (randomNumber == previousRandomNumber)
        {
            // Generate a new random number
            randomNumber = Random.Range(min, max);
        }

        // Update the previous random number
        previousRandomNumber = randomNumber;

        return randomNumber;
    }
}
