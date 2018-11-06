using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour {

    public RawImage img;
    public float offset = 0.8f;
    public float yoffset = 0.4f;
    public Vector2 yLimit = new Vector2(0.0f, -2.0f);
    public GameObject achievementObj;
    private GameObject progObj;
    private GameObject persistentObj;
    private PersistentData persistentDataScript;
    List<Achievements> achieveLst = new List<Achievements>();
    List<GameObject> achieveObjLst = new List<GameObject>();
    public List<Sprite> awardSprites;
    
    void Start () {
        yLimit = new Vector2(yLimit.x + 5.0f, yLimit.y - 5.0f);
        offset += 5.0f;
        yoffset *= 5.0f;
        persistentObj = GameObject.Find("Persistent Data"); 
        if (persistentObj != null) {    //  .   .   .   .   .   .   .   .   .   .   .   If persistent data exists
            persistentDataScript = persistentObj.GetComponent<PersistentData>();    //  Set script
            achieveLst = persistentDataScript.GetAchievements();    //  .   .   .   .   Get and display achievements
            for (int i = 0; i < achieveLst.Count; i++) {
                Vector3 pos = this.transform.position;
                pos.x -= offset;
                pos.y -= i * yoffset;
                pos.z = -6.0f;
                GameObject achObj = (GameObject)Instantiate(achievementObj, pos, this.transform.rotation);  //   Achievement obj
                achObj.GetComponent<TextMesh>().text = achieveLst[i].GetName();
                foreach (Transform child in achObj.transform) { //  .   .   .   .   .   Get achievement description/progress bar
                    foreach (Transform child2 in child) {
                        if (child2.tag == "AchieveDesc") {
                            child2.gameObject.GetComponent<TextMesh>().text = achieveLst[i].GetDesc();
                            child.gameObject.SetActive(false);
                        }
                    }
                    if (child.tag == "Progbar") {
                        progObj = child.gameObject;
                        float revealOffset = 0.0f;
                        if ((achieveLst[i].GetNextStageVal() != 0) && (achieveLst[i].GetValue() != 0)) {
                            revealOffset = ((achieveLst[i].GetValue())/ achieveLst[i].GetNextStageVal());
                        }
                        
                        child.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff", revealOffset);
                    }
                }
                if (achObj.GetComponentInChildren<SpriteRenderer>() != null) {  //  .   Get achievement stages and display award
                    if (achieveLst[i].GetMaxStage() == 3) {
                        achObj.GetComponentInChildren<SpriteRenderer>().sprite = awardSprites[achieveLst[i].GetStage()];
                    } else if (achieveLst[i].GetMaxStage() == 1) {
                        if(achieveLst[i].GetStage() == 1) {
                            achObj.GetComponentInChildren<SpriteRenderer>().sprite = awardSprites[3];
                        } else {
                            achObj.GetComponentInChildren<SpriteRenderer>().sprite = awardSprites[0];
                        }
                    }
                }
                achObj.GetComponent<HideOnY>().SetYLim(new Vector2(yLimit.x + (i * yoffset), yLimit.y + (i * yoffset)));
                achObj.transform.parent = GameObject.Find("AchieveNums").transform;
                if(achieveLst[i].GetStage() == achieveLst[i].GetMaxStage()) {
                    progObj.SetActive(false);
                }
                achieveObjLst.Add(achObj);
            }
        }
    }
}
