using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8prak
{
    public class Warrior
    {
        public string Name { get; set; }

        // Свойство — количество жизней
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }

        // Урон, реально прошедший после доспехов (для вывода в лог)
        public int LastDamage { get; protected set; }

        // Какая доля урона проходит сквозь доспех
        public virtual double ArmorFactor
        {
            get { return 1.0; } // без доспехов — полностью
        }

        public bool IsAlive
        {
            get { return Health > 0; }
        }

        public Warrior(string name, int health)
        {
            Name = name;
            MaxHealth = health;
            Health = health;
            LastDamage = 0;
        }

        // Виртуальный метод: получить урон
        public virtual void TakeDamage(int damage)
        {
            LastDamage = damage;
            Health -= LastDamage;
            if (Health < 0) Health = 0;
        }
    }
}
