using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMenu : MonoBehaviour
{

    [SerializeField] private GameObject[] monsters;

    public Transform spawnPoint;

    public static MonsterMenu main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonMonster(int mNumber)
    {
        Instantiate(monsters[mNumber], spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Summon monster: "+mNumber);
    }

    public void ChangeSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
    
    
}
