using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject
{

    public long damage;
    public string type;
    public long fireRate;
    public long projSpeed;
    public long health;
    public void initEquipStats()
    {
        //we are suppose to be doing some rolling here
        damage = 1;
        type = "bruh idk";
        fireRate = 1;
        projSpeed = 1;
        health = 1;
    }
    
    public class Weapon : Equipment
    {
        
    }
    
    public class Armor : Equipment
    {
        
    }

}