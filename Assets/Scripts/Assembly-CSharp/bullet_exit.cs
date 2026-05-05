using UnityEngine;

public class bullet_exit : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		bool flag = true;
		if (collision.GetComponent<boss_hp>() != null)
		{
			flag = false;
		}
		else if (collision.gameObject.name.StartsWith("ENV_"))
		{
			flag = false;
		}
		if (flag)
		{
			Object.Destroy(collision.gameObject);
		}
	}
}
