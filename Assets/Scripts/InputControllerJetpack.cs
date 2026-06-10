using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerJetpack : MonoBehaviour
{
    [SerializeField] private Jetpack _jetpack;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        _jetpack.IsMoving = Mathf.Abs(horizontal) > 0;

        if (horizontal < 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Left);
        }
        else if (horizontal > 0)
        {
            _jetpack.FlyHorizontal(Jetpack.Direction.Right);
        }

        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0)
            _jetpack.FlyUp();
        else
            _jetpack.StopFlying();
    }


}
