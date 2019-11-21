using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float _deltaTime = 0.0f;
    public int GameState = 0;

    [SerializeField] protected Collider scarfCol;
    [SerializeField] protected GameObject scarf;
    [SerializeField] protected GameObject scarfLoose;
    [SerializeField] protected Interaction treeInteraction;
    [SerializeField] protected Collider treeCollider;
    [SerializeField] protected NPCDialogueTrigger elder;
    [SerializeField] protected Dialogue newElderDialogue;


    public Material HornBlightMaterial;
    Material HornNormalMaterial;
    MeshRenderer HornsRenderer;
    public GameObject HornsPrefab;
    public GameObject VillageBoundary;

    public Image FadeImage;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        
    }

    private void Start()
    {
        GameState = 0;
        treeInteraction.NPC = false;
        treeCollider.enabled = false;
        HornsRenderer = HornsPrefab.gameObject.GetComponent<MeshRenderer>();
        HornNormalMaterial = HornsRenderer.material;
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
            scarfLoose.SetActive(true);
            scarfCol.enabled = true;
            HornsRenderer.material = HornBlightMaterial;
            VillageBoundary.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else {
            scarf.SetActive(false);
            scarfLoose.SetActive(false);
            scarfCol.enabled = false;
            HornsRenderer.material = HornNormalMaterial;
            VillageBoundary.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        if(GameState > 1)
        {
            //Increase Severity of the Blight Shader Affecting the Level
        }

        if (GameState > 2)
        {
            elder.setDialogue(newElderDialogue);
            //Activate Interaction Script for the Tree, Change Dialogue for the Elder
            //Increase Severity of the Blight Shader Affecting the Level
            treeInteraction.NPC = true;
        }
        else {
            treeInteraction.InteractButton.SetActive(false);    
        }
    }

    public void endGame()
    {
        //PUT IN STUFF TO IMPLEMENT CREDITS AND FADE TO BLACK
        StartCoroutine("FadeOut");
        Debug.Log("Game has ended");
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

    IEnumerator FadeOut(){

        AsyncOperation sceneLoad;
        sceneLoad = SceneManager.LoadSceneAsync(2,LoadSceneMode.Single);
        sceneLoad.allowSceneActivation = false;
        for (int i = 0; i < 200; i++)
        {
            Color FullColor = FadeImage.color;
            //fades to black in approx. 3 seconds
            FullColor.a += 0.3f * Time.fixedDeltaTime;
            FadeImage.color = FullColor;
            yield return new WaitForFixedUpdate();
        }
        sceneLoad.allowSceneActivation = true;
    }
}
