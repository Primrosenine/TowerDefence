using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public int damage = 50;
    public float speed = 40;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, target.position, Quaternion.identity);
            Destroy(effect, 1);
            Destroy(this.gameObject);
        }
    }
}
