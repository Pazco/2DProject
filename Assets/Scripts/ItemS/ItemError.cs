using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemError : Item
{
    const float ERROR_FORCE = 100;
    const float ERROR_DOWN_POS = 2.5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Recolected();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();

            switch (Type)
            {
                case ItemTypes.ErrorCode:
                    if (jetpack.Flying)
                        jetpack.GetComponent<Rigidbody2D>().AddForce(Vector2.down * ERROR_FORCE);
                    else if (jetpack.transform.position.y > 1)
                        jetpack.transform.Translate(Vector2.down * ERROR_DOWN_POS);

                    Recolected();
                    break; // Siempre debe haber un break en cada case
            }
        }
    } 
}