using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager main { get; private set; }

    private List<Torch> _torches;
    [SerializeField] private float _blackoutTime = 2;

    private DungeonGenerator _generator;
    private int _level = 1;
    [SerializeField] private int _roomIncrement = 3;

    private void Awake()
    {
        _generator = GetComponent<DungeonGenerator>();
        _torches = new List<Torch>();
    }

    public void NewDungeon()
    {
        _generator.GenerateDungeon(_level * _roomIncrement);
    }

    public void Blackout()
    {
        PerformBlackout();
    }

    private IEnumerator PerformBlackout()
    {
        _torches.ForEach(torch => torch.SwitchOff());
        yield return new WaitForSeconds(_blackoutTime);
        _torches.ForEach(torch => torch.SwitchOn());
    }
}
