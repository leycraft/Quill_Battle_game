using System.Collections;
using UnityEngine;

public class player_attack : MonoBehaviour
{
	private GameObject visual;

	private GameObject tail1;

	private GameObject tail2;

	public float tail_animation;

	public int attack_mode;

	private Vector3 mouse_pos;

	private GameObject attack_point;

	private GameObject melee_attack;

	public GameObject bullet;

	private GameObject range_spawn_point;

	public GameObject magic;

	private GameObject melee_attack_ex;

	public GameObject melee_ex;

	private GameObject bullet_ex;

	public GameObject magic_ex;

	public Animator ex_activate;

	private GameObject crosshair;

	public float range_stamina = 100f;

	public float magic_stamina = 100f;

	private float range_stamina_cap = 100f;

	private float magic_stamina_cap = 100f;

	public bool range_fatigue;

	public bool magic_fatigue;

	private float stamina_recovery_rate = 10f;

	public float melee_cooldown = 5f;

	public float melee_EX_cooldown = 5f;

	private float melee_cooldown_count;

	private float melee_EX_cooldown_count;

	public float range_cooldown = 5f;

	public float range_EX_cooldown = 5f;

	private float range_cooldown_count;

	private float range_EX_cooldown_count;

	public float magic_cooldown = 30f;

	public float magic_EX_cooldown = 5f;

	private float magic_cooldown_count;

	private float magic_EX_cooldown_count;

	private void Start()
	{
		visual = base.gameObject.transform.Find("visual/visual_quill").gameObject;
		tail1 = base.gameObject.transform.Find("visual/visual_quill/tail1").gameObject;
		tail2 = base.gameObject.transform.Find("visual/visual_quill/tail2").gameObject;
		attack_point = base.gameObject.transform.Find("attack").gameObject;
		melee_attack = base.gameObject.transform.Find("attack/PL_slash").gameObject;
		melee_attack_ex = base.gameObject.transform.Find("attack/PL_slash_ex").gameObject;
		bullet_ex = base.gameObject.transform.Find("attack/PL_range_ex").gameObject;
		crosshair = base.gameObject.transform.Find("visual/crosshair").gameObject;
		range_spawn_point = base.gameObject.transform.Find("attack/range_spawn_point").gameObject;
		range_stamina = range_stamina_cap;
		magic_stamina = magic_stamina_cap;
	}

	private void Update()
	{
		mouse_pos = Input.mousePosition;
		crosshair_location();
		attack_rotation();
		tail_animation_update();
		if (Input.GetKeyDown(KeyCode.E))
		{
			switch_mode(forward: true);
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			switch_mode(forward: false);
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			attack_mode = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			attack_mode = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			attack_mode = 2;
		}
		attacking();
		recover_stamina_cooldown();
	}

	private void switch_mode(bool forward)
	{
		if (forward)
		{
			attack_mode = (attack_mode + 1) % 3;
			return;
		}
		attack_mode--;
		if (attack_mode == -1)
		{
			attack_mode = 2;
		}
	}

	private void attacking()
	{
		if (Input.GetMouseButton(0))
		{
			if (attack_mode == 0 && melee_cooldown_count <= 0f)
			{
				melee_cooldown_count = melee_cooldown;
				melee_attack.SetActive(value: true);
				StartCoroutine(melee_disappear());
			}
			if (attack_mode == 1 && range_cooldown_count <= 0f && range_stamina >= 3f)
			{
				if (!range_fatigue)
				{
					range_cooldown_count = range_cooldown;
				}
				else
				{
					range_cooldown_count = range_cooldown * 2f;
				}
				spent_stamina(1, 3.5f);
				do_range_attack();
				tail_animation = 0.5f;
			}
			if (attack_mode == 2 && magic_cooldown_count <= 0f && magic_stamina >= 15f)
			{
				if (!magic_fatigue)
				{
					magic_cooldown_count = magic_cooldown;
				}
				else
				{
					magic_cooldown_count = magic_cooldown * 2f;
				}
				spent_stamina(2, 30f);
				do_magic_attack();
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (attack_mode == 0 && melee_cooldown_count <= 0f && range_stamina >= 100f && magic_stamina >= 100f)
			{
				melee_cooldown_count = 0.5f;
				range_stamina = 0f;
				magic_stamina = 0f;
				melee_attack_ex.SetActive(value: true);
				do_melee_ex();
				StartCoroutine(melee_ex_disappear());
				ex_activate.SetTrigger("activate");
			}
			if (attack_mode == 1 && range_cooldown_count <= 0f && range_stamina >= 100f)
			{
				melee_cooldown_count = 2f;
				range_cooldown_count = 2f;
				range_stamina = 0f;
				bullet_ex.SetActive(value: true);
				StartCoroutine(range_ex_disappear());
				ex_activate.SetTrigger("activate");
			}
			if (attack_mode == 2 && magic_cooldown_count <= 0f && magic_stamina >= 15f)
			{
				magic_cooldown_count = 3f;
				magic_stamina = 0f;
				do_magic_ex();
				ex_activate.SetTrigger("activate");
			}
		}
	}

	private void crosshair_location()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(mouse_pos);
		position.z = 0f;
		crosshair.transform.position = position;
		if (crosshair.transform.localPosition.x < 0f)
		{
			visual.transform.localScale = new Vector3(-1f, visual.transform.localScale.y, visual.transform.localScale.z);
		}
		else if (crosshair.transform.localPosition.x > 0f)
		{
			visual.transform.localScale = new Vector3(1f, visual.transform.localScale.y, visual.transform.localScale.z);
		}
	}

