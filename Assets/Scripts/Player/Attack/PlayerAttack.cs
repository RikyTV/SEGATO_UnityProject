using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public float attackCooldown;
    public Transform bulletPoint;
    public Transform fireballHolder;

    private List<GameObject> fireballs = new List<GameObject>();
    private Animator anim;
    private PlayerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake() {
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
        SetFireballs();
    }

    private void Update() {
        if ((Input.GetMouseButtonDown(0)) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack() {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = bulletPoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(bulletPoint.right);
    }

    private void SetFireballs() {
        for (int i = 0; i<fireballHolder.childCount; i++)
            fireballs.Add(fireballHolder.GetChild(i).gameObject);
    }

    private int FindFireball() {
        for (int i = 0; i < fireballs.Count; i++) {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}