using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(5.0f, 20.0f)]
    public float playerSpeed = 5.0f;

    [Range(2.0f, 20.0f)]
    public float playerJump = 5.0f;

    [Range(.1f, 3.0f)]
    public float blastForceMultiplier = 1.25f;

    public LayerMask groundLM;

    Vector2 checkpointValue;

    int maxHealth = 3;
    int currentHealth;

    #region Input Values
    float xInput = 0.0f;
    int slide = 0;
    int jump = 0;
    int facing = 1; //1 for right, -1 for left
    bool moving = false;

    


    public float XInput
    {
        get
        {
            return xInput;
        }
        set
        {
            xInput = value;
        }
    }

    public int Slide
    {
        get
        {
            return slide;
        }
    }

    public int Jump
    {
        get
        {
            return jump;
        }
    }

    public int Facing
    {
        get
        {
            return facing;
        }

        set
        {
            facing = value;
            groundCheckPoint = new Vector3(-facing * coll.bounds.extents.x, -coll.bounds.extents.y, 0);
        }
    }

    public bool Moving
    {
        get
        {
            return moving;
        }
        set
        {
            moving = value;
        }
    }

    #endregion

    //components on the player
    BoxCollider2D coll;
    Rigidbody2D rb;

    //tracked private variables
    Vector3 moveDirection, groundCheckPoint, colliderOffset;
    Vector3 extraMoveForce = new Vector3();
    Vector3 depreciationParams = new Vector3();
    bool grounded;
    bool playerInputEnabled = true;
    bool canSlide = true;
    float jumpTimer = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3.0f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + groundCheckPoint, transform.position + groundCheckPoint + 0.1f * Vector3.down);
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        
        colliderOffset =  new Vector3(transform.localScale.x * coll.offset.x, transform.localScale.y * coll.offset.y, 0);

        currentHealth = maxHealth;

    }

    private void Update()
    {
        
        //groundchecks and resetting
        groundCheckPoint = new Vector3(-facing * coll.bounds.extents.x, -coll.bounds.extents.y, 0);
        grounded = Physics2D.Raycast(transform.position + groundCheckPoint, Vector2.down, 0.1f, groundLM);
        //Debug.Log("Grounded? " + grounded);
        //take input
        //raw axis so we dont have annoying accel/decel on left-right movement, which can cause some inaccuracy
        //moveDirection = Vector3.zero; //reset?
        if (playerInputEnabled)
        {
            xInput = Input.GetAxis("Horizontal");
            if(xInput > 0.0f)
            {
                Facing = 1;
            }
            else if(xInput < 0.0f)
            {
                Facing = -1;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jump = 1;
            }
            else
            {
                jump = 0;
            }

            
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 j = Vector3.zero;
        if(jump == 1 && grounded)
        {
            j.y = playerJump;
            moveDirection.y = extraMoveForce.y + j.y;
        }
        else
        {
            moveDirection.y = extraMoveForce.y + rb.velocity.y;
        }
        //Debug.Log("blast force: " + extraMoveForce);
        moveDirection.x = xInput * playerSpeed + extraMoveForce.x;
        
        rb.velocity = moveDirection;
        DepreciateExplosionForce();
    }


    public void ExplosionForce(Vector2 center)
    {
        float dist = Vector2.Distance(center, (Vector2)transform.position);
        if (dist <= 3.0f)
        {
            Debug.DrawLine(center, transform.position, Color.cyan);
            Vector3 force = (Vector3)((3.0f - dist) * blastForceMultiplier * ((Vector2)(transform.position) - center).normalized);
            extraMoveForce = force;
            Debug.Log("BLAST: " + extraMoveForce);
        }
    }

    public void SetCheckpoint(Vector2 pt)
    {
        checkpointValue = pt;
    }

    #region Health/Death

    public void OnDeath()
    {
        transform.position = checkpointValue;
    }
    #endregion

    void DepreciateExplosionForce()
    {
        //depreciate the explosive force?
        if (extraMoveForce != Vector3.zero)
        {
            if (Mathf.Abs(extraMoveForce.x - 0.0f) < 1.0f)
            {
                extraMoveForce.x = 0.0f;
            }
            else if (extraMoveForce.x > 0.0f)
            {
                extraMoveForce.x -= 9.81f * Time.fixedDeltaTime;
            }
            else if (extraMoveForce.x < 0.0f)
            {
                extraMoveForce.x += 9.81f * Time.fixedDeltaTime;
            }


            if (Mathf.Abs(extraMoveForce.y - 0.0f) < 1.0f)
            {
                extraMoveForce.y = 0.0f;
            }
            else if (extraMoveForce.y > 0.0f)
            {
                extraMoveForce.y -= 9.81f * Time.fixedDeltaTime;
            }
            else if (extraMoveForce.y < 0.0f)
            {
                extraMoveForce.y += 9.81f * Time.fixedDeltaTime;
            }

            extraMoveForce.z = 0.0f;
        }
    }


}
