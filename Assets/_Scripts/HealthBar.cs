using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{


    static public HealthBar S;

    public float hp;
    Slider healthBar;
    public Slider slowHealthBar;
    public Slider dashBar;
    public float slowHealthBarRate;

    void Awake()
    {
        S = this;
    }

    // Use this for initialization
    void Start()
    {
        S.healthBar = this.GetComponent<Slider>();
        S.hp = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = hp;

        dashBar.value = TP_Motor.Instance.dashBar;

        slowHealthBar.value = Mathf.MoveTowards(slowHealthBar.value, healthBar.value, slowHealthBarRate * Time.deltaTime);
        //S.slowHealthBar.value = Mathf.SmoothDamp(S.slowHealthBar.value, S.hp, ref S.hp, S.slowHealthBarRate);
    }

}
