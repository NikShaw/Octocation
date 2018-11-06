using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [Header("Misc")]
    public float lifeBreakOffset = 1.0f;
    public int maxLives = 5;
    public int lives = 0;
    public float invincibilityTime = 0.7f;
    private float invincibilityTimer = 0.0f;
    public float xLimit = 3.0f;
    private bool inShip = false;
    //private bool prevFrameBoost = false;
    private bool prevDead = false;
    private bool alphaIncreasing = false;
    private bool paused = false;
    public bool playBoost = false;
    private string death = "";
    private bool bubbling = false;

    [Header("Jumping")]
    public int JumpCap = 2;
    private bool canJump = false;
    public float eggForce = 750.0f;
    public float pearlForce = 250.0f;
    public float jumpForce = 500.0f;
    public float moveForce = 100.0f;
    //private bool eggBoost = false;
    private int numJumps = 0;
    //private int numPearlJumps = 0;

    [Header("Timers")]
    //public float timedTime = 2.0f;
    //private float timedTimer = 0.0f;
    //private bool inTimedArea = false;
    public float jumpDelay = 0.3f;
    private float jumpTimer = 0.0f;
    public float tapJumpTime = 0.8f;
    private float tapJumpTimer = 0.0f;
    private float deadTimer = 0.0f;
    private float deadTime = 1.0f;
    public float deadzone = 0.2f;
    public float shipSlow = 0.2f;
    private float boostSoundTimer = 0.0f;
    public float boostSoundTime = 0.3f;
    private float bubbleTimer = 0.0f;
    public float bubbleTimerMax = 1.0f;

    [Header("Lists")]
    public List<GameObject> lstLifeBreak;
    public List<GameObject> lstLives;
    public List<GameObject> collectedEggs = new List<GameObject>();
    public List<GameObject> tmpEggs = new List<GameObject>();
    public List<GameObject> collectedPearls = new List<GameObject>();
    public List<GameObject> tmpPearls = new List<GameObject>();
    public List<GameObject> collectedPointObjs = new List<GameObject>();
    public List<GameObject> tmpPointObjs = new List<GameObject>();
    public List<GameObject> collectedPoints = new List<GameObject>();
    public List<GameObject> tmpPoints = new List<GameObject>();
    public List<float> pointJumps = new List<float>();

    [Header("Unity Onjects")]
    public GameObject inkObj;
    public GameObject bubblesObj;
    private ParticleSystem inkSquirt;
    private Rigidbody2D rigidbodyA;
    private Gyroscope gyro;
    private Animator animator;
    private SpriteRenderer sprtRndr;
    private GameObject controller;
    private Controller controllerScript;
    private GameObject EggCount;
    private EggCounting EggCountScript;
    private GameObject soundMan;
    private SoundManager soundManScript;
    private GameObject cam;


    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Main Camera");
        soundMan = GameObject.Find("Sound Manager");
        if (soundMan != null) {
            soundManScript = soundMan.GetComponent<SoundManager>();
        }
        EggCount = GameObject.Find("Egg Counter");
        if(EggCount != null) {
            EggCountScript = EggCount.GetComponent<EggCounting>();
        }
        controller = GameObject.Find("ASSUMING CONTROL");
        controllerScript = controller.GetComponent<Controller>();
        sprtRndr = this.GetComponent<SpriteRenderer>();
        inkSquirt = inkObj.GetComponent<ParticleSystem>();
        animator = this.gameObject.GetComponent<Animator>();
        rigidbodyA = this.gameObject.GetComponent<Rigidbody2D>();
        lives = maxLives;
	}

    public int GetLives() { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Return lives 
            return lives;
    }

   /* private void EnterTimedArea() {
        timedTimer = 0.0f;
        inTimedArea = true;
    }

    private void ExitTimedArea() {
        inTimedArea = false;
        timedTimer = 0.0f;
    }*/

    void Dead() {   //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update loop if dead
        if (prevDead == false) {
            if (EggCount != null) {
                EggCountScript.SaveEggs();
                EggCountScript.SaveShrimps();
                EggCountScript.SaveSecrets();
                //controllerScript.SaveEggShrimp();
            }
            if (soundMan != null) {
                soundManScript.PlaySound("dead", false, false, Vector3.zero, 1.0f);
            }
            bubblesObj.SetActive(true);
            bubblesObj.transform.Find("Bubbles").GetComponent<BubbleSpawner>().StartBubbles();
            prevDead = true;
        }
        if (alphaIncreasing) {  //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   Transparency 'flashing' based on time
            if (deadTimer < deadTime) {
                deadTimer += Time.deltaTime;
            } else {
                alphaIncreasing = false;
            }
        } else {
            if (deadTimer > 0.02f) {
                deadTimer -= Time.deltaTime;
            } else {
                alphaIncreasing = true;
            }
        }

        sprtRndr.material.color = new Color(1.0f, 0.4f, 0.2f); //  .   .   .   .   .   .   Red filter
        sprtRndr.color = new Color(1.0f, 1.0f, 1.0f, deadTimer / deadTime);

    }

    private void NewJump() {
        if (pointJumps.Count > 0) {
            if(pointJumps.Count > JumpCap) {
                for (int i = (pointJumps.Count - 1); i > JumpCap; i--) {
                    pointJumps.RemoveAt(i);
                }
            }
            if (jumpTimer >= jumpDelay) {   //  .   .   .   .   .   .   .   .   .   .   .   Jump on timer, lower number of jumps available
                rigidbodyA.velocity = new Vector2(rigidbodyA.velocity.x, 0.0f);
                if(soundMan != null) {
                    //soundManScript.playBoost();
                }
                if (inShip) {
                    rigidbodyA.AddForce(new Vector2(0.0f, pointJumps[0] * shipSlow));
                } else {
                    rigidbodyA.AddForce(new Vector2(0.0f, pointJumps[0]));
                }
                inkSquirt.Play();
                jumpTimer = 0.0f;
                pointJumps.RemoveAt(0);
            }
            jumpTimer += Time.deltaTime;
        }
    }

    public void MoveLeft(float amount) {
        float timee = Time.deltaTime * 100.0f;
        if (inShip) {
            rigidbodyA.AddForce(new Vector2(-moveForce * amount * shipSlow * timee, 0.0f));
        } else {
            if(rigidbodyA.velocity.x > -xLimit)
            rigidbodyA.AddForce(new Vector2(-moveForce * amount * timee, 0.0f));
        }
    }

    public void MoveRight(float amount) {
        float timee = Time.deltaTime * 100.0f;
        if (inShip) {
            rigidbodyA.AddForce(new Vector2(moveForce * amount * shipSlow * timee, 0.0f));
        } else {
            if(rigidbodyA.velocity.x < xLimit)
            rigidbodyA.AddForce(new Vector2(moveForce * amount * timee, 0.0f));
        }
    }

    public void StartJump(bool FromGround) {    //  *   *   *   *   *   *   *   *   *   *   Start jump timer and animation, lower lives if not on ground
        if (tapJumpTimer >= tapJumpTime) {
            if (FromGround) {
                animator.SetTrigger("Squidjump");
                pointJumps = new List<float>();
                pointJumps.Add(jumpForce);
                canJump = false;
                tapJumpTimer = 0.0f;
            } else {
                PlayerBoost();
                tapJumpTimer = 0.0f;
            }
        }
    }

    public void decLives(int amount) {  //  *   *   *   *   *   *   *   *   *   *   *   *   Display of lives
        lives -= amount;
        if (soundMan != null) {
            soundManScript.PlaySound("lifebreak", false, false, Vector3.zero, 1.0f);
        }
        switch (lives) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Toggle visibility of lives
            case 4:
                lstLives[4].SetActive(false);
                Instantiate(lstLifeBreak[4], new Vector2(lstLifeBreak[4].transform.position.x, lstLifeBreak[4].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                break;
            case 3:
                for (int i = 4; i >= 3; i--) {
                    if (lstLives[i].activeInHierarchy) {
                        lstLives[i].SetActive(false);
                        Instantiate(lstLifeBreak[i], new Vector2(lstLifeBreak[i].transform.position.x, lstLifeBreak[i].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                    }
                }
                break;
            case 2:
                for (int i = 4; i >= 2; i--) {
                    if (lstLives[i].activeInHierarchy) {
                        lstLives[i].SetActive(false);
                        Instantiate(lstLifeBreak[i], new Vector2(lstLifeBreak[i].transform.position.x, lstLifeBreak[i].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                    }
                }
                break;
            case 1:
                for (int i = 4; i >= 1; i--) {
                    if (lstLives[i].activeInHierarchy) {
                        lstLives[i].SetActive(false);
                        Instantiate(lstLifeBreak[i], new Vector2(lstLifeBreak[i].transform.position.x, lstLifeBreak[i].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                    }
                }
                break;
            case 0:
                for (int i = 4; i >= 0; i--) {
                    if (lstLives[i].activeInHierarchy) {
                        lstLives[i].SetActive(false);
                        Instantiate(lstLifeBreak[i], new Vector2(lstLifeBreak[i].transform.position.x, lstLifeBreak[i].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                    }
                }
                break;
            default:
                for (int i = 4; i >= 0; i--) {
                    if (lstLives[i].activeInHierarchy) {
                        lstLives[i].SetActive(false);
                        Instantiate(lstLifeBreak[i], new Vector2(lstLifeBreak[i].transform.position.x, lstLifeBreak[i].transform.position.y + cam.transform.position.y + lifeBreakOffset), this.transform.rotation);
                    }
                }
                break;
        }
    }

    public void IncLives(int amount) {  //  *   *   *   *   *   *   *   *   *   *   *   *   Display of lives
        if (lives < maxLives) {
            lives += amount;
            switch (lives) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Toggle visibility of lives
                case 5:
                    for (int i = 0; i < (maxLives - 0); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < (maxLives - 1); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < (maxLives - 2); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < (maxLives - 3); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < (maxLives - 4); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                case 0:
                    for (int i = 0; i < (maxLives - 5); i++) {
                        if (!lstLives[i].activeInHierarchy) {
                            lstLives[i].SetActive(true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void Pause() {
        if (!paused) {
            paused = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void MissedEgg() {
        Debug.Log("missed egg");
    }

    public void UpdatePlayer() {//  *   *   *   *   *   *   *   *   *   *   *   *   *   *  Update

        if (paused) {
            paused = false;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (lives < 0) {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Dead loop
            Dead();
            return;
        }
        if (invincibilityTimer < invincibilityTime) {
            invincibilityTimer += Time.deltaTime;
        }
        if (bubbling) {
            bubbleTimer += Time.deltaTime;
            if (bubbleTimer > bubbleTimerMax) {
                bubbling = false;
                bubblesObj.SetActive(false);
            }
        }
        /*if (inTimedArea) {  //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   If in timed area, start timer - die if time runs out
            if (timedTimer < timedTime) {
                timedTimer += Time.deltaTime;
            } else {
                decLives(5);
            }
        }*/

        if (boostSoundTimer >= boostSoundTime) {
            if (playBoost) {
                if (soundMan != null) {
                    soundManScript.PlaySound("boost", false, false, Vector3.zero, 5.0f);
                }
                boostSoundTimer = 0.0f;
                playBoost = false;
            }
        } else {
            boostSoundTimer += Time.deltaTime;
        }

        rigidbodyA.velocity = new Vector2(0.92f * rigidbodyA.velocity.x, rigidbodyA.velocity.y);
        tapJumpTimer += Time.deltaTime;
        NewJump();
    }

    void PlayerBoost() {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Boost player at expense of life
        animator.SetTrigger("Squidjump");
        pointJumps.Add(jumpForce);
        controllerScript.LifeBoost();
        numJumps += 1;
        decLives(1);
    }

    public List<GameObject> GetOtherPoints() { //  *   *   *   *   *   *   *   *   *   *   *   *   Returns a list of collected eggs then clears list
        tmpPointObjs = collectedPointObjs;
        collectedPointObjs = new List<GameObject>();
        return tmpPointObjs;
    }

    void PadHit() { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Boost player on jump pad hit
        animator.SetTrigger("Squidjump");
        numJumps++;
    }

    public bool CanJump() {
        return canJump;
    }

    void SecretGet(GameObject secret) {
        if (soundMan != null) {
            soundManScript.PlaySound("secretget", false, false, Vector3.zero, 0.4f);
        }
        EggCountScript.collectSecret(secret);
        Destroy(secret);
    }

    void LifeGet(GameObject life) {
        if (soundMan != null) {
            soundManScript.PlaySound("lifeget", false, false, Vector3.zero, 0.4f);
        }
        if (lives < maxLives) {
            IncLives(1);
            controllerScript.IncLives(1);
        }
        Destroy(life);
    }

    void PointHit(GameObject point) {
        PointScript pointScript = point.GetComponent<PointScript>();
        if (pointScript.GetBoostVal() != 0) {
            pointJumps.Add(pointScript.GetBoostVal());
            animator.SetTrigger("Squidjump");
        }
        collectedPoints.Add(point);
        controllerScript.AddPoints(pointScript.GetScoreVal(), pointScript.GetMultVal());
        if (EggCount != null) { // Detect if egg, shrimp or secret
            if (point.transform.name.Contains("A Fishy Egg")) {
                EggCountScript.collectEgg(point);
                if (soundMan != null) {
                    soundManScript.PlaySound("eggget", false, false, Vector3.zero, 0.3f);
                }
            } else if (point.transform.name.Contains("shrimp")) {
                EggCountScript.collectShrimp(point);
                if (soundMan != null) {
                    soundManScript.PlaySound("shrimpget", false, false, Vector3.zero, 0.4f);
                }
            } else if (point.transform.name.Contains("Pearl")) {
                if (soundMan != null) {
                    soundManScript.PlaySound("pearlget", false, false, Vector3.zero, 0.2f);
                }
            }
        }
        point.SetActive(false);
    }

    public List<GameObject> GetPoints() { //  *   *   *   *   *   *   *   *   *   *   *   *   Returns a list of collected eggs then clears list
        tmpPoints = collectedPoints;
        collectedPoints = new List<GameObject>();
        return tmpPoints;
    }

    void OnCollisionEnter2D(Collision2D coll) { //  *   *   *   *   *   *   *   *   *   *   Collider hit
        if (!paused) {
            switch (coll.gameObject.tag) {
                case "Bottom":
                    canJump = true;
                    break;
                case "Jumpto Pad":
                    PadHit();
                    break;
                case "Wall":
                    getHit(coll.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   Trigger hit
        if (!paused) {
            switch (coll.gameObject.tag) {
                case "Points":
                    PointHit(coll.gameObject);
                    break;
                case "Life":
                    LifeGet(coll.gameObject);
                    break;
                case "Secret":
                    SecretGet(coll.gameObject);
                    break;
                case "Ship":
                    if (!inShip) {
                        inShip = true;
                    }
                    break;
                case "End":
                    EggCountScript.SaveEggs();
                    EggCountScript.SaveShrimps();
                    EggCountScript.SaveSecrets();
                    controllerScript.End();
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll) {    //  *   *   *   *   *   *   *   *   *   *   Trigger hit
        if (!paused) {
            switch (coll.gameObject.tag) {
                case "Ship":
                    if (inShip) {
                        inShip = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll) {  //  *   *   *   *   *   *   *   *   *   *   Collider exit
        if (!paused) {
            switch (coll.gameObject.tag) {
                case "Bottom":
                    canJump = false;
                    break;
                default:
                    break;
            }
        }
    }

    public string GetDeath() {
        return death;
    }

    public void getHit(GameObject obj) {    //  *   *   *   *   *   *   *   *   *   *   *   Reaction to getting hit
        if (!paused) {
            switch (obj.tag) {
                case "Angular Fish":
                    if (invincibilityTimer >= invincibilityTime) {
                        decLives(10);
                        if (lives <= 0) {
                            death = "puff/ang";
                        }
                        if (obj.transform.localScale.x > 0) {
                            rigidbodyA.AddForce(new Vector2(1000.0f, 0.0f));
                        } else {
                            rigidbodyA.AddForce(new Vector2(-1000.0f, 0.0f));
                        }
                        invincibilityTimer = 0.0f;
                    }
                    break;
                case "Puffer Fish":
                    if (invincibilityTimer >= invincibilityTime) {
                        decLives(2);
                        if(lives <= 0) {
                            death = "puff/ang";
                        }
                        if (obj.transform.localScale.x > 0) {
                            rigidbodyA.AddForce(new Vector2(500.0f, 0.0f));
                        } else {
                            rigidbodyA.AddForce(new Vector2(-500.0f, 0.0f));
                        }
                        invincibilityTimer = 0.0f;
                    }
                    StartBubbleTimer();
                    break;
                case "Wall":
                    if (invincibilityTimer >= invincibilityTime) {
                        if (soundMan != null) {
                            soundManScript.PlaySound("wallhit", false, false, Vector3.zero, 1.0f);
                        }
                        invincibilityTimer = 0.0f;
                        decLives(1);
                    }
                    StartBubbleTimer();
                    break;
            }
        }
    }

    private void StartBubbleTimer() {
        bubblesObj.SetActive(true);
        bubblesObj.transform.Find("Bubbles").GetComponent<BubbleSpawner>().Emit(5);
        bubbling = true;
        bubbleTimer = 0.0f;
    }
}
