using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ShipManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D shipRigidbody2D;
    private bool isAlive = true;
    private bool isAccelerating = false;
    private Vector2 accelVector = new Vector2(0,0);
    public Transform bulletSpawner;
    
    [SerializeField]
    public BulletScript bulletPrefab;
    [SerializeField]
    public GameData gameData;
    
    public bool semaTriggered = false;

    [FormerlySerializedAs("gameManager")] public AsteroidManager asteroidManager;

    //all for the sake of iframes
    private SpriteRenderer spriteRenderer;
    public Material flashMaterial;
    private Material originalMaterial;
    public Collider2D shipCollider;
    
    [SerializeField] private ParticleSystem destroyedParticles;

    // public ObjectPool<Rigidbody2D> bulletPool;
    public ObjectPool<BulletScript> bulletPool;
    void Start()
    {
        // ParticleSystem.MainModule main = destroyedParticles.main;
        // main.startColor = Color.cyan; // <- or whatever color you want to assign
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        shipRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        shipCollider = GetComponent<Collider2D>();
        
        bulletPool = new ObjectPool<BulletScript>(
            () =>
            {
                BulletScript bullet = Instantiate(bulletPrefab);
                bullet.shipManager = this;
                bullet.gameObject.SetActive(true);
                bullet.bulletPool = bulletPool;
                return bullet;
            } //what to do on instantiate
            , bullet =>
            {
                bullet.gameObject.SetActive(true);
            },
            bullet =>
            {
                bullet.gameObject.SetActive(false);
            }, bullet =>
            {
                // bullet.enabled = false;
                // bullet.gameObject.SetActive(false);
                //i am actually not sure how to fix this.
                //sometimes the bullets are destroyed multiple times i suppose
                // Debug.Log("called to be destroyed");
                Destroy(bullet.gameObject);
            }, false, 1000, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            semaTriggered = false;
            handleGetKeyPress();
            handleShooting();
        }
    }

    void handleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            regularAttack();
        }
        if (Input.GetKey(KeyCode.C))
        {
            coneAttack();
        }
        // coneAttack();
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
            shipRigidbody2D.AddForce(gameData.shipAccelSpeed * accelVector);
            shipRigidbody2D.velocity = Vector2.ClampMagnitude(shipRigidbody2D.velocity, gameData.shipMaxVelSpeed);
            shipRigidbody2D.transform.Rotate(new Vector3(accelVector.x,accelVector.y,0));
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, accelVector);
            shipRigidbody2D.transform.rotation = Quaternion.RotateTowards(
                shipRigidbody2D.transform.rotation, toRotation, 750f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (semaTriggered) return;
            semaTriggered = true;
        if (other.CompareTag("AstTag"))
        {
            Debug.Log("got hit by ast");
            gameData.shipLives--;
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            if (gameData.shipLives < 0)
            {
                Debug.Log("devestated hit by ast");
                Destroy(gameObject);
            }
            else
            {
                coneAttack();
                StartCoroutine(shipIFrame(2));
            }
        }
    }

    void regularAttack()
    {
        BulletScript bullet = createPoolBullet();
        bullet.rb.AddForce(gameData.bulletSpeed * shipRigidbody2D.transform.up, ForceMode2D.Impulse);
    }

    BulletScript createPoolBullet()
    {
        BulletScript bullet = bulletPool.Get();
        bullet.transform.position = bulletSpawner.position;
        return bullet;
    }

    void coneAttack()
    {
        for (var i = 0; i < 360; i=i+30)
        {
            Vector2 dir = (Vector2)(Quaternion.Euler(0,0,i) * Vector2.right);
            BulletScript bulletObj = createPoolBullet();
            bulletObj.rb.AddForce(gameData.bulletSpeed * dir, ForceMode2D.Impulse);
        }
    }

    IEnumerator shipIFrame(float seconds)
    {
        shipCollider.enabled = false;
        float div = 10; //amount of flashes in those seconds
        seconds /= 2; // bruh
        for (var i = 0; i < div; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(seconds/div);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(seconds/div);
        }
            shipCollider.enabled = true;

    }
}
