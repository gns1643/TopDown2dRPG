using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    Rigidbody2D rigid;
    Animator anim;
    bool isHorizonMove;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");


        if (hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if(vDown || hUp)
        {
            isHorizonMove = false;
        }

        // 애니메이션
        if(anim.GetInteger("hAxisRaw") != (int)h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
    }
    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ?
            new Vector2(h, 0):
            new Vector2(0, v);
        rigid.linearVelocity = moveVec * 5;
    }
}
