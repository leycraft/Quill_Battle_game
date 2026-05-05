using System.Collections;
using UnityEngine;

public class bomb_player : MonoBehaviour
{
	public GameObject bomb;

	public GameObject indicator;

	private void Start()
	{
		StartCoroutine(bomb_away());
	}

	private void Update()
	{
	}

	private IEnumerator bomb_away()
	{
		yield return new WaitForSeconds(0.4f);
		indicator.SetActive(value: false);
		bomb.SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		Object.Destroy(base.gameObject);
	}
}
