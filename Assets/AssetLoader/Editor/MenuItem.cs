using System.Collections;
using System.Collections.Generic;
using Meow.AssetLoader.Core;
using UnityEditor;
using UnityEngine;

namespace Meow.AssetLoader.Editor
{
	public static class MenuItems
	{
		[MenuItem("Window/Meow Asset Loader/Simulation Mode")]
		public static void ToggleSimulationMode ()
		{
			MainLoader.IsSimulationMode = !MainLoader.IsSimulationMode;
		}
	
		[MenuItem("Window/Meow Asset Loader/Simulation Mode", true)]
		public static bool ToggleSimulationModeValidate ()
		{
			Menu.SetChecked("Window/Meow Asset Loader/Simulation Mode", MainLoader.IsSimulationMode);
			return true;
		}
	}

}
