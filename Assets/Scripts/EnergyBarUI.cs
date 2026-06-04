using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    [Header("Conexiones")]
    public Jetpack jetpackJugador;
    public Slider SliderBarra; // Ahora conectamos un Slider

    void Start()
    {
        // Le decimos al Slider cuál es su tope máximo de energía al arrancar el juego
        SliderBarra.maxValue = jetpackJugador._maxEnergy;
    }

    void Update()
    {
        // Simplemente le pasamos la energía actual, el Slider hace el resto
        SliderBarra.value = jetpackJugador.Energy;
    }
}