using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int startHealth;
    [SerializeField] private Slider healthBar;
    private int currentHealth;
    
    
    
    
    public static HealthManager main;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    public void ReduceHealth(int reduction)
    {
        currentHealth= Mathf.Max(currentHealth-reduction,0);
        UpdateBar();
    }

    public void IncreaseHealth(int addition)
    {
        currentHealth =Mathf.Min(currentHealth+addition,startHealth);
        UpdateBar();
        
    }

    void UpdateBar()
    {
        healthBar.value = currentHealth;
    }
    
    
}
