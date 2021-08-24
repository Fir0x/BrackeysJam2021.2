using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseClass : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public int attackSpeed;
    [Tooltip("Range of attack")]
    public int range;
    [Tooltip("How long is the character stunned?")]
    public int stunTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        Debug.Log(name+" has performed attack");
    }

    public void stun()
    {
        //call animator to put in stun state
        //use coroutine/invoke method to switch state to idle after stunTime
    }
}
