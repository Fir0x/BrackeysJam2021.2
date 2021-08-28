using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collide");
        if (collision.GetComponent<Player>() != null)
            DungeonManager.main.NewDungeon();
    }
}
