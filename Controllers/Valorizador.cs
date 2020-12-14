using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Valorizador
    {
        public double calcularValorEvento(double valorBase, double recargoAsistentes, double recargoPersonal, double personalbase)
        {
            recargoPersonal = recargoPersonal + personalbase;
            if (recargoAsistentes >= 1 && recargoAsistentes <= 20)
            {
                recargoAsistentes = 3;
            }
            else if (recargoAsistentes >= 21 && recargoAsistentes <= 50)
            {
                recargoAsistentes = 5;
            }
            else
            {
                recargoAsistentes = (recargoAsistentes / 20) * 2;
            }

            if (recargoPersonal == 2)
            {
                recargoPersonal = 2;
            }
            else if (recargoPersonal == 3)
            {
                recargoPersonal = 3;
            }
            else if (recargoPersonal == 4)
            {
                recargoPersonal = 3.5;
            }
            else
            {
                recargoPersonal = recargoPersonal - 4;
                recargoPersonal = 3.5 + (recargoPersonal * 0.5);
            }

            double valorTotal = valorBase + recargoAsistentes + recargoPersonal;
            return valorTotal;

        }
    }
}
