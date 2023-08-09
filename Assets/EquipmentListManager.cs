using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentListManager : MonoBehaviour
{
    
    public enum EquipType
    {
        WEAPON,
        ARMOR,
    }
    
    public GameData gameData;
    public GameObject weaponButtonFab;
    // Start is called before the first frame update
    public EquipType selectedEquipType = EquipType.WEAPON; // we can show weapons initally first
    public List<Equipment> currentDisplayingEquipList; 

    private void Start()
    {
        selectedEquipType = EquipType.WEAPON;
        refreshEquipView();
    }

    public void refreshEquipView()
    {
        //destroy all existing children
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        currentDisplayingEquipList = 
            selectedEquipType == 
            EquipType.WEAPON
            ? gameData.equipWeaponList
            : gameData.equipArmorList;
        foreach (Equipment equip in currentDisplayingEquipList)
        {
            //implement the logics for equiping and stuff\
            GameObject currentEquip = Instantiate(weaponButtonFab, transform);
            TMP_Text  text = currentEquip.GetComponentInChildren<TMP_Text >();
            text.text = "some sort of weapon description";
        }        
        
    }
    public void addEquipToList(Equipment equip)
    {
    }

    public void handleSelectWeaponOrArmorViewClick(string type)
    {
        selectedEquipType = type=="weapon"?EquipType.WEAPON:EquipType.ARMOR;
        refreshEquipView();
    }
}
