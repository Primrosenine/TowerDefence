using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject explosionEffect;
    public Slider hpSlider;
    public float speed = 20;
    public float totalHp = 150f;

    private Transform[] transforms;
    private float curHp;
    private int index;

    private void Start()
    {
        transforms = WayPoints.transforms;
        index = transforms.Length - 1;
        curHp = totalHp;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (index < 0)
        {
            ReachDestination();
            return;
        }
        transform.Translate((transforms[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(transforms[index].position, transform.position) < 0.2f)
        {
            index--;
        }
    }

    private void ReachDestination()
    {
        GameManager.instance.Fail();
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        hpSlider.value = curHp / totalHp;
        if (curHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
