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
    public LayerMask enemyLayer;

    public int MaxTeleports = 3;
    public float AnalogueResetAmt = 0.5f;
    private bool AnalogueIsReset = true;

    public static float distanceTraveled;

    private Renderer renderer;

    public enum BallState
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
    public BallState state = BallState.None;

    // First is used to remember the last direction fully pressed down
    // Used for charging shots, doing drop shots
    public List<Vector2> analogueDirRequests = new List<Vector2>();

    void Start()
    {
        physicsSphere = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.cyan;
    }

    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    void OnCollissionEnter(Collision coll)
    {
        if (coll.gameObject.layer == enemyLayer.value)
        {
            StartCoroutine(StunCoroutine(3f));
        }
    }

    // These handle state changes
    void Update()
    {
        distanceTraveled = transform.position.x;

        switch (state)
        {
            // When the player is holding B in the air to charge the boost
            // Cancels if hitting the ground before letting go, 
            case BallState.BoostCharging:
                SetLastAnalogueDir();
                if (Grounded)
                {
                    StartCoroutine(StunCoroutine(3f));
                    break;
                }
                if (Input.GetButtonUp("Right Move"))
                {
                    if (analogueDirRequests.Count == 0)
                    {
                        physicsSphere.velocity *= 2;
                        Debug.Log("No boost dir");
                    }
                    else
                    {
                        physicsSphere.velocity = analogueDirRequests[0]*4;
                        Debug.Log("Going to : " + analogueDirRequests[0]);
                    }
                        

                    state = BallState.Boosting;
                }
                    
                break;
            // 
            case BallState.Boosting:
                if (Grounded)
                    state = BallState.None;
                break;
            case BallState.Dropping:
                
                break;

            case BallState.DropStick:
                break;

            case BallState.TeleportCharging:

                break;
            case BallState.Teleporting:

                break;
            case BallState.Stunned:
                HandleHorizontalMovement(speed);
                break;
            case BallState.None:
                
                HandleHorizontalMovement(speed);
                if (Grounded)
                {
                    if (Input.GetButtonDown("Down Move"))
                    {
                        Debug.Log("Ground jump");
                        physicsSphere.velocity = new Vector2(physicsSphere.velocity.x, JumpSpeed);
                        break;
                    }

                    if (Input.GetButton("Right Move"))
                    {
                        Debug.Log("B on ground");
                        HandleHorizontalMovement(speed * 2);
                        break;
                    }

                    if (Input.GetButtonDown("Up Move"))
                    {
                        Console.WriteLine("Tele move");
                        //state = BallState.TeleportCharging;
                        analogueDirRequests.Clear();
                        SetLastAnalogueDir();
                        break;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Down Move"))
                    {
                        Console.WriteLine("Attempting jump in air");
                        //state = BallState.Dropping;
                        break;
                    }

                    if (Input.GetButtonDown("Right Move"))
                    {
                        state = BallState.BoostCharging;
                        analogueDirRequests.Clear();
                        break;
                    }
                    if (Input.GetButtonDown("Up Move"))
                    {
                        //state = BallState.TeleportCharging;
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
        if (AnalogueIsReset == false && currDir.SqrMagnitude() <= AnalogueResetAmt)
            AnalogueIsReset = true;

        if (AnalogueIsReset && currDir.SqrMagnitude() >= 0.9f)
        {
            if (state == BallState.TeleportCharging)
            {
                currDir.Normalize();
                analogueDirRequests.Add(currDir);
                AnalogueIsReset = false;
            }
            else
            {
                currDir.Normalize();
                analogueDirRequests.Clear();
                analogueDirRequests.Add(currDir);
                AnalogueIsReset = false;
                Debug.Log("Set to: " + currDir);
            }
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
        Debug.Log("Start stun");
        renderer.material.color = Color.red;
        state = BallState.Stunned;
        float timeStunned = 0f;
        while (timeStunned < stunTime)
        {
            timeStunned += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("End stun");
        state = BallState.None;
        renderer.material.color = Color.cyan;
    }


}
