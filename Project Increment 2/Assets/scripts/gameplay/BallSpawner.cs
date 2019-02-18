using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField] 
    GameObject stdBall;

    //spawn control 
    /* const float MinSpawnDelay = 5;
    const float MaxSpawnDelay = 10;
    Timer spawner; */

    //spawn location 
    /*const int SpawnBorderSize = 100;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY; */

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        //Code to spawn a ball randomly
        /*minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;

        spawner = gameObject.AddComponent<Timer>();
        spawner.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        spawner.Run();*/
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        //Code to spawn balls randomly

        /*if(spawner.Finished)
        {
            SpawnBall();

            spawner.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            spawner.Run();

        }*/       
    }

    public void SpawnBall()
    {
        //Code to spawn balls randomly around the map

        /*Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX)
        , Random.Range(minSpawnY, maxSpawnY)
        , -Camera.main.transform.position.z);

        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
        GameObject standardBall = Instantiate(stdBall) as GameObject;
        standardBall.transform.position = worldLocation; */

        //Spawn another ball
        GameObject standardBall = Instantiate(stdBall) as GameObject;

    }
}
