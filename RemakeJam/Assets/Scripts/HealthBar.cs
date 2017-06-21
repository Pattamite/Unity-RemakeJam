using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public void SetValue(float value)
    {
        gameObject.GetComponent<Slider>().value = value;
    }
}
