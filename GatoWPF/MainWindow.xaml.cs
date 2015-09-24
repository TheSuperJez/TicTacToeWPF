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
using System.Windows.Media.Animation;

namespace GatoWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GatoService gato;
        Boolean ganador;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            ganador = false;
            gato = new GatoService(ConsolaTxt);
            ((Storyboard)FindResource("iniciar")).Begin(this);
            ConsolaTxt.Items.Add("Presione Iniciar para empezar el juego");
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
            ((Storyboard)FindResource("fadeIn")).Begin(TituloLbl);
            ((Storyboard)FindResource("fadeIn")).Begin(DificultadLbl);
            ((Storyboard)FindResource("fadeIn")).Begin(DificultadCmb);
            ((Storyboard)FindResource("fadeIn")).Begin(IniciarBtn);
            ((Storyboard)FindResource("fadeIn")).Begin(DescripcionLbl);
            ((Storyboard)FindResource("fadeIn")).Begin(ConsolaLbl);
            ((Storyboard)FindResource("fadeIn")).Begin(ConsolaTxt);
        }

        private void Label_MouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           Application.Current.Shutdown();
        }

        private void Label01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(0,1,sender);

        }

        private void Label_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if (((Label)sender).Background.ToString() != "System.Windows.Media.ImageBrush")
            {
                ((Storyboard)FindResource("mouseEnter")).Begin((Label)sender);
            }
            
        }

        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            if (((Label)sender).Background.ToString() != "System.Windows.Media.ImageBrush")
            {
                ((Storyboard)FindResource("mouseLeave")).Begin((Label)sender);
            }
        }

        private void Label_MouseEnter_2(object sender, MouseEventArgs e)
        {
            ((Storyboard)FindResource("mouseEnterSalir")).Begin((Label)sender);
        }

        private void Label_MouseLeave_1(object sender, MouseEventArgs e)
        {
            ((Storyboard)FindResource("mouseLeaveSalir")).Begin((Label)sender);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ganador = false;
            ConsolaTxt.Items.Clear();
            ((Storyboard)FindResource("fadeIn")).Begin(tablero);
            Label00.Visibility = Visibility.Visible;
            Label01.Visibility = Visibility.Visible;
            Label02.Visibility = Visibility.Visible;
            Label10.Visibility = Visibility.Visible;
            Label11.Visibility = Visibility.Visible;
            Label12.Visibility = Visibility.Visible;
            Label21.Visibility = Visibility.Visible;
            Label20.Visibility = Visibility.Visible;
            Label22.Visibility = Visibility.Visible;

            Label00.Background = new SolidColorBrush(Colors.Transparent);
            Label01.Background = new SolidColorBrush(Colors.Transparent); ;
            Label02.Background = new SolidColorBrush(Colors.Transparent); ;
            Label10.Background = new SolidColorBrush(Colors.Transparent); ;
            Label11.Background = new SolidColorBrush(Colors.Transparent);
            Label12.Background = new SolidColorBrush(Colors.Transparent); ;
            Label20.Background = new SolidColorBrush(Colors.Transparent); ;
            Label21.Background = new SolidColorBrush(Colors.Transparent); ;
            Label22.Background = new SolidColorBrush(Colors.Transparent); ;

            gato.iniciarPartida(Convert.ToInt16(DificultadCmb.SelectedValue));
            actualizarTablero();
        }

        public void actualizarTablero()
        {

            for (Int32 i = 0; i < 3;i++ )
            {
                for (Int32 j = 0; j < 3; j++)
                {
                    //00
                    if (i == 0 && j == 0 && gato.tablero[i, j]!=0)
                    {
                        Label00.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j]+".png", UriKind.Relative)));
                    }
                    //01
                    if (i == 0 && j == 1 && gato.tablero[i, j] != 0)
                    {
                        Label01.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //02
                    if (i == 0 && j == 2 && gato.tablero[i, j] != 0)
                    {
                        Label02.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }

                    //10
                    if (i == 1 && j == 0 && gato.tablero[i, j] != 0)
                    {
                        Label10.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //11
                    if (i == 1 && j == 1 && gato.tablero[i, j] != 0)
                    {
                        Label11.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //12
                    if (i == 1 && j == 2 && gato.tablero[i, j] != 0)
                    {
                        Label12.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //20
                    if (i == 2 && j == 0 && gato.tablero[i, j] != 0)
                    {
                        Label20.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //21
                    if (i == 2 && j == 1 && gato.tablero[i, j] != 0)
                    {
                        Label21.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                    //22
                    if (i == 2 && j == 2 && gato.tablero[i, j] != 0)
                    {
                        Label22.Background = new ImageBrush(new BitmapImage(new Uri(@gato.tablero[i, j] + ".png", UriKind.Relative)));
                    }
                }
            }
            comprobarGanador('X');
            comprobarGanador('O');
        }

        public void comprobarGanador(char pieza)
        {
            if (gato.tablero[0, 0] == pieza && gato.tablero[0, 1] == pieza && gato.tablero[0, 2] == pieza)
            {
                setGanador(pieza);
            }

            if (gato.tablero[1, 0] == pieza && gato.tablero[1, 1] == pieza && gato.tablero[1, 2] == pieza)
            {
                setGanador(pieza);
            }
            if (gato.tablero[2, 0] == pieza && gato.tablero[2, 1] == pieza && gato.tablero[2, 2] == pieza)
            {
                setGanador(pieza);
            }

            //Vertical
            if (gato.tablero[0, 0] == pieza && gato.tablero[1, 0] == pieza && gato.tablero[2, 0] == pieza)
            {
                setGanador(pieza);
            }

            if (gato.tablero[0, 1] == pieza && gato.tablero[1, 1] == pieza && gato.tablero[2, 1] == pieza)
            {
                setGanador(pieza);
            }
            if (gato.tablero[0, 2] == pieza && gato.tablero[1, 2] == pieza && gato.tablero[2, 2] == pieza)
            {
                setGanador(pieza);
            }

            //Diagonales
            if (gato.tablero[0, 0] == pieza && gato.tablero[1, 1] == pieza && gato.tablero[2, 2] == pieza)
            {
                setGanador(pieza);
            }
            if (gato.tablero[2, 0] == pieza && gato.tablero[1, 1] == pieza && gato.tablero[0, 2] == pieza)
            {
                setGanador(pieza);
            }
        }

        public void setGanador(char pieza)
        {
            ConsolaTxt.Items.Add("Ganador: "+ pieza);
            MessageBox.Show("Han ganado " + pieza, "Hay un ganador", MessageBoxButton.OK, MessageBoxImage.Information);
            Label00.Visibility = Visibility.Hidden;
            Label01.Visibility = Visibility.Hidden;
            Label02.Visibility = Visibility.Hidden;
            Label10.Visibility = Visibility.Hidden;
            Label11.Visibility = Visibility.Hidden;
            Label12.Visibility = Visibility.Hidden;
            Label21.Visibility = Visibility.Hidden;
            Label20.Visibility = Visibility.Hidden;
            Label22.Visibility = Visibility.Hidden;
            
            ganador = true;
        }

        public void tirar(Int32 x, Int32 y, Object sender)
        {
            if (((Label)sender).Background.ToString() != "System.Windows.Media.ImageBrush")
            {
                gato.tirarJugador(x, y);
                if(!ganador){
                gato.tirarCpu();
                }
                actualizarTablero();
            }
        }

        private void Label00_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(0, 0, sender);
        }

        private void Label02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(0, 2, sender);
        }

        private void Label10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(1, 0, sender);
        }

        private void Label11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(1, 1, sender);
        }

        private void Label12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(1, 2, sender);
        }

        private void Label20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(2, 0, sender);
        }

        private void Label21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(2, 1, sender);
        }

        private void Label22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirar(2, 2, sender);
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.DragMove();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
