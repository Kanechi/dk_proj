using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    /// <summary>
    /// 遺物のタイプ
    /// </summary>
    public enum eRelicsType { 
    
        None = -1,
        
        /// <summary>
        /// ダンジョンコア
        /// </summary>
        DungeonCore,

        /// <summary>
        /// ダンジョンの入り口
        /// </summary>
        DungeonEntrance,

        /// <summary>
        /// マナ鉱脈
        /// </summary>
        ManaVein,

        /// <summary>
        /// 金鉱山
        /// </summary>
        GoldMine,

        /// <summary>
        /// ポータル
        /// </summary>
        Portal
    }

    /// <summary>
    /// 遺物データ
    /// </summary>
    public class RelicsData : MasuData
    {
        [SerializeField]
        private eRelicsType relicsType_;



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