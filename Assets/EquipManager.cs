using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{

    public GameData gameData;
    public EquipmentListManager equipList;
    public Equipment spawnNewEquip()
    {
        
        Equipment.Weapon a1 = ScriptableObject.CreateInstance<Equipment.Weapon>();
        a1.initEquipStats();
        
        Equipment.Armor a2 = ScriptableObject.CreateInstance<Equipment.Armor>();
        a2.initEquipStats();
        
        gameData.equipWeaponList.Add(a1);
        gameData.equipArmorList.Add(a2);
        equipList.refreshEquipView();
        return a1;
    }
}
