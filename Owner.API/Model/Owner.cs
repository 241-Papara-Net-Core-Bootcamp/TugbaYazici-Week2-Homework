using System;
namespace Owner.API.Model
{
    public class Owner
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set;}
        public string Comment { get; set; }
        public string Type { get; set; }
    }
}

