using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{

    [SerializeField] private Jetpack _jetpack;

    private Animator _anim;

   private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

 
    void Update()
    {
            _anim.SetBool("Flying",  _jetpack.Flying);
    }
}
