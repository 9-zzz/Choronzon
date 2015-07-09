using UnityEngine;
using System.Collections;

public class TriggerUnshatter : MonoBehaviour
{

    public float setDriftSeconds;
    public Component[] cScripts;
    public bool isWhole = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ActivateInAllChildren()
    {
        cScripts = GetComponentsInChildren<ReturnToStartingPosition>();
        foreach (ReturnToStartingPosition script in cScripts)
        {
            script.driftSeconds = setDriftSeconds;
            StartCoroutine(setWholeBool(setDriftSeconds));
            script.StartDrift();
        }
    }

    IEnumerator setWholeBool(float t)
    {
        yield return new WaitForSeconds(t);
        isWhole = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            ActivateInAllChildren();
    }


}
