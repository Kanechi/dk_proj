using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    public class CanvasManager : SingletonMonoBehaviour<CanvasManager>
    {
        [SerializeField]
        private Canvas current_;

        public Canvas Current => current_ != null ? current_ : current_ = GetComponent<Canvas>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}