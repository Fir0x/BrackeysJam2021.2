using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager main { get; private set; }


    private DungeonGenerator _generator;
    [SerializeField] private int _roomIncrement = 3;
    [SerializeField] private float _incrementFactor = 1.5f;
    private int _roomCount = 0;
    private List<Room> _roomList;

    [SerializeField] private GameObject _torchPrefab;
    [SerializeField] private GameObject _portalPrefab;
    private List<Torch> _torchList;
    [SerializeField] private float _blackoutTime = 2;

    private Portal _portal;

    private void Awake()
    {
        main = this;
        _generator = GetComponent<DungeonGenerator>();
        _roomList = new List<Room>();
        _torchList = new List<Torch>();
    }

    private void Start()
    {
        NewDungeon(false);
    }

    public void NewDungeon(bool upgradePlayer)
    {
        _roomList.ForEach(room => Destroy(room.gameObject));
        _roomList.Clear();
        _torchList.ForEach(torch => Destroy(torch.gameObject));
        _torchList.Clear();

        if (upgradePlayer)
            Player.main.UpgradePlayer();

        _roomCount = (int)(_incrementFactor * _roomCount) + _roomIncrement;
        _roomList = _generator.GenerateDungeon(_roomCount);
        Player.main.transform.position = _roomList[0].transform.position;

        _portal = Instantiate(_portalPrefab, (Vector2)_roomList[_roomList.Count - 1].transform.position + Vector2.one * 0.5f,
                              Quaternion.identity).GetComponent<Portal>();

        int remainingTorch = _roomList.Count / 2;
        int remainingRoom = _roomList.Count - 2;
        for (int i = 1; i < _roomList.Count - 1 && remainingTorch > 0; i++)
        {
            bool placeTorch = true;
            if (_roomList.Count - 1 - i > remainingRoom)
                placeTorch = Random.Range(0, 2) == 1;

            if (placeTorch)
            {
                _torchList.Add(Instantiate(_torchPrefab, (Vector2)_roomList[i].transform.position + Vector2.one * 0.5f,
                                           Quaternion.identity).GetComponent<Torch>());
                remainingTorch--;
            }

            remainingRoom--;
        }
    }

    public void Blackout()
    {
        StartCoroutine(PerformBlackout());
    }

    private IEnumerator PerformBlackout()
    {
        _torchList.ForEach(torch => torch.SwitchOff());
        yield return new WaitForSeconds(_blackoutTime);
        _torchList.ForEach(torch => torch.SwitchOn());
    }
}
