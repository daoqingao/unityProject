using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EquipmentListManager : MonoBehaviour
{
    
    public enum EquipType
    {
        WEAPON,
        ARMOR,
    }
    
    public GameData gameData;
    public ItemEquipFab itemEquipFab;
    
    public EquipType selectedEquipType = EquipType.WEAPON; // we can show weapons initally first
    public List<Equipment> currentDisplayingEquipList=null; 

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
            refreshAllEquipText(); //i dont even know man, how else am i suppose to use these stupid ass buttons
    }

    public void refreshAllEquipText()
    {
        foreach (Equipment equip in currentDisplayingEquipList)
        {
            ItemEquipFab equipFab = Instantiate(itemEquipFab, transform);
            equipFab.initItem(equip,selectedEquipType);
            equipFab.equipListManager = this;
        }       
    }

    public void handleDisplayWeaponOrArmor(string type)
    {
        selectedEquipType = type=="weapon"?EquipType.WEAPON:EquipType.ARMOR;
        refreshEquipView();
    }

    public void handleSelectWeapon(Equipment equip,EquipType type)
    {
        if (type == EquipType.WEAPON)
        {
            gameData.equpiedWeapon = equip;
        }
        else
        {
            gameData.equpiedArmor = equip;
        }
        gameData.calculateEquipmentValues();
    }
}
