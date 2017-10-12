using UnityEngine;
using System.Collections;

namespace Utility
{
	public class MarhUtility
	{
		public static float Round(float f, int acc)
		{
			float temp = f * Mathf.Pow(10, acc);
			return  Mathf.Round(temp) / Mathf.Pow(10, acc);
		}

	}
}
