using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoronzonCrosshair : MonoBehaviour
{

    Image crosshairImage;
    // Use this for initialization
    void Start()
    {
        crosshairImage = this.GetComponent<Image>();
        crosshairImage.CrossFadeAlpha(0.2f, 1, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(PulseCrosshair());
    }

    IEnumerator PulseCrosshair()
    {
        crosshairImage.CrossFadeAlpha(0.5f, 0.25f, true);
        yield return new WaitForSeconds(0.25f);
        crosshairImage.CrossFadeAlpha(0.2f, 0.30f, true);
    }

}
