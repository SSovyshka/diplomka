using UnityEngine;
using UnityEngine.UI;

public class HealtsBar : MonoBehaviour
{
    public Slider slider;
    public EntityStats playerHealth;

    private void Start()
    {
        SetMaxValue(playerHealth.maxHealth);
    }

    private void Update()
    {
        SetHealth(playerHealth.health);
    }

    private void SetMaxValue(float health) {
        slider.maxValue = health;
        slider.value = health;
    }

    private void SetHealth(float health) {
        slider.value = health;
    }

    private void OnDestroy()
    {
        slider.value = 0;
    }

}
