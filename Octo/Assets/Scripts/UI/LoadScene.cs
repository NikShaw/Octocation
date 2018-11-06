using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public bool In = false;

    void Start() {
    }

    public void Load(int sceneIndex){
        if (In == true) {
            if (sceneIndex == 0) {
            }
            Application.LoadLevel(sceneIndex);
        }

    }

	public void SetIn(bool i){
		In = i;
	}
}
