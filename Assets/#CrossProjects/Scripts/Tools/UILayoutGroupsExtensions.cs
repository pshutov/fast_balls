using UnityEngine.UI;

namespace _CrossProjects.Tools
{
    public static class UILayoutGroupsExtensions
    {
        public static void ForceUpdate(this LayoutGroup layoutGroup, bool reenable = true)
        {
            if (reenable)
            {
                layoutGroup.enabled = true;
            }
            
            layoutGroup.CalculateLayoutInputHorizontal();
            layoutGroup.CalculateLayoutInputVertical();
            layoutGroup.SetLayoutHorizontal();
            layoutGroup.SetLayoutVertical();
            
            // LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
            
            if (reenable)
            {
                layoutGroup.enabled = false;
            }
        }
    }
}