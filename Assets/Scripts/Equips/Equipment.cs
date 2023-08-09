using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class Equipment : ScriptableObject
{

    public enum FiringType
    {
        NORMAL,
        CIRCULAR,
        SHOTGUN
    }
    public long damage;
     public FiringType firingType;
    public long projSpeed;
    public abstract void initEquipStats(); //this class was supposed to be abstract but im too lazy

    public string ToString() { return "Dmg: " + damage +
                                      "proj Speed" + projSpeed +
                                      "Type: "+ firingType; }
    public class Weapon : Equipment
    {
        public long fireRate;
        override 
        public void initEquipStats()
        {
            damage = (long)Random.Range(1f, 10f);     
            firingType = FiringType.NORMAL;
            projSpeed = 1;
            fireRate = 1;
        } 
    }
    
    public class Armor : Equipment
    {
        public long health;
        override public void initEquipStats() //do your rolls 
        {
            damage = 1;
            firingType = FiringType.CIRCULAR;
            projSpeed = 1;
            health = 1;
        } 
    }
}