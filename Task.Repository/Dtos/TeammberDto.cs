﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.Core.Entites;

namespace taskm.Repository.Dtos
{
    public class TeammberDto
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        
    }
}
