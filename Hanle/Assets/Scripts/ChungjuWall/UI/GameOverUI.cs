using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChungjuWall.UI {
    /// <summary>
    ///     ���� ������ ���� UI�� �����ϴ� Ŭ����
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
        ///     ���� ����� ��ư�� ������ ���� �ٽ� �ε��Ų��.
        /// </summary>
        public void RestartGame() {
            // TODO: �� �ȱ�� ��� ���� ������ �����ϱ�
            SceneManager.LoadScene(gameObject.scene.name);
        }
    
        /// <summary>
        ///     ���� ���� ��ư�� ������ ���� ������ �̵��Ѵ�.
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