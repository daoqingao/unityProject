using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class AsteroidManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int asteroidCount = 0;


    [SerializeField] //this only works because we are referencing to the asteroid script that exist in that gameobject....
    public AsteroidScript astScript;
    public TMP_Text guiText;

    public ObjectPool<AsteroidScript> astPool;
    [SerializeField]
    public GameData gameData;
    
    // Update is called once per frame
    void Start()
    {
        astPool = new ObjectPool<AsteroidScript>(
            () =>
            {
                AsteroidScript asteroidScript = Instantiate(astScript);
                asteroidScript.asteroidManager = this;
                return asteroidScript;
            } //what to do on instantiate
            , ast =>
            {
                ast.gameObject.SetActive(true);
                ast.rb = ast.GetComponent<Rigidbody2D>();
            },
            ast =>
            {
                ast.gameObject.SetActive(false);
            }, ast =>
            {
                Destroy(ast.gameObject);
            }, false, 1000, 1000);
    }
    void Update()
    {
        if (asteroidCount == 0)
        {
            gameData.level++;
            int numAsteroids = 2 + (2 * gameData.level);
            for (int i = 0; i < numAsteroids; i++)
            {
                spawnAsteroid();
            }
        }

        guiText.text = " Score: " + gameData.score +
                       "\n Level: " + gameData.level +
                       "\n Lives: " + gameData.shipLives +
                       "\n # Left: " + asteroidCount;
    }
    void spawnAsteroid()
    {
        float offX = Random.Range(-1f, 1f); //
        float offY = Random.Range(-1f, 1f);
        Vector3 spawnPos = new Vector3(gameData.astSpawnLoc.x+offX,gameData.astSpawnLoc.y+offY,0f);
        createPoolAst(gameData.astInitSize,spawnPos);
    }


    public void destroyPoolAst(GameObject ast)
    {
        astPool.Release(ast.GetComponent<AsteroidScript>());
        asteroidCount--;
    }
    public void destroyPoolAst(AsteroidScript ast)
    {
        astPool.Release(ast);
        asteroidCount--;
    }

    public AsteroidScript createPoolAst(int size,Vector2 pos)
    {
        AsteroidScript astScript = astPool.Get();
        asteroidCount++;
        astScript.initAst(size,pos);
        return astScript;
    }
}
