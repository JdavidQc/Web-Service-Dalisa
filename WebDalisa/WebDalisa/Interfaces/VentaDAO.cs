using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDalisa.Models;

namespace WebDalisa.Interfaces
{
    abstract class VentaDAO
    {

        public abstract void GenerarBoleta(CabeceraBoletaDTO bc, List<DetalleBoletaDTO> db);

        public abstract string nextBoleta();

        public abstract List<CabeceraBoletaDTO> listadoCabcera(string idSocio, string opc);
        public abstract List<DetalleBoletaDTO> listadoDettalle(string nboleta);
        public abstract void ProcedoBoleta(string nboleta,string opc);
    }
}
