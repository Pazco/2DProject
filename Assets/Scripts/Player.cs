using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator), typeof(AudioSource))]

public class Player : MonoBehaviour
{

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private AudioClip _bonkClip;

    private Animator _anim;

   private void Awake()
    {
        //_anim = GetComponent<Animator>();
    }

  
    void Update()
    {
            //_anim.SetBool("Flying",  _jetpack.Flying);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Roof")
        {
            if (GameManager.Instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
                GameManager.Instance.StartTimer();
            }
            GameManager.Instance.FinishGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Platform"))
            return;

        Vector2 normal = collision.GetContact(0).normal;

        if (normal.y < -0.5f || Mathf.Abs(normal.x) > 0.5f)
        {
            GetComponent<AudioSource>().PlayOneShot(_bonkClip);
        }
    }
}
