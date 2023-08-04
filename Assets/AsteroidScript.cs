using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update


    private int size = 5;
    public GameManager gameManager;
    private int duplicationRate = 3; //split into 2 on destruction

    private bool semaTriggered = false;

    [SerializeField] private ParticleSystem destroyedParticles;
    void Start()
    {
        initAstSpeed();
        gameManager.asteroidCount++;
    }

    private void initAstSpeed()
    {
        transform.localScale = 0.5f * size * Vector3.one;
        Vector2 initialDirection = new Vector2(Random.value, Random.value).normalized;
        float initialSpeed = Random.Range(4f - size, 5f - size);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDirection*initialSpeed, ForceMode2D.Impulse);
    }
    
    //there exist a bug where if 2 bullets trigger at the same time
    //(too many bullets into a asteroid means we might not hit Destroy adn it would rbeak things.)
    //just know that this function can be run multiple async times and reach a race condition
    //fixed by makign the function async...
    //i really dont know how this work
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (semaTriggered) return;
        semaTriggered = true;
            if (collision.CompareTag("BulletTag"))
            {
                Destroy(collision.gameObject);
                if (size > 1)
                {
                    for (int i = 0; i < duplicationRate; i++)
                    {
                        AsteroidScript newAst = Instantiate(this, transform.position, Quaternion.identity);
                        newAst.size = size - 1;
                        newAst.gameManager = gameManager;
                    }
                }

                Instantiate(destroyedParticles, transform.position, Quaternion.identity);
                gameManager.asteroidCount--;
                Destroy(gameObject);
            }
    }


    // Update is called once per frame
    void Update()
    {
        semaTriggered = false;
    }
}
