using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrameRateCap : MonoBehaviour
{

	float deltaTime = 0.0f;

	void Awake(){
		QualitySettings.vSyncCount = 1;
		Application.targetFrameRate = 60;

	}


	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}
