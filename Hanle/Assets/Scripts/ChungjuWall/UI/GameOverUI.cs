using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChungjuWall.UI {
    /// <summary>
    ///     게임 오버시 나올 UI를 관리하는 클래스
    /// </summary>
    public class GameOverUI : MonoBehaviour {
        [Header("Initial Data")] 
        [SerializeField]
        private GameObject _stage1Object;

        [SerializeField] 
        private GameObject _stage2Object;

        private void OnEnable() {
            _stage1Object.SetActive(true);
            _stage2Object.SetActive(false);

            StartCoroutine(GameOverDelay());
        }

        /// <summary>
        ///     게임 재시작 버튼을 누르면 씬을 다시 로드시킨다.
        /// </summary>
        public void RestartGame() {
            // TODO: 씬 옴기는 방법 따로 있으면 적용하기
            SceneManager.LoadScene(gameObject.scene.name);
        }
    
        /// <summary>
        ///     게임 종료 버튼을 누르면 메인 씬으로 이동한다.
        /// </summary>
        public void ExitGame() {
            SceneManager.LoadScene("GameChoice");
        }

        IEnumerator GameOverDelay() {
            yield return new WaitForSeconds(2.0f);

            _stage1Object.SetActive(false);
            _stage2Object.SetActive(true);

            yield break;
        }
    }
}