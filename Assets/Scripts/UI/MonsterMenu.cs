using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Monster
{
    [Tooltip(("The monster gaemobject"))]
    public GameObject monster;

    public int soulCost;

};
public class MonsterMenu : MonoBehaviour
{

    [SerializeField] private Monster[] monsters;
    
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
        
        if (SoulManager.main.souls - monsters[mNumber].soulCost >= 0)
        {
            SoulManager.main.ReduceSouls(monsters[mNumber].soulCost);
            Instantiate(monsters[mNumber].monster, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Summon monster: " + mNumber);
            AudioManager.main.PlaySoundEffect(SoundEffects.monsterSpawn);
        }
    }

    public void ChangeSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
    
    
}
