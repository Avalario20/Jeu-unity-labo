using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradiant;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradiant.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradiant.Evaluate(slider.normalizedValue);
    }
}
