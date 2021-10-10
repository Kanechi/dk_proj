using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace dkproj {
    public class TutorialScene : BaseScene {

        /// <summary>
        /// 階層マップ
        /// </summary>
        private string[,] m_hierarcyMap = {
            { "DungeonEntrance", "TerrainSoil", "EmptyMasu", "DungeonCore" },
        };

        int m_loadingCount = 0;

        private enum eSceneState {
            None = -1,
            Initialize,
            Main,
            Finalize,
        }
        private eSceneState m_sceneState = eSceneState.None;

        private UnityAction[] m_updateMethod;

        // Start is called before the first frame update
        private async void Start() {

            // invoker
            m_updateMethod = new UnityAction[]{
                OnInitialize,
                OnMainUpdate,
                OnFinalize
            };


            // loading
            await OnLoading();


            // check loading
            while (true) {
                if (s_maxLoadingCt == s_loadingCt)
                    break;
            }

            m_sceneState = eSceneState.Initialize;
        }

        // Update is called once per frame
        void Update() {
            if (m_sceneState < 0)
                return;

            m_updateMethod[(int)m_sceneState]();
        }

        static private int s_loadingCt = 0;

        static private int s_maxLoadingCt = 0;

        static private async Task LoadResource<Ty>(string id) where Ty : UnityEngine.Object {
            s_maxLoadingCt++;
            await ResourceManager.Instance.LoadAsync<Ty>(id, prefab => s_loadingCt++);
        }

        /// <summary>
        /// リソースの読み込み
        /// </summary>
        /// <returns></returns>
        private async Task OnLoading() {
            await LoadResource<Sprite>("RoostTexture");
            await LoadResource<Sprite>("Black100");

            await LoadResource<GameObject>("DungeonCore");
            await LoadResource<GameObject>("DungeonEntrance");
            await LoadResource<GameObject>("EmptyMasu");
            await LoadResource<GameObject>("TerrainSoil");
            await LoadResource<GameObject>("Roost");

            await LoadResource<GameObject>("CommandSelectCell");
            await LoadResource<GameObject>("CommandSelectWindow");

            await LoadResource<GameObject>("Gobrin");
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void OnInitialize() {

            // 最初に空いているマスを敷き詰める
            for (int y = 0; y < m_hierarcyMap.GetLength(0); ++y) {
                for (int x = 0; x < m_hierarcyMap.GetLength(1); ++x) {
                    MasuFactoryManager.Instance.Create(transform, "EmptyMasu", x, y);
                }
            }

            // 空いているマスの上に地形マス、遺物マス、部屋マスを敷き詰める
            // 空マスには何もおかない
            // このつくりだと2段目以降のマイナスマスから始まる作りに汎用性がない。というか作れない・・・
            // ひとまずこれでつくって考える。まずは階層情報を含めたい。
            for (int y = 0; y < m_hierarcyMap.GetLength(0); ++y) {
                for (int x = 0; x < m_hierarcyMap.GetLength(1); ++x) {
                    if (m_hierarcyMap[y, x] == "EmptyMasu")
                        continue;
                    MasuFactoryManager.Instance.Create(transform, m_hierarcyMap[y, x], x, y);
                }
            }

            m_sceneState = eSceneState.Main;
        }

        /// <summary>
        /// メイン更新処理
        /// </summary>
        private void OnMainUpdate() {
        }

        private void OnFinalize() {
        }
    }
}