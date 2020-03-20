using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindDirection { Up, Down, Left, Right}

public class WindBlower : MonoBehaviour
{
    public WindDirection windDir;
    public float distance;
    public float windStrength;

    public bool playerInWind;

    public BoxCollider2D windFunnel;

    Rigidbody2D _rb;

    private void Start()
    {
        _rb = PlayerJump.instance._rb;
        if(windDir == WindDirection.Up)
        {
            windFunnel.size = new Vector2(2, distance);
            windFunnel.offset = new Vector2(0, (distance / 2) + 0.5f);
        }
        else if(windDir == WindDirection.Down)
        {
            windFunnel.size = new Vector2(2, distance);
            windFunnel.offset = new Vector2(0, ((distance / 2) + 0.5f) * -1);
        }
        else if(windDir == WindDirection.Left)
        {
            windFunnel.size = new Vector2(distance, 2);
            windFunnel.offset = new Vector2(((distance / 2) + 0.5f) * -1, 0);
        }
        else if(windDir == WindDirection.Right)
        {
            windFunnel.size = new Vector2(distance, 2);
            windFunnel.offset = new Vector2(((distance / 2) + 0.5f), 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerInWind && Umbrella.instance.umbrellaOpen)
        {
            switch (windDir)
            {
                case WindDirection.Up:
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + windStrength * Time.deltaTime);
                    break;

                case WindDirection.Down:
                    _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - windStrength * Time.deltaTime);
                    break;

                case WindDirection.Left:
                    _rb.velocity = new Vector2(_rb.velocity.x - windStrength * Time.deltaTime, _rb.velocity.y);
                    break;

                case WindDirection.Right:
                    _rb.velocity = new Vector2(_rb.velocity.x + windStrength * Time.deltaTime, _rb.velocity.y);
                    break;
            }
        }
    }
}
