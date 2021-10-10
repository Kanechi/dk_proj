using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    /// <summary>
    /// マス生成基底
    /// </summary>
    public abstract class MasuFactory {

        public string Identifier { get; private set; }

        public Masu Create(Transform parent, string identifier, float x, float hierarchy) {

            Identifier = identifier;

            Masu masu = CreateMasu(parent);

            SettingPos(masu, x, hierarchy);

            SettingTouchedEvent(masu);

            return masu;
        }

        protected abstract Masu CreateMasu(Transform parent);

        protected virtual void SettingPos(Masu masu, float x, float hierarchy) {
            var pos = masu.transform.localPosition;

            pos.x = x * 2.5f;
            pos.y = hierarchy * 2.5f;

            masu.transform.localPosition = pos;

            var masuData = masu.GetMasuData();

            masuData.m_hierarchyData.x = x;
            masuData.m_hierarchyData.y = hierarchy;
        }

        protected virtual void SettingObjectName(Masu masu) { 
        }

        protected abstract void SettingTouchedEvent(Masu masu);
    }

    public abstract class MasuCreateFactory : MasuFactory {

        protected override Masu CreateMasu(Transform parent) {
            return GameObject.Instantiate(ResourceManager.Instance.Get<GameObject>(Identifier), parent).GetComponent<Masu>();
        }
    }

    /// <summary>
    /// 空いているマス
    /// </summary>
    public class EmptyMasuFactory : MasuCreateFactory {

        protected override void SettingTouchedEvent(Masu masu) {

            masu.SetMasuTouchedEvent((Masu touchedMasu) => { 
                
                Debug.Log("tap : " + Identifier);

                // その他のスクロールウィンドウが開いている時点でおかしい？ それとも閉じる？ひとまずはじいておく
                if (PopupCommandSelectWindow.Instance.CheckOpen() == true) {
                    Debug.LogWarning("PopupCommandSelectWindow opened !!!");
                    return;
                }

                // 現在作成可能な部屋のリストを持ってきてここで表示されるリストを作成する
                var roomData = DungeonDataManager.Instance.LearnedRoomingSkillData;
                var trapData = DungeonDataManager.Instance.LearnedTrapSkillData;

                List<string> roomSkillList = new List<string>();
                if (roomData.m_learnedRoomingSkillIdList.Count > 0)
                    roomSkillList.AddRange(roomData.m_learnedRoomingSkillIdList);
                if (trapData.m_learnedTrapSkillIdList.Count > 0)
                    roomSkillList.AddRange(trapData.m_learnedTrapSkillIdList);

                // 空いているマスをタッチした際に表示されるリスト群の作成
                // 習得済みの部屋・罠
                List<CommandSelectCellData> dataList = new List<CommandSelectCellData>();
                foreach (var skill in roomSkillList) {
                    dataList.Add(CommandSelectCellDataFactoryManager.Instance.Create(skill, masu));
                }

                PopupCommandSelectWindow.Instance.Open(CanvasManager.Instance.Current.transform, dataList);
            });
        }
    }

    public class DungeonCoreFactory : MasuCreateFactory {

        protected override void SettingTouchedEvent(Masu masu) {

            masu.SetMasuTouchedEvent((Masu m) => {

                Debug.Log("tap : " + m.GetMasuData().m_identifier);

                if (PopupCommandSelectWindow.Instance.CheckOpen() == true) {
                    Debug.LogWarning("PopupCommandSelectWindow opened !!!");
                    return;
                }

                var summonData = DungeonDataManager.Instance.LearnedSummonmingSkillData;

                // 召喚スキルで覚えたモンスターのリストを取得
                List<string> summonList = new List<string>();
                if (summonData.m_learnedSummoningSkillIdList.Count > 0)
                    summonList.AddRange(summonData.m_learnedSummoningSkillIdList);

                // 覚えた召喚のモンスターが召喚可能かチェック
                // 必要な部屋をダンジョンに配置しているかどうかチェック
                // 召喚に必要な部屋を配置していない場合は CommandSelectCellDataFactoryManager でボタンを灰色に設定

                List<CommandSelectCellData> dataList = new List<CommandSelectCellData>();
                foreach (var summon in summonList) {
                    dataList.Add(CommandSelectCellDataFactoryManager.Instance.Create(summon, masu));
                }

                PopupCommandSelectWindow.Instance.Open(CanvasManager.Instance.Current.transform, dataList);

            });
        }
    }

    public class DungeonEntranceFactory : MasuCreateFactory {

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent((Masu m) => { Debug.Log("tap : " + m.GetMasuData().m_identifier); });
        }
    }

    public class TerrainSoilFactory : MasuCreateFactory {

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent((Masu m) => { Debug.Log("tap : " + m.GetMasuData().m_identifier); });
        }
    }

    public class RoostFactory : MasuCreateFactory {

        protected override void SettingTouchedEvent(Masu masu)
        {
            masu.SetMasuTouchedEvent((Masu m) => { Debug.Log("tap : " + m.GetMasuData().m_identifier); });
        }
    }

    public class MasuFactoryManager : Singleton<MasuFactoryManager> {

        private Dictionary<string,MasuFactory> m_maps = new Dictionary<string, MasuFactory>() {
            { "EmptyMasu", new EmptyMasuFactory() },
            { "DungeonCore", new DungeonCoreFactory() },
            { "DungeonEntrance", new DungeonEntranceFactory() },
            { "TerrainSoil", new TerrainSoilFactory() },
            { "Roost", new RoostFactory() },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="identifier"></param>
        /// <param name=""></param>
        /// <param name="hierarchy"></param>
        /// <returns></returns>
        public Masu Create(Transform parent, string identifier, float x, float hierarchy) {

            return m_maps[identifier].Create(parent, identifier, x, hierarchy);
        }
    }
}