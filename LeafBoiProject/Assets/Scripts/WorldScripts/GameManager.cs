using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float _deltaTime = 0.0f;
    public int GameState = 0;

    [SerializeField] protected Collider scarfCol;
    [SerializeField] protected GameObject scarf;
    [SerializeField] protected Interaction treeInteraction;
    [SerializeField] protected NPCDialogueTrigger elder;
    [SerializeField] protected Dialogue newElderDialogue;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        GameState = 0;
    }

    public void LoadState()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        int loadedState;

        loadedState = data.state;
        GameState = loadedState;
    }

    //Change state based on integer values
    public void UpdateState(int state)
    {
        if (state > GameState)
        {
            GameState = state;
        }
    }

    void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

        //Activating scarf after speaking to elder
        if (GameState > 0)
        {
            scarf.SetActive(true);
            scarfCol.enabled = true;
        }
        else {
            scarf.SetActive(false);
            scarfCol.enabled = false;
        }

        if(GameState > 1)
        {
            //Increase Severity of the Blight Shader Affecting the Level
        }

        if(GameState > 2)
        {
            elder.setDialogue(newElderDialogue);
            //Activate Interaction Script for the Tree, Change Dialogue for the Elder
            //Increase Severity of the Blight Shader Affecting the Level
        }

        if (GameState <= 2)
        {
            treeInteraction.enabled = false;
        }
        else {
            treeInteraction.enabled = true;
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = _deltaTime * 1000.0f;
        float fps = 1.0f / _deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
