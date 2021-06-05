using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
	Slider slider;
	public Text sliderValueText;

	private void Start()
	{
		slider = GetComponent<Slider>();
		sliderValueText.text = slider.value.ToString();

		slider.onValueChanged.AddListener(delegate { UpdateText(); });
	}

	void UpdateText()
	{
		sliderValueText.text = slider.value.ToString();
	}
}
