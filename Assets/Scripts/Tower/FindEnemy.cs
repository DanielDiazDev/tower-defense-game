using System;
using UnityEngine;

namespace Tower
{
    public class FindEnemy : MonoBehaviour
    {
        public float rangeDistance;
        public float fireRate;
        private Transform _target;
        private Vector3 _closestEnemyRef;
        private Vector2 _direction;
        private bool _detected = false;
        private float _nextTimeToFire = 0;
        private void Start()
        {
            InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        }

        private void Update()
        {
            
            if (_target != null)
            {

                Vector2 targetPos = _target.position;
                _direction = targetPos - (Vector2)transform.position;
                RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _direction, rangeDistance);
                if (rayInfo)
                {
                    if (rayInfo.collider.gameObject.tag == "Enemy")
                    {
                        if (_detected == false)
                        {
                            _detected = true;
                        }
                    }
                    else
                    {
                        if (_detected == true)
                        {
                            _detected = false;
                        }
                    }
                }
                if (_detected)
                {
                    if (Time.time > _nextTimeToFire)
                    {
                        _nextTimeToFire = Time.time + 1 / fireRate;
                        Debug.Log("Disparo");
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw View Distance
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangeDistance);
            // Draw View Draw collision Area
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, .25f);
            // Draw View Draw Enemy distance from player
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, _closestEnemyRef);
        }
        private void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float shortestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject currentEnemy in enemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = currentEnemy;
                    _closestEnemyRef = currentEnemy.transform.position;
                }
            }
            // Check that the enemy that is closer to the turret became the main target
            if (closestEnemy != null && shortestDistance <= rangeDistance)
            {
                _target = closestEnemy.transform;
            }
            else
            {
                _target = null;
            }
        }
        
    }
}