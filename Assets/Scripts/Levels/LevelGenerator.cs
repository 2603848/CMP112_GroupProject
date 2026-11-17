using Unity.AI.Navigation;
using UnityEngine;

//Parts of this code have been taken from https://www.youtube.com/watch?v=YZNxy2kabfI
//Parts of his code have been changed by me to fit my need.

public class LevelGenerator : MonoBehaviour
{
    public Texture2D levelPlan;
    //floor colour is 323c39
    //player colour is 99e550
    //skeleton colour is ff0000
    //cacodemon colour is c97c7c
    public Color32 wallsColour, playerColour, floorColour, skeletonColour, cacodemonColour;
    public GameObject floorPrefab, wallPrefab, playerPrefab, skeletonPrefab, cacodemonPrefab;

    private byte[,] levelData;

    public GameObject player;

    NavMeshSurface surface;

    private void Start()
    {
        levelData = new byte[levelPlan.width, levelPlan.height];
        surface = GetComponent<NavMeshSurface>();
        GenerateLevelData();
        BuildLevel();
    }

    void GenerateLevelData()
    {
        for (byte x = 0; x < levelPlan.width; x++)
        {
            for (byte y = 0; y < levelPlan.height; y++)
            {
                Color32 data = levelPlan.GetPixel(x, y);
                if (data.Equals(floorColour))
                {
                    levelData[x, y] = 1;
                }
                else if (data.Equals(playerColour))
                {
                    levelData[x, y] = 100;
                }
                else if (data.Equals(skeletonColour))
                {
                    levelData[x, y] = 201;
                }
                else if (data.Equals(cacodemonColour))
                {
                    levelData[x, y] = 202;
                }
            }
        }
        for (byte x = 0; x < levelPlan.width; x++)
        {
            for (byte y = 0; y < levelPlan.height; y++)
            {
                if (levelData[x, y] == 0 && IsNearWall(x, y))
                {
                    levelData[x, y] = 2;
                }
            }
        }
    }

    void BuildLevel()
    {
        for (byte x = 0; x < levelPlan.width; x++)
        {
            for (byte y = 0; y < levelPlan.height; y++)
            {
                switch (levelData[x, y])
                {
                    case 0:
                        break;

                    case 1: //Spawning the floors
                        Instantiate(floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, +4f, y * 3), Quaternion.identity);
                        break;

                    case 2: //Spawning the walls
                        Instantiate(wallPrefab, new Vector3(x * 3, 0, y * 3), Quaternion.identity);
                        break;

                    case 100:
                        player.transform.position = new Vector3(x * 3, 0, y * 3);
                        Instantiate(floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, +4f, y * 3), Quaternion.identity);
                        break;
                    case 201: //Spawning the skeleton
                        Instantiate(skeletonPrefab, new Vector3(x * 3, 0, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, +4f, y * 3), Quaternion.identity);
                        break;
                    case 202://Spawning the cacodemon
                        Instantiate(cacodemonPrefab, new Vector3(x * 3, 0, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, -1.5f, y * 3), Quaternion.identity);
                        Instantiate(floorPrefab, new Vector3(x * 3, +4f, y * 3), Quaternion.identity);
                        break;
                }
            }
        }

        BuildNavMesh();
    }

    bool IsNearWall(byte x, byte y)
    {
        if (x > 0 && levelData[x - 1, y] != 0)
            return true;
        if (y > 0 && levelData[x, y - 1] != 0)
            return true;
        if (x < levelPlan.width - 1 && levelData[x + 1, y] != 0)
            return true;
        if (y < levelPlan.height - 1 && levelData[x, y + 1] != 0)
            return true;
        return false;
    }

    void BuildNavMesh() 
    { 
        surface.BuildNavMesh(); 
    }
}
