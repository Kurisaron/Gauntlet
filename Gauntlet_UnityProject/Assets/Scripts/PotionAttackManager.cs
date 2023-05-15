using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAttackManager : Singleton<PotionAttackManager>
{
    
    public override void Awake()
    {
        base.Awake();

    }

    public void UsePotion(Player player)
    {
        Color classColor = player.characterClass._class switch
        {
            ClassEnum.Warrior => Color.red,
            ClassEnum.Wizard => Color.blue,
            ClassEnum.Valkyrie => Color.cyan,
            ClassEnum.Elf => Color.green,
            _ => Color.black
        };
        
        StartCoroutine(PotionAttackRoutine(player.gameObject.transform.position, player.Magic, classColor));
    }

    public void PotionHit(Vector3 position)
    {
        StartCoroutine(PotionAttackRoutine(position, 2.0f, Color.white));
    }

    private IEnumerator PotionAttackRoutine(Vector3 position, float magicPower, Color aoeColor)
    {
        float attackRadius = Mathf.Lerp(1.0f, 5.0f, Mathf.InverseLerp(0.0f, 4.0f, magicPower));

        Collider[] collidersInArea = Physics.OverlapSphere(position, attackRadius);
        if (collidersInArea != null)
        {
            foreach(Collider collider in collidersInArea)
            {
                if (collider.gameObject.GetComponentInParent<Foe>() != null) Potion_AttackFoe(collider.gameObject.GetComponentInParent<Foe>(), magicPower);
            }
        }

        GameObject aoe = ObjectPooler.Instance.GetPooledObject("AOE");
        aoe.SetActive(true);
        aoe.transform.position = position;
        aoe.transform.localScale = new Vector3(attackRadius, aoe.transform.localScale.y, attackRadius);
        aoe.GetComponent<Renderer>().material.color = aoeColor;

        float time = 0.0f;
        while (time < 2.0f)
        {
            yield return null;
            time += Time.deltaTime;
        }

        aoe.SetActive(false);
    }

    private void Potion_AttackFoe(Foe foe, float magicPower)
    {
        if (foe.gameObject.GetComponent<Generator>() != null) foe.gameObject.GetComponent<Generator>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Ghost>() != null) foe.gameObject.GetComponent<Ghost>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Grunt>() != null) foe.gameObject.GetComponent<Grunt>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Demon>() != null) foe.gameObject.GetComponent<Demon>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Lobber>() != null) foe.gameObject.GetComponent<Lobber>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Sorcerer>() != null) foe.gameObject.GetComponent<Sorcerer>().PotionAttack(magicPower);
        // ADD ONCE THIEF HAD BEEN ADDED: if (foe.gameObject.GetComponent<Thief>() != null) foe.gameObject.GetComponent<Thief>().PotionAttack(magicPower);
        if (foe.gameObject.GetComponent<Death>() != null) foe.gameObject.GetComponent<Death>().PotionAttack(magicPower);
    }
}
