using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour
{
	public Slider SliderRowZ;
	public Slider SliderRowX;
	public Slider densitySlider;
	public Toggle warpBlocksToggle;

	public TileWorldBuilder tileWorldBuilderInstance = null;
	public GridMap gridMapInstance = null;
	public PathFinder pathFinderInstance = null;

	public void BuildWorldButtonClick()
	{
		bool warpBlocksToggleValue = false;
		if (warpBlocksToggle.isOn) warpBlocksToggleValue = true;

		tileWorldBuilderInstance.BuildWorld((int)SliderRowZ.value, (int)SliderRowX.value, densitySlider.value, warpBlocksToggleValue);
	}

	public void BuildWorldGrid()
	{
		gridMapInstance.BuildGrid((int)SliderRowX.value, (int)SliderRowZ.value);
	}
}
