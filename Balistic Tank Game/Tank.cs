using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balistic_Tank_Game
{
    class Tank
    {
        public string name { get; internal set; }
        public string description { get; internal set; }
        public int ammo_storage {  get; internal set; }
        public int max_armor { get; internal set; }
        public int max_health { get; internal set; }
        public int damage { get; internal set; }

        public Tank(string name, string description, int ammo_storage, int max_armor, int max_health, int damage)
        {
            this.name = name;
            this.description = description;
            this.ammo_storage = ammo_storage;
            this.max_armor = max_armor;
            this.max_health = max_health;
            this.damage = damage;
        }
    }
}
