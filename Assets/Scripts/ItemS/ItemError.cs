using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemError : Item
{
    const float ERROR_DOWN_POS = 2.5f;
    const float ERROR_DAMAGE = -50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var blink = collision.GetComponent<DamageBlink>();
            if (blink != null) blink.PlayAlphaBlink(1.5f);

            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
            jetpack.AddEnergy(ERROR_DAMAGE);

            switch (Type)
            {
                case ItemTypes.ErrorCode:
                    jetpack.transform.Translate(Vector2.down * ERROR_DOWN_POS);
                    Recolected();
                    break;
            }
        }
    } 
}