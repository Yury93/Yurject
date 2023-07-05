using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace Yurject
{
    public class Context : MonoBehaviour
    {
        private void Awake()
        {
            ContainerServices.Init();
            OnRegister();
            ApplyInject();

        }
        public virtual void OnRegister()
        {
        }
        protected void Register(GameObject gameObject)
        {
            Component[] components = gameObject.GetComponents<Component>();

            foreach (Component component in components)
            {
                if (component is MonoBehaviour)
                {
                    Debug.Log("Script on GameObject: " + component.GetType().Name);
                    ContainerServices.Register(component);
                }
            }
        }
  
        private void ApplyInject()
        {
            string[] scriptGuids = AssetDatabase.FindAssets("t:Script");

            foreach (string scriptGuid in scriptGuids)
            {
                string scriptPath = AssetDatabase.GUIDToAssetPath(scriptGuid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);

                var getClass = script.GetClass();
                MethodInfo[] methods = getClass?.GetMethods();
                if (methods == null) return;
                foreach (MethodInfo method in methods)
                {
                    if (method.IsDefined(typeof(InjectAttribute), true))
                    {
                        var target = FindObjectsOfType(getClass).FirstOrDefault();
                        if (target != null)
                        {
                            ContainerServices.ApplyAttribute(target, methods);
                        }

                    }
                }
            }
        }
    }
}



