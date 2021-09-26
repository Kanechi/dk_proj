using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;
using UnityEngine.Events;
using UniRx;

namespace dkproj {
    public class CommandSelectCell : EnhancedScrollerCellView {

        [SerializeField]
        private Image m_image = null;

        [SerializeField]
        private Button m_button = null;

        private CommandSelectCellData m_data = null;
        public CommandSelectCellData Data => m_data;

        public UnityAction<CommandSelectCell> Selected { get; set; } = null;

        private void Start() {
            m_button.OnClickAsObservable().Subscribe(_ => OnClicked());
        }

        public void SetData(CommandSelectCellData data) {

            m_data = data;
            m_data.Cell = this;

            m_image.sprite = m_data.Icon;

            SetSelected(m_data.IsSelected);
        }

        public void SetSelected(bool select) { 
            
        }

        private void OnClicked() {

            if (m_data.IsSelected == false)
                Selected?.Invoke(this);
            else if (m_data.IsSelected == true)
                m_data.TouchedEvent?.Invoke();
        }
    }
}