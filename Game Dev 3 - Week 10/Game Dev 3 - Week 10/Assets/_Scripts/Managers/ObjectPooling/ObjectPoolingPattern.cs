using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco.DesignPattern
{
    public class ObjectPoolingPattern : Singleton<ObjectPoolingPattern>
    {
        [SerializeField] private PoolData goodPackagePool;
        [SerializeField] private PoolData badPackagePool;
        [SerializeField] private PoolData lifePackagePool;

        public List<GameObject> goodPool = new List<GameObject>();
        public List<GameObject> badPool = new List<GameObject>();
        public List<GameObject> lifePool = new List<GameObject>();

        public enum TypeOfPool
        {
            Good,
            Bad,
            Life
        }

        // Start is called before the first frame update
        private void Start()
        {
            FillThePool(goodPackagePool, goodPool);
            FillThePool(badPackagePool, badPool);
            FillThePool(lifePackagePool, lifePool);
        }

        private void FillThePool(PoolData poolData, List<GameObject> targetPool)
        {
            GameObject container = CreateAContainerForThePool(poolData);

            // Goes as many times as we want the pool amount to be
            for (int i = 0; i < poolData.poolAmount; i++)
            {
                // Instantiates one item in the pool
                GameObject thingToAddToThePool = Instantiate(poolData.poolItem, container.transform);

                // Deactivates it
                thingToAddToThePool.SetActive(false);

                // Adds it to the pool container list
                targetPool.Add(thingToAddToThePool);
            }
        }

        private GameObject CreateAContainerForThePool(PoolData poolData)
        {
            // Create a new GameObject with the name of the pool
            GameObject container = new GameObject(poolData.name + " Container");

            // Set the container as a child of this object for organization
            container.transform.parent = this.transform;

            // Return the created container
            return container;
        }

        public GameObject GetPoolItem(TypeOfPool poolToUse)
        {
            // To store the local pool
            PoolData pool = ScriptableObject.CreateInstance<PoolData>();

            switch (poolToUse)
            {
                case TypeOfPool.Good:
                    pool = goodPackagePool;
                    break;

                case TypeOfPool.Bad:
                    pool = badPackagePool;
                    break;

                case TypeOfPool.Life:
                    pool = lifePackagePool;
                    break;
            }

            // Goes through the pool
            for (int i = 0; i < pool.pooledObjectContainer.Count; i++)
            {
                // Looks for the first item that is not active
                if (!pool.pooledObjectContainer[i].activeInHierarchy)
                {
                    // Activates it
                    pool.pooledObjectContainer[i].SetActive(true);

                    // Returns it
                    return pool.pooledObjectContainer[i];
                }
            }

            // Gives us a warning that the pool might be too small
            Debug.LogWarning("No Available Items Found, Pool Too Small!");

            // If there are none, return null
            return null;
        }
    }
}
