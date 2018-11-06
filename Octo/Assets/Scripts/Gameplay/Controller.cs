using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    [Header("Settings")]
    public int maxMult = 8;
    public int scorePerGold = 100;
    public float addScoreTime = 0.05f;
    public float multiplierFalloff = 4f / 5f;
    public int eggPerMultiplier = 5;
    public float eggDeleteOffset = 10.0f;
    public int score = 0;
    private int goldCoins = 0;
    private int multiplier = 1;
    private int streak = 0;
    private int currStreak = 0;
    private int scoreToAdd = 0;
    public int pearlWorth = 50;
    public int eggWorth = 50;
    public float halpTime = 5.0f;
    public float deadTime = 1.7f;
    private float deadTimer = 0.0f;

    [Header("Misc")]
    public int maxScore = 0;
    private float initMultScale;
    public bool paused = false;
    private bool started = false;
    public bool helping = false;
    public float targetRatio = 9f / 16f;
    private int maxMultSprites = 0;
    private bool playerDead = false;
    private bool END = false;
    private int prevMult = 0;

    [Header("Timers")]
    private float addScoreTimer = 0.0f;
    public float initialMultiplierTimer = 10.0f;
    private float multiplierTimer = 0.0f;
    public float timer = 0.0f;
    private float unpauseTimer = 0.0f;
    private float unpauseTime = 0.5f;

    [Header("Unity Objects")]
    public GameObject player;
    public GameObject pauseBtn;
    public GameObject scoreObj;
    public GameObject timeObj;
    public GameObject multObj;
    public GameObject multScale;
    public GameObject HALP;
    public HALPScript HALPScript;
    public GameObject endCoinObj;
    public GameObject deadScreen;
    public List<GameObject> endAnimObjs = new List<GameObject>();
    private List<Animator> endAnimAnimators = new List<Animator>();
    private GameObject SoundMan;
    private List<Vector3> velocities = new List<Vector3>();
    public List<GameObject> pointsList = new List<GameObject>();
    public List<GameObject> secretsList = new List<GameObject>();
    public List<GameObject> eggDelList = new List<GameObject>();
    public List<GameObject> pearlDelList = new List<GameObject>();
    public List<GameObject> pointDelList = new List<GameObject>();
    public List<PointScript> pointsScripts = new List<PointScript>();
    private TextMesh multTxt;
    private TextMesh scoreTxt;
    private TextMesh timeTxt;
    private Player playerScript;
    private Controls controls;
    private CircleCollider2D[] objects;
    private Rigidbody2D[] rigidbodies;
    private SpriteRenderer multSpriteRenderer;
    public List<Sprite> multSprites = new List<Sprite>();
    private SoundManager SoundManScript;
    private GameObject persistentObj;
    private PersistentData persistentDataScript;
    private List<Achievements> achievements = new List<Achievements>();

    void Start () { //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   Init
        for(int i = 0; i < endAnimObjs.Count; i++) {
            endAnimAnimators.Add(endAnimObjs[i].GetComponent<Animator>());
        }

        SoundMan = GameObject.Find("Sound Manager");
        if (SoundMan != null) {
            SoundManScript = SoundMan.GetComponent<SoundManager>();
        }
        HALPScript = HALP.GetComponent<HALPScript>();
        multSpriteRenderer = multScale.GetComponent<SpriteRenderer>();
        maxMultSprites = multSprites.Count;
        controls = this.gameObject.GetComponent<Controls>();
        playerScript = player.GetComponent<Player>();
        multTxt = multObj.GetComponent<TextMesh>();
        scoreTxt = scoreObj.GetComponent<TextMesh>();
        timeTxt = timeObj.GetComponent<TextMesh>();
        objects = FindObjectsOfType(typeof(CircleCollider2D)) as CircleCollider2D[];
        for (int i = 0; i < objects.Length; i++) {  // Loop through objects, find points or secrets
            if (objects[i].gameObject.transform.tag == "Points") {
                pointsList.Add(objects[i].gameObject);
                pointsScripts.Add(objects[i].gameObject.GetComponent<PointScript>());
            }
            if (objects[i].gameObject.transform.tag == "Secret") {
                secretsList.Add(objects[i].gameObject);
            }
        }
        persistentObj = GameObject.Find("Persistent Data");
        if (persistentObj != null) {
            persistentDataScript = persistentObj.GetComponent<PersistentData>();
            achievements = persistentDataScript.GetAchievements();
            maxScore = persistentDataScript.GetScore();
            helping = persistentDataScript.FirstLaunch();
            if (helping) {
                StartHalp();
            }
        }
        SoundManScript.PlaySound("beachfront", true, true, Vector3.zero, 0.5f);
    }

    public void SaveEggShrimp() {
        persistentDataScript.SetAchievements(achievements);
    }

    public void End() { // If player reaches end
        goldCoins = score / scorePerGold;
        SoundManScript.PlaySound("win", false, false, Vector2.zero, 0.6f);
        endCoinObj.GetComponent<GoldSpawner>().SetSpawnLimit(goldCoins);
        persistentDataScript.AddVacation();
        persistentDataScript.SetScore(score);
        for (int i = 0; i < achievements.Count; i++) {  // Update achievements
            switch (achievements[i].GetReq()) {
                case "beattime":
                    if(persistentDataScript.GetTime() == 1000) {
                        achievements[i].StaticValue(timer);
                        achievements[i].StaticStageReq((int)timer);
                        persistentDataScript.SetTime((int)timer);
                    } else if (timer < persistentDataScript.GetTime()) {
                        achievements[i].SetValue(timer);
                        persistentDataScript.SetTime((int)timer);
                    }
                    break;
                case "beatourtime":
                    if (timer < persistentDataScript.GetDevTime()) {
                        achievements[i].SetValue(timer);
                        List<int> achieve10Reqs = new List<int>();
                        achieve10Reqs.Add(0);
                        achievements[i].SetAchievement(0, "Beat The Devs!", "beatourtime", "You beat our time! \n(" + persistentDataScript.GetDevTime() + ")", achieve10Reqs);
                    }
                    break;
                case "goldget":
                    achievements[i].SetValue(goldCoins);
                    break;
                case "complete":
                    achievements[i].AddValue(1);
                    break;
            }
        }
        for (int i = 0; i < endAnimAnimators.Count; i++) {
            endAnimAnimators[i].enabled = true;
        }
        SoundManScript.PlaySound("chestopen", false, false, Vector3.zero, 0.7f);
        persistentDataScript.SetAchievements(achievements);
        END = true;
    }

    public void GetSecret() {   // If player gets secret
        for (int i = 0; i < achievements.Count; i++) {
            switch (achievements[i].GetReq()) {
                case "secrets":
                    achievements[i].AddValue(1);
                    break;
            }
        }
    }

    public void IncLives(int l) {
        for (int i = 0; i < achievements.Count; i++) {
            switch (achievements[i].GetReq()) {
                case "lifeget":
                    achievements[i].AddValue(l);
                    break;
            }
        }
    }

    public void LifeBoost() {
        for (int i = 0; i < achievements.Count; i++) {
            switch (achievements[i].GetReq()) {
                case "boost":
                    achievements[i].AddValue(1);
                    break;
            }
        }
    }

	void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (!END) { 
            if (controls.PausePressed()) {  //  .   .   .   .   .   .   .   .   .   Check if paused pressed
                if (!paused) {  //  .   .   .   .   .   .   .   .   .   .   .   .   Pause - freeze all rigidbodies
                    Pause();
                }
            } else {
                if (paused) {
                    if (!helping) {
                        Unpause();
                    }
                }
            }
            if (paused) {
                if (helping) {
                    PlayerControls();
                }
                playerScript.Pause();
            } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   Check if player is dead
                if (playerScript.GetLives() < 0) {
                    if (!playerDead) {
                        playerDead = true;
                        if (paused) {
                            pauseBtn.GetComponent<ButtonScript>().DoMouseUp();
                            paused = false;
                        }
                        pauseBtn.SetActive(false);
                        SoundManScript.PlaySound("fail", false, false, Vector2.zero, 0.4f);
                        for (int i = 0; i < achievements.Count; i++) {
                            switch (achievements[i].GetReq()) {
                                case "deaths":
                                    achievements[i].AddValue(1);
                                    break;
                                case "puff/angdead":
                                    if (playerScript.GetDeath() == "puff/ang") {
                                        achievements[i].AddValue(1);
                                    }
                                    break;
                            }
                        }
                        persistentDataScript.AddDeaths(1);
                        if (persistentDataScript.SetScore(score)) {
                        } else {
                        }
                        persistentDataScript.SetAchievements(achievements);
                    }
                    PlayerDead();
                } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   .   Activate controls after unpause
                    if (unpauseTimer < unpauseTime) {
                        unpauseTimer += Time.deltaTime;
                        controls.JumpPressed();
                    } else {
                        PlayerControls();
                        if (!helping) {
                            playerScript.UpdatePlayer();
                        }
                    }
                    if ((started) && (!helping)) {  //  .   .   .   .   .   .   .   .   .   .   .   Count score
                        MultiplierUpdate();
                        AddScore();
                        timer += Time.deltaTime;
                        timeTxt.text = "Time:\n" + (int)timer;
                    }
                }
            }
        }
    }

    private void AddScore() {   //  *   *   *   *   *   *   *   *   *   *   *   Add score smoothly
        if (scoreToAdd > 0) {
            float scoreLimit = 0;
            if (scoreToAdd <= 4) {  //  .   .   .   .   .   .   .   .   .   .   Sets limit for how slow score is added
                scoreLimit = addScoreTime;
            } else {
                scoreLimit = (addScoreTime / (scoreToAdd));
            }
            if (addScoreTimer >= scoreLimit) {  //  .   .   .   .   .   .   .   Adds score and sets score text
                int tempScore = (scoreToAdd / 10) * 2;
                if (tempScore > 0) {

                    score += tempScore;
                    scoreToAdd -= tempScore;
                } else {
                    scoreToAdd -= 1;
                    score += 1;
                }
                SoundManScript.PlaySound("scoreget", false, false, Vector3.zero, 0.5f);
                scoreTxt.text = score.ToString();
                addScoreTimer = 0;
            }
            addScoreTimer += Time.deltaTime;
        }
    }

    public void MissedPoint() {
        playerScript.MissedEgg();
        streak = 0;
    }

    public void AddPoints(int p, int multVal) {
        if (multVal > 0) {
            currStreak += (eggPerMultiplier * multVal);
            streak += (eggPerMultiplier * multVal);
        } else {
            currStreak++; streak++;
        }
        AddMultiplier(p);
    }

    private void CountPoints() {
        pointDelList = playerScript.GetPoints();
        if (pointDelList.Count > 0) {
            for (int j = 0; j < pointDelList.Count; j++) {    //  .   .   .   .   Loop through collected eggs and delete
                if (pointsList.Contains(pointDelList[j])) {
                    int loc = pointsList.IndexOf(pointDelList[j]);
                    int mult = pointsScripts[loc].GetMultVal();
                    Destroy(pointDelList[j]);
                    if (pointsScripts[loc].GetMultVal() > 0) {
                        currStreak += (eggPerMultiplier * mult);
                        streak += (eggPerMultiplier * mult);
                    } else {
                        currStreak++; streak++;
                    }
                    AddMultiplier(pointsScripts[loc].GetScoreVal());
                    pointsList.Remove(pointDelList[j]);
                    pointsScripts.Remove(pointsScripts[loc]);
                } 
            }
        }
    }

    private void AddMultiplier(int sc) {
        if (multiplier > 1) {   //  .   .   .   .   .   .   .   .   .   Sets multiplier based on how many eggs are collected
            if(((currStreak/eggPerMultiplier + 1) > multiplier) && ((currStreak / eggPerMultiplier + 1) < maxMult)) {
            }
            multiplier = (currStreak / eggPerMultiplier) + 1;

            if (multiplier > maxMult) {
                multiplier = maxMult;
            }

            if ((prevMult < 8) && (multiplier == maxMult)) {
                if (SoundMan != null) {
                    SoundManScript.PlaySound("octocombo", false, false, Vector3.zero, 0.6f);
                }
            }
            prevMult = multiplier;
        } else {    //  .   .   .   .   .   .   .   .   .   .   .   .   Starts multiplier
            multiplier = (streak / eggPerMultiplier) + 1;
            currStreak = multiplier * eggPerMultiplier;
        }
        scoreToAdd += sc * multiplier;
        multTxt.text = "x" + multiplier.ToString();
        multiplierTimer = (initialMultiplierTimer / (multiplierFalloff * multiplier));
    }

    private void MultiplierUpdate() {
        if (multiplier > 1) {   //  .   .   .   .   .   .   .   .   .   .   .   Timer for multiplier and sets scale of multiplier bar
            multiplierTimer -= Time.deltaTime;
            if ((int)(maxMultSprites - 0.2f - (maxMultSprites * (multiplierTimer / ((initialMultiplierTimer / (multiplierFalloff * multiplier)))))) < maxMultSprites) {
                if (multSpriteRenderer.sprite != multSprites[(int)(maxMultSprites - 0.2f - (maxMultSprites * (multiplierTimer / ((initialMultiplierTimer / (multiplierFalloff * multiplier))))))]) {
                    multSpriteRenderer.sprite = multSprites[(int)(maxMultSprites - 0.2f - (maxMultSprites * (multiplierTimer / (initialMultiplierTimer / (multiplierFalloff * multiplier)))))];
                }
            }
            if (multiplierTimer <= 0) {
                multiplier = 1;
                multiplierTimer = initialMultiplierTimer;
                multTxt.text = "x" + multiplier.ToString();
                currStreak = 0;
                streak = 0;
            }
        } else {
            if(multSpriteRenderer.sprite != multSprites[0]) {
                multSpriteRenderer.sprite = multSprites[0];
            }
        }
    }

    private void Pause() {
        velocities.Clear();
        rigidbodies = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
        for (int i = 0; i < rigidbodies.Length; i++) {
            velocities.Add(rigidbodies[i].velocity);
            rigidbodies[i].isKinematic = true;
        }
        paused = true;
        unpauseTimer = 0.0f;
    }

    private void Unpause() {
        if ((paused) && (!helping)) {   //  .   .   .   .   .   .   .   .   .   .   .   .   Unpause - unfreeze all rigidbodies
            for (int i = 0; i < rigidbodies.Length; i++) {
                rigidbodies[i].isKinematic = false;
                rigidbodies[i].velocity = velocities[i];
            }
            paused = false;
        }
    }

    public void StartHalp() {
        HALPScript.Reset();
        HALPScript.ShowNext();
        helping = true;
    }

    private void PlayerDead() {
        if(deadTimer >= deadTime) {
            if (!deadScreen.activeSelf) {
                deadScreen.SetActive(true);
            }
        } else {
            deadTimer += Time.deltaTime;
        }
        playerScript.UpdatePlayer();
    }

    private void PlayerControls() {
        if (!helping) {
            if (player.transform.position.y < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y - 2.0f)) {
                playerScript.decLives(10);
            }
            if (playerScript.CanJump()) { //    .   .   .   .   .   .   .   .   .   If on platform
                if (controls.JumpPressed()) {
                    playerScript.StartJump(true);
                    started = true;
                }
            } else if (controls.JumpPressed()) {    //  .   .   .   .   .   .   .   Use life to jump
                if (playerScript.GetLives() > 0) {
                    playerScript.StartJump(false);
                }
            } else { //  .   .   .   .   .   .   .   .   .   

                if (controls.LeftPressed() > 0.0f) {//  .   .   .   .   .   .   .   Move left
                    playerScript.MoveLeft(controls.LeftPressed());
                }
                if (controls.RightPressed() > 0.0f) {// .   .   .   .   .   .   .   Move right
                    playerScript.MoveRight(controls.RightPressed());
                }
            }
        } else {
            if (HALPScript.Done()) {
                helping = false;
                Unpause();
            }
        }
    }
}
