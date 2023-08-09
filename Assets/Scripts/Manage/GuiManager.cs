using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{

    public Image astHpBar;
    public GameData gameData;
    public TMP_Text guiText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // astHpBar.fillAmount = gameData.getThisStageTotalAstHpCurrent() / gameData.getThisStageTotalAstHpMax();
        astHpBar.fillAmount = gameData.thisStageTotalAstHpCurrent / gameData.thisStageTotalAstHpMax;
        guiText.text = " Score: " + gameData.astDestroyed +
                       "\n Level: " + gameData.level +
                       "\n Lives: " + gameData.shipLives +
                       "\n # Left: " + gameData.asteroidCount +
        "\n cur hp: " + gameData.thisStageTotalAstHpCurrent +
            "\n max hp: " + gameData.thisStageTotalAstHpMax +
                       "\n bullet dmg: " + gameData.bulletDmg

                       ;
    }
}
