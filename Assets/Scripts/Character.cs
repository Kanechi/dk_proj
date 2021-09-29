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
        private CharacterData m_characterData = new CharacterData();

        protected SpriteRenderer m_spriteRenderer;

        protected Collider2D m_collider;

        protected Animator m_animator;

        // 移動ベクトル
        protected Vector3 m_moveVector;

        // true...スプライト上の画像の顔が左を向いている
        public bool m_spriteFaceLeft = false;
        // スプライト上の前のベクトル
        protected Vector2 m_spriteForward;

        // ロックオンターゲット
        protected Transform m_target;

        // ターゲットを発見してからターゲットを見失うまでのインターバル
        protected float m_timeSinceLastTargetView;

        // ショットインターバル
        protected float m_fireTimer = 0.0f;

        protected readonly int m_hashSpotted = Animator.StringToHash("Spotted");
        protected readonly int m_hashMeleeAttack = Animator.StringToHash("MeleeAttack");
        protected readonly int m_hashShooting = Animator.StringToHash("Shooting");
        protected readonly int m_hashTargetLost = Animator.StringToHash("TargetLost");
        protected readonly int m_hashHit = Animator.StringToHash("Hit");
        protected readonly int m_hashDeath = Animator.StringToHash("Death");
        protected readonly int m_hashGounded = Animator.StringToHash("Grounded");

        private void Awake() {

            m_collider = GetComponent<Collider2D>();
            m_animator = GetComponent<Animator>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            m_spriteForward = m_spriteFaceLeft ? Vector2.left : Vector2.right;
            if (m_spriteRenderer.flipX)
                m_spriteForward = -m_spriteForward;
        }

        private void OnEnable() {
            m_characterData.m_dead = false;
            m_collider.enabled = true;
        }

        // Start is called before the first frame update
        void Start() {

        }

        private void FixedUpdate() {
            if (m_characterData.m_dead)
                return;

            // 移動処理

            // コリジョンチェック

            UpdateTimers();

            // アニメーターへの地面フラグチェック
        }

        /// <summary>
        /// タイマーの更新
        /// </summary>
        void UpdateTimers() {
            if (m_timeSinceLastTargetView > 0.0f)
                m_timeSinceLastTargetView -= Time.deltaTime;

            if (m_fireTimer > 0.0f)
                m_fireTimer -= Time.deltaTime;
        }

        /// <summary>
        /// 移動速度の計算
        /// </summary>
        /// <param name="horizontalSpeed"></param>
        public void SetHorizontalSpeed(float horizontalSpeed) {
            m_moveVector.x = horizontalSpeed * m_spriteForward.x;
        }

        /// <summary>
        /// ターゲットの方を向く
        /// </summary>
        public void OrientToTarget() {
            if (m_target == null)
                return;

            Vector3 toTarget = m_target.position - transform.position;

            if (Vector2.Dot(toTarget, m_spriteForward) < 0) {
                SetFacingData(Mathf.RoundToInt(-m_spriteForward.x));
            }
        }

        /// <summary>
        /// スプライトの顔の向きを設定
        /// </summary>
        /// <param name="facing"></param>
        public void SetFacingData(int facing) {
            if (facing == -1) {
                m_spriteRenderer.flipX = !m_spriteFaceLeft;
                m_spriteForward = m_spriteFaceLeft ? Vector2.right : Vector2.left;
            }
            else if (facing == 1) {
                m_spriteRenderer.flipX = m_spriteFaceLeft;
                m_spriteForward = m_spriteFaceLeft ? Vector2.left : Vector2.right;
            }
        }

        /// <summary>
        /// 移動ベクトルの設定
        /// </summary>
        /// <param name="newMoveVector"></param>
        public void SetMoveVector(Vector2 newMoveVector) {
            m_moveVector = newMoveVector;
        }

        /// <summary>
        /// 顔の向きの更新
        /// </summary>
        public void UpdateFacing() {
            bool faceLeft = m_moveVector.x < 0f;
            bool faceRight = m_moveVector.x > 0f;

            if (faceLeft) {
                SetFacingData(-1);
            }
            else if (faceRight) {
                SetFacingData(1);
            }
        }
    }
}