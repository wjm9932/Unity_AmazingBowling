using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    public LayerMask whatIsProp;

    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();
            Prop targetProp = colliders[i].GetComponent<Prop>();

            targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            float damage = CalculateDamage(colliders[i].transform.position);
            
            targetProp.TakeDamage(damage);
        }

        explosionParticle.transform.parent = null;
        explosionParticle.Play();

        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPos)
    {
        Vector3 explosionToTarget = targetPos - transform.position;
        float distance = explosionToTarget.magnitude;
        float edgeToCentterDistance = explosionRadius - distance;
        float percentage = edgeToCentterDistance / explosionRadius;
        float damage = maxDamage * percentage;

        damage = Mathf.Max(0, damage);
        return damage;
    }
}
