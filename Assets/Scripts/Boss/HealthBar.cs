using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void setHealthBar(int health)
    {
        slider.value -= health;
    }

    public void setHealthBarUp(int health)
    {
        slider.value += health;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
