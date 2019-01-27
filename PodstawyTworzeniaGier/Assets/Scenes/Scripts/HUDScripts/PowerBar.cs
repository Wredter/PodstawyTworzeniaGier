using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {
    public HUD script;
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        float scale = script.GetHorde().GetPower();
        if (scale <= 1)
        {
            slider.value = scale;
        }
    }
}
