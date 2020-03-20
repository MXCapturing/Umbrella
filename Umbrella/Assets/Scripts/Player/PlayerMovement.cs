using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveInput;
    private float lastMoveInput;
    public float currentMoveSpeed;
    public float moveSpeedMax;
    public float timeZeroToMax;
    public float timeMaxToZero;
    float decelRatePerSec;
    float accelRatePerSec;

    public bool canMove;

    Rigidbody2D _rb;

    public static PlayerMovement instance = null;

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
        accelRatePerSec = moveSpeedMax / timeZeroToMax;
        decelRatePerSec = -moveSpeedMax / timeMaxToZero;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleMove();
        }
    }

    public void HandleMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput != 0)
        {
            currentMoveSpeed += accelRatePerSec * Time.deltaTime;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, moveSpeedMax);
            _rb.velocity = new Vector2(moveInput * currentMoveSpeed, _rb.velocity.y);
            lastMoveInput = moveInput;
        }
        else if(moveInput == 0)
        {
            currentMoveSpeed += decelRatePerSec * Time.deltaTime;
            currentMoveSpeed = Mathf.Max(currentMoveSpeed, 0);
            _rb.velocity = new Vector2(lastMoveInput * currentMoveSpeed, _rb.velocity.y);
        }
    }
}
