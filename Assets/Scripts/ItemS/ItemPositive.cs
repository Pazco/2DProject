using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPositive : Item
{
    const float POSITIVE_HEAL = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var blink = collision.GetComponent<DamageBlink>();
            if (blink != null) blink.PlayBlinkEffect(_effectColor, _effectDuration);

            Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();
                    jetpack.AddEnergy(POSITIVE_HEAL);
                Recolected();
            }
        }
}
