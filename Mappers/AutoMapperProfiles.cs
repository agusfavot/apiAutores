using apiVS.DTOs;
using AutoMapper;
using apiVS.Models;
using System.Collections.Generic;

namespace apiVS.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AuthorCreation, Authors>();
            CreateMap<Authors, AuthorDTO>();

            CreateMap<BookCreation, Books>().ForMember(book => book.ListAutoresLibros, options => options.MapFrom(x => MapAuthorsBooks(x)));
            CreateMap<Books, BookDTO>().ForMember(bookDTO => bookDTO.ListAuthors, options => options.MapFrom(x => MapBookDTOAuthors(x)));

            CreateMap<CommentCreation, Comments>();
            CreateMap<Comments, CommentDTO>();
        }


        //Mapear de Author a AuthorDTO que es lo que la BookDTO espera, un listado de AuthorDTO
        private List<AuthorDTO> MapBookDTOAuthors(Books books)
        {
            var result = new List<AuthorDTO>();
            if (books.ListAutoresLibros == null) { return result; }

            foreach (var authorsBooks in books.ListAutoresLibros)
            {
                result.Add(new AuthorDTO() { Id = authorsBooks.AuthorId, Name = authorsBooks.Authors.Name });
            }
            return result;
        }

        private List<AuthorsBooks> MapAuthorsBooks(BookCreation bookCreation)
        {
            var result = new List<AuthorsBooks>();
            foreach (var author in bookCreation.AuthorsIds)
            {
                result.Add(new AuthorsBooks() { AuthorId = author });
            }
            return result;
        }
    }
}
