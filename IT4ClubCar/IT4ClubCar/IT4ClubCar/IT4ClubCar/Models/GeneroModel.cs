﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IT4ClubCar.IT4ClubCar.Models
{
    class GeneroModel
    {
        /// <summary>
        /// Obtém e define o Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém e define o Nome.
        /// </summary>
        public string Nome { get; set; }



        public GeneroModel()
        {
            
        }



        public GeneroModel(int id,string nome)
        {
            Id = id;
            Nome = nome;
        }



    }
}
