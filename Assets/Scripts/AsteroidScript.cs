using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update

    private int size;
    private long astHp;
    [FormerlySerializedAs("gameManager")] public AsteroidManager asteroidManager;
    public bool semaTriggered = false;
    public Rigidbody2D rb;
    
    // public GameData gameData;

    [SerializeField] private ParticleSystem destroyedParticles;
    void Start()
    {
        // initSpeed();
    }
    public void initAst(int size, Vector3 position, long astHp)
    {
        this.astHp = astHp;
        this.semaTriggered = false;
        this.size = size;
        transform.position = position;
            
        transform.localScale = 0.8f * size * Vector3.one;
        Vector2 initialDirection = new Vector2(Random.value, Random.value).normalized;
        float initialSpeed = Random.Range(4f, 5f)/size;
        rb.AddForce(initialDirection*initialSpeed, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (semaTriggered) return;
        semaTriggered = true;
            // if (collision.CompareTag("BulletTag")) // || collision.CompareTag("ShipTag") //asteroid dont get destroyed when we crash into it ok.
                if (collision.CompareTag("BulletTag"))
                {
                    var x= collision.gameObject.GetComponent<BulletScript>();
                    x.DestroyPoolBullet();


                    var dmgToTake = Math.Min(asteroidManager.gameData.bulletDmg, astHp);
                    astHp-=dmgToTake;
                    asteroidManager.gameData.thisStageTotalAstHpCurrent -= dmgToTake;
                    if (astHp <= 0) //break ast if at 0 hp
                    {
                        breakAst();
                    }
                }
    }

    private void breakAst()
    {
        if (size >= 2) //2 or higher will split
        {
            for (int i = 0; i < asteroidManager.gameData.astDupRate; i++)
            {
                // AsteroidScript newAst = Instantiate(this, transform.position, Quaternion.identity);
                asteroidManager.createPoolAst(size - 1, this.transform.position,asteroidManager.gameData.astStageMaxHp);
            }
        }
        asteroidManager.gameData.astDestroyed++;
        Instantiate(destroyedParticles, transform.position, Quaternion.identity);
        asteroidManager.destroyPoolAst(this.gameObject);
    }
    void Update()
    {
        semaTriggered = false;
    }
}
