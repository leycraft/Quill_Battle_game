using System.Collections;
using UnityEngine;

public class ammo_blast : MonoBehaviour
{
	public float life_time = 1f;

	public Animator blast;

	private void Start()
	{
		StartCoroutine(remove_effect());
	}

	private void Update()
	{
	}

	private IEnumerator remove_effect()
	{
		yield return new WaitForSeconds(life_time);
		Object.Destroy(base.gameObject);
	}
}
