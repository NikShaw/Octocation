using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public List<AudioClip> boostAudio = new List<AudioClip>();
    public List<AudioClip> eggGetAudio = new List<AudioClip>();
    public List<AudioClip> secretGetAudio = new List<AudioClip>();
    public List<AudioClip> shrimpGetAudio = new List<AudioClip>();
    public List<AudioClip> scoreGetAudio = new List<AudioClip>();
    public List<AudioClip> angUlarBiteAudio = new List<AudioClip>();
    public List<AudioClip> angularMoveAudio = new List<AudioClip>();
    public List<AudioClip> deadedAudio = new List<AudioClip>();
    public List<AudioClip> wallBumpAudio = new List<AudioClip>();
    public List<AudioClip> coinFallAudio = new List<AudioClip>();
    public List<AudioClip> chestOpenAudio = new List<AudioClip>();
    public List<AudioClip> shipCreakAudio = new List<AudioClip>();
    public List<AudioClip> wallHitAudio = new List<AudioClip>();
    public List<AudioClip> winAudio = new List<AudioClip>();
    public List<AudioClip> failAudio = new List<AudioClip>();
    public List<AudioClip> buttonAudio = new List<AudioClip>();
    public List<AudioClip> octoComboAudio = new List<AudioClip>();
    public List<AudioClip> puffyAudio = new List<AudioClip>();
    public List<AudioClip> bubbleAudio = new List<AudioClip>();
    public List<AudioClip> clamAudio = new List<AudioClip>();
    public List<AudioClip> giantClamAudio = new List<AudioClip>();
    public List<AudioClip> lifeGetAudio = new List<AudioClip>();
    public List<AudioClip> volcanoAudio = new List<AudioClip>();
    public List<AudioClip> waterCurrentAudio = new List<AudioClip>();
    public List<AudioClip> chalkboardSwapAudio = new List<AudioClip>();
    public List<AudioClip> pearlGetAudio = new List<AudioClip>();
    public List<AudioClip> coinWhooshAudio = new List<AudioClip>();
    public List<AudioClip> lifeBreakAudio = new List<AudioClip>();
    public GameObject beachfrontObj;
    private List<AudioClip> loopingClips = new List<AudioClip>();
    private List<float> clipTime = new List<float>();
    private List<float> clipVolume = new List<float>();
    private float timer = 0.0f;
    private AudioSource source;
    private PersistentData perDataScript;

    public float volume = 0.5f;

    public static SoundManager Instance;

    void Awake() {
        if (Instance) {
            DestroyImmediate(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        perDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
        source = GetComponent<AudioSource>();
        if (perDataScript.getMute() == 0) {
            source.mute = true;
            beachfrontObj.GetComponent<AudioSource>().mute = true;
        }
    }

    public void Mute() {
        if (source != null) {
            source.mute = true;
        }
        beachfrontObj.GetComponent<AudioSource>().mute = true;
    }

    public void Unmute() {
        if (source != null) {
            source.mute = false;
        }
        beachfrontObj.GetComponent<AudioSource>().mute = false;
    }

    public void StopMusic() {
        beachfrontObj.GetComponent<AudioSource>().mute = true;
    }

    // Play specific sounds
    public void PlaySound(string sound, bool  repeat, bool on, Vector2 pos, float vol) {
        switch (sound) {
            case "boost":
                Play(boostAudio, repeat, on, pos, vol);
                break;
            case "coinwhoosh":
                Play(coinWhooshAudio, repeat, on, pos, vol);
                break;
            case "pearlget":
                Play(pearlGetAudio, repeat, on, pos, vol);
                break;
            case "eggget":
                Play(eggGetAudio, repeat, on, pos, vol);
                break;
            case "secretget":
                Play(secretGetAudio, repeat, on, pos, vol);
                break;
            case "shrimpget":
                Play(shrimpGetAudio, repeat, on, pos, vol);
                break;
            case "scoreget":
                Play(scoreGetAudio, repeat, on, pos, vol);
                break;
            case "angularbite":
                Play(angUlarBiteAudio, repeat, on, pos, vol);
                break;
            case "angularmove":
                Play(angularMoveAudio, repeat, on, pos, vol);
                break;
            case "dead":
                Play(deadedAudio, repeat, on, pos, vol);
                break;
            case "wallbump":
                Play(wallBumpAudio, repeat, on, pos, vol);
                break;
            case "coinfall":
                Play(coinFallAudio, repeat, on, pos, vol);
                break;
            case "chestopen":
                Play(chestOpenAudio, repeat, on, pos, vol);
                break;
            case "creak":
                Play(shipCreakAudio, repeat, on, pos, vol);
                break;
            case "wallhit":
                Play(wallHitAudio, repeat, on, pos, vol);
                break;
            case "win":
                Play(winAudio, repeat, on, pos, vol);
                break;
            case "fail":
                Play(failAudio, repeat, on, pos, vol);
                break;
            case "button":
                Play(buttonAudio, repeat, on, pos, vol);
                break;
            case "octocombo":
                Play(octoComboAudio, repeat, on, pos, vol);
                break;
            case "puff":
                Play(puffyAudio, repeat, on, pos, vol);
                break;
            case "bubble":
                Play(bubbleAudio, repeat, on, pos, vol);
                break;
            case "clam":
                Play(clamAudio, repeat, on, pos, vol);
                break;
            case "giantclam":
                Play(giantClamAudio, repeat, on, pos, vol);
                break;
            case "lifeget":
                Play(lifeGetAudio, repeat, on, pos, vol);
                break;
            case "volcano":
                Play(volcanoAudio, repeat, on, pos, vol);
                break;
            case "watercurrent":
                Play(waterCurrentAudio, repeat, on, pos, vol);
                break;
            case "chalkboard":
                Play(chalkboardSwapAudio, repeat, on, pos, vol);
                break;
            case "lifebreak":
                Play(lifeBreakAudio, repeat, on, pos, vol);
                break;
        }
    }

    // Play random sound from list
    void Play(List<AudioClip> audioLst, bool loop, bool on, Vector2 pos, float vol) {
        if(vol > 1.0f) {
            vol = 1.0f;
        }
        vol *= volume;
        if (audioLst.Count > 0) {
            if (loop) { // If looping, add sound to array and save time played at
                if (on) {
                    if (!loopingClips.Contains(audioLst[0])) {
                        loopingClips.Add(audioLst[0]);
                        float tempTime = timer;
                        clipTime.Add(tempTime);
                        clipVolume.Add(vol);
                        source.PlayOneShot(audioLst[0], vol);
                    }
                } else {
                    if (loopingClips.Contains(audioLst[0])) {
                        clipTime.RemoveAt(loopingClips.IndexOf(audioLst[0]));
                        clipVolume.RemoveAt(loopingClips.IndexOf(audioLst[0]));
                        loopingClips.Remove(audioLst[0]);
                    }
                }
            } else {
                if (pos != Vector2.zero) {
                    if (!source.mute) { 
                        AudioSource.PlayClipAtPoint(audioLst[Random.Range(0, audioLst.Count)], pos, vol);
                    }
                } else {
                    source.PlayOneShot(audioLst[Random.Range(0, audioLst.Count)], vol); 
                }
            }
        }
    }

    // Replay sound if set as looping and finished playing
    void Update () {
        for(int i = 0; i < loopingClips.Count; i++) {
            if ((timer - clipTime[i]) > loopingClips[i].length) {
                clipTime[i] = timer;
                source.PlayOneShot(loopingClips[i], clipVolume[i]);
            }
        }
        timer += Time.deltaTime;
    }
}
