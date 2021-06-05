using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWorldButtonHandler : MonoBehaviour
{
	public Slider rowSlider;
	public Slider columnSlider;
	public Slider densitySlider;

	public TileWorldBuilder tileWorldBuilderInstance = null;

	public void BuildWorldButtonClick()
	{
		tileWorldBuilderInstance.BuildWorld((int) rowSlider.value, (int) columnSlider.value, densitySlider.value);
	}
}
