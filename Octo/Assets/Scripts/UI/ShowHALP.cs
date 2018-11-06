using UnityEngine;
using System.Collections;

public class ShowHALP : MonoBehaviour {

    public GameObject ASSUMINGHALP;
    public GameObject disableObj;
    private ButtonScript btnScript;
    private Controller AssumingController;
    public GameObject pauseObj;
    private ButtonScript pauseScript;
    
    void Start () {
        AssumingController = ASSUMINGHALP.GetComponent<Controller>();
        btnScript = this.GetComponent<ButtonScript>();
    }
	
	void Update () {    // Show help menu on button press
        if (btnScript.isPressed()) {
            if (pauseObj != null) {
                if (pauseObj)
                    pauseScript = pauseObj.GetComponent<ButtonScript>();
                //pauseScript.OnMouseUp();
            }
            //disableObj.SetActive(false);
            AssumingController.StartHalp();
        }
	}
}
