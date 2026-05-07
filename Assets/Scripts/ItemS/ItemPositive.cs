using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPositive : Item
{
    const float POSITIVE_HEAL = 20;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
           Recolected();

        if (collision.gameObject.tag == "Player")
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
                    jetpack.AddEnergy(POSITIVE_HEAL);
                Recolected();
            }
        }
}
