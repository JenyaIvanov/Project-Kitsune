using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{

    public GameObject hitBox;
    private PlayerStats playerStats;


    // Combo System
    public GameObject comboText_count;
    public GameObject comboText;
    private TextMeshProUGUI comboText_TMP;

    private float comboCoolDown = 350;
    private bool inCombo = false;
    public int comboThreshold = 5;
    private float comboTimer = 0;
    private float comboCount = 0;

    void Start()
    {

        // Get Player Stats
        playerStats = GetComponentInParent<PlayerStats>();

        // Combo System
        comboCount = 0;
        comboTimer = 0;
        inCombo = false;
        comboText_TMP = comboText_count.GetComponent<TextMeshProUGUI>();
        if(comboText_TMP != null)
        {
            comboText_TMP.text = "0";
        }
    }

    public void PlayerAttack()
    {
        // Damage Calculation Formula
        float damage = playerStats.playerDamage;


        Collider enemy = hitBox.GetComponent<PlayerHitBox>().GetClosestCollider(gameObject.transform);
        if (enemy == null) return;

        // Combo System
        ComboSystem();

        // Damage Enemies + Floating Text
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        print("Player Has Attacked " + enemy.name + " for " + damage + " damage.");

    }

    void ComboSystem()
    {
        comboTimer = comboCoolDown;
        comboCount++;

        if(comboCount >= comboThreshold)
        {
            if (!inCombo)
            {
                comboText.SetActive(true);
                comboText_count.SetActive(true);
                inCombo = true;
            }
            comboText_TMP.text = comboCount.ToString();
            comboText_count.GetComponent<Animator>().SetTrigger("Hit");
        }
    }

    private void FixedUpdate()
    {
        //print("CT: " + comboTimer + ". CC: " + comboCount);
        if(comboTimer > 0)
        {
            comboTimer--;
        }

        if(comboTimer <= 0)
        {
            inCombo = false;
            comboTimer = 0;
            comboCount = 0;
            comboText.SetActive(false);
            comboText_count.SetActive(false);
        }
    }

}
