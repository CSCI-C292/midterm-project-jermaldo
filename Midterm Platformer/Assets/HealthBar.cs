using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider _slider;
    public void setMaxHealth(int health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }
    public void setHealth(int health)
    {
        _slider.value = health;
    }
}
