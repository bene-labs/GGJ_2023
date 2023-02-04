using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
	public static T GetRandom<T>(this T[] array)
	{
		if (array == null || array.Length > 0)
		{
			return array[Random.Range(0, array.Length)];
		}
		else
		{
			return default;
		}
	}
	public static T GetRandom<T>(this List<T> list)
	{
		if (list == null || list.Count > 0)
		{
			return list[Random.Range(0, list.Count)];
		}
		else
		{
			return default;
		}
	}

	public static Rect Offsetted(this Rect rect, Vector2 offset) => new Rect(rect.position + offset, rect.size);

	public static void SetXPosition(this Transform transform, float x)
	{
		var position = transform.position;
		position.x = x;
		transform.position = position;
	}
}
