using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Controls;
namespace GatoWPF
{
    class GatoService
    {
        public Int16 jugador;
        public Int16 turnoActual;
        public char[,] tablero;
        public Boolean ganador;
        ListBox consola;
        Int16 dificultad;
        public GatoService(ListBox consola)
        {
            this.consola = consola;
        }

        public void iniciarPartida(Int16 dificultad)
        {
            this.dificultad = dificultad;
            string dificultadStr="";
            switch (dificultad)
            {
                case 0: dificultadStr = "Dificil"; break;
                case 1: dificultadStr = "Medio"; break;
                case 2: dificultadStr = "Facil"; break;
            }
            consola.Items.Add("Inicializando en dificultad: "+dificultadStr);
            ganador = false;
            tablero=new char[3,3];
            Random r = new Random(DateTime.Now.Millisecond);
            this.jugador = Convert.ToInt16(r.Next(1,3));
            consola.Items.Add("El jugador es: " + (jugador==1?'X':'O'));
            turnoActual = 1;
            if (jugador == 2)
            {
                tirarCpu();
            }
            
        }

        public void cambiarTurno()
        {
            consola.Items.Add("Cambiando de turno");
            if(turnoActual==1){
                turnoActual = 2;
            }else{
                turnoActual=1;
            }
        }
       

        public void tirarCpu()
        {
            consola.Items.Add("CPU Debe tirar");
            List<char[,]> listaTableros = new List<char[,]>();
            for(Int16 i =0;i<3;i++){
                for (Int16 j = 0; j < 3;j++ )
                {
                    char[,] tableroContador = (char[,])tablero.Clone();
                    if (tableroContador[i, j]==0)
                    {
                        tableroContador[i, j] = obtenerPiezaCpu();
                        listaTableros.Add(tableroContador);
                    }
                }
            }
            if (listaTableros.Count != 0)
            {
                tablero = determinarMovimientoOptimo(dificultad, listaTableros);
                cambiarTurno();
            }
        }

        public char[,] determinarMovimientoOptimo(Int16 dificultad,List<char[,]> listaTableros)
        {
            List<Int32> listaProbabilidades = new List<Int32>();
            foreach(char[,] tableroTemp in listaTableros){
                    listaProbabilidades.Add(obtenerProbabilidadMin((char[,])tableroTemp.Clone()));
            }
            //Burbuja
            for (Int32 i = 0; i < listaProbabilidades.Count-1;i++)
            {
                for (Int32 j = 0; j < listaProbabilidades.Count-1; j++)
                {
                    if(listaProbabilidades[j]>listaProbabilidades[j+1]){
                        Int32 temp = listaProbabilidades[j];
                        char[,] tableroTemp=listaTableros[j];
                        listaProbabilidades[j] = listaProbabilidades[j + 1];
                        listaProbabilidades[j + 1] = temp;
                        listaTableros[j] = listaTableros[j + 1];
                        listaTableros[j + 1] = tableroTemp;
                    }
                }
            }
            if (jugador == 1)
            {
                consola.Items.Add("Tirar MIN");
                //Min
                if ((0 + dificultad) < 2)
                {
                    return listaTableros[(0 + dificultad)];
                }
                else
                {
                    return listaTableros[0];
                }
                
            }
            else
            {
                consola.Items.Add("Tirar MAX");
                //Max
                if (((listaTableros.Count - 1) - dificultad) > 0)
                {
                    return listaTableros[((listaTableros.Count - 1) - dificultad)];
                }
                else
                {
                    return listaTableros[(listaTableros.Count - 1)];
                }
                
            }
        }
        public Int32 obtenerProbabilidadMin(char[,] tableroContador)
        {
            char[,] copiaTablero = (char[,])tableroContador.Clone();
            char piezaJugador;
            if (jugador == 1)
            {
                piezaJugador = 'X';
            }
            else
            {
                piezaJugador = 'O';
            }
            for (Int16 i = 0; i < 3; i++)
            {
                for (Int16 j = 0; j < 3; j++)
                {
                    if(tableroContador[i,j]==0){
                        tableroContador[i, j] = obtenerPiezaCpu();
                    }
                }
            }
            
            for (Int16 i = 0; i < 3; i++)
            {
                for (Int16 j = 0; j < 3; j++)
                {
                    if (copiaTablero[i, j] == 0)
                    {
                        copiaTablero[i, j] = piezaJugador;
                    }
                }
            }
            Int32 resultado = (contarGanadas(obtenerPiezaCpu(), tableroContador) - contarGanadas(piezaJugador, copiaTablero));
            consola.Items.Add("Probabilidad de ganar con tiro: " + resultado);
            return (resultado);

        }

        public Int32 contarGanadas(char pieza, char[,] tablero)
        {
            Int32 contador=0;
            //Horizontal
            if (tablero[0, 0] == pieza && tablero[0, 1] == pieza && tablero[0, 2] == pieza)
            {
                contador++;
            }

            if (tablero[1, 0] == pieza && tablero[1, 1] == pieza && tablero[1, 2] == pieza)
            {
                contador++;
            }
            if (tablero[2, 0] == pieza && tablero[2, 1] == pieza && tablero[2, 2] == pieza)
            {
                contador++;
            }

            //Vertical
            if (tablero[0, 0] == pieza && tablero[1, 0] == pieza && tablero[2, 0] == pieza)
            {
                contador++;
            }

            if (tablero[0, 1] == pieza && tablero[1, 1] == pieza && tablero[2, 1] == pieza)
            {
                contador++;
            }
            if (tablero[0, 2] == pieza && tablero[1, 2] == pieza && tablero[2, 2] == pieza)
            {
                contador++;
            }

            //Diagonales
            if (tablero[0, 0] == pieza && tablero[1, 1] == pieza && tablero[2, 2] == pieza)
            {
                contador++;
            }
            if (tablero[2, 0] == pieza && tablero[1, 1] == pieza && tablero[0, 2] == pieza)
            {
                contador++;
            }
            
            return contador;
        }


        public char obtenerPiezaCpu() {
            if (jugador == 1)
            {
                return 'O';
            }
            else
            {
                return 'X';
            }
        }

        public Boolean tirarJugador(Int32 x, Int32 y)
        {
            consola.Items.Add("El jugador ha tirado en: " + x+","+y);
            Boolean retorno=false;
            if (turnoActual == jugador)
            {
                if (jugador == 1)
                {
                    tablero[x,y] = 'X';
                }
                else
                {
                    tablero[x,y] = 'O';
                }
                retorno=true;
            }
            else
            {
                retorno=false;
            }
            cambiarTurno();
            return retorno;
        }

        


    }
}
