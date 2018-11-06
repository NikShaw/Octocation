using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisableObj : MonoBehaviour {

    public List<GameObject> obj = new List<GameObject>();

    public void Disable() {
        for (int i = 0; i < obj.Count; i++) { 
            obj[i].SetActive(false);
        }
    }
}
