using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rig;
    [SerializeField] FixedJoystick fixJoy;
    [SerializeField] float speed;
    [SerializeField] Animator anim;
    public bool isDead, Finished, CanMove, Fight;
    [SerializeField] GameObject Spawn, FightSpawn, YouLoosePan, YouWinPan;

    [SerializeField] GameObject AttackButton, hpBarCanvas;
    [SerializeField] Image HpBar;
    int Lifes;

    private void Start()
    {
        Lifes = 10;
        CanMove = true;
    }
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
        if (CanMove)
        {
            rig.velocity = new Vector3(fixJoy.Horizontal * speed, rig.velocity.y, fixJoy.Vertical * speed);

            if (fixJoy.Horizontal != 0 || fixJoy.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rig.velocity);
                if (!Fight)
                {
                    anim.SetBool("Run", true);
                }
                else
                    anim.SetBool("RunBattle", true);
                // JoyGoes = false;
            }
            else if (fixJoy.Horizontal == 0 && fixJoy.Vertical == 0)
            {
                if (!Fight)
                {
                    anim.SetBool("Run", false);
                }
                else
                    anim.SetBool("RunBattle", false);
                //  JoyGoes = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !Finished)
        {
            anim.SetBool("Death", true);
            isDead = true;
            Destroy(other.gameObject);
            YouLoosePan.SetActive(true);
            CanMove = false;
        }
        else if(other.tag == "Finish" && !isDead)
        {
            YouWinPan.SetActive(true);
            Finished = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Boss")
        {
            GetDamaged();
        }
    }
    public void Death()
    {
        anim.SetBool("Death", false);
        anim.SetBool("BattleStart", false);
        transform.position = Spawn.transform.position;
        YouLoosePan.SetActive(false);
        isDead = false;
        CanMove = true;
        Finished = false;
        Fight = false;

        AttackButton.SetActive(false);
        hpBarCanvas.SetActive(false);
    }

    public void GoToFight()
    {
        Fight = true;
        YouWinPan.SetActive(false);
        anim.SetBool("BattleStart", true);
        transform.position = FightSpawn.transform.position;
        AttackButton.SetActive(true);
        hpBarCanvas.SetActive(true);
        Lifes = 10;
        HpBar.fillAmount = 1f;
    }

    public void GetDamaged()
    {
        HpBar.fillAmount -= 0.1f;
        Lifes-=1;
        if(Lifes == 0)
        {
            anim.SetBool("Death", true);
            isDead = true;
            YouLoosePan.SetActive(true);
            CanMove = false;
        }
    }

    public void Attack()
    {

    }
}
