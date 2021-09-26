using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace dkproj {
    public class TutorialScene : BaseScene {

        // 一階層
        private string[] m_hierarcyMap = { "DungeonEntrance", "TerrainSoil", "EmptyMasu", "DungeonCore" };

        int loadingCount_ = 0;

        delegate void UpdateMethod();
        private UpdateMethod[] updateMethods_;

        // Start is called before the first frame update
        private async void Start() {
            updateMethods_ = new UpdateMethod[]{
                OnInitialize,
                OnMain,
                OnFinalize
            };

            await ResourceManager.Instance.LoadAsync<GameObject>("DungeonCore", prefab => loadingCount_++);
            await ResourceManager.Instance.LoadAsync<GameObject>("DungeonEntrance", prefab => loadingCount_++);
            await ResourceManager.Instance.LoadAsync<GameObject>("EmptyMasu", prefab => loadingCount_++);
            await ResourceManager.Instance.LoadAsync<GameObject>("TerrainSoil", prefab => loadingCount_++);

            await ResourceManager.Instance.LoadAsync<GameObject>("CommandSelectCell", prefab => loadingCount_++);
            await ResourceManager.Instance.LoadAsync<GameObject>("CommandSelectWindow", prefab => loadingCount_++);

            // リソースをすべてロードしたかチェック
            while (true) {
                if (loadingCount_ == 6)
                    break;
            }

            sceneState_ = eSceneState.Initialize;
        }

        private enum eSceneState {
            None = -1,
            Initialize,
            Main,
            Finalize,
        }
        private eSceneState sceneState_ = eSceneState.None;

        // Update is called once per frame
        void Update() {
            if (sceneState_ < 0)
                return;

            updateMethods_[(int)sceneState_]();
        }

        public void OnInitialize() {

            // 最初に空いているマスを敷き詰める
            for (int i = 0; i < m_hierarcyMap.Length; ++i) {

                MasuFactoryManager.Instance.Create(transform, "EmptyMasu", 2.5f * i, 0.0f);
            }

            // 空いているマスの上に地形マス、遺物マス、部屋マスを敷き詰める
            // 空マスには何もおかない
            for (int i = 0; i < m_hierarcyMap.Length; ++i) {

                if (m_hierarcyMap[i] == "EmptyMasu")
                    continue;

                MasuFactoryManager.Instance.Create(transform, m_hierarcyMap[i], 2.5f * i, 0.0f);
            }






            // 空いているマスをタッチした際に表示されるリスト群の作成
            List<CommandSelectCellData> dataList = new List<CommandSelectCellData>();

            // ひとまず「ねぐら」
            dataList.Add(new CommandSelectCellData("Roost", "Roost_Icon", () => { 
            
                // 空いているマスにねぐらを設置
            }));

            PopupCommandSelectWindow.Instance.Open(transform, dataList);

            sceneState_ = eSceneState.Main;
        }

        public void OnMain() {
        }

        public void OnFinalize() {
        }
    }
}