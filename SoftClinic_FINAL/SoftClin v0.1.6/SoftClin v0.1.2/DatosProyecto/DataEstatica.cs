using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftClin_v0._1._2
{
    public static class DataEstatica
    {
        public static string CadConexionDB,Usuario,Contrasña;

        public static void SetUsuario(String prmusuario)
        {
            Usuario = prmusuario;
        }

        public static void SetContraseña(String prmContraseña)
        {
            Contrasña= prmContraseña;
        }

        public static void SetCadenaConexion()
        {
            CadConexionDB = @"Data Source=softclin.ckh24lrjlzsa.us-east-2.rds.amazonaws.com;Initial Catalog=SoftClin;User ID=" + Usuario + ";Password=" + Contrasña;
        }

        public static string GetCadenaConexion()
        {
            return CadConexionDB;
        }
    }
}
