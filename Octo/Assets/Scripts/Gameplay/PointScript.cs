using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

    public int scoreValue = 1;
    public float boostValue = 1400.0f;
    public int bonusMultiplier = 0;

    public int GetScoreVal() {
        return scoreValue;
    }

    public float GetBoostVal() {
        return boostValue;
    }

    public int GetMultVal() {
        return bonusMultiplier;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.tag == "PointEater") {
            this.gameObject.SetActive(false);
        }
    }
}
