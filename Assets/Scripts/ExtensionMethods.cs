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

	public static TEnum GetRandomValue<TEnum>(this System.Type type) where TEnum : System.Enum
	{
		return ((TEnum[])System.Enum.GetValues(typeof(TEnum))).GetRandom();
	}

	public static void DestroyGameObject<T>(this T component) where T : Component
	{
		Object.Destroy(component.gameObject);
	}

	public static void DestroyAllGameObjectsAndClear<T>(this List<T> components) where T : Component
	{
		foreach (var component in components)
		{
			component.DestroyGameObject();
		}
	}
	public static void DestroyAllAndClear(this List<GameObject> gameObjects)
	{
		foreach (var gameObject in gameObjects)
		{
			Object.Destroy(gameObject);
		}
	}

	public static void SetAllActive(this List<GameObject> gameObjects, bool active)
	{
		foreach (var gameObject in gameObjects)
		{
			gameObject.SetActive(active);
		}
	}
	public static void SetAllGameObjectsActive<T>(this List<T> components, bool active) where T : Component
	{
		foreach (var component in components)
		{
			component.gameObject.SetActive(active);
		}
	}
}
