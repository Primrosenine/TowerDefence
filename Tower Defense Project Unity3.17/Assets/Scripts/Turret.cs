using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject missile;
    public float attackRateTime = 1f;

    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;

    public bool useLaser;
    public LineRenderer lineRenderer;
    public float laserDamage = 50;

    private List<GameObject> enemys = new List<GameObject>();
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    private void Start()
    {
        timer = attackRateTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (enemys.Count > 0)
        {
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
        }
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPos = enemys[0].transform.position;
            targetPos.y = head.position.y;
            head.LookAt(targetPos);

            if (!useLaser)
            {
                if (timer >= attackRateTime)
                {
                    timer = 0;
                    Attack();
                }
            }
            else
            {
                if (lineRenderer.enabled == false)
                {
                    lineRenderer.enabled = true;
                }
                lineRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(laserDamage * Time.deltaTime);
            }
        }
        else
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
            }
        }
    }

    private void Attack()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
    }

    private void UpdateEnemys()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                enemys.RemoveAt(i);
            }
        }
    }
}
