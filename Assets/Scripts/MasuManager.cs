using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    public class MasuManager : SingletonMonoBehaviour<MasuManager>
    {
        [SerializeField]
        private PrefabController prefabCtrl_ = new PrefabController();

        public PrefabController PrefabCtrl => prefabCtrl_;
    }
}