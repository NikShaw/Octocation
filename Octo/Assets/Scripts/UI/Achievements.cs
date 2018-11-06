using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Achievements : ScriptableObject {

    private bool newStage = false;
    private int stage = 0;
    private float currVal = 0;
    private string namee = "";
    private string requirement = "";
    private string description = "";
    private List<int> stageReq = new List<int>();
    private GameObject persistentObj;
    private PersistentData persistentDataScript;

    // Set achievement attributes
    public void SetAchievement(float curv, string nam, string req, string desc, List<int> sr) {
        stageReq = sr;
        description = desc;
        currVal = curv;
        namee = nam;
        requirement = req;
        for (int i = 0; i < stageReq.Count; i++) {
            if (currVal > stageReq[i]) {
                stage = i + 1;
            }
        }
    }

    public string GetDesc() {
        return description;
    }

    public string GetReq() {
        return requirement;
    }

    // Add to achievement value
    public void AddValue(float v) {
        currVal += v;
        for (int i = 0; i < stageReq.Count; i++) {
            if (currVal > stageReq[i]) {
                stage = i + 1;
                newStage = true;
            }
        }
    }

    public void StaticValue(float v) {
        currVal = v;
    }
    public void StaticStageReq(int v) {
        stageReq[0] = v;
    }

    // Set achievement value
    public void SetValue(float v) {
        currVal = v;
        if ((requirement == "beattime") || (requirement == "beatourtime")) {
            for (int i = 0; i < stageReq.Count; i++) {
                if (currVal < stageReq[i]) {
                    stage = i + 1;
                    newStage = true;
                }
            }
        } else {
            for (int i = 0; i < stageReq.Count; i++) {
                if (currVal > stageReq[i]) {
                    stage = i + 1;
                    newStage = true;
                }
            }
        }
    }

    // Get value of next stage
    public float GetNextStageVal() {
        if ((stage) < GetMaxStage()) {
            return stageReq[stage];
        } else {
            return stageReq[stage-1];
        }
    }
    public float GetThisStageVal() {
        return stageReq[stage];
    }

    public int GetMaxStage() {
        return stageReq.Count;
    }

    public List<int> GetStageReq() {
        return stageReq;
    }

    public bool IsNewStage() {
        if (newStage) {
            newStage = false;
            return true;
        }
        return false;
    }

    public float GetValue() {
        return currVal;
    }

    public string GetRequirement() {
        return requirement;
    }

    public string GetName() {
        return namee;
    }

    public int GetStage() {
        return stage;
    }
}
