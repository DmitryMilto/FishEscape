using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums.Audios;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Audios
{
    [CreateAssetMenu(menuName = "Audio/SoundDatabase")]
    public class SoundDatabase : ScriptableObject
    {
        [System.Serializable]
        public class SoundEntry
        {
            public SoundType type;
            public SoundCategory category;
            public AudioClip clip;
        }
    
        public List<SoundEntry> sounds;
    
        public SoundEntry GetEntry(SoundType type)
            => sounds.Find(s => s.type == type);
    }
}