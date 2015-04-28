using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private float speed = 1000f;

    public float min = 2f;
    public float max = 3f;
    private Rigidbody2D rd;
    public Rigidbody2D playerRd;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        min = transform.position.x;
        max = transform.position.x + 3;

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 force = new Vector2(100, 0);
        Renderer center = GetComponent<Renderer>();
        Vector2 newForce = new Vector2(center.bounds.center.x, center.bounds.center.y);

        Vector2 dir = newForce - col.contacts[0].point;

        if (col.gameObject.tag == "Player")
        {

            playerRd.AddForce(dir * speed);
            playerRd.GetComponent<PlayerController>().Stun();
        }
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        
    }
}