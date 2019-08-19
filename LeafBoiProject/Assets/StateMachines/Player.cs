using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    public bool canMove;
    [SerializeField] protected Animator animator;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    public void Jump () {
        animator.SetBool("isGrounded", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            print("Can Move.");

            float x = Input.GetAxis("Horizontal");

            animator.SetBool("isWalking",x != 0);
            transform.Translate(Vector3.right * x * 10 * Time.deltaTime);
        }
    }
}
