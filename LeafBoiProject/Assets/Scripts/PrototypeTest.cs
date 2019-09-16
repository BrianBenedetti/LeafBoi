using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeTest : MonoBehaviour
{
    [SerializeField]
    protected Animator anim;

    [SerializeField]
    protected PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetInteger("Powers", 3);
            anim.SetBool("Cinematic", true);
            player.state = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetInteger("Powers", 0);
            anim.SetBool("Cinematic", false);
            player.state = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetInteger("Powers", 1);
            anim.SetBool("Cinematic", false);
            player.state = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetInteger("Powers", 2);
            anim.SetBool("Cinematic", false);
            player.state = 2;
        }
    }
}
