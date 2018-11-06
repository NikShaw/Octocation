using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoldSpawner : MonoBehaviour {

    public GameObject EndScreen;
    public GameObject Score;
    public List<GameObject> gold = new List<GameObject>();
    public List<GameObject> disable = new List<GameObject>();
    private bool disabled = false;
    private float enableTimer = 0.0f;
    private float enableTime = 2.0f;
    private int numSpawned = 0;
    public float spawnTime = 0.2f;
    public float spawnxRange = 0.2f;
    public float spawnyRange = 0.1f;
    public bool spawning = false;
    public bool start = false;
    private float spawnTimer = 0.0f;
    private int spawnLimit = 100;
    private SoundManager soundMan;

    void Start() {
        soundMan = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update() {
        if (spawning) {
            if (!disabled) {
                disabled = true;
                for(int i = 0; i < disable.Count; i++) {
                    disable[i].SetActive(false);
                }
            }
            start = true;
        }
        if (start) {    // Spawn gold coins based on score
            if (numSpawned < spawnLimit) {
                int tempSpawn = (((spawnLimit - numSpawned) / spawnLimit) * 20);    // Group large amounts together
                if ((tempSpawn > 1) && (tempSpawn <= (spawnLimit - numSpawned))) {
                    for (int i = 0; i < tempSpawn; i++) {
                        spawnTimer = 0.0f;
                        Vector3 pos = this.transform.position;
                        pos.x += Random.Range(-spawnxRange, spawnxRange) + 0.1f;
                        pos.y += Random.Range(0.0f, spawnyRange) + 0.3f;
                        GameObject tmpGold = (GameObject)Instantiate(gold[Random.Range(0, gold.Count)], pos, this.transform.rotation);
                        Rigidbody2D tmpRgd = tmpGold.GetComponent<Rigidbody2D>();
                        tmpRgd.AddForce(new Vector2(Random.Range(-200.0f, 200.0f), Random.Range(200.0f, 600.0f)));
                        tmpGold.transform.Rotate(new Vector3(Random.Range(-30.0f, 30.0f), 0.0f));
                        soundMan.PlaySound("coinfall", false, false, Vector3.zero, 0.1f);
                        numSpawned++;
                    }
                    soundMan.PlaySound("coinwhoosh", false, false, Vector3.zero, 1.0f);
                } else if (spawnTimer >= spawnTime) {
                    spawnTimer = 0.0f;
                    Vector3 pos = this.transform.position;
                    pos.x += Random.Range(-spawnxRange, spawnxRange) + 0.1f;
                    pos.y += Random.Range(0.0f, spawnyRange) + 0.3f;
                    GameObject tmpGold = (GameObject)Instantiate(gold[Random.Range(0, gold.Count)], pos, this.transform.rotation);
                    Rigidbody2D tmpRgd = tmpGold.GetComponent<Rigidbody2D>();
                    tmpRgd.AddForce(new Vector2(Random.Range(-200.0f, 200.0f), Random.Range(200.0f, 600.0f)));
                    tmpGold.transform.Rotate(new Vector3(Random.Range(-30.0f, 30.0f), 0.0f));
                    if (numSpawned % 2 == 0) {
                        soundMan.PlaySound("coinfall", false, false, Vector3.zero, 0.6f);
                    }
                    if(numSpawned % 4 == 0) {
                        soundMan.PlaySound("coinwhoosh", false, false, Vector3.zero, 0.08f);
                    }
                    numSpawned++;
                }
                spawnTimer += Time.deltaTime;
            } else {    // Trigger end screen when done
                if (!EndScreen.activeSelf) {
                    if (enableTimer >= enableTime) {
                        EndScreen.SetActive(true);
                        Score.transform.position = new Vector3(-3.0f, Score.transform.position.y - 5.0f, Score.transform.position.z);
                    } else {
                        enableTimer += Time.deltaTime;
                    }
                }
            }
        }
    }

    public void SetSpawnLimit(int lim) {
        spawnLimit = lim;
    }
}
