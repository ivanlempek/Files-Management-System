using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace NOVAteste.Models
{
    public class Arquivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public byte[] Dados { get; set; }
    }
}