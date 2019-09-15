using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickExample : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogFormat("Clicked {0}", eventData);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
