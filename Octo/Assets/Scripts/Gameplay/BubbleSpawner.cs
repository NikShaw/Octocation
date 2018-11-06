using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleSpawner : MonoBehaviour {

    [Header("Unity Objects")]
    public GameObject Bubbles;
    public List<GameObject> bubbList = new List<GameObject>();

    [Header("Bubble Settings")]
    public bool autoStart = true;
    public float spawnRadius = 10.0f;
    public float spawnRate = 1.0f;
    public int limit = 2;
    public float xVel = 0.0f;
    public float yVel = 0.0f;
    public float ScaleTime = 0.8f;
    public float bubbleSize = 1.2f;
    //public float xSpeed = 0.01f;
    //public float ySpeed = 0.0f;
    public float yDelOffset = -15.0f;
    public float ySpawnOffset = 30.0f;
    public float spawnTimer = 0.0f;
    public float maxYVel = 0.6f;
    public float xSlow = 0.9f;
    private int bubbleDestroyPoint = 0;
    private bool started = false;

    [Header("Timers")]
    public float destroyTime = 0.5f;
    private float destroyTimer = 0.0f;

    public void StartBubbles() {
        started = true;
    }

    public void StopBubbles() {
        started = false;
    }

    public void Emit(int num) {
        if (num > limit) {
            num = limit;
        }
        for (int j = 0; j < num; j++) {
            if (this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y + yDelOffset)) {//  Remove plankton slowly if below camera
                if (bubbleDestroyPoint != limit) {
                    if (destroyTimer >= destroyTime) {
                        Destroy(bubbList[limit - bubbleDestroyPoint]);
                        bubbleDestroyPoint++;
                    } else {
                        Destroy(this.gameObject);
                    }
                }
            } else if (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + ySpawnOffset)) { //  Do nothing if too far above camera

            } else if ((limit == 0) || (bubbList.Count < limit)) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   Create new plankton if no limit or limit not reached

                GameObject tmpBubble = (GameObject)Instantiate(Bubbles, new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f)), this.transform.rotation);
                tmpBubble.GetComponent<BubbleFloat>().Init(new Vector2(Random.Range(xVel * 0.6f, xVel * 1.2f), Random.Range(yVel * 0.6f, yVel * 1.2f)), maxYVel, xSlow, ScaleTime, bubbleSize, yDelOffset, ySpawnOffset);

                bubbList.Add(tmpBubble);

            } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Re-use plankton if plankton is not in use
                for (int i = 0; i < bubbList.Count; i++) {
                    if (bubbList[i].GetComponent<BubbleFloat>().IsInUse() == false) {
                        bubbList[i].transform.position = new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f));
                        bubbList[i].GetComponent<BubbleFloat>().Init(new Vector2(Random.Range(xVel * 0.6f, xVel * 1.2f), Random.Range(yVel * 0.6f, yVel * 1.2f)), maxYVel, xSlow, ScaleTime, bubbleSize, yDelOffset, ySpawnOffset);

                    }
                }
            }
        }
    }

    void Update() {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (started || autoStart) {
            if (this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y + yDelOffset)) {//  Remove plankton slowly if below camera
                if (bubbleDestroyPoint != limit) {
                    if (destroyTimer >= destroyTime) {
                        Destroy(bubbList[limit - bubbleDestroyPoint]);
                        bubbleDestroyPoint++;
                    } else {
                        Destroy(this.gameObject);
                    }
                }
                destroyTimer += Time.deltaTime;
            } else if (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + ySpawnOffset)) { //  Do nothing if too far above camera

            } else if ((limit == 0) || (bubbList.Count < limit)) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   Create new plankton if no limit or limit not reached
                if (spawnTimer >= spawnRate) {
                    GameObject tmpBubble = (GameObject)Instantiate(Bubbles, new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f)), this.transform.rotation);
                    tmpBubble.GetComponent<BubbleFloat>().Init(new Vector2(Random.Range(xVel * 0.6f, xVel * 1.2f), Random.Range(yVel * 0.6f, yVel * 1.2f)), maxYVel, xSlow, ScaleTime, bubbleSize, yDelOffset, ySpawnOffset);

                    bubbList.Add(tmpBubble);

                    spawnTimer = 0;
                }
                spawnTimer += Time.deltaTime;
            } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Re-use plankton if plankton is not in use
                for (int i = 0; i < bubbList.Count; i++) {
                    if (spawnTimer >= spawnRate) {
                        if (bubbList[i].GetComponent<BubbleFloat>().IsInUse() == false) {
                            bubbList[i].transform.position = new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f));
                            bubbList[i].GetComponent<BubbleFloat>().Init(new Vector2(Random.Range(xVel * 0.6f, xVel * 1.2f), Random.Range(yVel * 0.6f, yVel * 1.2f)), maxYVel, xSlow, ScaleTime, bubbleSize, yDelOffset, ySpawnOffset);
                        }
                        spawnTimer = 0;
                    }
                    spawnTimer += Time.deltaTime;
                }
            }
        }
    }
}
