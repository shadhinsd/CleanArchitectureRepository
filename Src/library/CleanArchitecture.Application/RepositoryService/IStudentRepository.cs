using CleanArchitecture.Application.Service;
using CleanArchitecture.Application.ViewModel;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.RepositoryService;

public interface IStudentRepository:IRepositoryService<Student,StudentVm>
{
}
