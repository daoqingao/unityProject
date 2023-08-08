using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update

    // private float bulletLifeTime = 3f;
    public ShipManager shipManager;
    public Rigidbody2D rb;
    public ObjectPool<BulletScript> bulletPool;

    void Start()
    {
         // Destroy(gameObject,bulletLifeTime);   
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StopCoroutine(destroyPoolBulletCountDown());
        StartCoroutine(destroyPoolBulletCountDown());
        // throw new NotImplementedException();
    }
    public IEnumerator destroyPoolBulletCountDown()
    {
        yield return new WaitForSeconds(1.5f);
        DestroyPoolBullet();
    }

    public void DestroyPoolBullet()
    {
        StopCoroutine(destroyPoolBulletCountDown());
        bulletPool.Release(this);
    }
}
