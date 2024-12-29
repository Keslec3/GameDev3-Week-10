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

        protected override void Awake()
        {
            base.Awake();
            FillThePool(goodPackagePool, goodPool);
            FillThePool(badPackagePool, badPool);
            FillThePool(lifePackagePool, lifePool);
        }

        private void FillThePool(PoolData poolData, List<GameObject> targetPool)
        {
            if (poolData == null)
            {
                Debug.LogError("PoolData is null. Please ensure it is assigned in the Inspector.");
                return;
            }

            GameObject container = CreateAContainerForThePool(poolData);

            for (int i = 0; i < poolData.poolAmount; i++)
            {
                GameObject thingToAddToThePool = Instantiate(poolData.poolItem, container.transform);
                thingToAddToThePool.SetActive(false);
                targetPool.Add(thingToAddToThePool);
            }
        }

        private GameObject CreateAContainerForThePool(PoolData poolData)
        {
            GameObject container = new GameObject(poolData.name + " Container");
            container.transform.parent = this.transform;
            return container;
        }

        public GameObject GetPoolItem(TypeOfPool typeOfPoolToUse)
        {
            List<GameObject> poolToUse = new();

            switch (typeOfPoolToUse)
            {
                case TypeOfPool.Good:
                    poolToUse = goodPool;
                    break;
                case TypeOfPool.Bad:
                    poolToUse = badPool;
                    break;
                case TypeOfPool.Life:
                    poolToUse = lifePool;
                    break;
            }

            int itemPoolCount = poolToUse.Count;

            for (int i = 0; i < itemPoolCount; i++)
            {
                if (!poolToUse[i].activeInHierarchy)
                {
                    poolToUse[i].SetActive(true);
                    return poolToUse[i];
                }
            }

            Debug.LogWarning("No Available Items Found, Pool Too Small!");
            return null;
        }
    }
}
