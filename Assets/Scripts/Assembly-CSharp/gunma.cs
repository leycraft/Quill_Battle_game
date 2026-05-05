using System.Collections.Generic;
using UnityEngine;

public class gunma : boss_base
{
	public GameObject spread_shot;

	public GameObject base_bullet;

	public GameObject base_bullet2;

	public GameObject rocket;

	public GameObject railgun;

	public LineRenderer railgun_laser;

	public GameObject railgun_attack;

	public GameObject laser_attack;

	public GameObject crosshair;

	public GameObject aimbot;

	public GameObject drone;

	private GameObject visual;

	private GameObject fire_slow;

	private GameObject fire_fast;

	private Transform railgun_point;

	private float bullet_drop_cooldown = 0.05f;

	private float visual_x = 1f;

	private float bullet_drop_timer;

	private float volley_loop;

	private int next_spot;

	private bool back_forth_check;

	private List<GameObject> aimbot_spot = new List<GameObject>();

	private new void Start()
	{
		base.Start();
		visual = base.transform.Find("visual").gameObject;
		visual_x = visual.transform.localScale.x;
		fire_slow = base.transform.Find("visual/fire_slow").gameObject;
		fire_fast = base.transform.Find("visual/fire_fast").gameObject;
		spellcard = GameObject.Find("spellcard_for_bosses").transform.Find("gunma/gunma_spell").gameObject.GetComponent<Animator>();
		spellcard_body = GameObject.Find("spellcard_for_bosses").transform.Find("gunma/gunma_body").gameObject.GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		check_spellcard();
		check_hp();
		check_animation();
		if (spellcard_phase == 1)
		{
			attack_SP1();
			return;
		}
		if (ending_phase == 1)
		{
			END1();
			return;
		}
		switch (attack_type)
		{
		case "A1":
			attack_A1();
			break;
		case "A1E1":
			attack_A1E1();
			break;
		case "A1E2":
			attack_A1E2();
			break;
		case "A2":
			attack_A2();
			break;
		case "A2E1":
			attack_A2E1();
			break;
		case "A3":
			attack_A3();
			break;
		case "A3E1":
			attack_A3E1();
			break;
		case "A3E2":
			attack_A3E2();
			break;
		case "A4":
			attack_A4();
			break;
		case "A5":
			attack_A5();
			break;
		case "A5E1":
			attack_A5E1();
			break;
		case "A6":
			attack_A6();
			break;
		case "A6E1":
			attack_A6E1();
			break;
		case "A7":
			attack_A7();
			break;
		case "A7A1":
			attack_A7A1();
			break;
		case "A7E1":
			attack_A7E1();
			break;
		default:
			initiate_new_attack();
			break;
		}
	}

	private new void reset_attack()
	{
		base.reset_attack();
		bullet_drop_timer = 0f;
		volley_loop = 0f;
		next_spot = 0;
		back_forth_check = false;
		railgun_laser.gameObject.SetActive(value: false);
	}

	private void check_spellcard()
	{
		if (spellcard_phase == 0 && (double)hp.hp < (double)hp.max_hp * 0.5)
		{
			spellcard_phase_increase();
			spellcard.SetTrigger("activate");
			spellcard_body.SetTrigger("activate");
		}
		else if (spellcard_phase == 1 && (double)hp.hp < (double)hp.max_hp * 0.3)
		{
			spellcard_phase_increase();
			spellcard.SetTrigger("deactivate");
		}
	}

	private void check_hp()
	{
		if (hp.hp <= 0f && !is_escaping)
		{
			ending_phase = 1;
			is_escaping = true;
			reset_attack();
		}
	}

	private void check_animation()
	{
		if (rb.linearVelocity.magnitude > 15f)
		{
			fire_slow.SetActive(value: false);
			fire_fast.SetActive(value: true);
		}
		else
		{
			fire_slow.SetActive(value: true);
			fire_fast.SetActive(value: false);
		}
		GameObject gameObject = GameObject.Find("player");
		if (gameObject != null)
		{
			if (gameObject.transform.position.x < base.gameObject.transform.position.x)
			{
				visual.transform.localScale = new Vector3(0f - visual_x, visual.transform.localScale.y, visual.transform.localScale.z);
			}
			else
			{
				visual.transform.localScale = new Vector3(visual_x, visual.transform.localScale.y, visual.transform.localScale.z);
			}
		}
	}

