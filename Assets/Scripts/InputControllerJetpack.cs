using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerJetpack : MonoBehaviour
{
    [SerializeField] private Jetpack _jetpack;

    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Left);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Right);
        }

        if (Input.GetAxis("Vertical") > 0)
            _jetpack.FlyUp();
        else
            _jetpack.StopFlying();
    }


}
