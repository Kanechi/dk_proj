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

            // 識別名
            public string m_identifier;

            // 種族名
            public string m_typeName;
            
            // ユニーク名
            public string m_uniqueName;

            // 最大ヘルス
            public float m_defalutMaxHealth;

            // 移動速度
            public float m_defaultMoveSpeed;

            // 攻撃速度
            public float m_defaultAttackSpeed;

            // true...移動可能 
            public bool m_moveFlag;

            // true...採掘可能
            public bool m_mineFlag;

            // true...範囲攻撃可能
            public bool m_areaAttackFlag;

            // true...食事必要
            public bool m_requireEatFlag;

            // true...睡眠必要
            public bool m_requireSleepFlag;

            // true...死亡
            public bool m_dead = false;
        }

        protected SpriteRenderer m_spriteRenderer;
        protected Collider2D m_collider;
        protected Animator m_animator;

        protected Transform m_target;

        protected readonly int m_hashSpotted = Animator.StringToHash("Spotted");
        protected readonly int m_hashMeleeAttack = Animator.StringToHash("MeleeAttack");
        protected readonly int m_hashShooting = Animator.StringToHash("Shooting");
        protected readonly int m_hashTargetLost = Animator.StringToHash("TargetLost");
        protected readonly int m_hashHit = Animator.StringToHash("Hit");
        protected readonly int m_hashDeath = Animator.StringToHash("Death");
        protected readonly int m_hashGounded = Animator.StringToHash("Grounded");

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}