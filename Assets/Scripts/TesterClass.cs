using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterClass : MonoBehaviour
{
    public LayerMask layermask;
    public GameObject Coin;
    //parent transform
    Vector3 parentTransform;
    float[] xValues = new float[] { -10, 0, 10 };

    private void Start()
    {
        parentTransform = transform.position;

        MakeCoins();
    }
   

    void MakeCoins()
    {
        int z = (int)parentTransform.z;
        int y = 0;
        int minRange = z - 100;
        int maxRange = z + 100;
        for(int i=minRange;i<=maxRange;i=i+10) //z axis , 
        {
            for(int j=0;j<3;j++) // x axis
            {
                Vector3 possibleNode=new Vector3(xValues[j],y,i);
               // Debug.Log(possibleNode);

                bool validNode = Physics.CheckBox(possibleNode, new Vector3(4, 4, 4), Quaternion.identity,layermask);

                if(validNode)
                {
                    Debug.Log("collision");
                }
                else
                {
                    GameObject CoinClone=Instantiate(Coin);
                    CoinClone.transform.SetParent(gameObject.transform);
                    CoinClone.transform.position = possibleNode;
                }

            }
        }
    }
    /*public GameObject gb,parentobj; // gb for tile , parentob for spawnposition.
    public GameObject Coins;
    public LayerMask layermask;

    GameObject k,pobj,designObj; // k for tile list , pobj for spawnposition list , designobj for obstacles.

    Vector3 offset=new Vector3(0,0,275f); // Distance between the transform of tiles.

    Vector3 SpawnPosition; // Transform position for generated tiles and parent of obstacles.
    
    bool game; // Level will be generated as long as game is playable. On player death , stop generating.

     List<GameObject> list=new List<GameObject>(); // tiles
    List<GameObject> obstacleList = new List<GameObject>(); // spawnobject
    public List<GameObject> design = new List<GameObject>(); // obstacles

    public Dictionary<int, GameObject> obstacles = new Dictionary<int, GameObject>();


    WaitForSeconds TileSpawnTImer;
    WaitForSeconds DestroyTileTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        game = true;
        TileSpawnTImer = new WaitForSeconds(2f);
        DestroyTileTimer = new WaitForSeconds(6f);
        SpawnPosition = transform.position;
       
       

        for(int i=0; i<design.Count;i++)
        {
            obstacles.Add(i + 1, design[i]);
        }

        pobj = Instantiate(parentobj); 
        

        GameObject testing = Instantiate(obstacles[1]);
        testing.transform.SetParent(pobj.transform);
        //testing.transform.position += new Vector3(0, 0, 100f);

        pobj.transform.position = SpawnPosition;
        pobj.transform.rotation = Quaternion.identity;

        k = Instantiate(gb, SpawnPosition, Quaternion.identity);
        
        list.Add(k); //tiles
        obstacleList.Add(pobj); // spawnobject

        StartCoroutine(Levelgen()); //generate
        StartCoroutine(DestroyTiles()); //destroy offscreen
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Levelgen()
    {

        while (game)
        {

            yield return TileSpawnTImer;
            SpawnPosition += offset;            
            k=Instantiate(gb, SpawnPosition, Quaternion.identity); //tile
             

            pobj = Instantiate(parentobj);
            SpawnObstacles(pobj.transform.position);
             

            pobj.transform.position = SpawnPosition;
            pobj.transform.rotation = Quaternion.identity;
           // SpawnCoins(pobj.transform.position);

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
        float offset=30f;
        Vector3 spawnposition = parentPosition - new Vector3(0, 0, 100f);
        while (spawnposition.z<=(parentPosition.x+110f))
        {
            
            GameObject designBlock = Instantiate(obstacles[RandomIndex()]);
            designBlock.transform.SetParent(pobj.transform);
            designBlock.transform.position += spawnposition;
            spawnposition.z += offset;

        }
        return;
    }

    public IEnumerator DestroyTiles()
    {
        while (game)
        {

            yield return DestroyTileTimer;
            if(list.Count>0 && obstacleList.Count>0)
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

    void SpawnCoins(Vector3 parentPosition)
    {
        float z = parentPosition.z;
        float y = 0;
        float[] x = new float[] { -10, 0, 10 };
        float minRange = z - 100f;
        float maxrange = z + 100f;
        
        for(int i=(int)minRange;i<=(int)maxrange;i+=10)
        {
            for(int j=0;j<3;j++)
            {
                Vector3 possibleNode = new Vector3(x[j], y, i);
                bool freenode = Physics.CheckBox(possibleNode, new Vector3(4,4,4), Quaternion.identity, layermask);

                if (freenode)
                    continue;
                else
                {
                    GameObject CoinObject= Instantiate(Coins);
                    CoinObject.transform.SetParent(pobj.transform);
                    CoinObject.transform.position += possibleNode;
                }
                        
            }
        }    
       

    }    

    void SetTransform(GameObject gameObject)
    {
        

         
    }*/
}
