using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
//relates to all user upgrade and stuff i suppose
public class GameData : MonoBehaviour
{
    
       
    //level related
    public int level = 1;
    public int astDestroyed = 0;
    public int asteroidCount = 0;

    public int astToSpawnPerLevel = 1;
    public float thisStageTotalAstHpMax; //generated values 
    public float thisStageTotalAstHpCurrent;
    
    //math and scaling related

    public float scalingAstMaxHp = 1.1f;
    
    
    //individual ast related
    public int astInitSize = 3;
    public Vector2 astSpawnLoc = new Vector2(200, 0);
    public int astDupRate = 2; //split into 2 on destruction
    public long astStageMaxHp = -1;
    
    //ship related
    public int shipLives = 3;
    public float shipAccelSpeed = 20f;
    public float shipMaxVelSpeed = 20f;
    
    //bullet related
    public float bulletSpeed = 10f;
    public int bulletDmg = 1;

    //debug related.
    public bool astNoMove;

    public bool dev=false;
    //equipment related.

    public List<Equipment> equipWeaponList=null;
    public List<Equipment> equipArmorList=null;

    public Equipment equpiedWeapon=null;
    public Equipment equpiedArmor =null;


    void Start()
    {
        
        //well we are suppose to load in the data, probably somewhere here
        equipWeaponList = new List<Equipment>();
        equipArmorList = new List<Equipment>();
        level = 1;
        //
        
        dev = true;
        astNoMove = false;
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
        this.astStageMaxHp = (long)Math.Pow(this.level,scalingAstMaxHp);
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
}
