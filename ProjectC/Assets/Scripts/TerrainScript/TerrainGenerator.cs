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
    [SerializeField] private GameObject RodDaeng;
    [SerializeField] private GameObject RodDaeng2;
    [SerializeField] private GameObject PickUp;
    [SerializeField] private GameObject PickUp2;
    [SerializeField] private GameObject plank;
    [SerializeField] private GameObject plank2;
    private bool spawntree = false;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject bush;
    [SerializeField] private GameObject coin;
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
            if (isFirst && isStart) //If is first time spawn, only spawn grass
            {
                whichTerrain = 0;
            }
            else //If not
            {
                whichTerrain = RandomTerrain(0, terrainsDatas.Count); //Random terrain
            }
            int terrainMaxRange; //Max terrain per random
            if (whichTerrain == 1) //If terrain = Road can only spawn 2
            {
                terrainsDatas[whichTerrain].maxRange = 4;
                terrainMaxRange = terrainsDatas[whichTerrain].maxRange;
            }
            if (whichTerrain == 4)
            {
                whichTerrain = Random.Range(2, terrainsDatas[whichTerrain].maxRange);
            }
            if (whichTerrain == 3)
            {
                terrainsDatas[whichTerrain].maxRange = 2;
                terrainMaxRange = terrainsDatas[whichTerrain].maxRange;
            }
            else //If not random max range
            {
                terrainMaxRange = Random.Range(2, terrainsDatas[whichTerrain].maxRange);
            }
            for (int i = 0; i < terrainMaxRange; i++) //Spawn Terrain loop
            {
                Debug.Log("Spawned" + whichTerrain + terrainMaxRange);
                Vector3 currentTerrainPosition = currentPosition;
                if (whichTerrain == 0)
                {
                    int amount = Random.Range(0,5);
                    spawnTree(amount);
                    if (isFirst && isStart)
                    {
                        spawntree = false;
                    }
                    else
                    {
                        spawntree = true;
                    }
                }
                if (whichTerrain == 1) //If terrain = Road
                {
                    roadnum = Random.Range(0,7);
                    Spawncar(roadnum);
                }
                if (whichTerrain == 2) //If terrain = water
                {
                    plankside = Random.Range(0,2);
                    Spawnplank(plankside);
                }
                GameObject terrain = Instantiate(terrainsDatas[whichTerrain].terrain, currentPosition, Quaternion.identity); //SpawnTerrain
                terrain.transform.SetParent(terrainSpawned); //Group it
                currentTerrains.Add(terrain); //Add data
                if (whichTerrain == 3) //If terrain = Traffic
                {
                    whichTerrain = 4;
                }
                if (!isStart) //If it's not start remove old data
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
    void Spawncar(int whichCar)
    {
        switch(whichCar)
        {
            case 0:
                Instantiate(car, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z - 12), Quaternion.identity);
                break;
            case 1:
                Instantiate(car2, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z + 12), Quaternion.Euler(0, 180, 0));
                break;
            case 2:
                Instantiate(RodDaeng, new Vector3(currentPosition.x, currentPosition.y + 0.7f, currentPosition.z - 12), Quaternion.identity);
                break;
            case 3:
                Instantiate(RodDaeng2, new Vector3(currentPosition.x, currentPosition.y + 0.7f, currentPosition.z + 12), Quaternion.Euler(0, 180, 0));
                break;
            case 4:
                Instantiate(PickUp, new Vector3(currentPosition.x, currentPosition.y + 0.65f, currentPosition.z - 12), Quaternion.identity);
                break;
            case 5:
                Instantiate(PickUp2, new Vector3(currentPosition.x, currentPosition.y + 0.5f, currentPosition.z + 12), Quaternion.Euler(0, 180, 0));
                break;
            default:
                break;
        }
    }
    void Spawnplank(int plankside)
    {
        if (plankside == 0)
        {
            Instantiate(plank, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z + 4), Quaternion.Euler(270, 270, 180));
        }
        if (plankside == 1)
        {
            Instantiate(plank2, new Vector3(currentPosition.x, currentPosition.y + 0.3f, currentPosition.z - 4), Quaternion.Euler(270, 0, 90));
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

    void spawnTree(int amount)
    {
        if (spawntree)
        {
            //Create Available position
            List<int> availablePositions = new List<int>();
            for (int i = -4; i <= 4; i++)
            {
                availablePositions.Add(i);
            }

            for (int i = 0 ; i <= amount; i++)
            {
                int whattype = Random.Range(0,3); //Random Object Type

                if (availablePositions.Count > 0)
                {
                    int randomIndex = Random.Range(0, availablePositions.Count);
                    int position = availablePositions[randomIndex];
                    availablePositions.RemoveAt(randomIndex);
                    if (whattype == 0)
                    {
                        Instantiate(tree, new Vector3(currentPosition.x, currentPosition.y + 0.5f, position), Quaternion.identity);
                    }
                    else if (whattype == 1)
                    {
                        Instantiate(bush, new Vector3(currentPosition.x, currentPosition.y + 0.5f, position), Quaternion.Euler(-90, 0, 0));
                    }
                    else if (whattype == 2)
                    {
                        Instantiate(coin, new Vector3(currentPosition.x, currentPosition.y + 1f, position), Quaternion.identity);
                    }
                }
                else
                {
                    break;
                }

                
            }
        }
        
    }
}
