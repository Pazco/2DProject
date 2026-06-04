using UnityEngine;
using TMPro; // ¡OBLIGATORIO para usar TextMeshPro!

public class AltimeterUI : MonoBehaviour
{
    [Header("Conexiones")]
    public Transform jugador;            // Para leer la posición del jugador
    public TextMeshProUGUI textoAltura;  // Para escribir en la pantalla

    [Header("Ajustes")]
    public float multiplicador = 1f;     // Por si quieres que 1 unidad de Unity sea 10 metros, etc.
    public float alturaBase = 0f;        // Por si el suelo de tu nivel no está exactamente en Y=0

    void Update()
    {
        // 1. Calculamos a qué altura está el jugador
        // Le restamos la alturaBase por si el "suelo" de tu juego está más abajo o más arriba del 0 de Unity.
        float alturaActual = (jugador.position.y - alturaBase) * multiplicador;

        // 2. Evitamos que salgan números negativos si el jugador cae por debajo del suelo
        if (alturaActual < 0)
        {
            alturaActual = 0;
        }

        // 3. Actualizamos el texto
        // ToString("F0") formatea el número para que NO muestre decimales (F1 mostraría un decimal, etc.)
        textoAltura.text = "Altura: " + alturaActual.ToString("F0") + "m";
    }
}