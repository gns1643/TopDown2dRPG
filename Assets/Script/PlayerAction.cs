using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    Rigidbody2D rigid;
    Animator anim;
    bool isHorizonMove;
    Vector3 dirvec;
    GameObject scanObject;
    public GameManager manager;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");


        if (hDown || vUp)
        {
            isHorizonMove = true;
        }
        else if(vDown || hUp)
        {
            isHorizonMove = false;
        }

        // 애니메이션
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        //방향 : 상
        if (vDown && v == 1)
            dirvec = Vector3.up;
        //방향 : 하
        if (vDown && v == -1)
            dirvec = Vector3.down;
        //방향 : 상
        if (hDown && h == -1)
            dirvec = Vector3.left;
        //방향 : 상
        if (hDown && h == 1)
            dirvec = Vector3.right;

        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }
    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ?
            new Vector2(h, 0):
            new Vector2(0, v);
        rigid.linearVelocity = moveVec * 5;

        //Ray
        Debug.DrawRay(rigid.position, dirvec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirvec, 0.7f, LayerMask.GetMask("Object"));
        if (rayhit.collider != null)
        {
            //raycast된 오브젝트를 변수로 저장
            scanObject = rayhit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
