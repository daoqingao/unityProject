using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int asteroidCount = 0;
    public int level = 0;

    [SerializeField] //this only works because we are referencing to the asteroid script that exist in that gameobject....
    public AsteroidScript astScript;
    void Start()
    {
        // astScript = new AsteroidScript();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (asteroidCount == 0)
        {
            level++;
            int numAsteroids = 2 + (2 * level);
            for (int i = 0; i < numAsteroids; i++)
            {
                spawnAsteroid();
            }
        }
    }

    void spawnAsteroid()
    {
        float offset = Random.Range(0f, 1f); //
        Vector2 viewportPos = Vector2.zero;
        int edge = Random.Range(0, 4);
        viewportPos = new Vector2(offset, 0);

        Vector2 spawnPos = Camera.main.ViewportToWorldPoint(viewportPos);
        AsteroidScript asteroidScript = Instantiate(astScript, spawnPos, Quaternion.identity);
        asteroidScript.gameManager = this;
    }

    // private IEnumerator spawnAst()
    // {
    //     
    // }
}
