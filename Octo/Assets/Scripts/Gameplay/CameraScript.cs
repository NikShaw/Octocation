using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    [Header("Unity Objects")]
    public GameObject target;

    [Header("Vectors")]
    private Vector2 ScreenRect;
    private Vector2 newScreenRect;
    private Vector2 screenSize;
    //private Vector2 aspectRatio = new Vector2(1, 1);

    [Header("Movement Settings")]
    public float yOffset = 2.0f;
    public bool moveDown = false;
    private float yTarget = 0.0f;
    public float smoothness = 6.0f;

    [Header("Camera Settings")]
    public Vector2 targetSize = new Vector2(600, 960);
    public float targetAspect = 10f / 16f;
    public float screenAspect = 0.0f;
    public float aspectDiff = 0.0f;
    public float aspectFloat = 0.0f;

    public void SetTarget(GameObject targ, float camSmoothness, bool down) {    //  *   *   *   *   Sets target for camera to follow
        moveDown = down;
        target = targ;
        smoothness = camSmoothness;
    }

	void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update - follow target with 'smoothness'
        yTarget = target.transform.position.y;
        float distance = yTarget - this.transform.position.y;
        if (distance >= 0) {
            if (distance > yOffset) {
                float tmpY = (distance - yOffset) / smoothness;
                this.transform.position = new Vector3(this.transform.position.x, (tmpY += this.transform.position.y), this.transform.position.z);
            }
        } else {
            if (moveDown) {
                if ((-distance) > yOffset) {
                    float tmpY = (distance - yOffset) / smoothness;
                    this.transform.position = new Vector3(this.transform.position.x, (tmpY += this.transform.position.y), this.transform.position.z);
                }
            }
        }
	}
}
