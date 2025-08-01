using System.Collections.Generic;
using __Project.Scripts.NewSystem.Elements;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database
{
    [CreateAssetMenu(menuName = "Book/FishBookDatabase", fileName = "FishBookDatabase")]
    public class FishBookDatabase : ScriptableObject
    {
        public List<FishBookEntry> entries;
    }
}