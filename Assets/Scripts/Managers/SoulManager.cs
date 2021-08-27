using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private Text soulsUI;
    public int souls;

    public static SoulManager main;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReduceSouls(int reduction)
    {
        souls= Mathf.Max(souls-reduction,0);
        UpdateUI();
    }

    public void IncreaseSouls(int addition)
    {
        souls += addition;
        UpdateUI();
        
    }

    void UpdateUI()
    {
        soulsUI.text = ""+souls;
    }
}
