using UnityEngine;

public class drone_hp : MonoBehaviour
{
	public float max_hp = 1000f;

	public float hp = 1000f;

	public GameObject explosion;

	private GameObject drone_location;

	private void Start()
	{
		drone_location = base.transform.Find("drone").gameObject;
		hp = max_hp;
	}

	private void Update()
	{
	}

	private void is_hurt(float damage)
	{
		hp -= damage;
		if (hp < 0f)
		{
			Object.Instantiate(explosion, drone_location.transform.position, Quaternion.identity);
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.name.StartsWith("PL_"))
		{
			can_hurt component = collision.GetComponent<can_hurt>();
			if (component != null)
			{
				is_hurt(component.damage);
				component.remove_attack();
			}
		}
	}
}
