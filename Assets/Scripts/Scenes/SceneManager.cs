using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkproj
{
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        [SerializeField]
        private BaseScene current_;

        public BaseScene Current => current_ != null ? current_ : current_ = GetComponent<BaseScene>(); 

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