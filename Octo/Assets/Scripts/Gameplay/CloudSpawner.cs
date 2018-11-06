using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour {

    [Header("Unity Objects")]
    public GameObject Cloud;
    public List<GameObject> cloudList = new List<GameObject>();

    [Header("Plankton Settings")]
    public Vector2 spawnRadius = new Vector2(5.0f, 5.0f);
    public float spawnRate = 0.5f;
    public int limit = 5;
    public float maxAlpha = 0.5f;
    private float xSpeed = 0.01f;
    private float ySpeed = 0.05f;
    public Vector2 speed = new Vector2(0.01f, 0.05f);
    public float fadeTimer = 2.0f;
    public float spawnTimer = 0.0f;
    public Vector2 maxVel = new Vector2(0.6f, 0.6f);
    public Vector2 drift = new Vector2(2.0f, 2.0f);
    private int cloudDestroyPoint = 0;

    [Header("Timers")]
    public float destroyTime = 0.5f;
    private float destroyTimer = 0.0f;

    void Update() {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y - 15.0f)) {//  Remove cloud slowly if below camera
            if (cloudDestroyPoint != limit) {
                if (destroyTimer >= destroyTime) {
                    Destroy(cloudList[limit - cloudDestroyPoint]);
                    cloudDestroyPoint++;
                } else {
                    Destroy(this.gameObject);
                }
            }
            destroyTimer += Time.deltaTime;
        } else if (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + 70.0f)) { //  Do nothing if too far above camera

        } else if ((limit == 0) || (cloudList.Count < limit)) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   Create new cloud if no limit or limit not reached
            if (spawnTimer >= spawnRate) {
                GameObject tmpPlankton = (GameObject)Instantiate(Cloud, new Vector3(transform.position.x + Random.Range(spawnRadius.x, -spawnRadius.x), transform.position.y + Random.Range(spawnRadius.y, -spawnRadius.y), Random.Range(1.0f, -1.0f)), this.transform.rotation);
                tmpPlankton.GetComponent<CloudFloat>().Init(speed * (Random.Range(0.5f, 2.0f)), new Vector2(xSpeed, ySpeed), drift, maxVel * (Random.Range(0.2f, 1.5f)), fadeTimer * (Random.Range(0.5f, 1.5f)), maxAlpha);
                cloudList.Add(tmpPlankton);

                spawnTimer = 0;
            }
            spawnTimer += Time.deltaTime;
        } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Re-use cloud if cloud is not in use
            for (int i = 0; i < cloudList.Count; i++) {
                if (cloudList[i].GetComponent<CloudFloat>().IsInUse() == false) {
                    cloudList[i].transform.position = new Vector3(transform.position.x + Random.Range(spawnRadius.x, -spawnRadius.x), transform.position.y + Random.Range(spawnRadius.y, -spawnRadius.y), Random.Range(1.0f, -1.0f));
                    cloudList[i].GetComponent<CloudFloat>().Init(speed * (Random.Range(0.5f, 2.0f)), new Vector2(xSpeed, ySpeed), drift, maxVel * (Random.Range(0.2f, 1.5f)), fadeTimer * (Random.Range(0.5f, 1.5f)), maxAlpha);
                }
            }
        }
    }
}
