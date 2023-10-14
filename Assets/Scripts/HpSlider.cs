using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    Slider slider;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
         player = FindObjectOfType<Player>();
        slider.maxValue = player.health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.health;
    }
}
