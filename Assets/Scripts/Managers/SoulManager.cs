using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private Text soulsUI;

    public static SoulManager main;

    private void Awake()
    {
        main = this;
    }

    public void UpdateUI(int soulCount)
    {
        soulsUI.text = "" + soulCount;
    }
}
