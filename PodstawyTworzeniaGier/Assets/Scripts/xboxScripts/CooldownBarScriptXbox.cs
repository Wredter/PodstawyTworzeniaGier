using UnityEngine;
using UnityEngine.UI;

public class CooldownBarScriptXbox : MonoBehaviour {
    public HUDScriptXbox script;
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
