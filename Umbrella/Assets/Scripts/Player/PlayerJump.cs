using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //Jump
    public float jumpVelocity;
    public float groundedSkin = 0.05f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplayer = 2f;
    public float defaultGrav = 3f;
    public float maxVelocityDown = -20f;
    public float boxOffset;
    public static float t = 0.0f;
    public float currentVelocityDown;
    public float groundedRemember;
    public float groundedRememberTime = 0.5f;

    //Ground Check
    public LayerMask layerMask;
    public Transform footPos;

    //Player states
    bool jumpRequest;
    public bool isGrounded;
    bool hasJumped;

    Vector2 playerSize;
    public Vector2 boxSize;

    public Rigidbody2D _rb;

    public static PlayerJump instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x - 0.03f, groundedSkin);
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGravity();
        PlayerMovement.instance.canMove = true;
        PlayerNormal();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocityDown = _rb.velocity.y;
        groundedRemember -= Time.deltaTime;
        if (isGrounded)
        {
            groundedRemember = groundedRememberTime;
            hasJumped = false;
        }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || groundedRemember > 0) && !jumpRequest)
        {
            groundedRemember = 0;
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            ResetGravity();
            Jump();
            jumpRequest = false;
        }
        else
        {
            isGrounded = Physics2D.OverlapBox(footPos.position, boxSize, 0f, layerMask) != null;
        }
        maxVelocityDown = -20f;

        if(_rb.velocity.y < 0 && !Umbrella.instance.umbrellaOpen)
        {
            _rb.gravityScale = fallMultiplier;
        }
        else if(_rb.velocity.y > 0 && !Umbrella.instance.umbrellaOpen && (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space)))
        {
            _rb.gravityScale = lowJumpMultiplayer;
        }
        else
        {
            if (!Umbrella.instance.umbrellaOpen)
            {
                _rb.gravityScale = defaultGrav;
            }
        }

        if(_rb.velocity.y < maxVelocityDown && !Umbrella.instance.umbrellaOpen)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, maxVelocityDown);
        }
    }

    void Jump()
    {
        groundedRemember = 0;
        _rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        isGrounded = false;
        Umbrella.instance.umbrellaReady = true;
        PlayerMovement.instance.canMove = true;
        hasJumped = true;
        t = 0;
    }

    public void ResetGravity()
    {
        PlayerMovement.instance.canMove = true;
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = defaultGrav;
        t = 0f;
    }

    public void FreezePos()
    {
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0;
        PlayerMovement.instance.canMove = false;
        hasJumped = false;
    }

    public void PlayerNormal()
    {
        PlayerMovement.instance.canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(footPos.transform.position, new Vector3(playerSize.x - 0.03f, groundedSkin, 1));
    }
}