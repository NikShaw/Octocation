using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideOnY : MonoBehaviour {

    public float yLimit = 0.0f;
    public float yNegLimit = -0.0f;
    public float fadeDist = 0.4f;
    public List<GameObject> child;
    private MeshRenderer txtMesh;
    private SpriteRenderer SprRndr;
    private MeshRenderer[] txtMeshChild;
    private SpriteRenderer[] SprRndrChild;
    private Color defaultColour;
    private Color defaultSprite;
    private Color defaultColourChild;
    private Color defaultSpriteChild;

    // Use this for initialization
    void Start () {
        txtMeshChild = this.GetComponentsInChildren<MeshRenderer>();
        SprRndrChild = this.GetComponentsInChildren<SpriteRenderer>();
        txtMesh = this.GetComponent<MeshRenderer>();
        SprRndr = this.GetComponent<SpriteRenderer>();
        if (txtMesh != null) {
            defaultColour = txtMesh.material.color;
        }
        if (SprRndr != null) {
            defaultSprite = SprRndr.material.color;
        }
        for (int i = 0; i < txtMeshChild.Length; i++) {
            if (txtMeshChild[i] != null) {
                defaultColourChild = txtMeshChild[i].material.color;
            }
        }
        for (int i = 0; i < SprRndrChild.Length; i++) {
            if (SprRndrChild[i] != null) {
                defaultSpriteChild = SprRndrChild[i].material.color;
            }
        }
        yLimit += this.transform.position.y;
        yNegLimit += this.transform.position.y;
        Update();
    }

    public Vector2 GetYLim() {
        return new Vector2(yLimit, yNegLimit);
    }

    public void SetYLim(Vector2 lim) {
        yLimit = lim.x;
        yNegLimit = lim.y;
    }
	
	void Update () {    // Slow fade object (spriterenderer OR textmesh and all children) based on y
        float yPos = this.transform.position.y;
	    if(this.transform.position.y > yLimit) {    // Check positive y
            if (((fadeDist / (yPos - yLimit)) - 1.0f) > 1.0f) {
                if (txtMesh != null) {
                    txtMesh.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                if (SprRndr != null) {
                    SprRndr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                for (int i = 0; i < txtMeshChild.Length; i++) {
                    if (txtMeshChild[i] != null) {
                        txtMeshChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                }
                for (int i = 0; i < SprRndrChild.Length; i++) {
                    if (SprRndrChild[i] != null) {
                        SprRndrChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                }
            } else {
                if (txtMesh != null) {
                    txtMesh.material.color = new Color(1.0f, 1.0f, 1.0f, (fadeDist / (yPos - yLimit)) - 1.0f);
                }
                if (SprRndr != null) {
                    SprRndr.material.color = new Color(1.0f, 1.0f, 1.0f, (fadeDist / (yPos - yLimit)) - 1.0f);
                }
                for (int i = 0; i < txtMeshChild.Length; i++) {
                    if (txtMeshChild[i] != null) {
                        txtMeshChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, (fadeDist / (yPos - yLimit)) - 1.0f);
                    }
                }
                for (int i = 0; i < SprRndrChild.Length; i++) {
                    if (SprRndrChild[i] != null) {
                        SprRndrChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, (fadeDist / (yPos - yLimit)) - 1.0f);
                    }
                }
            }
        } else if (this.transform.position.y < yNegLimit) { // Check negative y
            if ((-(fadeDist / (yPos - yNegLimit)) - 1.0f) > 1.0f) {
                if (txtMesh != null) {
                    txtMesh.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                if (SprRndr != null) {
                    SprRndr.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                for (int i = 0; i < txtMeshChild.Length; i++) {
                    if (txtMeshChild[i] != null) {
                        txtMeshChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    }
                }
                for (int i = 0; i < SprRndrChild.Length; i++) {
                    if (SprRndrChild[i] != null) {
                            SprRndrChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                        }
                    }
            } else {
                if (txtMesh != null) {
                    txtMesh.material.color = new Color(1.0f, 1.0f, 1.0f, -(fadeDist / (yPos - yNegLimit)) - 1.0f);
                }
                if (SprRndr != null) {
                    SprRndr.material.color = new Color(1.0f, 1.0f, 1.0f, -(fadeDist / (yPos - yNegLimit)) - 1.0f);
                }
                for (int i = 0; i < txtMeshChild.Length; i++) {
                    if (txtMeshChild[i] != null) {
                        txtMeshChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, -(fadeDist / (yPos - yNegLimit)) - 1.0f);
                    }
                }
                for (int i = 0; i < SprRndrChild.Length; i++) {
                    if (SprRndrChild[i] != null) {
                            SprRndrChild[i].material.color = new Color(1.0f, 1.0f, 1.0f, -(fadeDist / (yPos - yNegLimit)) - 1.0f);
                        }
                    }
            }
        } else {

            if (txtMesh != null) {
                txtMesh.material.color = defaultColour;
            }
            if (SprRndr != null) {
                SprRndr.material.color = defaultSprite;
            }
            for (int i = 0; i < txtMeshChild.Length; i++) {
                if (txtMeshChild[i] != null) {
                    txtMeshChild[i].material.color = defaultColourChild;
                }
            }
            for (int i = 0; i < SprRndrChild.Length; i++) {
                if (SprRndrChild[i] != null) {
                    SprRndrChild[i].material.color = defaultSpriteChild;
                }
            }
        }
	}
}
