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
        Destroy(gameObject);
        CreateParticles();
    }

    private void CreateParticles()
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }
}
