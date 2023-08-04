using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D shipRigidbody2D;


    private float shipAccelSpeed = 20f;
    private float shipMaxVelSpeed = 20f;

    private bool isAlive = true;
    private bool isAccelerating = false;
    private Vector2 accelVector = new Vector2(0,0);


    private float bulletSpeed = 10f;
    public Transform bulletSpawner;
    
    [SerializeField]
    public Rigidbody2D bulletPrefab;



    
    void Start()
    {
        shipRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            handleGetKeyPress();
            handleShooting();
        }
    }

    void handleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.C))
        {
            Rigidbody2D bulletObj = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
            bulletObj.AddForce(bulletSpeed * shipRigidbody2D.transform.up, ForceMode2D.Impulse);
        }
    }

    void handleGetKeyPress()
    {
        float x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 :0 ; 
        float y = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 :0 ;
        
        if (x != 0 || y!=0)
        {
            accelVector.x = x;
            accelVector.y = y;
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
    }
    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            // Debug.Log(accelVector);
            shipRigidbody2D.AddForce(shipAccelSpeed * accelVector);
            shipRigidbody2D.velocity = Vector2.ClampMagnitude(shipRigidbody2D.velocity, shipMaxVelSpeed);
            shipRigidbody2D.transform.Rotate(new Vector3(accelVector.x,accelVector.y,0));

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, accelVector);
            shipRigidbody2D.transform.rotation = Quaternion.RotateTowards(
                shipRigidbody2D.transform.rotation, toRotation, 750f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AstTag"))
        {
            // Destroy(gameObject);
        }
    }
}
