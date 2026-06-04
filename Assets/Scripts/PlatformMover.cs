using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float velocidad = 2f;    // Qué tan rápido se mueve
    public float distancia = 1.5f;  // Cuánto se aleja del centro

    private Vector3 posicionInicial;

    void Start()
    {
        // Guardamos su posición original para que oscile alrededor de ese punto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calculamos la nueva posición X usando una onda senoidal
        float nuevaX = posicionInicial.x + Mathf.Sin(Time.time * velocidad) * distancia;

        // Aplicamos la nueva posición, manteniendo su Y y Z intactas
        transform.position = new Vector3(nuevaX, transform.position.y, transform.position.z);
    }
}