using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {
    
    private int vacations = 0;
    private int gold = 0;
    private int totalEggs = 0;
    private int totalShrimp = 0;
    private int totalScore = 0;
    private int totalSecrets = 0;
    private int useTilt = 1;
    private int mute = 0;
    private int collectedEggs = 0;
    private int collectedShrimps = 0;
    private int collectedSecrets = 0;
    private int collectedSessEggs = 0;
    private int collectedSessShrimps = 0;
    private int score = 0;
    private int time = 1000;
    private int devtime = 247;
    private int deaths = 0;
    private int nearPuffs = 0;
    private int nearAngle = 0;
    private bool firstTimeLaunch = true;
    private bool gotTotalShrimp = false;
    private bool gotTotalSecrets = false;
    private bool gotTotalEggs = false;
    public List<int> collectedPoints = new List<int>();
    private List<int> collectedPointSecrets = new List<int>();
    public List<int> collectedPointShrimps = new List<int>();
    private List<Achievements> achieveList = new List<Achievements>();
    public static PersistentData Instance;

    // Keep single copy of persistent data, does not destroy on load
    void Awake() {
        if (Instance) {
            DestroyImmediate(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    // Get saved tilt/mute settings
    public void setTilt() {
        if (useTilt == 1) {
            useTilt = 0;
        } else {
            useTilt = 1;
        }
        PlayerPrefs.SetInt("tilt", useTilt);
    }

    public void setMute() {
        if (mute == 1) {
            mute = 0;
        } else {
            mute = 1;
        }
        PlayerPrefs.SetInt("mute", mute);
    }

    // Set player time
    public void SetTime(int t) {
        if (t < time) {
            time = t;
            PlayerPrefs.SetInt("time", time);
        }
    }

    // Get/set doubloons
    public void AddGold(int g) {
        gold += g;
    }

    public int GetGold() {
        return gold;
    }

    public int GetTime() {
        return time;
    }

    public int GetDevTime() {
        return devtime;
    }

    public int getTilt() {
        return useTilt;
    }

    public int getMute() {
        return mute;
    }

    // Save player score
    public bool SetScore(int scre) {
        totalScore += scre;
        PlayerPrefs.SetInt("totalscore", totalScore);
        if (scre > score) {
            score = scre;
            PlayerPrefs.SetInt("score", score);
            return true;
        }
        return false;
    }

    public int GetScore() {
        score = PlayerPrefs.GetInt("score");
        return score;
    }

    // Set number of eggs/shrimp/secrets
    public void setPointSize(int size) {
        if ((collectedPoints == null) || (collectedPoints.Count != size)) {
            collectedPoints.Clear();
            for (int i = 0; i < size; i++) {
                collectedPoints.Add(0);
            }
        }
    }

    public void setPointShrimpSize(int size) {
        if ((collectedPointShrimps == null) || (collectedPointShrimps.Count != size)) {
            collectedPointShrimps.Clear();
            for (int i = 0; i < size; i++) {
                collectedPointShrimps.Add(0);
            }
        }
    }

    public void setPointSecretSize(int size) {
        if ((collectedPointSecrets == null) || (collectedPointSecrets.Count != size)) {
            collectedPointSecrets.Clear();
            for (int i = 0; i < size; i++) {
                collectedPointSecrets.Add(0);
            }
        }
    }

    public List<int> getPoints() {
        return collectedPoints;
    }

    public List<int> getPointShrimps() {
        return collectedPointShrimps;
    }
    public List<int> getPointSecrets() {
        return collectedPointSecrets;
    }

    public int GetTotalEggs() {
        return totalEggs;
    }

    public int GetTotalScore() {
        return totalScore;
    }

    public int GetTotalShrimp() {
        return totalShrimp;
    }

    // Returns number of eggs/shrimp/secrets and list of which is collected
    public Vector2 GetNumEggs() {
        collectedEggs = 0;
        for (int i = 0; i < collectedPoints.Count; i++) {
            if (collectedPoints[i] == 1) {
                collectedEggs++;
            }
        }
        if (collectedPoints.Count == 1) {
            return new Vector2(680, collectedEggs);
        } else {
            return new Vector2(collectedPoints.Count, collectedEggs);
        }
    }
    public Vector2 GetNumShrimps() {
        collectedShrimps = 0;
        for (int i = 0; i < collectedPointShrimps.Count; i++) {
            if (collectedPointShrimps[i] == 1) {
                collectedShrimps++;
            }
        }
        if (collectedPoints.Count == 1) {
            return new Vector2(268, collectedShrimps);
        } else {
            return new Vector2(collectedPointShrimps.Count, collectedShrimps);
        }
    }

    public Vector2 GetNumSecrets() {
        collectedSecrets = 0;
        for (int i = 0; i < collectedPointSecrets.Count; i++) {
            if (collectedPointSecrets[i] == 1) {
                collectedSecrets++;
            }
        }
        return new Vector2(collectedPointSecrets.Count, collectedSecrets);
    }

    public int GetDeaths() {
        return deaths;
    }

    public void AddDeaths(int d) {
        deaths += d;
        PlayerPrefs.SetInt("dead", deaths);
    }

    public void AddNearPuffs(int d) {
        nearPuffs += d;
        PlayerPrefs.SetInt("puffnear", nearPuffs);
    }

    public int GetNearPuffs() {
        return nearPuffs;
    }

    public void AddNearAngle(int d) {
        nearAngle += d;
        PlayerPrefs.SetInt("anglenear", nearAngle);
    }

    public int GetNearAngles() {
        return nearAngle;
    }

    public int GetVacations() {
        return vacations;
    }

    public void AddVacation() {
        vacations += 1;
        PlayerPrefs.SetInt("vacations", vacations);
    }

    public void CollectEgg() {
        collectedSessEggs++;
        totalEggs++;
    }

    public void CollectSecret() {
        totalSecrets++;
    }

    public void CollectShrimp() {
        collectedSessShrimps++;
        totalShrimp++;
    }

    // Set egg/shrimp/secret collected, set achievement and save
    public void SetPoints(List<int> points) {
        int tempcollected = 0;
        collectedPoints = points;
        string pointStr = "";
        for (int i = 0; i < points.Count; i++) {
            if (points[i] == 1) {
                tempcollected++;
            }
            if (i != (points.Count - 1)) {
                pointStr += points[i].ToString() + "*";
            } else {
                pointStr += points[i].ToString();
            }
        }
        for (int i = 0; i < achieveList.Count; i++) {
            switch (achieveList[i].GetReq()) {
                case "uniqueeggget":
                    achieveList[i].SetValue(tempcollected);
                    break;
            }
        }
        gotTotalEggs = true;
        PlayerPrefs.SetString("EggPoints", pointStr);
        PlayerPrefs.SetInt("totaleggs", totalEggs);
    }

    public void SetShrimps(List<int> points) {
        int tempcollected = 0;
        collectedPointShrimps = points;
        string pointStr = "";
        for (int i = 0; i < points.Count; i++) {
            if (points[i] == 1) {
                tempcollected++;
            }
            if (i != (points.Count - 1)) {
                pointStr += points[i].ToString() + "*";
            } else {
                pointStr += points[i].ToString();
            }
        }
        for (int i = 0; i < achieveList.Count; i++) {
            switch (achieveList[i].GetReq()) {
                case "uniqueshrimpget":
                    achieveList[i].SetValue(tempcollected);
                    break;
            }
        }
        gotTotalShrimp = true;
        PlayerPrefs.SetInt("totalshrimp", totalShrimp);
        PlayerPrefs.SetString("ShrimpPoints", pointStr);
    }

    public void SetSecrets(List<int> points) {
        int tempcollected = 0;
        collectedPointSecrets = points;
        string pointStr = "";
        for (int i = 0; i < points.Count; i++) {
            if (points[i] == 1) {
                tempcollected++;
            }
            if (i != (points.Count - 1)) {
                pointStr += points[i].ToString() + "*";
            } else {
                pointStr += points[i].ToString();
            }
        }
        for (int i = 0; i < achieveList.Count; i++) {
            switch (achieveList[i].GetReq()) {
                case "secrets":
                    achieveList[i].SetValue(tempcollected);
                    break;
            }
        }
        gotTotalSecrets = true;
        PlayerPrefs.SetInt("totalsecrets", totalSecrets);
        PlayerPrefs.SetString("SecretPoints", pointStr);
    }

    // Load eggs/shrimp/secrets collected
    void LoadPoints() {
        collectedPoints.Clear();
        string pointStr = PlayerPrefs.GetString("EggPoints");
        string[] pointStrArr = pointStr.Split('*');
        for (int i = 0; i < pointStrArr.Length; i++) {
            if (pointStrArr[i] == "1") {
                collectedPoints.Add(1);
                collectedEggs++;
            } else {
                collectedPoints.Add(0);
            }
        }
        collectedPointShrimps.Clear();
        string pointStrShr = PlayerPrefs.GetString("ShrimpPoints");
        string[] pointStrShrArr = pointStrShr.Split('*');
        for (int i = 0; i < pointStrShrArr.Length; i++) {
            if (pointStrShrArr[i] == "1") {
                collectedPointShrimps.Add(1);
                collectedShrimps++;
            } else {
                collectedPointShrimps.Add(0);
            }
        }
        collectedPointSecrets.Clear();
        string pointStrScr = PlayerPrefs.GetString("SecretPoints");
        string[] pointStrScrArr = pointStrScr.Split('*');
        for (int i = 0; i < pointStrScrArr.Length; i++) {
            if (pointStrScrArr[i] == "1") {
                collectedPointSecrets.Add(1);
                collectedSecrets++;
            } else {
                collectedPointSecrets.Add(0);
            }
        }
    }

    // Set achievement values and save
    public void SetAchievements(List<Achievements> a) {
        for (int j = 0; j < a.Count; j++) {
            switch (a[j].GetReq()) {
                case "puff/angdead":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "deaths":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "boost":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "lifeget":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "secrets":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "complete":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "goldget":
                    if (a[j].GetValue() > achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "beatourtime":
                    if (a[j].GetValue() < achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "beattime":
                    if (a[j].GetValue() < achieveList[j].GetValue()) {
                        achieveList[j] = a[j];
                    }
                    break;
                case "scoreget":
                    achieveList[j].SetValue(score);
                    break;
                case "dodge":
                    achieveList[j].SetValue(nearAngle + nearPuffs);
                    break;
            }
        }
        for (int i = 0; i < achieveList.Count; i++) {
            switch (achieveList[i].GetReq()) {
                case "scoreget":
                    achieveList[i].SetValue(score);
                    break;
                case "dodge":
                    achieveList[i].SetValue(nearAngle + nearPuffs);
                    break;
            }
        }
        SaveAchievements();
    }

    public List<Achievements> GetAchievements() {
        return achieveList;
    }

    // Save total shrimp/eggs/secrets collected when ready
    void Update() {
        if((gotTotalShrimp) && (gotTotalEggs) && (gotTotalSecrets)) {
            gotTotalShrimp = false;
            gotTotalEggs = false;
            gotTotalSecrets = false;
            for (int i = 0; i < achieveList.Count; i++) {
                switch (achieveList[i].GetReq()) {
                    case "eggshrimpget":
                        achieveList[i].SetValue(totalEggs + totalShrimp);
                        break;
                }
            }
            SaveAchievements();
        }
    }
    
    public bool FirstLaunch() {
        if (firstTimeLaunch) {
            firstTimeLaunch = false;
            PlayerPrefs.SetInt("played", 1);
            return true;
        } else {
            return false;
        }
    }

    // Load all the things
	void Start () {
        //PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this.gameObject);
        LoadPoints();
        if (PlayerPrefs.GetInt("played") == 1){
            firstTimeLaunch = false;

        }else {
            PlayerPrefs.SetInt("played", 1);
        }
        vacations = PlayerPrefs.GetInt("vacations");
        totalEggs = PlayerPrefs.GetInt("totaleggs");
        totalShrimp = PlayerPrefs.GetInt("totalshrimp");
        totalScore = PlayerPrefs.GetInt("totalscore");
        totalSecrets = PlayerPrefs.GetInt("totalsecrets");
        deaths = PlayerPrefs.GetInt("dead");
        useTilt = PlayerPrefs.GetInt("tilt");	
        mute = PlayerPrefs.GetInt("mute");
        score = PlayerPrefs.GetInt("score");
        nearPuffs = PlayerPrefs.GetInt("puffnear");
        nearAngle = PlayerPrefs.GetInt("anglenear");
        if (PlayerPrefs.GetInt("time") != 0) {
            time = PlayerPrefs.GetInt("time");
        }
        if (PlayerPrefs.GetInt("devtime") != 0) {
            devtime = PlayerPrefs.GetInt("devtime");
        }
        if (firstTimeLaunch) {
            mute = 1;
            useTilt = 1;
            SetAchievements();
        } else {
            LoadAchievements();
        }
    }

    // Save all the achievements as string in playerprefs
    public void SaveAchievements() {
        string achieveStr = "";
        for(int i = 0; i < achieveList.Count; i++) {
            achieveStr += achieveList[i].GetValue().ToString();
            achieveStr += '-';
            achieveStr += achieveList[i].GetName();
            achieveStr += '-';
            achieveStr += achieveList[i].GetRequirement();
            achieveStr += '-';
            achieveStr += achieveList[i].GetDesc();
            List<int> tempLst = new List<int>();
            tempLst = achieveList[i].GetStageReq();
            for(int j = 0; j < tempLst.Count; j++) {
                achieveStr += '-';
                achieveStr += tempLst[j].ToString();
            }
            if(i < (achieveList.Count - 1)) {
                achieveStr += '*';
            }
        }
        PlayerPrefs.SetString("achievements", achieveStr);
    }

    // Load all achievements from string in playerprefs
    public void LoadAchievements() {
        string achieveStr = PlayerPrefs.GetString("achievements");
        string[] achieveStrSpl = achieveStr.Split('*');
        for(int i = 0; i < achieveStrSpl.Length; i++) {
            string[] achieveStrComps = achieveStrSpl[i].Split('-');
            List<int> achieveReqs = new List<int>();
            for(int k = 4; k < (achieveStrComps.Length); k++) {
                int tempint = 0;
                if (int.TryParse(achieveStrComps[k], out tempint)) {
                    achieveReqs.Add(tempint);
                }
                else{
                    Debug.Log("tryintnope");
                    break;
                }
            }
            Achievements ach = ScriptableObject.CreateInstance("Achievements") as Achievements;
            float parse;
            if (float.TryParse(achieveStrComps[0], out parse))
            {
            }
            else
            {
                Debug.Log("achfailed");
            }
            ach.SetAchievement(parse, achieveStrComps[1], achieveStrComps[2], achieveStrComps[3], achieveReqs);
            achieveList.Add(ach);
        }
    }

    // Set initial achievements and values
    public void SetAchievements() {
        Achievements achieve0 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve0Reqs = new List<int>();
        achieve0Reqs.Add(10);
        achieve0Reqs.Add(50);
        achieve0Reqs.Add(200);
        achieve0.SetAchievement(0, "Deep Doom", "deaths", "Let the poor innocent octopus \ndie multiple times! \n(Obtain 10/50/200 deaths)",achieve0Reqs);
        achieveList.Add(achieve0);

        Achievements achieve1 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve1Reqs = new List<int>();
        achieve1Reqs.Add(10);
        achieve1Reqs.Add(50);
        achieve1Reqs.Add(250);
        achieve1.SetAchievement(0, "Safeguard", "boost", "Boost at the cost of your life! \n(Boost 10/50/250 times)",achieve1Reqs);
        achieveList.Add(achieve1);

        Achievements achieve2 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve2Reqs = new List<int>();
        achieve2Reqs.Add(5);
        achieve2Reqs.Add(25);
        achieve2Reqs.Add(100);
        achieve2.SetAchievement(0, "Lunch Tag", "puff/angdead", "The creatures of the deep \ngot to you! \n(Obtain 5/25/100 deaths to fish)", achieve2Reqs);
        achieveList.Add(achieve2);

        Achievements achieve3 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve3Reqs = new List<int>();
        achieve3Reqs.Add(1);
        achieve3Reqs.Add(5);
        achieve3Reqs.Add(25);
        achieve3.SetAchievement(0, "Completionist!", "complete", "Finish the game! \n(Finish the game 1/5/25 times)",achieve3Reqs);
        achieveList.Add(achieve3);

        Achievements achieve4 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve4Reqs = new List<int>();
        achieve4Reqs.Add(100);
        achieve4Reqs.Add(1000);
        achieve4Reqs.Add(5000);
        achieve4.SetAchievement(0, "Collector!", "eggshrimpget", "Collect shrimp and eggs! \n(Collect 100/1000/5000 total)",achieve4Reqs);
        achieveList.Add(achieve4);

        Achievements achieve5 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve5Reqs = new List<int>();
        achieve5Reqs.Add(25);
        achieve5Reqs.Add(75);
        achieve5Reqs.Add(150);
        achieve5.SetAchievement(0, "Buffet", "uniqueshrimpget", "Can you collect all of the \nshrimp? \n(Collect 25/75/150)", achieve5Reqs);
        achieveList.Add(achieve5);

        Achievements achieve6 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve6Reqs = new List<int>();
        achieve6Reqs.Add(100);
        achieve6Reqs.Add(250);
        achieve6Reqs.Add(500);
        achieve6.SetAchievement(0, "Collector's Edition", "uniqueeggget", "Can you collect all of the \neggs? \n(Collect 100/250/500)", achieve6Reqs);
        achieveList.Add(achieve6);

        Achievements achieve7 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve7Reqs = new List<int>();
        achieve7Reqs.Add(50);
        achieve7Reqs.Add(250);
        achieve7Reqs.Add(1000);
        achieve7.SetAchievement(0, "Revitalized", "lifeget", "Replenish your life! \n(Collect 50/250/1000)", achieve7Reqs);
        achieveList.Add(achieve7);

        Achievements achieve8 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve8Reqs = new List<int>();
        achieve8Reqs.Add(10000);
        achieve8Reqs.Add(100000);
        achieve8Reqs.Add(500000);
        achieve8.SetAchievement(0, "Omnom", "scoreget", "Can you get the most score? \n(Get 10,000/100,000/500,000 total)",achieve8Reqs);
        achieveList.Add(achieve8);

        Achievements achieve9 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve9Reqs = new List<int>();
        achieve9Reqs.Add(time);
        achieve9.SetAchievement(0, "Racer (secs)", "beattime", "How fast can you go? \n(Beat your previous time)",achieve9Reqs);
        achieveList.Add(achieve9);

        Achievements achieve10 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve10Reqs = new List<int>();
        achieve10Reqs.Add(devtime);
        achieve10.SetAchievement(0, "???", "beatourtime", "Mystery Achievemnt! \n(???)",achieve10Reqs);
        achieveList.Add(achieve10);

        Achievements achieve11 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve11Reqs = new List<int>();
        achieve11Reqs.Add(10);
        achieve11Reqs.Add(25);
        achieve11Reqs.Add(100);
        achieve11.SetAchievement(0, "Slick Octopus", "dodge", "Those are some close calls! \n(Get 10/25/100 near misses)",achieve11Reqs);
        achieveList.Add(achieve11);

        Achievements achieve12 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve12Reqs = new List<int>();
        achieve12Reqs.Add(2);
        achieve12Reqs.Add(5);
        achieve12Reqs.Add(10);
        achieve12.SetAchievement(0, "Explorer", "secrets", "Can you find these hidden \nsecrets? \n(Find 2/5/10 secrets)", achieve12Reqs);
        achieveList.Add(achieve12);

        Achievements achieve13 = ScriptableObject.CreateInstance("Achievements") as Achievements;
        List<int> achieve13Reqs = new List<int>();
        achieve13Reqs.Add(100);
        achieve13Reqs.Add(750);
        achieve13Reqs.Add(2500);
        achieve13.SetAchievement(0, "Doubloons!", "goldget", "Collect gold when you \nget to your vacation! \n(Collect 100/750/2500 gold)", achieve13Reqs);
        achieveList.Add(achieve13);
        SaveAchievements();

    }
}
