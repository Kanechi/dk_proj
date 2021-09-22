using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace dkproj
{
    public enum eBiome
    {
        None = 0,
        Cave = 1
    }

    public enum eMasuType
    {
        None = 0,

        Terrain,

        Relics,

        Room,

        Trap,
    }

    /// <summary>
    /// マス
    /// </summary>
    public class Masu : MonoBehaviour
    {
        [Serializable]
        public class MasuData
        {
            /// <summary>
            /// 名前
            /// </summary>
            public string name_;

            /// <summary>
            /// マスタイプ
            /// </summary>
            public eMasuType masuType_ = eMasuType.None;

            /// <summary>
            /// true...移動可能
            /// 移動は非戦闘時のみ
            /// </summary>
            public bool moveFlag_;

            /// <summary>
            /// true...破壊可能
            /// </summary>
            public bool destroyFlag_;

            /// <summary>
            /// 最大耐久度(最大ライフ)
            /// </summary>
            public int maxDurability_;

            /// <summary>
            /// true...通行可能
            /// </summary>
            public bool passable_;






            /// <summary>
            /// 何階層目か
            /// 上から昇順
            /// </summary>
            public int hierarchy_;

            /// <summary>
            /// その階層の何番目か
            /// 左から昇順
            /// </summary>
            public int index_;

            /// <summary>
            /// このマスに取り付けられれているマス
            /// </summary>
            public MasuData child_;

            /// <summary>
            /// このマスのバイオーム
            /// </summary>
            public eBiome biome_ = eBiome.None;

            /// <summary>
            /// 現在耐久度(ライフ)
            /// </summary>
            public int durability_;
        }

        [SerializeField]
        private MasuData masu_;
    }
}