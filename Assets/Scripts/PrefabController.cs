using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace dkproj
{
    [Serializable]
    public class PrefabController
    {
        [Serializable]
        public class PrefabData
        {
            public string name_;

            public GameObject prefab_;
        }

        [SerializeField]
        private PrefabData[] prefabDataList_;

        public GameObject Get(string name) {

            GameObject prefab = null;
            
            foreach (var prefabData in prefabDataList_)
            {
                if (prefabData.name_ == name)
                {
                    prefab = prefabData.prefab_;
                    break;
                }
            }

            return prefab;
        }
    }
}