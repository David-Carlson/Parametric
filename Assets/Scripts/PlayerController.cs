using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    private float speed = 10f;
    public GameObject enemy;
    public Vector2 enemyStart;
    public Vector2 spacing;

    void ReflectVelocity()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if(coll.gameObject.layer == "")
        
    }
    void Start()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                //Instantiate(enemy, enemyStart + new Vector2(spacing.x*x, spacing.y*y), Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dir.Normalize();
            GetComponent<Rigidbody2D>().velocity = dir*speed;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 normal = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            normal.Normalize();

            GetComponent<Rigidbody2D>().velocity *= -1;

        }

        Vector2 force = new Vector2(Input.GetAxis("Horizontal"), 0);
        GetComponent<Rigidbody2D>().AddForce(force * speed);

    }
}
