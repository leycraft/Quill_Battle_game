using System.Collections;
using UnityEngine;

public class gunma_crosshair : MonoBehaviour
{
	public GameObject shot_fired;

	public float life_time = 1f;

	private void Start()
	{
		StartCoroutine(shooting());
	}

	private void FixedUpdate()
	{
	}

	private IEnumerator shooting()
	{
		yield return new WaitForSeconds(life_time);
		Object.Instantiate(shot_fired, base.transform.position, base.transform.rotation);
		Object.Destroy(base.gameObject);
	}
}
