using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Webnfwithapi1.Models
{
    public class MyContext:DbContext
    {
        public MyContext() : base("myConnectString") { }


        public virtual DbSet<Zayavki> Zayavkis { get; set; } // создать таблицу  с  именем  Zayavkis !!! надоменять имя  класса!!!
        public virtual DbSet<UsersFKUs> USERFKU { get; set; } // таблица  назвалась именем класса 
    }
}