using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour {
    public HUD script;
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        float scale = 1 - script.GetHorde().GetCooldown();
        if (scale <= 1)
        {
            slider.value = scale;
        }
    }
}
