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
        if (gameData.asteroidCount == 0) //all got destroyed
        {
            
            equipManager.spawnNewEquip();
            
            
            
            gameData.level++;
            if (gameData.level >= 1) //from level 1 and onwards, beating the level means...
            {
            }
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
