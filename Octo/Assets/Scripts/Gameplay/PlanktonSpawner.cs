using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanktonSpawner : MonoBehaviour {

    [Header("Unity Objects")]
    public GameObject Plankton;
    public List<GameObject> plankList = new List<GameObject>();

    [Header("Plankton Settings")]
    public float spawnRadius = 10.0f;
    public float spawnRate = 1.0f;
    public int limit = 2;
    public float xSpeed = 0.01f;
    public float ySpeed = 0.0f;
    public float spawnTimer = 0.0f;
    public float maxYVel = 0.6f;
    public float maxXVel = 0.6f;
    private int planktonDestroyPoint = 0;

    [Header("Timers")]
    public float destroyTime = 0.5f;
    private float destroyTimer = 0.0f;
    
	void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (this.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y - 15.0f)) {//  Remove plankton slowly if below camera
            if (planktonDestroyPoint != limit) {
                if (destroyTimer >= destroyTime) {
                    Destroy(plankList[limit - planktonDestroyPoint]);
                    planktonDestroyPoint++;
                } else {
                    Destroy(this.gameObject);
                }
            }
            destroyTimer += Time.deltaTime;
        }else if (this.transform.position.y > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0.0f, 0.0f)).y + 70.0f)) { //  Do nothing if too far above camera

        } else if ((limit == 0) || (plankList.Count < limit)) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   Create new plankton if no limit or limit not reached
            if (spawnTimer >= spawnRate) {
                GameObject tmpPlankton = (GameObject)Instantiate(Plankton, new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f)), this.transform.rotation);
                tmpPlankton.GetComponent<ParticleFloat>().Init(new Vector2(Random.Range(0.1f, 2.0f) * xSpeed, Random.Range(0.01f, 3.0f) * ySpeed), new Vector2(Random.Range(0.2f, 8.0f), Random.Range(0.2f, 8.0f)), new Vector2(Random.Range(0.1f, maxXVel), Random.Range(0.1f, maxYVel)), Random.Range(15, 7));
                plankList.Add(tmpPlankton);

                spawnTimer = 0;
            }
            spawnTimer += Time.deltaTime;
        } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Re-use plankton if plankton is not in use
            for (int i = 0; i < plankList.Count; i++) {
                if (plankList[i].GetComponent<ParticleFloat>().IsInUse() == false) {
                    plankList[i].transform.position = new Vector3(transform.position.x + Random.Range(spawnRadius, -spawnRadius), transform.position.y + Random.Range(spawnRadius, -spawnRadius), Random.Range(1.0f, -1.0f));
                    plankList[i].GetComponent<ParticleFloat>().Init(new Vector2(Random.Range(0.1f, 2.0f) * xSpeed, Random.Range(0.01f, 3.0f) * ySpeed), new Vector2(Random.Range(0.2f, 8.0f), Random.Range(0.2f, 8.0f)), new Vector2(Random.Range(0.1f, maxXVel), Random.Range(0.1f, maxYVel)), Random.Range(15, 7));
                }
            }
        }
    }
}
