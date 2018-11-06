using UnityEngine;
using System.Collections;

public class AngUlarfish : MonoBehaviour {
	
    [Header("AngUlar Fish Settings")]
    public float biteLength = 0.3f;
    public float moveTime = 1.0f;
    //private float moveTimer = 0.0f;
    //private float biteTimer = 0.0f;
    public bool move = false;
    public bool bite = false;
    public bool biting = false;
    public bool bitten = false;
    private bool nomming = false;
    private bool moving = false;
    private bool hitPlayer = false;
    private bool angUlarEnabled = false;

    [Header("Unity Objects")]
    private GameObject player;
    private Animator animator;
    private Rigidbody2D colliderA;
    private GameObject soundMan;
    private GameObject perData;
    private PersistentData perDataScript;
    private SoundManager soundManScript;

    void Start() {
        soundMan = GameObject.Find("Sound Manager");
        perData = GameObject.Find("Persistent Data");
        perDataScript = perData.GetComponent<PersistentData>();
        if (soundMan != null) {
            soundManScript = soundMan.GetComponent<SoundManager>();
        }
        colliderA = this.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        colliderA.gravityScale = 0.0f;
    }

    public void GOFISH() {  //      *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Get angular fish ready to move
        moving = true;
        colliderA.gravityScale = 0.1f;
        angUlarEnabled = false;
    }

    public void FISHWENT() {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Move angular fish
        if (!angUlarEnabled) {
            angUlarEnabled = true;
        }
    }
    
	void Update () {    //  *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   *   Update
        if (moving) {
            if (!biting) {  //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   If not biting and can move, move randomly, moves based on - or + scale
                if (move) {
					if (this.transform.localScale.x < 0){
                        colliderA.AddForce(new Vector2(-Random.Range(50.0f, 100.0f), Random.Range(-10.0f, 30.0f)));
					}else{
                        colliderA.AddForce(new Vector2(Random.Range(50.0f, 100.0f), Random.Range(-10.0f, 30.0f)));
                    }
                    soundManScript.PlaySound("angularmove", false, false, this.transform.position, 0.2f);
                    move = false;
                }
            }
            if (bite) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   If biting and player is in collider, hit player
                if (!bitten) {
                    if (hitPlayer) {
                        if (nomming) {
                            perDataScript.AddNearAngle(-1);
                        }
                        player.GetComponent<Player>().getHit(this.gameObject);
                    }
                    bitten = true;
                }
                nomming = false;
            } else {
                if (bitten) {
                    bitten = false;
                }
            }//  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Delete if off edge of screen
			if (this.transform.localScale.x < 0){
				if (this.transform.position.x < (Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x - 5.0f)) {
					Destroy(this.gameObject);
				}

			}else{
				if (this.transform.position.x > (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x + 5.0f)) {
	                Destroy(this.gameObject);
				}
			}
        }
    }

    void OnTriggerEnter2D(Collider2D col) { //  *   *   *   *   *   *   *   *   *   *   *   *   *   Trigger collisions
        if (angUlarEnabled) {
            if (col.gameObject.tag == "Player") {
                if (!hitPlayer) { //  .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Trigger bite animation if player collided
                    animator.SetTrigger("Squid Near");
                    player = col.gameObject;
                    hitPlayer = true;
                    colliderA.AddForce(new Vector2(-(colliderA.velocity.x * 10), 0.0f));
                    if (!nomming) {
                        nomming = true;
                        perDataScript.AddNearAngle(1);
                        if (soundMan != null) {
                            soundManScript.PlaySound("angularbite", false, false, this.transform.position, 0.6f);
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {  //  *   *   *   *   *   *   *   *   *   *   *   *   *   Trigger exit
        if (angUlarEnabled) {  //      .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   .   Tell if player has left trigger area
            if (col.gameObject.tag == "Player") {
                hitPlayer = false;
            }
        }
    }
}
