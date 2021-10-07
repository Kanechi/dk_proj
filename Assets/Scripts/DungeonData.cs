using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{


#if false

    /// <summary>
    /// ダンジョンのデータ
    /// クリアもしくはゲームオーバーしたらリセットされるデータ
    /// 保存
    /// </summary>
    public class DungeonData : MonoBehaviour
    {
        /// <summary>
        /// このダンジョン中に覚えたスキル
        /// </summary>
        [Serializable]
        public class SkillData {

            // 部屋
            [Serializable]
            public class RoomSkillData {
                // ねぐら
                [SerializeField]
                public bool m_roost = false;
            }

            // 召喚
            [Serializable]
            public class SummonSkillData {
                // ゴブリン
                [SerializeField]
                public bool m_gobrin = false;
            }

            // 罠
            [Serializable]
            public class TrapSkillData {
                // 回転のこぎり
                [SerializeField]
                public bool m_kaitenNokogiri = false;
            }

            [SerializeField]
            public RoomSkillData m_roomSkillData = new RoomSkillData();

            [SerializeField]
            public SummonSkillData m_summonSkillData = new SummonSkillData();

            [SerializeField]
            public TrapSkillData m_trapSkillData = new TrapSkillData();
        }

        /// <summary>
        /// このダンジョン中に配置している部屋と罠
        /// </summary>
        [Serializable]
        public class RoomData {

            [SerializeField]
            public SkillData.RoomSkillData m_room = new SkillData.RoomSkillData();

            [SerializeField]
            public SkillData.TrapSkillData m_trap = new SkillData.TrapSkillData();
        }

        [SerializeField]
        private SkillData m_skillData = new SkillData();

        [SerializeField]
        private RoomData m_roomData = new RoomData();

        /// <summary>
        /// このダンジョン中で作成したスペルカード
        /// </summary>
        private List<uint> m_spellCardId = new List<uint>();
        public List<uint> SpellCardID { get => m_spellCardId; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
#endif
}