using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balistic_Tank_Game
{
    class Player
    {
        public string username { get; internal set; }
        public int ammo {  get; internal set; }
        public int health { get; internal set; }
        public int armor { get; internal set; }
        public Tank tank { get; internal set; }
        public double gun_angle { get; set; }
        public double position { get; internal set; }

        public Player(string username, Tank tank, double position) 
        {
            this.username = username;
            this.tank = tank;
            this.ammo = tank.ammo_storage;
            this.health = tank.max_health;
            this.gun_angle = 45;
            this.position = position;
        }

        Random random = new Random();

        public void Hit(int potencial_damage)
        {
            health -= potencial_damage - (armor / 4);
            armor -= (armor / 4) - random.Next(0,(armor/6));
        }

        public delegate void ShotHandler(Player sender, int potencial_damage, double angle);
        public event ShotHandler Shot;

        public void Shoot()
        {
            ammo--;
            Shot.Invoke(this, tank.damage, gun_angle);
            gun_angle += random.Next(-5, 5);
            gun_align();    
        }

        public void Set_gunangle(double angle)
        {
            gun_angle = angle;
            gun_align();
        }

        public void Set_position(double postion)
        {
            if(position >= 0 && position <= 1 )
            {
                 this.position = position;
            }
        }

        private void gun_align()
        {   
            if (gun_angle < 0) gun_angle = 0;
            else if (gun_angle > 90) gun_angle = 90;
        }



    }
}
