using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dkproj {

    /// <summary>
    /// 汎用選択セルデータ
    /// </summary>
    public class CommandSelectCellData {

        // マス識別名
        public string Identifier { get; private set; } = null;

        // アイコン画像
        public Sprite Icon { get; private set; } = null;

        // タッチイベント
        public UnityAction TouchedEvent { get; private set; } = null;

        // true...ボタン有効     false...ボタン無効
        public bool IsBtnEnabled { get; set; } = false;

        // true...選択中
        public bool IsSelected { get; set; } = false;

        private CommandSelectCell m_cell = null;
        public CommandSelectCell Cell { get => m_cell; set => m_cell = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier">マスプレハブ識別 ID</param>
        /// <param name="selected">true ボタン有効化</param>
        public CommandSelectCellData(string identifier, bool enabled = true) {
            Identifier = identifier;
            IsBtnEnabled = enabled;
        }

        /// <summary>
        /// アイコンの作成
        /// </summary>
        /// <param name="id">アイコン画像 id</param>
        /// <returns></returns>
        public bool CreateIcon(string id) {
            Icon = ResourceManager.Instance.Get<Sprite>(id);
            if (Icon == null)
                return false;
            return true;
        }

        public void SetTouchedEvent(UnityAction touched) => TouchedEvent = touched;
    }
}