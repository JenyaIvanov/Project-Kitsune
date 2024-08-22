using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject FloatingText;

    [SerializeField] private float enemyHealth = 100;

    public Animator enemyAnimator;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead)
            return;

        enemyHealth -= damageAmount;

        if (FloatingText)
        {
            ShowFloatingText(damageAmount);
        }

        if(enemyHealth <= 0)
        {
            Death();
        } else
        {
            enemyAnimator.SetTrigger("TakeDamage");
        }
            

    }

    void ShowFloatingText(float damageText)
    {
        var floatingText = Instantiate(FloatingText, gameObject.transform.position, Quaternion.identity, transform);
        floatingText.GetComponentInChildren<TextMeshPro>().text = damageText.ToString();
    }

    void Death()
    {
        isDead = true;
        Destroy(gameObject, 3f);
        enemyAnimator.SetTrigger("Death");
    }
}
