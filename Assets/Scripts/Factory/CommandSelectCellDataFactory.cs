using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    /// <summary>
    /// コマンドセレクトセル生成時にそれぞれタッチした際のふるまいが設定されている
    /// </summary>
    public abstract class CommandSelectCellDataFactory {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier">セルの識別</param>
        /// <param name="parentMasu">コマンドセルの親マス</param>
        /// <returns></returns>
        public CommandSelectCellData Create(string identifier, Masu parentMasu) {

            var cellData = CreateCommandSelectCellData(identifier);

            CreateCellIcon(cellData);

            SettingCellTouchedEvent(cellData, parentMasu);

            return cellData;
        }

        protected abstract CommandSelectCellData CreateCommandSelectCellData(string identifier);

        protected abstract void CreateCellIcon(CommandSelectCellData cellData);

        protected abstract void SettingCellTouchedEvent(CommandSelectCellData cellData, Masu parentMasu);
    }

    /// <summary>
    /// 選択コマンドセル
    /// 
    /// MOD など外部で作る際は特に意識して分ける必要はないが
    /// 内部で作成する際はカテゴリで分けておいた方が楽
    /// </summary>
    public abstract class CommandSelectCellDataCreateFactory : CommandSelectCellDataFactory {

        protected override CommandSelectCellData CreateCommandSelectCellData(string identifier) {
            return new CommandSelectCellData(identifier);
        }
    }



    /// <summary>
    /// ねぐらセルデータ
    /// </summary>
    public class RoomRoostCellDataFactory : CommandSelectCellDataCreateFactory {

        protected override void CreateCellIcon(CommandSelectCellData cellData) {
            cellData.CreateIcon("RoostTexture");
        }

        protected override void SettingCellTouchedEvent(CommandSelectCellData cellData, Masu parentMasu) {

            cellData.SetTouchedEvent(() => {
                // ねぐらアイコンをタッチしたらそのマスにねぐらを設置
                var hierarchy = parentMasu.GetMasuData().m_hierarchyData;

                parentMasu.GetMasuData().m_child = MasuFactoryManager.Instance.Create(parentMasu.transform.parent.transform, "Roost", hierarchy.x, hierarchy.y);

                // ねぐらを設置したらダンジョン設置データにねぐらをフラグを追加
                DungeonDataManager.Instance.InstalledRoomData.Add("Roost");

                PopupCommandSelectWindow.Instance.Close();
            });
        }
    }

    /// <summary>
    /// 召喚用選択コマンドセル
    /// </summary>
    public abstract class CommandSelectSummonCellDataCreateFactory : CommandSelectCellDataFactory {
        protected override CommandSelectCellData CreateCommandSelectCellData(string identifier) {

            bool enabled = CheckBtnEnabled();

            return new CommandSelectCellData(identifier, enabled);
        }

        /// <summary>
        /// 召喚に必要な部屋を配置しているかどうかのチェック
        /// </summary>
        /// <returns>true...ボタンを押すことができる</returns>
        protected abstract bool CheckBtnEnabled();
    }

    /// <summary>
    /// ゴブリンセルデータ
    /// </summary>
    public class SummonGobrinCellDataFactory : CommandSelectSummonCellDataCreateFactory {

        protected override bool CheckBtnEnabled() {

            // ねぐらが配置されているかどうかのチェック
            var roomData = DungeonDataManager.Instance.InstalledRoomData;

            return roomData.Check("Roost");
        }

        protected override void CreateCellIcon(CommandSelectCellData cellData) {

            cellData.CreateIcon("RoostTexture");
        }

        protected override void SettingCellTouchedEvent(CommandSelectCellData cellData, Masu parentMasu) {

            cellData.SetTouchedEvent(() => {
                
                var hierarchy = parentMasu.GetMasuData().m_hierarchyData;

                var prefab = ResourceManager.Instance.Get<GameObject>("Gobrin");

                var obj = GameObject.Instantiate(prefab, SceneManager.Instance.Current.transform);

                var pos = obj.transform.localPosition;

                var targetPos = parentMasu.transform.localPosition;

                pos.x = targetPos.x;
                pos.y = targetPos.y;

                obj.transform.localPosition = pos;

                //parentMasu.GetMasuData().m_child = MasuFactoryManager.Instance.Create(parentMasu.transform.parent.transform, "Roost", hierarchy.x, hierarchy.y);

                // ねぐらを設置したらダンジョン設置データにねぐらをフラグを追加
                //DungeonDataManager.Instance.InstalledRoomData.Add("Roost");

                PopupCommandSelectWindow.Instance.Close();
            });
        }
    }





    /// <summary>
    /// 選択コマンドセルデータ生成工場管理
    /// </summary>
    class CommandSelectCellDataFactoryManager : Singleton<CommandSelectCellDataFactoryManager> {

        private Dictionary<string, CommandSelectCellDataFactory> m_maps = new Dictionary<string, CommandSelectCellDataFactory>() {
            { "Roost", new RoomRoostCellDataFactory() },

            { "Gobrin", new SummonGobrinCellDataFactory() },
        };

        public CommandSelectCellData Create(string identifier, Masu parentMasu) {

            return m_maps[identifier].Create(identifier, parentMasu);
        }
    }
}