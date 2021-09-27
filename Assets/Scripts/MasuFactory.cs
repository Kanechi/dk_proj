using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    public abstract class MasuFactory {

        public abstract string Identifier { get; }

        public Masu Create(Transform parent, float posX, float posY) {

            Masu masu = CreateMasu(parent);

            SettingPos(masu, posX, posY);

            SettingTouchedEvent(masu);

            return masu;
        }

        protected abstract Masu CreateMasu(Transform parent);

        protected virtual void SettingPos(Masu masu, float posX, float posY) {
            var pos = masu.transform.localPosition;

            pos.x = posX;
            pos.y = posY;

            masu.transform.localPosition = pos;
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

        public override string Identifier => "EmptyMasu";

        protected override void SettingTouchedEvent(Masu masu) {

            masu.SetMasuTouchedEvent(() => { 
                
                Debug.Log("tap : " + Identifier);

                

                // その他のスクロールウィンドウが開いている時点でおかしい？ 
                //PopupCommandSelectWindow.Instance.Close();

                // 現在作成可能な部屋のリストを持ってきてここで表示されるリストを作成する

                // 空いているマスをタッチした際に表示されるリスト群の作成
                List<CommandSelectCellData> dataList = new List<CommandSelectCellData>();

                // ひとまず「ねぐら」アイコンを作成
                dataList.Add(new CommandSelectCellData("Roost", "Roost_Icon", () => {

                    // ねぐらアイコンをタッチしたらそのマスにねぐらを設置

                    var pos = masu.transform.localPosition;

                    masu.Child = MasuFactoryManager.Instance.Create(masu.transform.parent.transform, "Roost", pos.x, pos.y);

                    // ねぐら作成したらウィンドウ閉じる
                    PopupCommandSelectWindow.Instance.Close();
                }));

                PopupCommandSelectWindow.Instance.Open(CanvasManager.Instance.Current.transform, dataList);
            });
        }
    }

    public class DungeonCoreFactory : MasuCreateFactory {

        public override string Identifier => "DungeonCore";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class DungeonEntranceFactory : MasuCreateFactory {

        public override string Identifier => "DungeonEntrance";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class TerrainSoilFactory : MasuCreateFactory {

        public override string Identifier => "TerrainSoil";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class RoostFactory : MasuCreateFactory {

        public override string Identifier => "Roost";

        protected override void SettingTouchedEvent(Masu masu)
        {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class MasuFactoryManager : Singleton<MasuFactoryManager> {

        private List<MasuFactory> m_factoryList = new List<MasuFactory>() {
            new EmptyMasuFactory(),
            new DungeonCoreFactory(),
            new DungeonEntranceFactory(),
            new TerrainSoilFactory(),
            new RoostFactory()
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="identifier"></param>
        public Masu Create(Transform parent, string identifier, float posX, float posY) {

            foreach (var factory in m_factoryList) {
                if (factory.Identifier == identifier) {
                    return factory.Create(parent, posX, posY);
                }
            }

            Debug.Log("identifier is nothing !!");
            return null;
        }
    }
}