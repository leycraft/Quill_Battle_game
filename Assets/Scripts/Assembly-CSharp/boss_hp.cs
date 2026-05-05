using UnityEngine;

public class boss_hp : MonoBehaviour
{
	public float max_hp = 1000f;

	public float hp = 1000f;

	public float i_frame;

	private float i_frame_duration = 0.1f;

	private void Start()
	{
		hp = max_hp;
	}

	private void Update()
	{
		if (i_frame > 0f)
		{
			i_frame -= Time.deltaTime;
		}
	}

	private void is_hurt(float damage)
	{
		if (i_frame <= 0f)
		{
			hp -= damage;
			i_frame = i_frame_duration;
			if (hp <= 0f)
			{
				hp = 0f;
			}
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
