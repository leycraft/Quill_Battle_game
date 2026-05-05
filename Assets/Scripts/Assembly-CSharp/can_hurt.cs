using UnityEngine;

public class can_hurt : MonoBehaviour
{
	public float damage = 1f;

	public bool gone_on_hit;

	public GameObject ammo_blast;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void remove_attack()
	{
		if (gone_on_hit)
		{
			if (ammo_blast != null)
			{
				Object.Instantiate(ammo_blast, base.gameObject.transform.position, Quaternion.identity);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
