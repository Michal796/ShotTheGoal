using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//typ wyliczeniowy wszystkich możliwych stanów gry
public enum GameMode
{
    idle, playing, levelEnd
}
//klasa umożliwia kontrolę aktualnego poziomu gry i interfejsu oraz możliwość wybrania położenia kamery
public class ShotTheGoal : MonoBehaviour
{
    static private ShotTheGoal S; //singleton

    [Header("Definiowanie ręczne w panelu inspector")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles; //tablica zamków

    public int level;
    public int levelMax;
    public int shotsTaken; //ilość oddanych strzałów
    public GameObject castle;
    public GameMode mode = GameMode.idle; //tryb domyślny
    public string showing = "Pokaż procę"; //treść przycisku do zmiany położenia kamery

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }
    void StartLevel() //usuniecie zamku jesli istnieje
    {
        if (castle != null)
        {
            Destroy(castle);
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile"); //usuniecie pociskow
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Pokaż odległość");//kamera domyslna
        ProjectileLine.S.Clear(); //usunięcie linii

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }
    void UpdateGUI() //aktualizacja informacji
    {
        uitLevel.text = "Poziom: " + (level + 1) + " z " + levelMax;
        uitShots.text = "Strzały: " + shotsTaken;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //sprawdz czy koniec poziomu
        if((mode==GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;

            SwitchView("Pokaż odległość");

            Invoke("NextLevel", 2f); // za dwie sekundy nastepny level
        }
    }
    void NextLevel()
    {
        level++;
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }
    //funkcja przełączająca położenie kamery
    public void SwitchView(string eView = "")
    {
        if (eView =="")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Pokaż procę":
                CamFollow.POI = null;
                uitButton.text = "Pokaż zamek";
                break;

            case "Pokaż zamek":
            CamFollow.POI = S.castle;
            uitButton.text = "Pokaż odległość";
            break;

            case "Pokaż odległość":
            CamFollow.POI = GameObject.Find("ViewBoth");
            uitButton.text = "Pokaż procę";
            break;
        }
    }
    //metoda statyczna umożliwiająca przekazanie informacji o oddaniu strzału przez klasę Slingshot
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
