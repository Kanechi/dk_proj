using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{






    public interface IExistCheckData {

        bool Check(string id);
    }

    /// <summary>
    /// 現在ダンジョンに配置している(部屋と罠)の ID リスト
    /// 
    /// ダンジョン全体に配置されているかどうか
    /// 階層に配置されているかどうかも必要かもしれない・・・
    /// </summary>
    [Serializable]
    public class InstalledRoomData : IExistCheckData {

        [SerializeField]
        public List<string> m_installedRoomIdList = new List<string>();

        public void Add(string room) {
            if (m_installedRoomIdList.Contains(room) == true)
                return;
            m_installedRoomIdList.Add(room);
        }

        public bool Check(string id) => m_installedRoomIdList.Contains(id);
    }

    /// <summary>
    /// このダンジョン中に覚えた部屋スキルの ID リスト
    /// </summary>
    [Serializable]
    public class LearnedRoomingSkillData : IExistCheckData {

        [SerializeField]
        public List<string> m_learnedRoomingSkillIdList = new List<string>();

        public bool Check(string id) => m_learnedRoomingSkillIdList.Contains(id);
    }

    /// <summary>
    /// このダンジョン中に覚えた召喚スキルの ID リスト
    /// </summary>
    [Serializable]
    public class LearnedSummoningSkillData : IExistCheckData {

        [SerializeField]
        public List<string> m_learnedSummoningSkillIdList = new List<string>();

        public bool Check(string id) => m_learnedSummoningSkillIdList.Contains(id);
    }

    /// <summary>
    /// このダンジョン中に覚えた罠スキルの ID リスト
    /// </summary>
    [Serializable]
    public class LearnedTrapSkillData : IExistCheckData {

        [SerializeField]
        public List<string> m_learnedTrapSkillIdList = new List<string>();

        public bool Check(string id) => m_learnedTrapSkillIdList.Contains(id);
    }

    /// <summary>
    /// ダンジョン中に作成したスペルカードの ID リスト
    /// </summary>
    [Serializable]
    public class CreatedSpellCardData : IExistCheckData {

        [SerializeField]
        public List<string> m_createdSpellCardIdList = new List<string>();

        public bool Check(string id) => m_createdSpellCardIdList.Contains(id);
    }

    /// <summary>
    /// ダンジョンデータ管理
    /// デバッグ中はデバッグダンジョンデータのデータをここに設定する
    /// リリース時は新規で生成したのち保存データが存在したら保存データを取得し設定
    /// </summary>
    public class DungeonDataManager : Singleton<DungeonDataManager> {

        // 配置済みの部屋・罠データ
        private InstalledRoomData m_installedRoomData = null;
        public InstalledRoomData InstalledRoomData { get => m_installedRoomData; set => m_installedRoomData = value; }

        // 習得済みの部屋スキルデータ
        private LearnedRoomingSkillData m_learnedRoomingSkillData = null;
        public LearnedRoomingSkillData LearnedRoomingSkillData { get => m_learnedRoomingSkillData; set => m_learnedRoomingSkillData = value; }

        // 習得済みの罠スキルデータ
        private LearnedTrapSkillData m_learnedTrapSkillData = null;
        public LearnedTrapSkillData LearnedTrapSkillData { get => m_learnedTrapSkillData; set => m_learnedTrapSkillData = value; }

        // 習得済みの召喚スキルデータ
        private LearnedSummoningSkillData m_learnedSummoningSkillData = null;
        public LearnedSummoningSkillData LearnedSummonmingSkillData { get => m_learnedSummoningSkillData; set => m_learnedSummoningSkillData = value; }

        // 作成済みのスキルカードデータ
        private CreatedSpellCardData m_createdSpellCardData = null;
        public CreatedSpellCardData CreatedSpellCardData { get => m_createdSpellCardData; set => m_createdSpellCardData = value; }
    }
}