using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    private float speed = 25f;
    public GameObject enemy;
    public Vector2 enemyStart;
    public Vector2 spacing;
	public static float distanceTraveled;

    private Rigidbody2D physicsSphere;

    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private enum BallState
    {
        BoostCharging,
        Boosting,
        Dropping,
        DropStick,
        TeleportCharging,
        Teleporting,
        Stunned,
        None,
    };
    private BallState state = BallState.None;

    public Vector2 lastAnalogueDir;

    void OnCollisionEnter2D(Collision2D coll)
    {

        //if(coll.gameObject.layer == "")
        
    }
    void Start()
    {
        physicsSphere = GetComponent<Rigidbody2D>();
    }

    // These handle state changes
    void Update()
    {
		distanceTraveled = transform.localPosition.x;

        switch (state)
        {
            // When the player is holding B in the air to charge the boost
            // Cancels if hitting the ground before letting go, 
            case BallState.BoostCharging:

                break;
            // 
            case BallState.Boosting:

                break;
            case BallState.Dropping:

                break;
            case BallState.DropStick:

                break;

            case BallState.TeleportCharging:

                break;
            case BallState.Teleporting:

                break;
        }
       
    }

    void GetAnalogueInput()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"),0 );
		physicsSphere.AddForce (speed*move);
    }

    void HandleHorizontalMovement()
    {
        physicsSphere.AddForce(new Vector2(Input.GetAxis("Horizontal")*speed, 0));
    }

    IEnumerator StunCoroutine(float stunTime)
    {
        float timeStunned = 0f;
        while (timeStunned < stunTime)
        {
            
        }


        yield return null;
    }

  

    void FixedUpdate()
    {
		GetAnalogueInput ();
        //OnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).


    }
}
