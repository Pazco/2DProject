using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMover : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float velocidad = 2f;
    public float distancia = 1.5f;

    private Vector3 _posicionInicial;
    private Rigidbody2D _rb;

    void Start()
    {
        _posicionInicial = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        float nuevaX = _posicionInicial.x + Mathf.Sin(Time.time * velocidad) * distancia;
        _rb.MovePosition(new Vector2(nuevaX, _rb.position.y));
    }
}
