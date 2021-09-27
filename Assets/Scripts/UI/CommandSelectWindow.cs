using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

namespace dkproj {
    public class CommandSelectWindow : BasePopupWindow, IEnhancedScrollerDelegate {

        static public readonly string PREFAB_IDENTIFIER = "CommandSelectWindow";

        [SerializeField]
        private float CellSize => 100.0f;

        [SerializeField]
        private EnhancedScroller m_scroller = null;

        [SerializeField]
        private EnhancedScrollerCellView m_cell = null;

        // 表示中のリストのデータ
        private List<CommandSelectCellData> m_dataList = null;

        // 選択中のセルのデータ
        private CommandSelectCellData m_selectedCellData = null;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void CreateData(List<CommandSelectCellData> dataList) {

            m_dataList = new List<CommandSelectCellData>();
            foreach (var data in dataList) {
                m_dataList.Add(data);
            }
        }

        public void Open(List<CommandSelectCellData> dataList) {

            CreateData(dataList);

            m_scroller.Delegate = this;

            m_scroller.ReloadData();

            base.Open(null);
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex) {

            var cell = scroller.GetCellView(m_cell) as CommandSelectCell;

            cell.name = m_dataList[dataIndex].Identifier + "_Cell";

            cell.SetData(m_dataList[dataIndex]);

            cell.Selected = OnSelected;

            return cell;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) {
            return CellSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller) {
            return m_dataList.Count;
        }

        public void OnSelected(CommandSelectCell cell) {

            if (ReferenceEquals(m_selectedCellData, cell.Data))
                return;

            foreach (var data in m_dataList)
                data.IsSelected = false;

            m_selectedCellData = cell.Data;

            cell.Data.IsSelected = true;

            foreach (var data in m_dataList)
                cell.SetSelected(data.IsSelected);

            // 更新処理

            m_scroller.ReloadData(m_scroller.ScrollRect.horizontalNormalizedPosition);
        }
    }

    public class PopupCommandSelectWindow : Singleton<PopupCommandSelectWindow> {

        public CommandSelectWindow Window { get; private set; } = null;

        public void Open(Transform parent, List<CommandSelectCellData> dataList) {

            if (Window != null && Window.IsOpen == true)
                return;

            var prefab = ResourceManager.Instance.Get<GameObject>(CommandSelectWindow.PREFAB_IDENTIFIER);

            var obj = GameObject.Instantiate(prefab as GameObject, parent);

            Window = obj.GetComponent<CommandSelectWindow>();


            // 閉じるボタンの設定


            Window.Open(dataList);

            // 背景設定があれば背景設定
        }

        public void Close() {

            if (Window == null)
                return;

            if (Window.IsOpen == false)
                return;

            Window.Close(() => {

                GameObject.Destroy(Window.gameObject);
            });
        }
    }
}