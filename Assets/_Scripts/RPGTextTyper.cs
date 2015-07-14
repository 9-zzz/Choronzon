using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPGTextTyper : MonoBehaviour
{
    static public RPGTextTyper S;

    public Image faceImage; // Back Window of text.
    public Text targetText; // Text you see

    public bool isCurrentlyPrinting = false; // Must be false to begin with, logic of class.

    public int maxChars; // WHAT WAS THE IDEA WITH THIS AGAIN?
    string[] forcedDia;


    // Use this for initialization
    void Awake() // Or awake? haven't had problems.
    {
        S = this;

        targetText = transform.GetChild(0).GetComponent<Text>(); // Only if you place this on my specific object setup of UI Image with child UI Text.
        this.GetComponent<Image>().CrossFadeAlpha(0, 0, true);
        targetText.CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void printThing(string[] InputDialogue, float textSpeed, float textWaitNewScreen)
    {
        StartCoroutine(printRPGtext(InputDialogue, textSpeed, textWaitNewScreen));
    }

    public IEnumerator printRPGtext(string[] InputDialogue, float textSpeed, float textWaitNewScreen)
    {
        //playerRef.GetComponent<StopAllPlayerMotion>().toggleAllInput();
        if (!isCurrentlyPrinting)
        {
            this.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
            targetText.CrossFadeAlpha(1, 0.95f, true);


            for (int i = 0; i < InputDialogue.Length; i++)
            {

                targetText.text = ""; // clear at the end but not before you fade, so they see text during fade

                for (int k = 0; k < InputDialogue[i].Length; k++)
                {
                    targetText.text += InputDialogue[i].Substring(k, 1);

                    if (InputDialogue[i].Substring(k, 1) == ".")
                        yield return new WaitForSeconds(0.07f); // special wait time for periods. "added" on to regular time.

                    yield return new WaitForSeconds(textSpeed);
                }
                yield return new WaitForSeconds(textWaitNewScreen);
            }

            this.GetComponent<Image>().CrossFadeAlpha(0, 1, true);
            targetText.CrossFadeAlpha(0, 0.95f, true);

            yield return new WaitForSeconds(5.0f);// time so it doesn't constantly go.

            isCurrentlyPrinting = false;  // So OnTriggerEnter sdoesn't call this constantly.
        }
    }

}
