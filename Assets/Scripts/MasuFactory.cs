using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj {

    public abstract class MasuFactory {

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

        protected abstract string Identifier { get; }

        protected override Masu CreateMasu(Transform parent) {
            return GameObject.Instantiate(ResourceManager.Instance.Get<GameObject>(Identifier), parent).GetComponent<Masu>();
        }
    }

    public class EmptyMasuFactory : MasuCreateFactory {

        protected override string Identifier => "EmptyMasu";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class DungeonCoreFactory : MasuCreateFactory {

        protected override string Identifier => "DungeonCore";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class DungeonEntranceFactory : MasuCreateFactory {

        protected override string Identifier => "DungeonEntrance";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class TerrainSoilFactory : MasuCreateFactory {

        protected override string Identifier => "TerrainSoil";

        protected override void SettingTouchedEvent(Masu masu) {
            masu.SetMasuTouchedEvent(() => { Debug.Log("tap : " + Identifier); });
        }
    }

    public class MasuFactoryManager : Singleton<MasuFactoryManager> {

        private Dictionary<string, MasuFactory> m_factoryMap = new Dictionary<string, MasuFactory>() {
            { "EmptyMasu", new EmptyMasuFactory() },
            { "DungeonCore", new DungeonCoreFactory() },
            { "DungeonEntrance", new DungeonEntranceFactory() },
            { "TerrainSoil", new TerrainSoilFactory() }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="identifier"></param>
        public Masu Create(Transform parent, string identifier, float posX, float posY) {

            if (m_factoryMap.ContainsKey(identifier) == false) {
                Debug.Log("identifier is nothing !!");
                return null;
            }

            return m_factoryMap[identifier].Create(parent, posX, posY);
        }
    }
}