using UnityEngine;
using System.Collections;

public class ScaleToCam : MonoBehaviour {
    public Texture text;

    void Start() {
        float width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float height = Camera.main.orthographicSize * 2.0f;
        this.transform.localScale = new Vector2(width, height);
    }

    void OnGUI() {
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), text, ScaleMode.ScaleToFit, true);
    }
}
