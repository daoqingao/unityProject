using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject
{

    public long damage;
    public long type;
    public long fireRate;
    public long projSpeed;
    
    void spawnNewEquip()
    {

    }
    
    public class Weapon : Equipment
    {
        
    }
    
    public class Armor : Equipment
    {
        
    }

}