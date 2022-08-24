using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Image HpBar;
    int Lifes;
    [SerializeField] GameObject chest;
    GameObject Player;
    bool canGet;
    private void Start()
    {
        canGet = true;
        Player = GameObject.FindGameObjectWithTag("Player");
        Lifes = 5;
        HpBar.fillAmount = 1;
        StartCoroutine(BossAttack());
    }
    private void FixedUpdate()
    {
        transform.LookAt(Player.transform);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Rist" && Player.GetComponent<PlayerController>().canAttack == false && canGet)
        {
            Lifes--;
            HpBar.fillAmount -= 0.2f;
            if(Lifes <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(BossDeath());
            }
            canGet = false;
            StartCoroutine(GetDamage());
        }
    }
    IEnumerator GetDamage()
    {
        yield return new WaitForSeconds(0.35f);
        canGet = true;
    }
    IEnumerator BossAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            anim.SetBool("Attack", true);
            yield return new WaitForSeconds(0.8f);
            anim.SetBool("Attack", false);
        }
    }
    IEnumerator BossDeath()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(2);
        Player.GetComponent<PlayerController>().BossKilled++;
        if(Player.GetComponent<PlayerController>().BossKilled == 2)
        {
            Instantiate(chest, transform.position, Quaternion.Euler(0,180,0));
        }
        Destroy(gameObject);
    }
}
