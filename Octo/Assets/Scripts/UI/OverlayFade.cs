using UnityEngine;
using System.Collections;

public class OverlayFade : MonoBehaviour {

    public GameObject target;
    public bool fadeIn = true;
    public float distance = 5.0f;
    public float linearity = 0.6f;
    private SpriteRenderer spriteRenderer;
    
	void Start () {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f);
	}
	
	void Update () {    // Fade overlay based on distance to target
        if (target.transform.position.y < this.transform.position.y) {
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, linearity / ((this.transform.position.y - target.transform.position.y) / (distance)));
        } else {
            spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
        }
    }
}
