using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
    private float speed = 10f;
    private Rigidbody2D physicsSphere;
    private bool OnGround = false;


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

    // First is used to remember the last direction fully pressed down
    // Used for charging shots, doing drop shots
    public Vector2 firstAnalogueDir = new Vector2();
    public Vector2 secondAnalogueDir = new Vector2();

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
        switch (state)
        {
            // When the player is holding B in the air to charge the boost
            // Cancels if hitting the ground before letting go, 
            case BallState.BoostCharging:
                if (OnGround)
                {
                    state = BallState.Stunned;

                }
                    
                break;
            // 
            case BallState.Boosting:

                break;
            case BallState.Dropping:
                if(this.g)
                break;

            case BallState.DropStick:

                break;

            case BallState.TeleportCharging:

                break;
            case BallState.Teleporting:

                break;
        }
       
    }

    Vector2 GetAnalogueInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        //OnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer).


    }
}
