using UnityEngine;
using System.Collections;

public class DisplayStat : MonoBehaviour {

    public GameObject Vacations;
    public GameObject EggStat;
    public GameObject ShrimpStat;
    public GameObject DeadStat;
    public GameObject PuffStat;
    public GameObject AngleStat;
    public GameObject TotalEggStat;
    public GameObject TotalScoreStat;
    public GameObject TotalShrimpStat;
    public GameObject HighscoreStat;
    private GameObject PerData;
    private PersistentData PerScript;

    // Use this for initialization
    void Start () {
        PerData = GameObject.Find("Persistent Data");
        PerScript = PerData.GetComponent<PersistentData>();

        int Vacation = PerScript.GetVacations();            // Load saved stats
        Vacations.GetComponent<TextMesh>().text = Vacation.ToString();
        int TotalScore = PerScript.GetTotalScore();
        TotalScoreStat.GetComponent<TextMesh>().text = TotalScore.ToString();
        int TotalEgg = PerScript.GetTotalEggs();
        TotalEggStat.GetComponent<TextMesh>().text = TotalEgg.ToString();
        int TotalShrimp = PerScript.GetTotalShrimp();
        TotalShrimpStat.GetComponent<TextMesh>().text = TotalShrimp.ToString();
        int Highscore = PerScript.GetScore();
        HighscoreStat.GetComponent<TextMesh>().text = Highscore.ToString();

        Vector2 EggVec = PerScript.GetNumEggs();
        int EggGot = (int)EggVec.y;
        int EggMax = (int)EggVec.x;
        string EggStr = (EggGot + "/" + EggMax);
        EggStat.GetComponent<TextMesh>().text = EggStr;

        Vector2 ShrimpVec = PerScript.GetNumShrimps();
        int ShrimpGot = (int)ShrimpVec.y;
        int ShrimpMax = (int)ShrimpVec.x;
        string ShrimpStr = (ShrimpGot + "/" + ShrimpMax);
        ShrimpStat.GetComponent<TextMesh>().text = ShrimpStr;

        string deadededs = PerScript.GetDeaths().ToString();
        DeadStat.GetComponent<TextMesh>().text = deadededs;

        string puffypuff = PerScript.GetNearPuffs().ToString();
        PuffStat.GetComponent<TextMesh>().text = puffypuff;

        string angUlarangle = PerScript.GetNearAngles().ToString();
        AngleStat.GetComponent<TextMesh>().text = angUlarangle;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
