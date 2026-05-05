using System.Collections;
using UnityEngine;

public class gunma_explosion : MonoBehaviour
{
	public float life_time = 1f;

	private void Start()
	{
		StartCoroutine(explosion_end());
	}

	private void Update()
	{
	}

	private IEnumerator explosion_end()
	{
		yield return new WaitForSeconds(life_time);
		Object.Destroy(base.gameObject);
	}
}
