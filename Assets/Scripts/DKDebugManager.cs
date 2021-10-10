using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace dkproj {

    /// <summary>
    /// デバッグ用ダンジョンデータ
    /// デバッグ中はこのデータをシーンに取り付け扱う
    /// リリース時は DungeonData を管理クラスで生成して Singleton で扱いゲーム終了時に保存
    /// および任意のタイミングで保存
    /// </summary>
    [Serializable]
    public class DebugDungeonData {

        [SerializeField]
        private InstalledRoomData m_installedRoomData = new InstalledRoomData();
        public InstalledRoomData InstalledRoomData { get => m_installedRoomData; }

        [SerializeField]
        private LearnedRoomingSkillData m_learnedRoomingSkillData = new LearnedRoomingSkillData();
        public LearnedRoomingSkillData LearnedRoomingSkillData { get => m_learnedRoomingSkillData; }

        [SerializeField]
        private LearnedTrapSkillData m_learnedTrapSkillData = new LearnedTrapSkillData();
        public LearnedTrapSkillData LearnedTrapSkillData { get => m_learnedTrapSkillData; }

        [SerializeField]
        private LearnedSummoningSkillData m_learnedSummoningSkillData = new LearnedSummoningSkillData();
        public LearnedSummoningSkillData LearnedSummoningSkillData { get => m_learnedSummoningSkillData; }

        [SerializeField]
        private CreatedSpellCardData m_createdSpellCardData = new CreatedSpellCardData();
        public CreatedSpellCardData CreatedSpellCardData { get => m_createdSpellCardData; }
    }

    /// <summary>
    /// デバッグ用
    /// リリース時はアクティブを切っておく
    /// </summary>
    public class DKDebugManager : SingletonMonoBehaviour<DKDebugManager> {

        [SerializeField]
        private DebugDungeonData m_debugDungeonData = new DebugDungeonData();

        /// <summary>
        /// デバッグ用データの設定
        /// </summary>
        void Start() {
            DungeonDataManager.Instance.InstalledRoomData = m_debugDungeonData.InstalledRoomData;

            DungeonDataManager.Instance.LearnedRoomingSkillData = m_debugDungeonData.LearnedRoomingSkillData;

            DungeonDataManager.Instance.LearnedTrapSkillData = m_debugDungeonData.LearnedTrapSkillData;

            DungeonDataManager.Instance.LearnedSummonmingSkillData = m_debugDungeonData.LearnedSummoningSkillData;

            DungeonDataManager.Instance.CreatedSpellCardData = m_debugDungeonData.CreatedSpellCardData;
        }

        // Update is called once per frame
        void Update() {

        }
    }
}