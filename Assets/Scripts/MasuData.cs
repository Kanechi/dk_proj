using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    public enum eBiome
    {
        None = 1,
        Cave = 0
    }

    /// <summary>
    /// マスのデータ
    /// 何もない空間
    /// </summary>
    public class MasuData : MonoBehaviour
    {
        /// <summary>
        /// 何階層目か
        /// 上から昇順
        /// </summary>
        private int hierarchy_;

        /// <summary>
        /// その階層の何番目か
        /// 左から昇順
        /// </summary>
        private int index_;

        /// <summary>
        /// このマスに取り付けられれているマス
        /// </summary>
        private MasuData child_;

        /// <summary>
        /// このマスのバイオーム
        /// </summary>
        private eBiome biome_ = eBiome.Cave;

        /// <summary>
        /// true...移動可能
        /// </summary>
        protected bool moveFlag_;

        /// <summary>
        /// true...破壊可能
        /// </summary>
        protected bool destroyFlag_;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}