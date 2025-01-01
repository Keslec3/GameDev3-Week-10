using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Managers;
using GameDevWithMarco.DesignPattern;

namespace GameDevWithMarco.spawners
{
    public class FallenObjectsSpawner : MonoBehaviour
    {
        [Header("Packages Spawn Position")]
        [SerializeField] private GameObject[] spawners;

        [Header("Package Delay Variables")]
        [SerializeField] private float initialDelay = 1.0f;
        [SerializeField] private float minDelay = 0.5f;
        [SerializeField] private float delayIncreaseRate = 0.1f;
        private float currentDelay;

        [Header("Packages Drop Chance Percentages")]
        [SerializeField] private float goodPackagePercentage;
        [SerializeField] private float badPackagePercentage;
        [SerializeField] private float lifePackagePercentage;
        [SerializeField] private float minimum_GoodPercentage;
        [SerializeField] private float maximum_BadPercentage;
        [SerializeField] private float percentageChangeRatio = 0.1f;

        void Start()
        {
            StartCoroutine(SpawningLoop()); // Begin the spawning process
        }

        public void GrowBadPercentage()
        {
            goodPackagePercentage -= percentageChangeRatio;
            badPackagePercentage += percentageChangeRatio;
            CapThePercentages(); // Ensure percentages stay within bounds
        }

        public void GrowGoodPercentage()
        {
            goodPackagePercentage += percentageChangeRatio;
            badPackagePercentage -= percentageChangeRatio;
            CapThePercentages(); // Ensure percentages stay within bounds
        }

        private void SpawnPackageAtRandomLocation(ObjectPoolingPattern.TypeOfPool poolType)
        {
            GameObject spawnedPackage = ObjectPoolingPattern.Instance.GetPoolItem(poolType); // Get object from pool
            int randomInteger = Random.Range(0, spawners.Length - 1); // Choose random spawner
            Vector2 spawnPosition = spawners[randomInteger].transform.position;
            spawnedPackage.transform.position = spawnPosition;
        }

        private IEnumerator SpawningLoop()
        {
            SpawnPackageAtRandomLocation(GetPackageBasedOnPercentage()); // Spawn a package based on chance
            yield return new WaitForSeconds(currentDelay); // Wait before spawning next
            currentDelay = Mathf.Max(currentDelay - delayIncreaseRate, minDelay); // Reduce delay but keep above min
            StartCoroutine(SpawningLoop()); // Repeat the process
        }

        private ObjectPoolingPattern.TypeOfPool GetPackageBasedOnPercentage()
        {
            float randomValue = Random.Range(0, 100.1f);

            if (randomValue <= goodPackagePercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Good;
            }
            else if (randomValue <= goodPackagePercentage + badPackagePercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Bad;
            }
            else
            {
                return ObjectPoolingPattern.TypeOfPool.Life;
            }
        }

        private void CapThePercentages()
        {
            if (goodPackagePercentage <= minimum_GoodPercentage && badPackagePercentage >= maximum_BadPercentage)
            {
                goodPackagePercentage = minimum_GoodPercentage;
                badPackagePercentage = maximum_BadPercentage;
            }
        }
    }
}
