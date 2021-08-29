using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Pathfinder p;
    public Torch t;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            p.FindPath(transform.position);
        else if (Input.GetKeyDown(KeyCode.T))
            t.FindTarget();
    }
}
