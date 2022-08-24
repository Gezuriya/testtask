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
    private void Start()
    {
        Lifes = 5;
        HpBar.fillAmount = 1;
        StartCoroutine(BossAttack());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Rist" && FindObjectOfType<PlayerController>().canAttack == false)
        {
            Lifes--;
            HpBar.fillAmount -= 0.2f;
            if(Lifes == 0)
            {
                anim.SetBool("Death", true);
                Invoke("BossDeath", 2);
            }
        }
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
    void BossDeath()
    {
        FindObjectOfType<PlayerController>().BossKilled++;
        if(FindObjectOfType<PlayerController>().BossKilled == 2)
        {
            Instantiate(chest, transform.position, Quaternion.Euler(0,180,0));
        }
        Destroy(gameObject);
    }
}
