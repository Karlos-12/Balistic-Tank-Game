using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Balistic_Tank_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game main_game;


        DispatcherTimer clickAndHoldTimer_up;
        DispatcherTimer clickAndHoldTimer_down;
        const int clikandhold_time = 25;

        public MainWindow()
        {
            InitializeComponent();
            Timer_setup();

            //pseudo data
            main_game = new Game("test_1", "test_2", new Tank("Tiger-1a", "Mega mrdnik to je", 15, 75, 275, 60, 10), new Tank("Tiger-2a", "Mega mrdnik to je", 15, 75, 275, 60, 10));
            
            
            Render();
        }

        private void Timer_setup()
        {
            clickAndHoldTimer_up = new DispatcherTimer();
            clickAndHoldTimer_up.Tick += ClickAndHoldTimer_Tick_up;
            clickAndHoldTimer_up.Interval = TimeSpan.FromMilliseconds(clikandhold_time);

            clickAndHoldTimer_down = new DispatcherTimer();
            clickAndHoldTimer_down.Tick += ClickAndHoldTimer_Tick_down;
            clickAndHoldTimer_down.Interval = TimeSpan.FromMilliseconds(clikandhold_time);
        }

        private void ClickAndHoldTimer_Tick_up(object? sender, EventArgs e)
        {
            anglup_btn_Click(sender, new RoutedEventArgs());
        }

        private void ClickAndHoldTimer_Tick_down(object? sender, EventArgs e)
        {
            angldown_btn_Click(sender, new RoutedEventArgs());
        }

        public void Render()
        {
            user1_dis.Content = main_game.player_1.username;
            user2_dis.Content = main_game.player_2.username;

            if (main_game.turn == Turn.player_1)
            {
                user1_dis.Foreground = Brushes.Green;
                user2_dis.Foreground = Brushes.White;
                angle_dis.Content = main_game.player_1.gun_angle;
                health_bar.Value = main_game.player_1.health;
                health_bar.Maximum = main_game.player_1.tank.max_health;
                armor_bar.Value = main_game.player_1.armor;
                armor_bar.Maximum = main_game.player_1.tank.max_armor;
            }
            else
            {
                user2_dis.Foreground = Brushes.Green;
                user1_dis.Foreground = Brushes.White;
                angle_dis.Content = main_game.player_2.gun_angle;
                health_bar.Value = main_game.player_2.health;
                health_bar.Maximum = main_game.player_2.tank.max_health;
                armor_bar.Value = main_game.player_2.armor;
                armor_bar.Maximum = main_game.player_2.tank.max_armor;
            }
        }

        private void angldown_btn_Click(object sender, RoutedEventArgs e)
        {
            double new_angle;
            if(main_game.turn == Turn.player_1)
            {
                new_angle = main_game.player_1.gun_angle -= 0.5;
                main_game.player_1.Set_gunangle(new_angle);
            }
            else
            {
                new_angle = main_game.player_1.gun_angle -= 0.5;
                main_game.player_2.Set_gunangle(new_angle);
            }
            Render();
        }

        private void anglup_btn_Click(object sender, RoutedEventArgs e)
        {
            double new_angle;
            if (main_game.turn == Turn.player_1)
            {
                new_angle = main_game.player_1.gun_angle += 0.5;
                main_game.player_1.Set_gunangle(new_angle);
            }
            else
            {
                new_angle = main_game.player_1.gun_angle += 0.5;
                main_game.player_2.Set_gunangle(new_angle);
            }
            Render();
        }

        private void angldown_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_down.Start();
        }

        private void angldown_btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_down.Stop();
        }

        private void anglup_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_up.Start();
        }

        private void anglup_btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_up.Stop();
        }



    }
}
