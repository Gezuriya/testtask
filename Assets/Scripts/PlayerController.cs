using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] FixedJoystick fixJoy;
    [SerializeField] float speed;
    [SerializeField] Animator anim;
    public bool isDead;
    [SerializeField] GameObject Spawn, FightSpawn, YouLoosePan, YouWinPan;

    void FixedUpdate()
    {
      /*  if (PlayerHealth == 0)
        {
            Death();
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;*/

        rig.velocity = new Vector3(fixJoy.Horizontal * speed, rig.velocity.y, fixJoy.Vertical * speed);

        if (fixJoy.Horizontal != 0 || fixJoy.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rig.velocity);
            anim.SetBool("Run", true);
           // JoyGoes = false;
        }
        else if (fixJoy.Horizontal == 0 && fixJoy.Vertical == 0)
        {
            anim.SetBool("Run", false);
            //  JoyGoes = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            anim.SetBool("Death", true);
            isDead = true;
            Destroy(other.gameObject);
            YouLoosePan.SetActive(true);
        }
        else if(other.tag == "Finish" && !isDead)
        {
            YouWinPan.SetActive(true);
        }
    }

    public void Death()
    {
        anim.SetBool("Death", false);
        transform.position = Spawn.transform.position;
        YouLoosePan.SetActive(false);
        isDead = false;
    }

    public void GoToFight()
    {
        YouWinPan.SetActive(false);
        transform.position = FightSpawn.transform.position;
    }
}
