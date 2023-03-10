using RPGgame.Models;

namespace RPGgame.Dto
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Ishwar";
        public int HitPoints { get; set; } = 0;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Intellgence { get; set; } = 0;
        public RPGClass Type { get; set; } = RPGClass.Mage;
    }
}
