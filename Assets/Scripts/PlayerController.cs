using System;
using System.IO;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    private float speed = 10f;
    public EnemyPlayer enemy;
    void FixedUpdate()
    {
        void Start()
        {
            
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dir.Normalize();
            rigidbody2D.velocity = dir*speed;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 normal = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            normal.Normalize();

            rigidbody2D.velocity *= -1;

        }

        Vector2 force = new Vector2(Input.GetAxis("Horizontal"), 0);
        rigidbody2D.AddForce(force * speed);

    }
}
