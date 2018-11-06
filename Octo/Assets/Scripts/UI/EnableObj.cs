using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnableObj : MonoBehaviour {

    public List<GameObject> obj = new List<GameObject>();

    public void Enable() {
        for (int i = 0; i < obj.Count; i++) {
            obj[i].SetActive(true);
        }
    }
}
