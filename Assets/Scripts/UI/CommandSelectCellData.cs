using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dkproj {

    /// <summary>
    /// 汎用選択セルデータ
    /// </summary>
    public class CommandSelectCellData {

        // 識別文字
        public string Identifier { get; private set; } = null;

        // アイコン画像
        public Sprite Icon { get; private set; } = null;

        // タッチイベント
        public UnityAction TouchedEvent { get; private set; } = null;

        // true...選択中
        public bool IsSelected { get; set; } = false;

        private CommandSelectCell m_cell = null;
        public CommandSelectCell Cell { get => m_cell; set => m_cell = value; }

        public CommandSelectCellData(string identifier, string iconName, UnityAction touchedEvent) {

            Identifier = identifier;

            Icon = ResourceManager.Instance.Get<Sprite>(iconName);

            TouchedEvent = touchedEvent;
        }
    }
}