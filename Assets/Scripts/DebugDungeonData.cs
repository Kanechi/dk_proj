using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    /// <summary>
    /// デバッグ用ダンジョンデータ
    /// デバッグ中はこのデータをシーンに取り付け扱う
    /// リリース時は DungeonData を管理クラスで生成して Singleton で扱いゲーム終了時に保存
    /// および任意のタイミングで保存
    /// </summary>
    public class DebugDungeonData : MonoBehaviour {

        [SerializeField]
        private InstalledRoomData m_installedRoomData = new InstalledRoomData();

        [SerializeField]
        private LearnedSkillData m_learnedSkillData = new LearnedSkillData();

        [SerializeField]
        private CreatedSpellCardData m_createdSpellCardData = new CreatedSpellCardData();

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}