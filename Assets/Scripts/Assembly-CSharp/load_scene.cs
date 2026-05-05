using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene : MonoBehaviour
{
	public string sceneName;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}
