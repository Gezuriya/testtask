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
    public bool isDead, Finished, CanMove, Fight, canAttack, damaged;
    [SerializeField] GameObject Spawn, FightSpawn, YouLoosePan, YouWinPan;

    [SerializeField] GameObject AttackButton, hpBarCanvas, bossPref, chestButton;
    [SerializeField] Image HpBar;
    int Lifes;
    public int BossKilled;

    public int AttackValue;

    private void Start()
    {
        BossKilled = 0;
        Lifes = 10;
        CanMove = true;
    }
    void FixedUpdate()
    {
        if(BossKilled == 2)
        {
            AttackButton.SetActive(false);
        }
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
            }
            else if (fixJoy.Horizontal == 0 && fixJoy.Vertical == 0)
            {
                if (!Fight)
                {
                    anim.SetBool("Run", false);
                }
                else
                    anim.SetBool("RunBattle", false);
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
        else if(other.tag == "Chest")
        {
            chestButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Chest")
        {
            chestButton.SetActive(false);
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
        BossKilled = 0;
        anim.SetBool("Death", false);
        anim.SetBool("BattleStart", false);
        transform.position = Spawn.transform.position;
        YouLoosePan.SetActive(false);
        isDead = false;
        CanMove = true;
        Finished = false;
        Fight = false;
        if(GameObject.FindGameObjectWithTag("ToDestroy") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("ToDestroy"));
            if (GameObject.FindGameObjectWithTag("ToDestroy") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("ToDestroy"));
            }
        }
        AttackButton.SetActive(false);
        hpBarCanvas.SetActive(false);
    }

    public void GoToFight()
    {
        Fight = true;
        canAttack = true;
        YouWinPan.SetActive(false);
        anim.SetBool("BattleStart", true);
        transform.position = FightSpawn.transform.position;
        AttackButton.SetActive(true);
        hpBarCanvas.SetActive(true);
        Lifes = 10;
        HpBar.fillAmount = 1f;
        Instantiate(bossPref, new Vector3(123, 0.5f, -4), Quaternion.Euler(0, 180, 0));
        Instantiate(bossPref, new Vector3(117, 0.5f, -4), Quaternion.Euler(0, 180, 0));
    }

    public void GetDamaged()
    {
        if (!damaged)
        {
            damaged = true;
            StartCoroutine(CanBeDamaged());
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
    }

    public void Attack()
    {
        if (canAttack)
        {
            AttackValue++;
            canAttack = false;
            StartCoroutine(AttackCont());
            anim.SetInteger("Attack", AttackValue);
            if(AttackValue == 2)
            {
                AttackValue = 0;
            }
        }
    }

    IEnumerator CanBeDamaged()
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }
    IEnumerator AttackCont()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Attack", 0);
        canAttack = true;
    }

    public void ChestTrig()
    {

    }
}
