using System.Xml.Serialization;

namespace Gameplay.FightSystem
{
    [System.Serializable]
    public enum WeaponType
    {
        [XmlEnum("pistol")]
        pistol,
        [XmlEnum("shotgun")]
        shotgun,
    }
}
