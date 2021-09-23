using UnityEngine;
using System.Collections.Generic;
using System;

namespace ManagerPooling
{
    public class PoolManager
    {
        public static Dictionary<Type, Pool<GameObject>> AllPools = new Dictionary<Type, Pool<GameObject>>();
        public static int defaultSize = 3;
        private static GameObject rootTransform;
        private static void PoolInstaller<T>(T prefab, Type idGroup) where T : MonoBehaviour
        {
            rootTransform = new GameObject();
            rootTransform.name = prefab.name + " Pool";
            UnityEngine.GameObject.DontDestroyOnLoad(rootTransform);
            Pool<GameObject> pool = new Pool<GameObject>(() => 
            {
                T v = InstantiateObject<T>(prefab, rootTransform);
                return v.gameObject; 
            }, defaultSize);


            AllPools.Add(idGroup, pool);
        }

        public static void ReturnToRoot()
        {
            foreach (KeyValuePair<Type, Pool<GameObject>> item in AllPools)
            {
                foreach (KeyValuePair<GameObject, ItemPool<GameObject>> objects in item.Value.ObjectsInPool)
                {
                    BackToPool(objects.Value.ItemObject.gameObject, item.Key);
                    objects.Value.ItemObject.transform.SetParent(rootTransform.transform);
                }
            }
        }

        public static T GetPooledObject<T>(T prefab, Type idGroup) where T : MonoBehaviour
        {
            if (!AllPools.ContainsKey(idGroup))
            {
                PoolInstaller(prefab, idGroup);
            }

            T objectFromPool = AllPools[idGroup].GetFromPool().GetComponent<T>();
            objectFromPool.gameObject.SetActive(true);
            return objectFromPool;
        }

        public static void BackToPool(GameObject item, Type idGroup) 
        {
            if (AllPools.ContainsKey(idGroup))
            {
                item.gameObject.SetActive(false);
                AllPools[idGroup].BackToPool(item);
            }
        }

        private static T InstantiateObject<T>(T prefab, GameObject rootTransform) where T : MonoBehaviour
        {
            T newObject = UnityEngine.GameObject.Instantiate(prefab);
            newObject.transform.SetParent(rootTransform.transform);
            newObject.gameObject.SetActive(false);
            return newObject as T;
        }
    }
}
