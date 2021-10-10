using System;
using UnityEngine;
using TouchScript.Gestures;
using UnityEngine.Events;

namespace dkproj {

    /// <summary>
    /// バイオームタイプ
    /// </summary>
    public enum eBiome {

        None = 0,

        Cave = 1    // 洞窟(仮)
    }

    /// <summary>
    /// マスタイプ
    /// </summary>
    public enum eMasuType {
        None = 0,

        Empty,      // 空いている地形

        Terrain,    // 地形

        Relics,     // 遺物

        Room,       // 部屋

        Trap,       // 罠
    }

    // 階層データ (x...左からの番号 y...階層番号)
    public struct Point {
        public float x;
        public float y;
    }

    /// <summary>
    /// マス
    /// </summary>
    public class Masu : MonoBehaviour {

        [Serializable]
        public class MasuData {
            // 識別名
            public string m_identifier;

            // 名前
            public string m_name;

            // マスタイプ
            public eMasuType m_masuType;

            // バイオームタイプ
            public eBiome m_biomeType;

            // 現在耐久度
            public int m_health;

            // 最大耐久度
            public int m_maxHealth;

            // 位置データ (保存用 translate.localPosition の値と同じ)
            public Point m_pos;

            // 階層データ
            public Point m_hierarchyData;


            // true...自身を移動させることが出来る
            public bool m_enableMoveFlag;

            // true...自身を破壊可能
            public bool m_enableDestroyFlag;

            // true...モンスターやユニットが通行可能
            public bool m_enablePassableFlag;


            // 小マス(null...空いている空間)
            public Masu m_child;

            // 自マス(自身のマス)
            public Masu m_self;
        }

        [SerializeField]
        private MasuData m_masuData = new MasuData();
        public MasuData GetMasuData() => m_masuData;

        [SerializeField]
        private TapGesture m_tapGesture;

        private void OnEnable() {

            m_tapGesture.Tapped += OnTapped;
        }

        private void OnDisable() {
            m_tapGesture.Tapped -= OnTapped;
        }

        private void OnTapped(object sender, EventArgs e) {
            Debug.Log("Tapped !!");

            m_masuTouchedEvent?.Invoke(this);
        }

        // このマスをタッチした際のイベント
        private UnityAction<Masu> m_masuTouchedEvent;
        public void SetMasuTouchedEvent(UnityAction<Masu> tapped) => m_masuTouchedEvent = tapped;
    }
}