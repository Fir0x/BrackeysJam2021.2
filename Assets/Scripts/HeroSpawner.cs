using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Tooltip(("The order in which you create heroes in a wave"))]
    public GameObject[] heroes;
    public float heroSpawnTime;
    public Transform spawnPoint;
    
};

public class HeroSpawner : MonoBehaviour
{
    [Header("Time")] 
    [SerializeField] private float beginSpawnTime;
    [Tooltip("Time between the end of a wave and the beginning of the next one")]
    [SerializeField] private float waveTime;

    [Header("Waves")] 
    [SerializeField] private Wave[] waves;
    private int waveNumber;

    //Variables for time display
    
    /*
     *a constant string for when the wave is begin deployed
     * a float time for when the wave is over, and the next one will begin in x seconds
     * take time from wave, use coroutine to change text every second and display to player
     * 
     */
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextWave",beginSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CreateHeroes()//get hero from wave, produce after given seconds
    {
        int i = 0;
        while (i < waves[waveNumber].heroes.Length)
        {
            yield return new WaitForSeconds(waves[waveNumber].heroSpawnTime);
            Debug.Log("Created hero");
            // Instantiate(waves[waveNumber].heroes[i], waves[waveNumber].spawnPoint);
        }
        NextWave();
    }

    public void NextWave()
    {
        if (waveNumber > waves.Length)
        {
            GameManager.main.Win();
        }
        StartCoroutine(CreateWave());
        waveNumber++;
    }

    IEnumerator CreateWave()
    {
        yield return new WaitForSeconds(waveTime);
        StartCoroutine(CreateHeroes());
    }
    
}
