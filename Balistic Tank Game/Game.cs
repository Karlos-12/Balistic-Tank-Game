using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Balistic_Tank_Game
{
    class Game
    {
        public Player player_1;
        public Player player_2;
        public Turn turn;

        private const double gravity = 9.8;
        private const double explosion_spred = 0.075;

        public Game(string name_1, string name_2, Tank tank_1, Tank tank_2)
        {
            player_1 = new Player(name_1, tank_1, 0.1);
            player_2 = new Player(name_2, tank_2, 0.9);

            player_1.Shot += Shothandler;
            player_2.Shot += Shothandler;

            turn = Turn.player_1;
        }

        private void Shothandler(Player sender, int potencial, double angle)
        {
            if((sender == player_1 && turn == Turn.player_1) || (sender == player_2 && turn == Turn.player_2))
            {
                double range = (((sender.tank.gun_velocity) * (sender.tank.gun_velocity) * Math.Sin(angle *2)) / gravity) / 10;
                if((((sender.position + range + explosion_spred) > player_1.position) && ((sender.position + range - explosion_spred) < player_1.position)) || (((sender.position - range + explosion_spred) > player_1.position) && ((sender.position - range - explosion_spred) < player_1.position)))
                {
                    player_1.Hit(potencial);
                    MessageBox.Show("Hit confirmed!");
                }
                else if ((((sender.position + range + explosion_spred) > player_2.position) && ((sender.position + range - explosion_spred) < player_2.position)) || (((sender.position - range + explosion_spred) > player_2.position) && ((sender.position - range - explosion_spred) < player_2.position)))
                {
                    player_1.Hit(potencial);
                    MessageBox.Show("Hit confirmed!");
                }

                Changeturn();
            }
        }

                
        private void Changeturn()
        {
            if(turn == Turn.player_1) turn = Turn.player_2;
            else turn = Turn.player_1;
        }

    }

    public enum Turn
    {
        player_1,
        player_2
    }
}
