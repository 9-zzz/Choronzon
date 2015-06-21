using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwordPickUp : MonoBehaviour
{

    public Text centerText;
    public GameObject go;

    // Use this for initialization
    void Start()
    {
        centerText.CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        centerText.text = "CLICK TO PICK UP SWORD";
        centerText.CrossFadeAlpha(1, 1, true);
        if (Input.GetMouseButtonDown(0))
        {
            centerText.CrossFadeAlpha(0, 1, true);
            go.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnMouseExit()
    {
        centerText.CrossFadeAlpha(0, 1, true);
    }

}