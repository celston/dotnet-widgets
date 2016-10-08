using Gnosis.Entities;

namespace Fruits
{
    public abstract class Fruit : Entity, IFruit
    {
        public decimal Weight { get; set; }
    }
}
