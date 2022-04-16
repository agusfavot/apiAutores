namespace apiVS.Models
{
    public class AuthorsBooks
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        //Puede ser una regla de negocio, que consiste en ordenar los autores de acuerdo a su contribución
        public int Order { get; set; }

        //Propiedades de navegacion
        public Authors Authors { get; set; }
        public Books Books { get; set; }
    }
}
