using System.Collections;
using UnityEngine;

public class DamageBlink : MonoBehaviour
{
    [Header("Configuración del Efecto")]
    public Renderer characterRenderer;

    private Color originalColor;
    private Coroutine blinkCoroutine;

    void Awake()
    {
        if (characterRenderer == null)
            characterRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        if (characterRenderer != null)
            originalColor = characterRenderer.material.color;
    }

    public void PlayBlinkEffect(Color tintColor, float duration)
    {
        if (characterRenderer == null) return;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            characterRenderer.material.color = originalColor;
        }

        blinkCoroutine = StartCoroutine(BlinkRoutine(tintColor, duration));
    }

    private IEnumerator BlinkRoutine(Color tintColor, float duration)
    {
        float blinkRate = 0.1f;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            characterRenderer.material.color = tintColor;
            yield return new WaitForSeconds(blinkRate);
            characterRenderer.material.color = originalColor;
            yield return new WaitForSeconds(blinkRate);
        }

        blinkCoroutine = null;
    }

    public void PlayAlphaBlink(float duration)
    {
        if (characterRenderer == null) return;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            SetAlpha(originalColor.a);
        }

        blinkCoroutine = StartCoroutine(AlphaBlinkRoutine(duration));
    }

    private IEnumerator AlphaBlinkRoutine(float duration)
    {
        float blinkRate = 0.1f;
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            SetAlpha(0f);
            yield return new WaitForSeconds(blinkRate);
            SetAlpha(originalColor.a);
            yield return new WaitForSeconds(blinkRate);
        }

        SetAlpha(originalColor.a);
        blinkCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color color = characterRenderer.material.color;
        color.a = alpha;
        characterRenderer.material.color = color;
    }
}
