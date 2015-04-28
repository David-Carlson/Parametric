using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 10f;
    public float JumpSpeed = 10f;

    private Rigidbody2D physicsSphere;

    public Transform groundCheck;
    public float radius;
    public bool Grounded = false;
    public LayerMask groundLayer;

    public int MaxTeleports = 3;
    public float AnalogueResetAmt = 0.5f;
    private bool AnalogueReset = true;


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
    public List<Vector2> analogueDirRequests = new List<Vector2>();

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if(coll.gameObject.layer == "")
        
    }
    void Start()
    {
        physicsSphere = GetComponent<Rigidbody2D>();
    }



    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    // These handle state changes
    void Update()
    {
        switch (state)
        {
            // When the player is holding B in the air to charge the boost
            // Cancels if hitting the ground before letting go, 
            case BallState.BoostCharging:
                if (Grounded)
                {
                    state = BallState.Stunned;
                }
                    
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
            case BallState.None:
                
                HandleHorizontalMovement(speed);
                if (Grounded)
                {
                    if (Input.GetButtonDown("Down Move"))
                    {
                        physicsSphere.velocity = new Vector2(physicsSphere.velocity.x, JumpSpeed);
                        break;
                    }

                    if (Input.GetButton("Right Move"))
                    {
                        Console.WriteLine("Right boost");
                        HandleHorizontalMovement(speed * 2);
                    }

                    if (Input.GetButtonDown("Up Move"))
                    {
                        Console.WriteLine("Tele move");
                        state = BallState.TeleportCharging;
                        analogueDirRequests.Clear();
                        SetLastAnalogueDir();
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Down Move"))
                    {
                        Console.WriteLine("Attempng jump in air");
                        state = BallState.Dropping;
                        break;
                    }

                    if (Input.GetButtonDown("Right Move"))
                    {
                        state = BallState.BoostCharging;
                        break;
                    }

                    if (Input.GetButtonDown("Up Move"))
                    {
                        state = BallState.TeleportCharging;
                        analogueDirRequests.Clear();
                        SetLastAnalogueDir();
                    }
                    
                }
                break;
        }
       
    }

    void SetLastAnalogueDir()
    {
        Vector2 currDir = GetAnalogueInput();
        if (AnalogueReset == false && currDir.SqrMagnitude() <= 0.9f)
            AnalogueReset = true;

        if(AnalogueReset == true)
        if (state == BallState.TeleportCharging)
        {
            currDir.Normalize();
            analogueDirRequests.Add(currDir);
            AnalogueReset = false;
        }
        else
        {
            currDir.Normalize();
            analogueDirRequests.Clear();
            analogueDirRequests.Add(currDir);
            AnalogueReset = false;
        }
    }

    Vector2 GetAnalogueInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void HandleHorizontalMovement(float magnitude)
    {
        physicsSphere.AddForce(new Vector2(Input.GetAxis("Horizontal")*magnitude, 0));
    }

    IEnumerator StunCoroutine(float stunTime)
    {
        float timeStunned = 0f;
        while (timeStunned < stunTime)
        {
            
        }


        yield return null;
    }
}
