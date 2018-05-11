using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webnfwithapi1.Models
{
    public class Zayavki
    {
        public int Id { get; set; }
        public string Num_insp { get; set; }
        public string Num_zayavki { get; set; }
        public string Opisanie { get; set; }
        public DateTime Data_start { get; set; }
        public DateTime Data_end { get; set; }
        public string Zayavitel { get; set; }
        public string Ispolnitel { get; set; }
        public string Type_zayavki { get; set; }
    }
}