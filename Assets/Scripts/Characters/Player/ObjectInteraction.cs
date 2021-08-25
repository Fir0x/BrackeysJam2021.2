using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectInteraction : MonoBehaviour
{

    CircleCollider2D objectTrigger;
    List<GameObject> interactables = new List<GameObject>();
    public float refreshTime = 5f;
    public GameObject player;
    public float moveDistance;
    public float detectionRadius;

    public int moveLimit;
    int moveCount;


    Vector2 playerPosition;
    Vector2 objectPosition;
    private void Start()
    {
        objectTrigger = GetComponent<CircleCollider2D>();
        StartCoroutine("checkForInterTimer");

    }
    void Update()
    {
        playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        foreach (GameObject gameObject in interactables)
        {
            objectPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            if (Vector2.Distance(playerPosition, objectPosition) < detectionRadius && moveCount < moveLimit)
            {
                Debug.Log(gameObject);
                if (Input.GetKeyUp(KeyCode.F))
                {
                    gameObject.transform.Translate(Vector3.left * moveDistance, Space.World);
                    moveCount++;
                }
            }
        }
    }

    IEnumerator checkForInterTimer()
    {
        while (true)
        {
            checkForInter();
            Debug.Log("Timer started");
            yield return new WaitForSeconds(refreshTime);
        }

    }

    public void checkForInter()
    {
        interactables.Clear();
        // get root objects in scene
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            if (gameObject.tag == "Interactable")
            {
                interactables.Add(gameObject);
            }
        }
    }

}
