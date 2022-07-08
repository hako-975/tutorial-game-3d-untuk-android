using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;

    Slider slider;
    int maxHealth;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        playerStats = FindObjectOfType<PlayerStats>();
        maxHealth = playerStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetMaxHealth(maxHealth);
        SetHealth(PlayerPrefsManager.instance.GetHealth());
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
