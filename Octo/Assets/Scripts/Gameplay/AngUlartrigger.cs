using UnityEngine;
using System.Collections;

public class AngUlartrigger : MonoBehaviour {

    private GameObject angUlarFish;
    
	void Start () {
        angUlarFish = this.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D coll) {//  *   *   *   *   *   *   *   If player reaches trigger, get angular fish ready
        if (coll.gameObject.tag == "Player") {
            angUlarFish.GetComponent<AngUlarfish>().GOFISH();
        }
    }
    void OnTriggerExit2D(Collider2D coll) {//   *   *   *   *   *   *   *   If player leaves trigger, move angular fish and self destruct
        if (coll.gameObject.tag == "Player") {
            angUlarFish.GetComponent<AngUlarfish>().FISHWENT();
            Destroy(this.gameObject);
        }
    }
}
