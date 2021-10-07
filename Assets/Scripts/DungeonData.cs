using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    /// <summary>
    /// 現在ダンジョンに配置している部屋(部屋と罠)の ID リスト
    /// </summary>
    [Serializable]
    public class InstalledRoomData {

        [SerializeField]
        public List<uint> m_installedRoomIdList = new List<uint>();
    }

    /// <summary>
    /// このダンジョン中に覚えたスキルの ID リスト
    /// </summary>
    [SerializeField]
    public class LearnedSkillData {

        [SerializeField]
        public List<uint> m_learnedSkillIdList = new List<uint>();
    }

    /// <summary>
    /// ダンジョン中に作成したスペルカードの ID リスト
    /// </summary>
    [SerializeField]
    public class CreatedSpellCardData {

        [SerializeField]
        public List<uint> m_createdSpellCardIdList = new List<uint>();
    }

    /// <summary>
    /// ダンジョンデータ管理
    /// デバッグ中はデバッグダンジョンデータのデータをここに設定する
    /// リリース時は新規で生成したのち保存データが存在したら保存データを取得し設定
    /// </summary>
    public class DungeonDataManager : Singleton<DungeonDataManager> {

        public InstalledRoomData m_installedRoomData = null;
        public LearnedSkillData m_learnedSkillData = null;
        public CreatedSpellCardData m_createdSpellCardData = null;
    }
}