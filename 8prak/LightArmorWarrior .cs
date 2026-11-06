using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8prak
{
    public class LightArmorWarrior : Warrior
    {
        public LightArmorWarrior(string name, int health) : base(name, health) { }

        // Лёгкие доспехи: проходит 70% урона
        public override double ArmorFactor
        {
            get { return 0.7; }
        }

        // Переопределение метода получения урона
        public override void TakeDamage(int damage)
        {
            LastDamage = (int)Math.Round(damage * ArmorFactor);
            Health -= LastDamage;
            if (Health < 0) Health = 0;
        }
    }
}
