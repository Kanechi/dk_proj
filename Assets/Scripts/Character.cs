using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    public enum eCharaType {
        None = -1,
        Human,
        DemiHuman,
        MagicalCreature,
        Undead,
        Deamon
    }

    public enum eCharaState {
        None = -1,
        Wait,
        Free,
        Battle,
        Mining,
        Sleep,
        Eat
    }

    /// <summary>
    /// キャラクター
    /// </summary>
    public class Character : MonoBehaviour {
        [Serializable]
        public class CharacterData {

            public string identifier_;
            
            public string name_;

            public float defalutMaxHealth_;

            public float defaultMoveSpeed_;

            public float defaultAttackSpeed_;

            // true...移動可能 
            public bool moveFlag_;

            // true...採掘可能
            public bool mineFlag_;

            // true...範囲攻撃可能
            public bool areaAttackFlag_;

            // true...食事必要
            public bool requireEatFlag_;

            // true...睡眠必要
            public bool requireSleepFlag_;

            
        }

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}