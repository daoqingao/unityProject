using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AsteroidManager astManager;
    public GameData gameData;
    public EquipManager equipManager;
    void Update()
    {
        if (gameData.dev)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                equipManager.spawnNewEquip();
            }
        }
        if (gameData.asteroidCount == 0) //all got destroyed
        {
            
            //get new gear every level i suuppose
            equipManager.spawnNewEquip();
            
            gameData.level++;
            gameData.calculateThisStageIndivAstMaxHp();
            gameData.calculateTotalStageAstHpMax(); //recalcuating the current level
            gameData.thisStageTotalAstHpCurrent = gameData.thisStageTotalAstHpMax; //resetting the hp.
            for (int i = 0; i < gameData.astToSpawnPerLevel; i++)
            {
                astManager.spawnAsteroid();
            }
        }
    }
}
