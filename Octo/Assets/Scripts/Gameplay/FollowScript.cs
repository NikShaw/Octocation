using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {

    public GameObject Target;
    public float smoothness = 0.0f;
    private float xOffset = 0.0f;
    private float yOffset = 0.0f;

    
    void Start () {
        xOffset = Target.transform.position.x - this.gameObject.transform.position.x;
        yOffset = Target.transform.position.y - this.gameObject.transform.position.y;

    }

    // Follow target smoothly
    void Update () {
        float distancex = Target.transform.position.x - this.gameObject.transform.position.x;
        float distancey = Target.transform.position.y - this.gameObject.transform.position.y;
        float tmpX = 0.0f;
        float tmpY = 0.0f;
        if (distancex > xOffset) {
            tmpX = (distancex - xOffset) / smoothness;
        } else if (-distancex > xOffset) {
            tmpX = (distancex - xOffset) / smoothness;
        }
        if (distancey >= yOffset) {
            tmpY = (distancey - yOffset) / smoothness;
        } else if (-distancey > yOffset) {
            tmpY = (distancey - yOffset) / smoothness;
        }

        this.transform.position = new Vector3(tmpX += this.transform.position.x, tmpY += this.transform.position.y, this.gameObject.transform.position.z);
	}
}
