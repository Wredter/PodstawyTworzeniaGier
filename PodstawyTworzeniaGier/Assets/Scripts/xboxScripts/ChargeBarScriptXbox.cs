using UnityEngine;
using UnityEngine.UI;

public class ChargeBarScriptXbox : MonoBehaviour {
    public HUDScriptXbox script;
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    void Update () {
        float scale = 1-script.GetHorde().GetChargeActualValue();
        if (scale <= 1)
        {
            slider.value = scale;
        }         
	}
}
