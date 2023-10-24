using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    void Start() {

        Slider slider = GetComponent<Slider>();
        AudioListener.volume = slider.value;
        slider.onValueChanged.AddListener(sliderValue => {
            AudioListener.volume = sliderValue;
        });
    }
}
