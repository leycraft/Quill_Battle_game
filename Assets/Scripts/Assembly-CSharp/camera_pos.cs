using UnityEngine;

public class camera_pos : MonoBehaviour
{
	public GameObject player;

	private void Start()
	{
	}

	private void Update()
	{
		if (player != null)
		{
			Vector3 position = player.transform.position;
			position.z = base.transform.position.z;
			base.transform.position = position;
		}
	}
}
