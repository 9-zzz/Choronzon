using UnityEngine;
using System.Collections;

public class GameEventHandler : MonoBehaviour
{
    static string[] dia;
    public bool playerHasBeenHurt = false;
    public bool playerHasBeenTipped = false;
    public static bool bossHasLoaded = false;

    // Use this for initialization
    void Start()
    {
        if(!bossHasLoaded)
        {
        dia = new string[2];
        dia[0] = "HELP CHORONZON DEFEAT THE ZAPPA IN THE SKY";
        dia[1] = "COLLECT 3 FRIPP GEMS TO POWER YOUR SWORD";
        RPGTextTyper.S.printThing(dia, 0.05f, 1.0f);
        }
    }

    void displayGiraTip()
    {
        if (!playerHasBeenTipped)
        {
            dia = new string[1];
            dia[0] = "DESTROY THE GIRA BY DEFLECTING ITS ATTACKS WITH YOUR SWORD";
            RPGTextTyper.S.printThing(dia, 0.05f, 1.0f);
            playerHasBeenHurt = false;
            playerHasBeenTipped = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (HealthBar.S.hp < 50)
            playerHasBeenHurt = true;

        if(playerHasBeenHurt)
            displayGiraTip();

        if (ReflectorSword.S.powerCtr == 3 && !bossHasLoaded)
        {
            StartCoroutine(loadBoss());
            bossHasLoaded = true; 
        }
    }

    IEnumerator loadBoss()
    {
        dia = new string[1];
        dia[0] = "DEFEAT THE ZAPPA";
        yield return new WaitForSeconds(2.0f);
        Application.LoadLevel(2);
    }
}
