using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    private int _maxHealth;
    [SerializeField] private Slider healthBar;
    
    public static HealthManager main;

    private void Awake()
    {
        main = this;
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        UpdateHealthBar(100);
    }

    public void UpdateHealthBar(int health)
    {
        healthBar.value = (float)health / _maxHealth * 100f;
    }
    
    
}
