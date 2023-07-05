using UnityEngine;

namespace Yurject
{
    /// <summary>
    /// add component on gameObject and realize attribute [inject]
    /// </summary>

    public class TestGameObject : MonoBehaviour
    {
        [SerializeField] private TestService testService;


        [Inject]
        public void Container(TestService testService)
        {
            this.testService = testService;
        }
    }
}