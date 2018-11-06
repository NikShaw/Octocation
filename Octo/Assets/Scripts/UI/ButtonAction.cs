using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ChartboostSDK;

public class ButtonAction : MonoBehaviour {

    public string buttonName = "";
    public bool death = false;
    public bool removeAd = false;
    public GameObject pauseObj;
    public GameObject ad;
    public GameObject transitioner;
	public List<Texture2D> transitionTextures = new List<Texture2D>();
    public List<GameObject> disableObjs = new List<GameObject>();
    public List<GameObject> enableObjs = new List<GameObject>();
    private ButtonScript pauseScript;
    public int sceneLoad = 0;
    public Sprite defSprite;
    public Sprite pressSprite;
    private ButtonScript btnScript;

    void OnEnable() {
        if (this.GetComponent<SpriteRenderer>() != null) {
            this.GetComponent<SpriteRenderer>().sprite = defSprite;
        }
    }
    
    void Start () {
        if (death) {
            Chartboost.cacheInterstitial(CBLocation.Default);
        }
        btnScript = this.GetComponent<ButtonScript>();
	    if(pauseObj != null) {
            pauseScript = pauseObj.GetComponent<ButtonScript>();
        }
	}

    private void ShowAd() {
        //SHOW AD HERE
        Debug.Log("AD");
        Chartboost.showInterstitial(CBLocation.Default);
    }
	
	void Update () {
        if (btnScript.isPressed()) {    // Change sprite on button press
            if (this.GetComponent<SpriteRenderer>() != null) {
                this.GetComponent<SpriteRenderer>().sprite = pressSprite;
            }
            if (disableObjs.Count > 0) {    // Disable/enable objects
                for (int i = 0; i < disableObjs.Count; i++) {
                    disableObjs[i].SetActive(false);
                }
            }
            if (enableObjs.Count > 0) {
                for (int i = 0; i < enableObjs.Count; i++) {
                    enableObjs[i].SetActive(true);
                }
            }
            switch (buttonName) {   // Take action based on purpose
                case "resume":
                    pauseScript.DoMouseUp();
                    break;
                case "home":
                    if (death) {
                        if ((PersistentData.Instance.GetDeaths() % 3 == 0) && (PersistentData.Instance.GetDeaths() > 0)) {
                            ShowAd();
                        }
                    }
                    Application.LoadLevel(sceneLoad);
                    break;
                case "retry":
                    if (death) {
                        if ((PersistentData.Instance.GetDeaths() % 3 == 0) && (PersistentData.Instance.GetDeaths() > 0)) {
                            ShowAd();
                        }
                    }
                    Application.LoadLevel(sceneLoad);
                    break;
			    case "transition":
                    if (removeAd) {
                        ad.GetComponent<ShowBanner>().Destroy();
                    }
                    transitioner.GetComponent<LoadScene>().SetIn(true);
                    transitioner.GetComponent<EMTransition>().gradationTexture = transitionTextures[0];
                    transitioner.GetComponent<EMTransition>().Play();
				break;


            }
        }
	}
}
