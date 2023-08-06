using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update

    private int size;
    public GameManager gameManager;
    private int duplicationRate = 2; //split into 2 on destruction
    public bool semaTriggered = false;
    public Rigidbody2D rb;

    [SerializeField] private ParticleSystem destroyedParticles;
    void Start()
    {
        // initSpeed();
    }
    public void initAst(int size, Vector3 position)
    {
        // rb = GetComponent<Rigidbody2D>();
        this.semaTriggered = false;
        this.size = size;
        transform.position = position;
        
        // rb = gameObject.GetComponent<Rigidbody2D>(); //apparently disabling means the rigidbody would no longer be the same, so we have to reassign everytime
        transform.localScale = 0.8f * size * Vector3.one;
        Vector2 initialDirection = new Vector2(Random.value, Random.value).normalized;
        float initialSpeed = Random.Range(4f, 5f)/size;
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
            if (collision.CompareTag("BulletTag") || collision.CompareTag("ShipTag"))
            {
                if (collision.CompareTag("BulletTag"))
                {
                    var x= collision.gameObject.GetComponent<BulletScript>();
                    x.DestroyPoolBullet();
                }
                if (size > 1)
                {
                    for (int i = 0; i < duplicationRate; i++)
                    {
                        // AsteroidScript newAst = Instantiate(this, transform.position, Quaternion.identity);
                        gameManager.createPoolAst(size - 1, this.transform.position);
                    }
                }
                gameManager.score++;
                // Instantiate(destroyedParticles, transform.position, Quaternion.identity);
                gameManager.destroyPoolAst(this.gameObject);
            }
    }


    // Update is called once per frame
    void Update()
    {
        semaTriggered = false;
    }
}
