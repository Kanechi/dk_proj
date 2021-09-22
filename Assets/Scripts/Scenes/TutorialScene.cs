using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace dkproj
{
    public class TutorialScene : BaseScene
    {
        [SerializeField]
        private int hierarchy_; // 階層数

        [SerializeField]
        private int count_;     // マス数

        // プレハブ管理
        [SerializeField]
        private PrefabController prefabCtrl_ = new PrefabController();

        // 一階層
        private string[] map_ = { "DungeonEntrance", "TerrainSoil", "EmptyMasu", "DungeonCore" };


        int loadingCount_ = 0;

        // Start is called before the first frame update
        private async void Start()
        { 

            // マップから指定のプレハブを取得
            for (int i = 0; i < 4; ++i)
            {
                await Create(i);
            }

            while (true)
            {
                await Task.Delay(1);
                if (loadingCount_ == 4)
                    break;
            }
        }


        // Update is called once per frame
        void Update()
        {

        }

        public async Task Create(int index)
        {
            await ResourceManager.Instance.LoadAsync<GameObject>(map_[index], prefab => {

                // ゲームオブジェクトを作成
                var gameObject = GameObject.Instantiate(prefab, transform);

                var pos = gameObject.transform.localPosition;

                pos.x += (2.5f * index);

                Debug.Log("pos.x : " + pos.x + ", index : " + index);

                //pos.x += 0.0f;

                gameObject.transform.localPosition = pos;

                loadingCount_++;
            });
        }
    }
}