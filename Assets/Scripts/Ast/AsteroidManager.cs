using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class AsteroidManager : MonoBehaviour
{
    public AsteroidScript astScript;
    public ObjectPool<AsteroidScript> astPool;
    public GameData gameData;
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
    }
    public void spawnAsteroid()
    {
        float offX = Random.Range(-1f, 1f); //
        float offY = Random.Range(-1f, 1f);      
        Vector3 spawnPos = new Vector3(gameData.astSpawnLoc.x+offX,gameData.astSpawnLoc.y+offY,0f);
        createPoolAst(gameData.astInitSize,spawnPos,gameData.calculateThisStageIndivAstMaxHp());
    }


    public void destroyPoolAst(GameObject ast)
    {
        astPool.Release(ast.GetComponent<AsteroidScript>());
        gameData.asteroidCount--;
    }
    public void destroyPoolAst(AsteroidScript ast)
    {
        astPool.Release(ast);
        gameData.asteroidCount--;
    }

    public AsteroidScript createPoolAst(int size,Vector2 pos, long astHp)
    {
        AsteroidScript astScript = astPool.Get();
        gameData.asteroidCount++;
        astScript.initAst(size,pos, astHp);
        return astScript;
    }
}
