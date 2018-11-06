using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour {

    private GameObject target;
    public float moveFactor;
    private float offset;
    private float targetStartPos;
    private float thisStartPos;
    
    // Set target and offsets
	void Start () {
        target = Camera.main.gameObject;
        targetStartPos = target.transform.position.y;
        thisStartPos = this.transform.position.y;
	}
	
    // Move this object in relation to target
	void Update () {
        float distanceTarget = target.transform.position.y - targetStartPos;
        float difference = (distanceTarget * moveFactor) + thisStartPos;
        this.transform.position = new Vector3(this.transform.position.x, difference, this.transform.position.z);
    }
}
