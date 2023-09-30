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
        Brush green_brush = new SolidColorBrush(Color.FromArgb(26,25,186,0));

        public MainWindow()
        {
            InitializeComponent();
            Timer_setup();

            //pseudo data
            main_game = new Game("Player 1", "Player 2", new Tank("Tiger-1a", "Mega mrdnik to je", 15, 75, 275, 60, 9, "Tank1.png"), new Tank("Tiger-2a", "Mega mrdnik to je", 15, 75, 275, 60, 10, "Tank2.png"));
            
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
                user2_dis.Background = null;
                user1_dis.Background = green_brush;
                angle_dis.Content = main_game.player_1.gun_angle +"°";
                health_bar.Value = main_game.player_1.health;
                health_bar.Maximum = main_game.player_1.tank.max_health;
                health_lb.Content = main_game.player_1.health +"/"+ main_game.player_1.tank.max_health;
                armor_bar.Value = main_game.player_1.armor;
                armor_bar.Maximum = main_game.player_1.tank.max_armor;
                armor_lb.Content = main_game.player_1.armor +"/"+ main_game.player_1.tank.max_armor;
            }
            else if(main_game.turn == Turn.player_2)
            {
                user1_dis.Background = null;
                user2_dis.Background = green_brush;
                angle_dis.Content = main_game.player_2.gun_angle +"°";
                health_bar.Value = main_game.player_2.health;
                health_bar.Maximum = main_game.player_2.tank.max_health;
                health_lb.Content = main_game.player_2.health + "/" + main_game.player_2.tank.max_health;
                armor_bar.Value = main_game.player_2.armor;
                armor_bar.Maximum = main_game.player_2.tank.max_armor;
                armor_lb.Content= main_game.player_2.armor;
                armor_lb.Content = main_game.player_2.armor + "/" + main_game.player_2.tank.max_armor;
            }




            main_dis.Children.Clear();
            var temp1 = prerender_tank(main_game.player_1.tank);
            main_dis.Children.Add(temp1);
            Canvas.SetLeft(temp1, main_dis.ActualWidth * (main_game.player_1.position/2));

            var temp1g = prerender_gun(main_game.player_1);
            main_dis.Children.Add(temp1g);
            Canvas.SetLeft(temp1g, main_dis.ActualWidth * (main_game.player_1.position/2 + 0.11));
            Canvas.SetBottom(temp1g, (main_dis.ActualHeight * 0.395));


            var temp2 = prerender_tank(main_game.player_2.tank);
            main_dis.Children.Add(temp2);
            Canvas.SetRight(temp2, main_dis.ActualWidth * (main_game.player_2.position/2));

            var temp2g = prerender_gun(main_game.player_2);
            main_dis.Children.Add(temp2g);
            Canvas.SetRight(temp2g, main_dis.ActualWidth * (main_game.player_2.position/2 + 0.075));
            Canvas.SetBottom(temp2g, (main_dis.ActualHeight * 0.41));
        }

        private Image prerender_tank(Tank tank)
        {
            Image temp = new Image();
            temp.Source = new BitmapImage(new Uri("Media/"+ tank.texture, UriKind.Relative));
            temp.Height = main_dis.ActualHeight*0.25;
            temp.Width = main_dis.ActualWidth*0.15;
            Canvas.SetBottom(temp, (main_dis.ActualHeight*0.3));
            return temp;
        }

        private Image prerender_gun(Player player)
        {
            Image temp = new Image();
            temp.Source = new BitmapImage(new Uri("Media/"+player.tank.texture.Substring(0,5)+"_gun.png", UriKind.Relative));
            temp.Height = main_dis.ActualHeight * 0.015;
            temp.Width = main_dis.ActualWidth * 0.075;
            RotateTransform rotate = new RotateTransform();
            if (player == main_game.player_1)
            {
                rotate = new RotateTransform(-player.gun_angle, 0, temp.Height / 2);
            }
            else if(player == main_game.player_2)
            {
                rotate = new RotateTransform(player.gun_angle, main_dis.ActualWidth * 0.063, temp.Height / 2);
            }
            temp.RenderTransform = rotate;
            return temp;
        }

        private void angldown_btn_Click(object sender, RoutedEventArgs e)
        {
            double new_angle;
            if(main_game.turn == Turn.player_1)
            {
                new_angle = main_game.player_1.gun_angle -= 0.5;
                main_game.player_1.Set_gunangle(new_angle);
            }
            else if(main_game.turn==Turn.player_2)
            {
                new_angle = main_game.player_2.gun_angle -= 0.5;
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
                new_angle = main_game.player_2.gun_angle += 0.5;
                main_game.player_2.Set_gunangle(new_angle);
            }
            Render();
        }

        private void angldown_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_down.Start();
            Render();
        }

        private void angldown_btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_down.Stop();
            Render();
        }

        private void anglup_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_up.Start();
            Render();
        }

        private void anglup_btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            clickAndHoldTimer_up.Stop();
            Render();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(main_game.turn == Turn.player_1)
            {
                main_game.player_1.Shoot();
            }
            else if(main_game.turn == Turn.player_2)
            {
                main_game.player_2.Shoot();
            }
            Render();
        }
    }
}
