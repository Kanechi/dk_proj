using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    public abstract class MasuFactory
    {
        public MasuData Create(Transform parent)
        {
            MasuData masuData = CreateMasuData();

            return masuData;
        }

        protected abstract MasuData CreateMasuData();
    }


}