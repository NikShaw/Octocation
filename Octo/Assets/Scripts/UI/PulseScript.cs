using UnityEngine;
using System.Collections;

public class PulseScript : MonoBehaviour {

    public float outPulseTime = 0.3f;
    public float inPulseTime = 0.1f;
    private float currAlpha = 0.0f;
    private float pulseTimer = 0.0f;
	
	// Update is called once per frame
	void Update () {
	    if(pulseTimer <= inPulseTime) {
            currAlpha = (pulseTimer / inPulseTime);
        } else if (pulseTimer <= (inPulseTime + outPulseTime)) {
            currAlpha = (1 - ((pulseTimer - inPulseTime) / outPulseTime));
        }
        Color tmpC = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(tmpC.r, tmpC.b, tmpC.g, currAlpha);
        pulseTimer += Time.deltaTime;
	}
}
