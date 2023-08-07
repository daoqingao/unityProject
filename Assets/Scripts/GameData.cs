using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameData : MonoBehaviour
{
    //relates to all user upgrade and stuff i suppose
    
    // Start is called before the first frame update
    
    public int level = 0;
    public int score = 0;
    
    //ast related
    public int astInitSize = 3;
    public Vector2 astSpawnLoc = new Vector2(200, 0);
    public int astDupRate = 2; //split into 2 on destruction

    
    //ship related
    public int shipLives = 3;
    public float shipAccelSpeed = 20f;
    public float shipMaxVelSpeed = 20f;
    public float bulletSpeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
