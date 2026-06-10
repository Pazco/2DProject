using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IRecolectable
{
    [SerializeField] private GameObject _particles;
    [SerializeField] protected Color _effectColor = Color.magenta;
    [SerializeField] protected float _effectDuration = 1f;
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;

    void Start() => Destroy(gameObject, 8f);

    public enum ItemTypes
    {
        None,
        Nose,
        ErrorCode,
        PositiveWords
    }

    [field: SerializeField] public ItemTypes Type { get; set; }

    public void Recolected()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && _clip != null)
            audio.PlayOneShot(_clip, _volume);

        Destroy(gameObject, 0.15f);
        CreateParticles();
    }

    private void CreateParticles()
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }
}
