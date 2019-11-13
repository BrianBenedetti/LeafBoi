using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderTreeManager : MonoBehaviour
{
    [SerializeField] protected GameManager _gm;
    [SerializeField] protected Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gm.GameState > 2)
        {
            _anim.enabled = true;
        }
    }
}
