using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
//relates to all user upgrade and stuff i suppose
public class GameData : MonoBehaviour
{
    
    
    //level related
    public int level = 0;
    [FormerlySerializedAs("score")] public int astDestroyed = 0;
    public int asteroidCount = 0;

    public int astToSpawnPerLevel = 4;
    public float thisStageTotalAstHpMax = 1;
    public float thisStageTotalAstHpCurrent = 1;
    
    //individual ast related
    public int astInitSize = 3;
    public Vector2 astSpawnLoc = new Vector2(200, 0);
    public int astDupRate = 2; //split into 2 on destruction
    [FormerlySerializedAs("astHp")] public long astStageMaxHp = 3;
    
    

    
    //ship related
    public int shipLives = 3;
    public float shipAccelSpeed = 20f;
    public float shipMaxVelSpeed = 20f;
    
    //bullet related
    public float bulletSpeed = 10f;
    public int bulletDmg = 2;
    
    void Start()
    {
        astToSpawnPerLevel = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //###################calculating the values for below. all values should be access via getters for calculation
    //calculate the asthp for the level.
    public long calculateThisStageIndivAstMaxHp()
    {
        this.astStageMaxHp = this.level * 2;
        return astStageMaxHp;
    }


    //what dictates the total asteroids hp of the level....
    public void calculateTotalStageAstHpMax()
    {
        float sum = 0;
        float numberOfAstWithChild = astToSpawnPerLevel * ((float)Math.Pow(astDupRate, astInitSize) - 1);
        float totalHpMax = numberOfAstWithChild * calculateThisStageIndivAstMaxHp();
        this.thisStageTotalAstHpMax = totalHpMax;
    }
    
    //might not be needed
    // public float getThisStageTotalAstHpCurrent()
    // {
    //     return totalStageAstHpCurrent;
    // }
    //
    // //might not be needed...
    // public float getthisStageTotalAstHpMax()
    // {
    //     return totalStageAstHpMax;
    // }
}
