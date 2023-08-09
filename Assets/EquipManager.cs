using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{

    public GameData gameData;
    public EquipmentListManager equipList;
    public Equipment spawnNewEquip()
    {
        
        Equipment a1 = ScriptableObject.CreateInstance<Equipment>();
        a1.initEquipStats();

        gameData.equipWeaponList.Add(a1);
        equipList.refreshEquipView();
        return a1;
    }
}
