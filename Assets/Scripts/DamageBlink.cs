using System.Collections;
using UnityEngine;

public class DamageBlink : MonoBehaviour
{
    [Header("Configuración del Efecto")]
    [Tooltip("Arrastra aquí el SpriteRenderer o MeshRenderer de tu personaje")]
    public Renderer characterRenderer;

    [Tooltip("Duración de cada fase del parpadeo")]
    public float blinkDuration = 0.1f;

    [Tooltip("Cuántas veces parpadeará por cada golpe")]
    public int blinkCount = 3;

    [Tooltip("0 es totalmente invisible, 1 es totalmente opaco")]
    [Range(0f, 1f)]
    public float alphaAlParpadear = 0f;

    private Color originalColor;
    private Coroutine blinkCoroutine;

    void Start()
    {
        // Guardamos el color original del material al iniciar
        if (characterRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
    }

    // Llama a este método al recibir daño
    public void PlayBlinkEffect()
    {
        // Reseteamos si ya estaba parpadeando
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            characterRenderer.material.color = originalColor;
        }

        blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        // Creamos una copia del color original, pero le cambiamos la transparencia (Alpha)
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, alphaAlParpadear);

        for (int i = 0; i < blinkCount; i++)
        {
            // 1. Se vuelve transparente
            characterRenderer.material.color = transparentColor;
            yield return new WaitForSeconds(blinkDuration);

            // 2. Vuelve a ser visible
            characterRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        blinkCoroutine = null;
    }
}