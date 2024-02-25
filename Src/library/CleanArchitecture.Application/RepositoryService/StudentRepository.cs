using AutoMapper;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Application.ViewModel;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infructure.DatabaseContext;

namespace CleanArchitecture.Application.RepositoryService;

public class StudentRepository:RepositoryService<Student,StudentVm>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext dbContext,IMapper mapper):base(dbContext,mapper)
    {
        
    }
}
