using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDamage;

    private void OnCollisionEnter(Collision objectWehit)
    {
        if(objectWehit.gameObject.CompareTag("Target"))
        {
            print("hit" + objectWehit.gameObject.name + " !");

            CreateBulletImpactEffect(objectWehit);

            Destroy(gameObject);
        }

        if (objectWehit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall" );

            CreateBulletImpactEffect(objectWehit);

            Destroy(gameObject);
        }

        if (objectWehit.gameObject.CompareTag("Beer"))
        {
            print("hit a beer bottle");

            objectWehit.gameObject.GetComponent<BearBottle>().Shatter();

            // We will not destroy the bullet on impact, it will get destroyed according to its life
        }

        if (objectWehit.gameObject.CompareTag("Enemy"))
        {

            print("hit zombie");

            if (objectWehit.gameObject.GetComponent<Enemy>().isDead == false)
            {
                objectWehit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }


            objectWehit.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);

            CreateBloodSprayEffect(objectWehit);

            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectWehit)
    {
        ContactPoint contact = objectWehit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(
            GlobalReferences.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        bloodSprayPrefab.transform.SetParent(objectWehit.gameObject.transform);
    }

    void CreateBulletImpactEffect( Collision objectWehit)
    {
        ContactPoint contact = objectWehit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefap,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWehit.gameObject.transform);


    }
}
