using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update

    // private float bulletLifeTime = 3f;
    public ShipManager shipManager;
    public Rigidbody2D rb;
    void Start()
    {
         // Destroy(gameObject,bulletLifeTime);   
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DestroyPoolBullet()
    {
        shipManager.DestroyPoolBullet(this);
    }
}
