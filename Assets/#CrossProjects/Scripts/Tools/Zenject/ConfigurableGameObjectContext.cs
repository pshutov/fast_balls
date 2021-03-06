using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.Internal;

namespace _CrossProjects.Tools.Zenject
{
    public class ConfigurableGameObjectContext : GameObjectContext
    {
        [Header("CONFIGS")]
        [SerializeField]
        private bool m_injectInChildrenMonoBehaviours = true;
        [SerializeField]
        private bool m_injectInChildrenAnimators = true;


        protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
        {
            if (m_injectInChildrenMonoBehaviours && m_injectInChildrenAnimators)
            {
                base.GetInjectableMonoBehaviours(monoBehaviours);
                return;
            }
            
            if (m_injectInChildrenAnimators)
            {
                ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);
            }

            // We inject on all components on the root except ourself
            foreach (var monoBehaviour in GetComponents<MonoBehaviour>())
            {
                if (monoBehaviour == null)
                {
                    // Missing script
                    continue;
                }

                if (!ZenUtilInternal.IsInjectableMonoBehaviourType(monoBehaviour.GetType()))
                {
                    continue;
                }

                if (monoBehaviour == this)
                {
                    continue;
                }

                monoBehaviours.Add(monoBehaviour);
            }

            if (m_injectInChildrenMonoBehaviours)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);

                    if (child != null)
                    {
                        ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(
                            child.gameObject, monoBehaviours);
                    }
                }
            }
        }
    }
}
