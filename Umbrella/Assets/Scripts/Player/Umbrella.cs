using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    public static Umbrella instance;

    Rigidbody2D _rb;

    public bool umbrellaReady;
    public bool umbrellaOpen;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && !PlayerJump.instance.isGrounded && !umbrellaOpen && umbrellaReady)
        {
            _rb.gravityScale = 0.5f;
            umbrellaOpen = true;
        }

        if (PlayerJump.instance.isGrounded && umbrellaOpen)
        {
            umbrellaOpen = false;
            PlayerJump.instance.ResetGravity();
        }
    }
}
