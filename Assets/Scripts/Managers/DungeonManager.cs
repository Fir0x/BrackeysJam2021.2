using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager main { get; private set; }

    [SerializeField] private Light _globalLight;
    [SerializeField] private float _blackoutIntensity = 0.1f;
    [SerializeField] private float _blackoutTime = 2;

    private DungeonGenerator _generator;
    private int _waveCount = 0;

    private List<CharacterBaseClass> _stunnable;

    private void Awake()
    {
        _generator = GetComponent<DungeonGenerator>();
        _stunnable = new List<CharacterBaseClass>();
    }

    public void NextWave()
    {
        _waveCount++;
        print($"Wave {_waveCount} starts");
    }

    public void RegisterStunnable(CharacterBaseClass character)
    {
        _stunnable.Add(character);
    }

    public void Blackout()
    {
        _stunnable.RemoveAll(character => character == null);
        PerformBlackout();
        foreach (CharacterBaseClass character in _stunnable)
            character.Stun();
    }

    private IEnumerator PerformBlackout()
    {
        float originalIntensity = _globalLight.intensity;
        _globalLight.intensity = _blackoutIntensity;
        yield return new WaitForSeconds(_blackoutTime);
        _globalLight.intensity = originalIntensity;
    }
}
