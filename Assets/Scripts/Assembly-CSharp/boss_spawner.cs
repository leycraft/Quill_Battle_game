using TMPro;
using UnityEngine;

public class boss_spawner : MonoBehaviour
{
	public string file_to_load;

	public string text_show;

	public string boss_ui_to_go;

	public GameObject boss_to_spawn;

	public GameObject parent_obj;

	private TextMeshPro text;

	private GameObject spawn_area;

	private void Start()
	{
		text = base.transform.Find("text_obj").gameObject.GetComponent<TextMeshPro>();
		spawn_area = base.transform.Find("spawn_point").gameObject;
	}

	private void Update()
	{
		if (text_show != "")
		{
			text.SetText(text_show);
		}
		else
		{
			text.SetText(file_to_load);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "player" && file_to_load != "" && boss_to_spawn != null)
		{
			GameObject gameObject = Object.Instantiate(boss_to_spawn, spawn_area.transform.position, Quaternion.identity);
			gameObject.name = boss_to_spawn.name;
			gameObject.GetComponent<boss_base>().attack_file = file_to_load;
			GameObject.Find(boss_ui_to_go).gameObject.GetComponent<boss_hp_ui>().boss = gameObject.GetComponent<boss_hp>();
			if (parent_obj != null)
			{
				Object.Destroy(parent_obj);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
