﻿namespace Model
{
    public class MateriaPrimaRolado : MateriaPrima
    {
        public double Thickness { get; set; }

        public double _Width { get; set; }

        public double Groove { get; set; }

        public string UM { get; set; }

        public bool IsSelected { get; set; }

        public string Ubicacion { get; set; }

        public bool Encontrado { get; set; }

        public string Comentarios { get; set; }

    }
}