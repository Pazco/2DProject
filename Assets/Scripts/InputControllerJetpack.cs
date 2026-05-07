using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerJetpack : MonoBehaviour
{
    
    [SerializeField] private Jetpack _jetpack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Fly
    if(Input.GetAxis("Horizontal") < 0)
            _jetpack.FlyHorizontal(Jetpack.Direction.Left);
    if(Input.GetAxis("Horizontal") > 0)
            _jetpack.FlyHorizontal(Jetpack.Direction.Right);

        //Vertical Fly
        if (Input.GetAxis("Vertical") > 0)
            _jetpack.FlyUp();
        else
            _jetpack.StopFlying();


    }
    

}
