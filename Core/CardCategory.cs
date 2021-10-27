using System;

namespace Core
{
    [Flags]
    public enum CardCategory
    {
        None,
        Field,
        Farm,
        Shop,
        Food,
        Natural,
        Building,
        Factory,
        Fruit
    }
}