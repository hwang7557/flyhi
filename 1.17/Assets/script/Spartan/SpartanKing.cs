using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanKing : MonoBehaviour {

    enum Action
    {
        S_ATTACK,
        S_MOVE,
    }

    Action status;
    AttackingCamera GoCamera;
    bool GoCameraBool = true;

    // >> : 1
    Animation spartanKing;

    public AnimationClip IDLE;
    public AnimationClip RUN;
    public AnimationClip ATTACK;
    public AnimationClip CHARGE;
    public AnimationClip DIE;
    public AnimationClip DIEHARD;
    public AnimationClip IDLEBATTLE;
    public AnimationClip RESIST;
    public AnimationClip SALUTE;
    public AnimationClip VICTORY;
    public AnimationClip WALK;


    public bool typeSelect = true;

    CharacterController pcControl;
    public float runSpeed = 6.0f;
    Vector3 veloctiy;

    BoxCollider sword;

    // Use this for initialization
    void Start()
    {
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
        status = Action.S_MOVE;
        sword = gameObject.GetComponent<BoxCollider>();
        sword.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.R))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade(RUN.name, 0.3f);
        }

        if(typeSelect && Input.GetKeyDown(KeyCode.Z))
        {
            typeSelect = false;
        }
        else if (!typeSelect && Input.GetKeyDown(KeyCode.Z))
        {
            typeSelect = true;
        }

        if(status == Action.S_MOVE)
        {
            PlayerMove_2();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            status = Action.S_ATTACK;
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade(ATTACK.name, 0.3f);
            sword.enabled = true;
        }

        if (!spartanKing.IsPlaying(ATTACK.name))
        {
            status = Action.S_MOVE;
            sword.enabled = false;
        }



    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            //Animation d = (Animation)GetComponent<hit.gameObject.animation>();
        }
        
    }


    private void PlayerMove_1()
    {
        veloctiy = new Vector3(-Input.GetAxis("Horizontal"),
            0,
            -Input.GetAxis("Vertical"));
        veloctiy *= runSpeed;
        if (veloctiy.magnitude > 0.5f)
        {
            spartanKing.CrossFade(RUN.name, 0.3f);
            transform.LookAt(transform.position + veloctiy);
        }
        else
        {
            spartanKing.CrossFade(IDLE.name, 0.3f);
        }

        pcControl.Move(veloctiy * Time.deltaTime);
    }

    static float rotation = 360.0f;

    private void PlayerMove_2()
    {
        Vector3 direction = new Vector3(-Input.GetAxis("Horizontal"),
            0,
            -Input.GetAxis("Vertical"));


        float d = direction.sqrMagnitude;

        if (direction.sqrMagnitude > 0.01f)
        {
            spartanKing.CrossFade(RUN.name, 0.3f);
            Vector3 forward = Vector3.Slerp(transform.forward,
                direction,
                rotation * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + direction);
        }
        else
        {
            spartanKing.CrossFade(IDLE.name, 0.3f);
        }

        pcControl.Move(direction * runSpeed * Time.deltaTime);
    }
}