	private void spellcard_phase_increase()
	{
		spellcard_phase++;
		reset_attack();
	}

	private void attack_A1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(15f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "temp_point_gunma";
			gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
			goto_point = gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 3)
		{
			move_to(45f);
			if ((double)((Vector2)(base.transform.position - goto_point.position)).magnitude <= 0.2)
			{
				attack_sequence++;
				Object.Destroy(goto_point.gameObject);
			}
		}
		else if (attack_sequence == 4)
		{
			reset_attack();
		}
	}

	private void attack_A1E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(15f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "temp_point_gunma";
			gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
			goto_point = gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				bullet_drop_timer = bullet_drop_cooldown;
			}
		}
		else if (attack_sequence == 3)
		{
			move_to(45f);
			Vector2 vector = base.transform.position - goto_point.position;
			bullet_drop_timer -= Time.deltaTime;
			if (bullet_drop_timer <= 0f)
			{
				Object.Instantiate(spread_shot, base.transform.position, Quaternion.identity);
				bullet_drop_timer = bullet_drop_cooldown;
			}
			if ((double)vector.magnitude <= 0.2)
			{
				attack_sequence++;
				Object.Destroy(goto_point.gameObject);
			}
		}
		else if (attack_sequence == 4)
		{
			reset_attack();
		}
	}

	private void attack_A1E2()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				repeat_attack = 3;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(15f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "temp_point_gunma";
			gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
			goto_point = gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 3)
		{
			move_to(45f, slow_in: false);
			if ((double)((Vector2)(base.transform.position - goto_point.position)).magnitude <= 0.5)
			{
				float num2 = 45f;
				for (int i = 0; i < 8; i++)
				{
					Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, num2 * (float)i));
					Object.Instantiate(base_bullet, base.transform.position, rotation);
				}
				attack_sequence++;
				Object.Destroy(goto_point.gameObject);
				attack_timer = 0.5f;
			}
		}
		else if (attack_sequence == 4)
		{
			attack_timer -= Time.deltaTime;
			if (repeat_attack <= 0)
			{
				reset_attack();
			}
			else if (attack_timer <= 0f)
			{
				attack_sequence = 2;
				repeat_attack--;
			}
		}
	}

	private void attack_A2()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			if (num > 1)
			{
				num += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
				repeat_attack = 1;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.5f;
			}
		}
		else if (attack_sequence == 2)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				float num2 = 45f;
				for (int i = 0; i < 3; i++)
				{
					Quaternion rotation = Quaternion.Euler(0f, 0f, 90f + (0f - num2 + num2 * (float)i));
					Object.Instantiate(rocket, base.transform.position, rotation);
				}
				if (volley_loop == 0f)
				{
					attack_sequence++;
					return;
				}
				attack_timer = 0.3f;
				volley_loop -= 1f;
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			if (repeat_attack > 0)
			{
				int num3 = Random.Range(1, 4);
				if (num3 > 1)
				{
					num3 += 2;
				}
				goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num3}").gameObject.transform;
				if (goto_point != null)
				{
					attack_timer = 1.5f;
				}
				attack_sequence = 1;
				repeat_attack--;
				volley_loop += 1f;
			}
			else
			{
				reset_attack();
			}
		}
	}

	private void attack_A2E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			if (num > 1)
			{
				num += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
				repeat_attack = 2;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.5f;
			}
		}
		else if (attack_sequence == 2)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			float num2 = 45f;
			for (int i = 0; i < 5; i++)
			{
				Quaternion rotation = Quaternion.Euler(0f, 0f, 90f + ((0f - num2) * 2f + num2 * (float)i));
				gunma_rocket component = Object.Instantiate(rocket, base.transform.position, rotation).GetComponent<gunma_rocket>();
				component.add_spread = true;
				if (Random.Range(0, 2) == 0)
				{
					component.diagonal = true;
				}
			}
			if (volley_loop == 0f)
			{
				attack_sequence++;
				return;
			}
			attack_timer = 0.3f;
			volley_loop -= 1f;
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			if (repeat_attack > 0)
			{
				int num3 = Random.Range(1, 4);
				if (num3 > 1)
				{
					num3 += 2;
				}
				goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num3}").gameObject.transform;
				if (goto_point != null)
				{
					attack_timer = 1.5f;
					attack_sequence = 1;
					repeat_attack--;
				}
				if (repeat_attack == 1)
				{
					volley_loop += 1f;
				}
				else
				{
					volley_loop += 2f;
				}
			}
			else
			{
				reset_attack();
			}
		}
	}

	private void attack_A3()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
				repeat_attack = 2;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.3f;
				railgun_laser.gameObject.SetActive(value: true);
			}
		}
		else if (attack_sequence == 2)
		{
			if (railgun_point == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "temp_point_gunma";
				gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
				railgun_point = gameObject.transform;
			}
			Vector3 normalized = (railgun.transform.position - railgun_point.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			railgun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			railgun_laser.SetPosition(0, base.transform.position);
			railgun_laser.SetPosition(1, railgun_point.position);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.25f;
				railgun_laser.gameObject.SetActive(value: false);
				Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
				Object.Instantiate(railgun_attack, base.transform.position, rotation);
				Object.Destroy(railgun_point.gameObject);
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					reset_attack();
					return;
				}
				railgun_laser.gameObject.SetActive(value: true);
				attack_timer = 0.3f;
				repeat_attack--;
				attack_sequence = 2;
			}
		}
	}

	private void attack_A3E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
				repeat_attack = 4;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.3f;
				railgun_laser.gameObject.SetActive(value: true);
			}
		}
		else if (attack_sequence == 2)
		{
			if (railgun_point == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "temp_point_gunma";
				gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
				railgun_point = gameObject.transform;
			}
			Vector3 normalized = (railgun.transform.position - railgun_point.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			railgun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			railgun_laser.SetPosition(0, base.transform.position);
			railgun_laser.SetPosition(1, railgun_point.position);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 0.25f;
				railgun_laser.gameObject.SetActive(value: false);
				Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
				Object.Instantiate(railgun_attack, base.transform.position, rotation).GetComponent<gunma_railgun>().fire_bullet = true;
				Object.Destroy(railgun_point.gameObject);
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					reset_attack();
					return;
				}
				railgun_laser.gameObject.SetActive(value: true);
				attack_timer = 0.3f;
				repeat_attack--;
				attack_sequence = 2;
			}
		}
	}

	private void attack_A3E2()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
				repeat_attack = 2;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 1f;
				railgun_laser.gameObject.SetActive(value: true);
			}
		}
		else if (attack_sequence == 2)
		{
			if (railgun_point == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "temp_point_gunma";
				gameObject.transform.position = GameObject.Find("gunma_guide_point").transform.position;
				railgun_point = gameObject.transform;
			}
			Vector3 normalized = (railgun.transform.position - railgun_point.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			railgun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			railgun_laser.SetPosition(0, base.transform.position);
			railgun_laser.SetPosition(1, railgun_point.position);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				attack_timer = 5.2f;
				railgun_laser.gameObject.SetActive(value: false);
				Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
				Object.Instantiate(laser_attack, base.transform.position, rotation);
				Object.Destroy(railgun_point.gameObject);
			}
		}
		else if (attack_sequence == 3)
		{
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				reset_attack();
			}
		}
	}

	private void attack_A4()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(6, 8);
			if (num == 6)
			{
				next_spot = 7;
			}
			else
			{
				next_spot = 6;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				repeat_attack = 7;
				attack_timer = 0.3f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					attack_sequence++;
					repeat_attack = 7;
					attack_timer = 0.3f;
				}
				else
				{
					GameObject gameObject = GameObject.Find("gunma_guide_point");
					Object.Instantiate(crosshair, gameObject.transform.position, Quaternion.identity);
					attack_timer = 0.3f;
					repeat_attack--;
				}
			}
		}
		else if (attack_sequence == 2)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				repeat_attack = 14;
				attack_timer = 0.15f;
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					reset_attack();
					return;
				}
				GameObject gameObject2 = new GameObject();
				gameObject2.name = "temp_point_gunma";
				gameObject2.transform.position = GameObject.Find("gunma_guide_point").transform.position;
				float num2 = Random.Range(-2f, 2f);
				float num3 = Random.Range(-2f, 2f);
				gameObject2.transform.position = new Vector3(gameObject2.transform.position.x + num2, gameObject2.transform.position.y + num3, 0f);
				Object.Instantiate(crosshair, gameObject2.transform.position, Quaternion.identity);
				Object.Destroy(gameObject2);
				attack_timer = 0.15f;
				repeat_attack--;
			}
		}
	}

	private void attack_A5()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			if (num > 1)
			{
				num += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 5;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					attack_sequence++;
					repeat_attack = 5;
					return;
				}
				GameObject gameObject = GameObject.Find("gunma_guide_point");
				int num2 = Random.Range(0, 2);
				Quaternion rotation = Quaternion.Euler(0f, 0f, 180 * num2);
				Object.Instantiate(aimbot, gameObject.transform.position, rotation);
				attack_timer = 0.2f;
				repeat_attack--;
			}
		}
		else if (attack_sequence == 2)
		{
			int num3 = Random.Range(1, 4);
			if (num3 > 1)
			{
				num3 += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num3}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 5;
			}
		}
		else if (attack_sequence == 3)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					attack_sequence++;
					return;
				}
				GameObject gameObject2 = GameObject.Find("gunma_guide_point");
				int num4 = Random.Range(0, 2);
				Quaternion rotation2 = Quaternion.Euler(0f, 0f, 90 + 180 * num4);
				Object.Instantiate(aimbot, gameObject2.transform.position, rotation2);
				attack_timer = 0.2f;
				repeat_attack--;
			}
		}
		else if (attack_sequence == 4)
		{
			int num5 = Random.Range(1, 4);
			if (num5 > 1)
			{
				num5 += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num5}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.3f;
				for (int i = 0; i < 8; i++)
				{
					aimbot_spot.Add(GameObject.Find("gunma_guide_point").transform.Find($"cross{i + 1}").gameObject);
				}
			}
		}
		else if (attack_sequence == 5)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (aimbot_spot.Count == 0)
				{
					attack_sequence++;
					return;
				}
				int index = Random.Range(0, aimbot_spot.Count);
				float num6 = 15f;
				GameObject gameObject3 = aimbot_spot[index];
				GameObject gameObject4 = GameObject.Find("gunma_guide_point");
				Vector3 normalized = (gameObject3.transform.position - gameObject4.transform.position).normalized;
				float num7 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
				Quaternion rotation3 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num7 - 90f));
				Object.Instantiate(aimbot, gameObject3.transform.position, rotation3);
				rotation3 *= Quaternion.Euler(new Vector3(0f, 0f, num6));
				Object.Instantiate(aimbot, gameObject3.transform.position, rotation3);
				rotation3 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num6 * 2f));
				Object.Instantiate(aimbot, gameObject3.transform.position, rotation3);
				aimbot_spot.Remove(aimbot_spot[index]);
				attack_timer = 0.3f;
			}
		}
		else if (attack_sequence == 6)
		{
			int num8 = Random.Range(1, 4);
			if (num8 > 1)
			{
				num8 += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num8}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.4f;
				repeat_attack = 3;
			}
		}
		else
		{
			if (attack_sequence != 7)
			{
				return;
			}
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (repeat_attack == 0)
			{
				reset_attack();
				return;
			}
			GameObject gameObject5 = GameObject.Find("gunma_guide_point");
			for (int j = 0; j < 8; j++)
			{
				Quaternion rotation4 = Quaternion.Euler(0f, 0f, 45 * j);
				Object.Instantiate(aimbot, gameObject5.transform.position, rotation4);
			}
			attack_timer = 0.4f;
			repeat_attack--;
		}
	}

	private void attack_A5E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(1, 4);
			if (num > 1)
			{
				num += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.3f;
				repeat_attack = 10;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					attack_sequence++;
					repeat_attack = 5;
					return;
				}
				GameObject gameObject = GameObject.Find("gunma_guide_point");
				int num2 = Random.Range(0, 4);
				Quaternion rotation = Quaternion.Euler(0f, 0f, 90 * num2);
				Object.Instantiate(aimbot, gameObject.transform.position, rotation).GetComponent<gunma_aimbot>().hard_version = true;
				attack_timer = 0.3f;
				repeat_attack--;
			}
		}
		else if (attack_sequence == 2)
		{
			int num3 = Random.Range(1, 4);
			if (num3 > 1)
			{
				num3 += 2;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num3}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.4f;
				for (int i = 0; i < 8; i++)
				{
					aimbot_spot.Add(GameObject.Find("gunma_guide_point").transform.Find($"cross{i + 1}").gameObject);
				}
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (aimbot_spot.Count == 0)
			{
				reset_attack();
				return;
			}
			int index = Random.Range(0, aimbot_spot.Count);
			float num4 = 15f;
			GameObject gameObject2 = aimbot_spot[index];
			GameObject gameObject3 = GameObject.Find("gunma_guide_point");
			Vector3 normalized = (gameObject2.transform.position - gameObject3.transform.position).normalized;
			float num5 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num5 - 90f));
			rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num4));
			for (int j = 0; j < 3; j++)
			{
				gunma_aimbot component = Object.Instantiate(aimbot, gameObject2.transform.position, rotation2).GetComponent<gunma_aimbot>();
				component.hard_version = true;
				component.bullet_life = 0.44f;
				rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, num4));
			}
			aimbot_spot.Remove(aimbot_spot[index]);
			attack_timer = 0.4f;
		}
	}

	private void attack_A6()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			if (num == 4)
			{
				next_spot = 5;
			}
			else
			{
				next_spot = 4;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(20f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				repeat_attack = 7;
				attack_timer = 0.2f;
			}
		}
		else if (attack_sequence == 2)
		{
			move_to(20f);
			GameObject gameObject = GameObject.Find("gunma_guide_point");
			Vector3 normalized = (base.transform.position - gameObject.transform.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			float num3 = 10f;
			rotation *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num3 * 3f + num3 * (float)(repeat_attack - 1)));
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (repeat_attack == 0)
			{
				attack_sequence++;
				return;
			}
			for (int i = 0; i < 5; i++)
			{
				Object.Instantiate(base_bullet, base.transform.position, rotation).GetComponent<bullet_movement>().bullet_speed = 5 * (i + 1);
			}
			attack_timer = 0.2f;
			repeat_attack--;
		}
		else if (attack_sequence == 3)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 4)
		{
			move_to(20f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				repeat_attack = 9;
				volley_loop = 6f;
				attack_timer = 0.2f;
			}
		}
		else
		{
			if (attack_sequence != 5)
			{
				return;
			}
			move_to(20f);
			GameObject gameObject2 = GameObject.Find("gunma_guide_point");
			Vector3 normalized2 = (base.transform.position - gameObject2.transform.position).normalized;
			float num4 = Mathf.Atan2(normalized2.x, normalized2.y) * 57.29578f;
			Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num4 - 90f));
			float num5 = 10f;
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (volley_loop == 0f)
			{
				reset_attack();
				return;
			}
			if (back_forth_check)
			{
				rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num5 * 4f + num5 * (float)(repeat_attack - 1)));
			}
			else
			{
				rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, num5 * 4f - num5 * (float)(repeat_attack - 1)));
			}
			for (int j = 0; j < 2; j++)
			{
				Object.Instantiate(base_bullet2, base.transform.position, rotation2).GetComponent<bullet_movement>().bullet_speed = 10 * (j + 1);
			}
			attack_timer = 0.1f;
			repeat_attack--;
			if (repeat_attack == 0)
			{
				back_forth_check = !back_forth_check;
				volley_loop -= 1f;
				repeat_attack = 9;
			}
		}
	}

	private void attack_A6E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			if (num == 4)
			{
				next_spot = 5;
			}
			else
			{
				next_spot = 4;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
				repeat_attack = 5;
				volley_loop = 4f;
				attack_timer = 0.2f;
			}
		}
		else if (attack_sequence == 2)
		{
			move_to(20f);
			GameObject gameObject = GameObject.Find("gunma_guide_point");
			Vector3 normalized = (base.transform.position - gameObject.transform.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			Quaternion rotation2 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			float num3 = 15f;
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (volley_loop == 0f)
			{
				attack_sequence++;
				return;
			}
			if (back_forth_check)
			{
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, num3 * (float)repeat_attack));
				rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num3 * (float)repeat_attack));
			}
			else
			{
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, num3 * (float)(5 - repeat_attack)));
				rotation2 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num3 * (float)(5 - repeat_attack)));
			}
			for (int i = 0; i < 5; i++)
			{
				Object.Instantiate(base_bullet, base.transform.position, rotation).GetComponent<bullet_movement>().bullet_speed = 5 * (i + 1);
			}
			for (int j = 0; j < 5; j++)
			{
				Object.Instantiate(base_bullet, base.transform.position, rotation2).GetComponent<bullet_movement>().bullet_speed = 5 * (j + 1);
			}
			repeat_attack--;
			attack_timer = 0.2f;
			if (repeat_attack == 0)
			{
				back_forth_check = !back_forth_check;
				volley_loop -= 1f;
				repeat_attack = 5;
			}
		}
		else if (attack_sequence == 3)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find("spot1").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 8;
			}
		}
		else if (attack_sequence == 4)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				GameObject gameObject2 = GameObject.Find("gunma_guide_point");
				Vector3 normalized2 = (base.transform.position - gameObject2.transform.position).normalized;
				float num4 = Mathf.Atan2(normalized2.x, normalized2.y) * 57.29578f;
				Quaternion rotation3 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num4 - 90f));
				rotation3 *= Quaternion.Euler(new Vector3(0f, 0f, -30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation3);
				rotation3 *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation3);
				rotation3 *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation3);
				attack_timer = 0.2f;
				repeat_attack--;
				if (repeat_attack == 0)
				{
					attack_sequence++;
				}
			}
		}
		else if (attack_sequence == 5)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 8;
			}
		}
		else if (attack_sequence == 6)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				GameObject gameObject3 = GameObject.Find("gunma_guide_point");
				Vector3 normalized3 = (base.transform.position - gameObject3.transform.position).normalized;
				float num5 = Mathf.Atan2(normalized3.x, normalized3.y) * 57.29578f;
				Quaternion rotation4 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num5 - 90f));
				rotation4 *= Quaternion.Euler(new Vector3(0f, 0f, -30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation4);
				rotation4 *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation4);
				rotation4 *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation4);
				attack_timer = 0.2f;
				repeat_attack--;
				if (repeat_attack == 0)
				{
					attack_sequence++;
					repeat_attack = 5;
					volley_loop = 4f;
					attack_timer = 0.2f;
				}
			}
		}
		else
		{
			if (attack_sequence != 7)
			{
				return;
			}
			move_to(20f);
			GameObject gameObject4 = GameObject.Find("gunma_guide_point");
			Vector3 normalized4 = (base.transform.position - gameObject4.transform.position).normalized;
			float num6 = Mathf.Atan2(normalized4.x, normalized4.y) * 57.29578f;
			Quaternion rotation5 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num6 - 90f));
			Quaternion rotation6 = Quaternion.Euler(new Vector3(0f, 0f, 0f - num6 - 90f));
			float num7 = 15f;
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (volley_loop == 0f)
			{
				reset_attack();
				return;
			}
			if (repeat_attack % 2 == 0)
			{
				Object.Instantiate(base_bullet2, base.transform.position, rotation5);
			}
			else
			{
				Quaternion rotation7 = Quaternion.Euler(0f, 0f, 0f - num6 - 90f);
				rotation7 *= Quaternion.Euler(0f, 0f, 5f);
				Object.Instantiate(base_bullet2, base.transform.position, rotation7);
				rotation7 *= Quaternion.Euler(0f, 0f, -10f);
				Object.Instantiate(base_bullet2, base.transform.position, rotation7);
			}
			if (back_forth_check)
			{
				rotation5 *= Quaternion.Euler(new Vector3(0f, 0f, num7 * (float)repeat_attack));
				rotation6 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num7 * (float)repeat_attack));
			}
			else
			{
				rotation5 *= Quaternion.Euler(new Vector3(0f, 0f, num7 * (float)(5 - repeat_attack)));
				rotation6 *= Quaternion.Euler(new Vector3(0f, 0f, 0f - num7 * (float)(5 - repeat_attack)));
			}
			for (int k = 0; k < 5; k++)
			{
				Object.Instantiate(base_bullet, base.transform.position, rotation5).GetComponent<bullet_movement>().bullet_speed = 5 * (k + 1);
			}
			for (int l = 0; l < 5; l++)
			{
				Object.Instantiate(base_bullet, base.transform.position, rotation6).GetComponent<bullet_movement>().bullet_speed = 5 * (l + 1);
			}
			repeat_attack--;
			attack_timer = 0.2f;
			if (repeat_attack == 0)
			{
				back_forth_check = !back_forth_check;
				volley_loop -= 1f;
				repeat_attack = 5;
			}
		}
	}

	private void attack_A7()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			if (num == 4)
			{
				next_spot = 5;
			}
			else
			{
				next_spot = 4;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 10;
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					reset_attack();
					return;
				}
				Object.Instantiate(spread_shot, base.transform.position, Quaternion.identity);
				repeat_attack--;
				attack_timer = 0.2f;
			}
		}
	}

	private void attack_A7A1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			if (num == 4)
			{
				next_spot = 5;
			}
			else
			{
				next_spot = 4;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 10;
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				if (repeat_attack == 0)
				{
					reset_attack();
					return;
				}
				Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
				rotation *= Quaternion.Euler(0f, 0f, 15f);
				Object.Instantiate(rocket, base.transform.position, rotation);
				rotation *= Quaternion.Euler(0f, 0f, -30f);
				Object.Instantiate(rocket, base.transform.position, rotation);
				repeat_attack--;
				attack_timer = 0.2f;
			}
		}
	}

	private void attack_A7E1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			if (num == 4)
			{
				next_spot = 5;
			}
			else
			{
				next_spot = 4;
			}
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 1.5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(25f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				attack_sequence++;
			}
		}
		else if (attack_sequence == 2)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{next_spot}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.2f;
				repeat_attack = 10;
			}
		}
		else
		{
			if (attack_sequence != 3)
			{
				return;
			}
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (!(attack_timer <= 0f))
			{
				return;
			}
			if (repeat_attack == 0)
			{
				reset_attack();
				return;
			}
			if (repeat_attack % 2 == 0)
			{
				Object.Instantiate(spread_shot, base.transform.position, Quaternion.identity);
			}
			else
			{
				Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
				rotation *= Quaternion.Euler(0f, 0f, 15f);
				Object.Instantiate(rocket, base.transform.position, rotation).GetComponent<gunma_rocket>().add_spread = true;
				rotation *= Quaternion.Euler(0f, 0f, -30f);
				Object.Instantiate(rocket, base.transform.position, rotation).GetComponent<gunma_rocket>().add_spread = true;
			}
			repeat_attack--;
			attack_timer = 0.2f;
		}
	}

	private void attack_SP1()
	{
		if (attack_sequence == 0)
		{
			int num = Random.Range(4, 6);
			goto_point = GameObject.Find("gunma_guide_point").transform.Find($"spot{num}").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 0.3f;
				repeat_attack = 10;
			}
		}
		else
		{
			if (attack_sequence != 1)
			{
				return;
			}
			move_to(30f);
			attack_timer -= Time.deltaTime;
			if (!(attack_timer < 0f))
			{
				return;
			}
			if (repeat_attack == 0)
			{
				Object.Instantiate(drone, base.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
				Object.Instantiate(drone, base.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 180f)));
				attack_sequence = 0;
				back_forth_check = !back_forth_check;
				return;
			}
			GameObject gameObject = GameObject.Find("gunma_guide_point");
			Vector3 normalized = (base.transform.position - gameObject.transform.position).normalized;
			float num2 = Mathf.Atan2(normalized.x, normalized.y) * 57.29578f;
			Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - num2 - 90f));
			if (back_forth_check)
			{
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, -30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation);
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation);
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, 30f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation);
			}
			else
			{
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, -20f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation);
				rotation *= Quaternion.Euler(new Vector3(0f, 0f, 40f));
				Object.Instantiate(base_bullet2, base.transform.position, rotation);
			}
			repeat_attack--;
			attack_timer = 0.3f;
		}
	}

	private void END1()
	{
		if (attack_sequence == 0)
		{
			goto_point = GameObject.Find("gunma_guide_point").transform.Find("ending1").gameObject.transform;
			if (goto_point != null)
			{
				attack_sequence++;
				attack_timer = 5f;
			}
		}
		else if (attack_sequence == 1)
		{
			move_to(40f);
			attack_timer -= Time.deltaTime;
			if (attack_timer <= 0f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
