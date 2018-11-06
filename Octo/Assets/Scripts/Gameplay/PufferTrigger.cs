using UnityEngine;
using System.Collections;

public class PufferTrigger : MonoBehaviour {

    private GameObject puffer;
    private PufferFish pufferScript;
    
    void Start () {
        puffer = this.transform.parent.gameObject;
        pufferScript = puffer.GetComponent<PufferFish>();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.gameObject.transform.tag == "Player") {
            pufferScript.Puff();
        }
    }
}
