using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AsteroidManager astManager;
    public GameData gameData;
    
    void Update()
    {
        if (gameData.asteroidCount == 0) //all got destroyed
        {
            Debug.Log(gameData.thisStageTotalAstHpMax);
            Debug.Log(gameData.thisStageTotalAstHpCurrent);
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
