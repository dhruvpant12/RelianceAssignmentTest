using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Logic : Based on the size of the environment tile , we know how big it is . We can calculate the distance between the centers of two tiles
// which gives the amount by how much we need to place the new tile in the scene. This difference value acts as an offset and is added to the
//center of the new tile for its position. 
//The obstacles and the coins are made children of an empty object . This empty object has the same position as the new tile that is being
//made . So whenever a new tile is made , an empty object is made on the same position . Making obstacles and coins children to this empty
//object helps in placing them in the map properly, avoid overlaps and for deleting them later on. When the tile is offscreen , its respective empty object is deleted as well
// which causes the obstacles and coins that are its children to be deleted as well . Since the obstacles and coins are children, we donot have
// to keep a record of them and delete them seperatly . This reduces new code implementation and chances of errors.

public class TileGeneration : MonoBehaviour
{
    public GameObject gb;// reference to Environment prefab to be generated. 
    public GameObject parentobj;//This object will act like a parent to all the obstacles and coins . We have done this so that we can destroy the parent object and all its children like obstacles and coins will be deleted.
    public GameObject Coins; //reference to Coin prefab
    public LayerMask layermask; // Reference to layermask Obstacles . We use this for collision checks. 

    GameObject k ; //  This object will point to newly created tiles and will be used to add to a List.
    GameObject pobj;//  This object will point to newly created ParentObject (parentobj) and will be used to add to a List.
     


    Vector3 offset = new Vector3(0, 0, 275f); // Distance between the transform of tiles. Tiles will be made with their centers seperated by this distance.

    Vector3 SpawnPosition; // Transform position for generated tiles and parent of obstacles. This will be updated every time we create a new tile.

    bool game; // Level will be generated as long as game is playable. On player death , stop generating.

    List<GameObject> list = new List<GameObject>(); // List for tiles.
    List<GameObject> obstacleList = new List<GameObject>(); // list for ParentObj for obstacles and coins.
    public List<GameObject> design = new List<GameObject>(); // list of Obstacle Prefab.

    public Dictionary<int, GameObject> obstacles = new Dictionary<int, GameObject>(); // Storing reference to obstacles for fast access.


    WaitForSeconds TileSpawnTImer; 
    WaitForSeconds DestroyTileTimer;

    // Start is called before the first frame update
    void Start()
    {
        game = true;

        TileSpawnTImer = new WaitForSeconds(2f);
        DestroyTileTimer = new WaitForSeconds(10f);

        SpawnPosition = transform.position; //First tile position.

        for (int i = 0; i < design.Count; i++) //Initializing Dictionary
        {
            obstacles.Add(i + 1, design[i]);
        }

        k = Instantiate(gb, SpawnPosition, Quaternion.identity); //First tile made.

        pobj = Instantiate(parentobj); //First parent obj made   
        pobj.transform.position = SpawnPosition;
        pobj.transform.rotation = Quaternion.identity;

        list.Add(k); //Tiles added to list.
        obstacleList.Add(pobj); //Parent Object added to list

        StartCoroutine(Levelgen()); //Generate level .
        StartCoroutine(DestroyTiles()); //Generate level offscreen.
    }   

    public IEnumerator Levelgen()
    {

        while (game)
        {

            yield return TileSpawnTImer;//2sec

            SpawnPosition += offset; //Increase to new tiles position.

            k = Instantiate(gb, SpawnPosition, Quaternion.identity); //Tile instantiated.

            //Debug.Log("make tile");

            pobj = Instantiate(parentobj); //Parent obj made.

            SpawnObstacles(pobj.transform.position); // Obstacles instantiated as child of Parent Object.


            pobj.transform.position = SpawnPosition;
            pobj.transform.rotation = Quaternion.identity;

            SpawnCoins(pobj.transform.position); // Coins instantiated as child of Parent Object.

            list.Add(k);
            obstacleList.Add(pobj);
        }
    }

    int RandomIndex()
    {
        int index = UnityEngine.Random.Range(1, 7);
        return index;
    }

    void SpawnObstacles(Vector3 parentPosition)
    {
        float offset = 30f; // Obstacles will be placed 30 units away from each other.

        Vector3 spawnposition = parentPosition - new Vector3(0, 0, 100f); // Shifting position to the end of tile.

        while (spawnposition.z <= (parentPosition.x + 110f)) // So we dont generate off the tile.
        {

            GameObject designBlock = Instantiate(obstacles[RandomIndex()]); //Pick random obstacle design.
            designBlock.transform.SetParent(pobj.transform);
            designBlock.transform.position += spawnposition;
            spawnposition.z += offset;

        }
        return;
    }

    void SpawnCoins(Vector3 parentPosition)
    {
        int z = (int)parentPosition.z;  //Position of Parent object.
        float y = 0;
        float[] xValues = new float[] { -10, 0, 10 };  // We can choose which lane to put the coins on.
        int minRange = z - 100; //End of tile position.
        int maxRange = z + 100;//Front of tile position.

        for (int i = minRange; i <= maxRange; i = i + 10) //Representing z axis of the coin, 
        {
            for (int j = 0; j < 3; j++) //Representing x axis of the coin, 
            {
                Vector3 possibleNode = new Vector3(xValues[j], y, i);

                // Debug.Log(possibleNode);

                //Check if an obstacle is already present on the position. If yes , dont make coin there.
                bool validNode = Physics.CheckBox(possibleNode, new Vector3(4, 4, 4), Quaternion.identity, layermask);

                if (validNode)
                {
                    Debug.Log("collision");
                }
                else
                {
                    if (RandomIndex() % 3 == 0) //30% chance of spawning coin . SO that scene is not overpopulated with coins which will reduce FPS.
                    {
                        GameObject CoinClone = Instantiate(Coins);
                        CoinClone.transform.SetParent(gameObject.transform);
                        CoinClone.transform.position = possibleNode;
                    }
                }

            }
        }


    }

    public IEnumerator DestroyTiles()
    {
        while (game)
        {

            yield return DestroyTileTimer;
            if (list.Count > 0 && obstacleList.Count > 0)
            {
                Debug.Log("Destroyed");

                GameObject gameObject = list[0]; //tile
                GameObject Obstacle = obstacleList[0]; //spawnobject

                list.RemoveAt(0);
                obstacleList.RemoveAt(0);

                Destroy(gameObject);
                Destroy(Obstacle);
            }



        }

    }

    

    
}
