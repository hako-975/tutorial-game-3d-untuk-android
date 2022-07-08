using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarNPC : MonoBehaviour
{
    Slider slider;
    int maxHealth;

    public Gradient gradient;
    public Image fill;
    public NPCStats nPCStats;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        maxHealth = nPCStats.maxHealth;

        SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(nPCStats.currentHealth);
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
