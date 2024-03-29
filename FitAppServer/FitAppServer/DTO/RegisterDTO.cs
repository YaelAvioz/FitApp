﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public int age { get; set; }

        public int height { get; set; }

        public double weight { get; set; }

        public string gender { get; set; }

        public string goal { get; set; }

        public string mentor { get; set; }

        public List<string> tags { get; set; }

    }
}