	private void attack_rotation()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouse_pos);
		Vector3 normalized = (attack_point.transform.position - vector).normalized;
		float num = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
		attack_point.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num - 90f));
	}

	private void tail_animation_update()
	{
		if (tail_animation > 0f)
		{
			tail_animation -= Time.deltaTime;
			tail1.SetActive(value: false);
			tail2.SetActive(value: true);
		}
		else
		{
			tail1.SetActive(value: true);
			tail2.SetActive(value: false);
		}
	}

	private IEnumerator melee_disappear()
	{
		yield return new WaitForSeconds(0.1f);
		melee_attack.SetActive(value: false);
	}

	private IEnumerator melee_ex_disappear()
	{
		yield return new WaitForSeconds(0.3f);
		melee_attack_ex.SetActive(value: false);
	}

	private IEnumerator range_ex_disappear()
	{
		yield return new WaitForSeconds(2f);
		bullet_ex.SetActive(value: false);
	}

	private void spent_stamina(int type, float amount_spent)
	{
		switch (type)
		{
		case 1:
			range_stamina -= amount_spent;
			if (range_stamina < 0f)
			{
				range_stamina = 0f;
				range_fatigue = true;
			}
			break;
		case 2:
			magic_stamina -= amount_spent;
			if (magic_stamina < 0f)
			{
				magic_stamina = 0f;
				magic_fatigue = true;
			}
			break;
		}
	}

	private void recover_stamina_cooldown()
	{
		if (melee_cooldown_count > 0f)
		{
			melee_cooldown_count -= Time.deltaTime;
		}
		if (range_cooldown_count > 0f)
		{
			range_cooldown_count -= Time.deltaTime;
		}
		if (magic_cooldown_count > 0f)
		{
			magic_cooldown_count -= Time.deltaTime;
		}
		if (range_stamina < range_stamina_cap)
		{
			if (range_fatigue)
			{
				range_stamina += stamina_recovery_rate / 2f * Time.deltaTime;
			}
			else
			{
				range_stamina += stamina_recovery_rate * Time.deltaTime;
			}
			if (range_stamina > range_stamina_cap)
			{
				range_stamina = range_stamina_cap;
				range_fatigue = false;
			}
		}
		if (magic_stamina < magic_stamina_cap)
		{
			if (magic_fatigue)
			{
				magic_stamina += stamina_recovery_rate / 2f * Time.deltaTime;
			}
			else
			{
				magic_stamina += stamina_recovery_rate * Time.deltaTime;
			}
			if (magic_stamina > magic_stamina_cap)
			{
				magic_stamina = magic_stamina_cap;
				magic_fatigue = false;
			}
		}
	}

	private void do_range_attack()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouse_pos);
		Vector3 normalized = (attack_point.transform.position - vector).normalized;
		float num = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
		Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num - 90f));
		Object.Instantiate(bullet, range_spawn_point.transform.position, rotation);
	}

	private void do_magic_attack()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(mouse_pos);
		position.z = 0f;
		Object.Instantiate(magic, position, Quaternion.identity);
	}

	private void do_melee_ex()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouse_pos);
		Vector3 normalized = (attack_point.transform.position - vector).normalized;
		float num = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
		Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num - 90f));
		Object.Instantiate(melee_ex, range_spawn_point.transform.position, rotation);
	}

	private void do_magic_ex()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(mouse_pos);
		position.z = 0f;
		Object.Instantiate(magic_ex, position, Quaternion.identity);
	}

	public Vector3 get_crosshair_pos()
	{
		return crosshair.transform.localPosition;
	}
}
