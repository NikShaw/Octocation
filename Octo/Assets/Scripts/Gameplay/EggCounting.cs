using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EggCounting : MonoBehaviour {

    public GameObject area;
    public Vector4 ColorMult = new Vector4(1.0f, 0.8f, 0.8f, 1.0f);
    private List<GameObject> EggAreas = new List<GameObject>();
    private List<GameObject> ShrimpAreas = new List<GameObject>();
    private List<GameObject> SecretAreas = new List<GameObject>();
    private List<GameObject> Eggs = new List<GameObject>();
    private List<GameObject> Shrimps = new List<GameObject>();
    private List<GameObject> Secrets = new List<GameObject>();
    private GameObject persistentObj;
    private PersistentData persistentDataScript;
    public List<int> collectedEggs = new List<int>();
    public List<int> collectedShrimps = new List<int>();
    public List<int> collectedSecrets = new List<int>();


    // Use this for initialization
    void Start() {
        foreach (Transform child in area.transform) {   // Get eggs/shrimp/secrets areas
            if(child.tag == "Area") {
                EggAreas.Add(child.FindChild("Objects").FindChild("Eggs").gameObject);
                ShrimpAreas.Add(child.FindChild("Objects").FindChild("Shrimp").gameObject);
                SecretAreas.Add(child.FindChild("Objects").FindChild("Secrets").gameObject);
            }
        }
        for (int j = 0; j < EggAreas.Count; j++) {  // Get egg objects
            foreach (Transform child in EggAreas[j].transform) {
                if (child.tag == "Points") {
                    Eggs.Add(child.gameObject);
                }
            }
        }
        for (int j = 0; j < ShrimpAreas.Count; j++) {   // Get shrimp objects
            foreach (Transform child in ShrimpAreas[j].transform) {
                if (child.tag == "Points") {
                    Shrimps.Add(child.gameObject);
                }
            }
        }
        for (int j = 0; j < SecretAreas.Count; j++) {   // Get secret objects
            foreach (Transform child in SecretAreas[j].transform) {
                if (child.tag == "Secret") {
                    Secrets.Add(child.gameObject);
                }
            }
        }
        persistentObj = GameObject.Find("Persistent Data");
        if (persistentObj != null) {
            persistentDataScript = persistentObj.GetComponent<PersistentData>();
            persistentDataScript.setPointSize(Eggs.Count);
            persistentDataScript.setPointShrimpSize(Shrimps.Count);
            persistentDataScript.setPointSecretSize(Secrets.Count);
            collectedEggs = persistentDataScript.getPoints();
            collectedShrimps = persistentDataScript.getPointShrimps();
            collectedSecrets = persistentDataScript.getPointSecrets();
            SetEggColour();
        }
    }

    private void SetEggColour() {   // Darkens egg colour if previously picked up
        for (int k = 0; k < collectedEggs.Count; k++) {
            if (collectedEggs[k] == 1) {
                SpriteRenderer sprte = Eggs[k].GetComponent<SpriteRenderer>();
                sprte.color *= new Color(ColorMult.x, ColorMult.y, ColorMult.z, ColorMult.w);
            }
        }
        for (int k = 0; k < collectedShrimps.Count; k++) {
            if (collectedShrimps[k] == 1) {
                SpriteRenderer sprte = Shrimps[k].GetComponent<SpriteRenderer>();
                sprte.color *= new Color(ColorMult.x, ColorMult.y, ColorMult.z, ColorMult.w);
            }
        }
    }

    // Save objects
    public void SaveEggs() {
        if (persistentObj != null) {
            persistentDataScript.SetPoints(collectedEggs);
        }
    }

    public void SaveShrimps() {
        if (persistentObj != null) {
            persistentDataScript.SetShrimps(collectedShrimps);
        }
    }

    public void SaveSecrets() {
        if (persistentObj != null) {
            persistentDataScript.SetSecrets(collectedSecrets);
        }
    }

    // Collect objects
    public void collectSecret(GameObject shhhh) {
        persistentDataScript.CollectSecret();
        if (Secrets.Contains(shhhh)) {
            int index = Secrets.IndexOf(shhhh);
            collectedSecrets[index] = 1;
        }
    }

    public void collectEgg(GameObject eg) {
        persistentDataScript.CollectEgg();
        if (Eggs.Contains(eg)) {
            int index = Eggs.IndexOf(eg);
            collectedEggs[index] = 1;
        }
    }
    public void collectShrimp(GameObject sh) {
        persistentDataScript.CollectShrimp();
        if (Shrimps.Contains(sh)) {
            int index = Shrimps.IndexOf(sh);
            collectedShrimps[index] = 1;
        }
    }
}
