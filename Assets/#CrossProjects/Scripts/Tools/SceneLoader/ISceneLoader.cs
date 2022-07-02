using System;

namespace _CrossProjects.Tools.SceneLoader
{
    public interface ISceneLoader
    {
        int SceneIndex { get; }
        
        
        void Load();
    }
}