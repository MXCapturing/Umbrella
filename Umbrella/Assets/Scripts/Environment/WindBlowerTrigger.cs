using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlowerTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<WindBlower>().playerInWind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<WindBlower>().playerInWind = false;
        }
    }
}
