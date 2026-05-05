using System.Collections;
using UnityEngine;

public class gunma_omni : MonoBehaviour
{
	public GameObject spread_shot;

	private float life_time = 1.5f;

	private void Start()
	{
		StartCoroutine(explode());
	}

	private void Update()
	{
	}

	private IEnumerator explode()
	{
		yield return new WaitForSeconds(life_time);
		do_explode();
	}

	private void do_explode()
	{
		float num = 45f;
		for (int i = 0; i < 8; i++)
		{
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, num * (float)i));
			Object.Instantiate(spread_shot, base.transform.position, rotation);
		}
		Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "player")
		{
			do_explode();
		}
	}
}
