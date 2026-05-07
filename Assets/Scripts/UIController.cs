using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _textSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _energySlider.value = _jetpack.Energy;
    }
}
