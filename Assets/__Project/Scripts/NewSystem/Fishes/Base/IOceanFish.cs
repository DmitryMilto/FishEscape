using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;

namespace __Project.Scripts.NewSystem.Fishes.Base
{
    public interface IOceanFish
    {
        public List<TypeOceans> Oceans { get; }
        public bool IsInOcean(TypeOceans ocean);
    }
}