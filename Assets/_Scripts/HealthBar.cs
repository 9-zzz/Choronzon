using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    static public HealthBar S;

    public float hp;
    Slider healthBar;
    public Slider slowHealthBar;
    public Slider dashSlider;
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
        TP_Motor.Instance.dashBar = dashSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = hp;

        slowHealthBar.value = Mathf.MoveTowards(slowHealthBar.value, healthBar.value, slowHealthBarRate * Time.deltaTime);
        //S.slowHealthBar.value = Mathf.SmoothDamp(S.slowHealthBar.value, S.hp, ref S.hp, S.slowHealthBarRate);

        dashSlider.value = TP_Motor.Instance.dashBar;

        if (!TP_Motor.Instance.currentlyDashing)
            rechargeDashBar();
    }

    void rechargeDashBar()
    {
        if (TP_Motor.Instance.dashBar < dashSlider.maxValue)
        {
            TP_Motor.Instance.dashBar += TP_Motor.Instance.dashRechargeRate;//when not dashing
        }
    }

}
