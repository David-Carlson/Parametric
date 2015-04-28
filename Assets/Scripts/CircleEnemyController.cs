using System;
using UnityEngine;
using System.Collections;

public class CircleEnemyController : MonoBehaviour
{
    private float speed = 1000f;

    public Vector3 center;
    public float radius;
    public float radianOffset;
    public float angle;
    public float radiansPerSecond;

    //public Rigidbody2D playerRd;

    // Use this for initialization
    void Start()
    {

    }

    public void Initialize(Vector3 center, float radius, float radianOffset, float radiansPersec)
    {
        this.center = center;
        this.radius = radius;
        this.radianOffset = radianOffset;
        this.radiansPerSecond = radiansPersec;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 force = new Vector2(100, 0);
        Renderer center = GetComponent<Renderer>();
        Vector2 newForce = new Vector2(center.bounds.center.x, center.bounds.center.y);

        Vector2 dir = newForce - col.contacts[0].point;

        if (col.gameObject.tag == "Player")
        {

            //playerRd.AddForce(dir * speed);
            //playerRd.GetComponent<PlayerController>().Stun();
        }
    }


    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime*radiansPerSecond;
        transform.position = new Vector3((float) Math.Cos(angle), (float) Math.Sin(angle), 0) + center;
    }
}
