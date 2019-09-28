using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _position;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerController.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
    }
}
