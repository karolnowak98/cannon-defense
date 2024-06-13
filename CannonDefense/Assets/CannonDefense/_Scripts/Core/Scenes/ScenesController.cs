using UnityEngine.SceneManagement;

namespace GlassyCode.CannonDefense.Core.Scenes
{
    public sealed class ScenesController : IScenesController
    {
        private const int AimTrainingSceneId = 0;
        private const int DustMapSceneId = 1;

        public void LoadAimMapScene()
        {
            SceneManager.LoadScene(AimTrainingSceneId);
        }
        
        public void LoadAimMapSceneAsync()
        {
            SceneManager.LoadSceneAsync(AimTrainingSceneId);
        }
        
        public void LoadDustMapScene()
        {
            SceneManager.LoadScene(DustMapSceneId, LoadSceneMode.Single);
        }
        
        public void LoadDustMapSceneAsync()
        {
            SceneManager.LoadSceneAsync(DustMapSceneId);
        }
    }
}