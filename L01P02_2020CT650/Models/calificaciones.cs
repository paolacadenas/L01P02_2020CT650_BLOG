﻿using System.ComponentModel.DataAnnotations;
namespace L01P02_2020CT650.Models
{
    public class calificaciones
    {
        [Key]

        public int calificacionId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public int calificacion { get; set; }
    }
}
