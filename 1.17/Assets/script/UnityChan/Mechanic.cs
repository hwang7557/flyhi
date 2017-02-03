using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic : MonoBehaviour {

    public AudioClip[] m_audopClip;
    private AudioSource m_audioSource;

    enum Action
    {
        E_MOVE,
        E_ATTACK,
        E_DAMAGE,
        E_DIE,
    }
    
    public float runSpeed = 0.0f;
    public float roataionSpeed = 360.0f;

    CharacterController pcControl;
    Vector3 direction;
    Action status = Action.E_MOVE;

    Animator animator;

	// Use this for initialization
	void Start () {
        pcControl = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.F))
        {

            status = Action.E_ATTACK;

            animator.SetBool("Win", true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("Test", true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Victory"))
        {
            status = Action.E_MOVE;
            animator.SetBool("Win", false);
        }

        if (status == Action.E_ATTACK)
        {
            //if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Victory"))
            {
                status = Action.E_MOVE;
                //animator.SetBool("Win", false);
            }
        }

        if (status == Action.E_DAMAGE)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                animator.SetBool("Hit", false);
                status = Action.E_MOVE;
                animator.SetFloat("Speed", 0.0f);
                UnityChan_ItemMake.Life -= 1;

            }
        }
            if (status == Action.E_MOVE)
        {
            PlayerMove_2();
        }

        if(UnityChan_ItemMake.Life <= 0)
        {
            status = Action.E_DIE;
            animator.SetBool("Die", true);
            
            //애니메이션을 좀 더 재생하고싶다면 내가 직접 델타타임을 불러와야함..
            if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                Destroy(gameObject);
            }
        }
      //  Input_Animaiton();
    }

    private void Input_Animaiton()
    {
        if(Input.GetMouseButtonDown(0) && status != (Action.E_ATTACK))
        {
            animator.SetBool("Win", true);
            StartCoroutine("Attack_Routine");
        }
    }

    IEnumerator Attack_Routine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.0f);
            if(
                (status == (Action.E_ATTACK) && !animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Victory")) ||
                (status == (Action.E_ATTACK) && !animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Attack")
                ))
            {
                if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 1.0f)
                {
                    status = Action.E_MOVE;
                    animator.SetBool("Win", false);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            status = Action.E_DAMAGE;
            animator.SetBool("Hit", true);
        }
    }

    private void PlayerMove_2()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));
         
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward,
                direction,
                roataionSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + direction);
        }

        if(direction == new Vector3(0,0,0))
        {
            if (runSpeed > 0)
                runSpeed -= 0.1f;
            pcControl.Move(direction * runSpeed * Time.deltaTime);   
        }
        else
        {
            if(runSpeed <10)
                runSpeed += 0.1f;
            pcControl.Move(direction * runSpeed * Time.deltaTime);
        }

        if(pcControl.velocity.magnitude > 0.5f)
        {
            PlaySound(1);
        }

        animator.SetFloat("Speed", runSpeed);
    }

    void PlaySound(int i)
    {
        if (m_audioSource.isPlaying) return;
        m_audioSource.clip = m_audopClip[i];
        m_audioSource.Play();
    }
}
