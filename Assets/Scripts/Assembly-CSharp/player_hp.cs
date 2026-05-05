using UnityEngine;

public class player_hp : MonoBehaviour
{
	public float hp = 500f;

	private float i_frame;

	public float hp_cap = 500f;

	private float hp_recovery_rate = 4f;

	private float dash_cooldown;

	public float dash_cooldown_counter = 5f;

	private float i_frame_duration = 0.5f;

	public bool invincible;

	private float heal_disable_time = 5f;

	private float heal_disable_time_count;

	public GameObject shield;

	public Animator hurt_screen;

	private player player;

	public GameObject death_explosion;

	private void Start()
	{
		player = GameObject.Find("player").GetComponent<player>();
	}

	private void Update()
	{
		recovery_counter();
		if (i_frame > 0f)
		{
			shield.SetActive(value: true);
		}
		else
		{
			shield.SetActive(value: false);
		}
		if (Input.GetKeyDown(KeyCode.Space) && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && dash_cooldown <= 0f)
		{
			dash_cooldown = dash_cooldown_counter;
			i_frame = 1f;
			player.enter_dash_mode(1f);
		}
	}

	private void is_hurt(float damage)
	{
		if (!invincible && i_frame <= 0f)
		{
			hp -= damage;
			i_frame = i_frame_duration;
			heal_disable_time_count = heal_disable_time;
			if (hp <= 0f)
			{
				GameObject obj = GameObject.Find("player");
				Object.Instantiate(death_explosion, base.transform.position, Quaternion.identity);
				Object.Destroy(obj);
			}
			hurt_screen.SetTrigger("get_hit");
		}
	}

	private void recovery_counter()
	{
		if (heal_disable_time_count <= 0f && hp < hp_cap)
		{
			hp += hp_recovery_rate * Time.deltaTime;
			if (hp > hp_cap)
			{
				hp = hp_cap;
			}
		}
		if (i_frame > 0f)
		{
			i_frame -= Time.deltaTime;
		}
		if (heal_disable_time_count > 0f)
		{
			heal_disable_time_count -= Time.deltaTime;
		}
		if (dash_cooldown > 0f)
		{
			dash_cooldown -= Time.deltaTime;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.name.StartsWith("EN_"))
		{
			can_hurt component = collision.GetComponent<can_hurt>();
			if (component != null && i_frame <= 0f)
			{
				is_hurt(component.damage);
				component.remove_attack();
			}
		}
	}
}
