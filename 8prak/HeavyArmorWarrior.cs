using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8prak
{
    public class HeavyArmorWarrior : Warrior
    {
        public HeavyArmorWarrior(string name, int health) : base(name, health) { }

        // Тяжёлые доспехи: проходит 40% урона (но не меньше 1)
        public override double ArmorFactor
        {
            get { return 0.4; }
        }

        // Переопределение метода получения урона
        public override void TakeDamage(int damage)
        {
            LastDamage = (int)Math.Round(damage * ArmorFactor);
            if (LastDamage < 1) LastDamage = 1; // минимальный урон
            Health -= LastDamage;
            if (Health < 0) Health = 0;
        }
    }
}