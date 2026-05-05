using UnityEngine;

public class aspect_ratio : MonoBehaviour
{
	private void Start()
	{
		adjust();
	}

	private void Update()
	{
	}

	private void adjust()
	{
		float num = 1.7777778f;
		float num2 = (float)Screen.width / (float)Screen.height / num;
		Camera component = GetComponent<Camera>();
		if (num2 < 1f)
		{
			Rect rect = component.rect;
			rect.width = 1f;
			rect.height = num2;
			rect.x = 0f;
			rect.y = (1f - num2) / 2f;
			component.rect = rect;
		}
		else
		{
			float num3 = 1f / num2;
			Rect rect2 = component.rect;
			rect2.width = num3;
			rect2.height = 1f;
			rect2.x = (1f - num3) / 2f;
			rect2.y = 0f;
			component.rect = rect2;
		}
	}
}
