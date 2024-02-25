using AutoMapper;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.ViewModel;
[AutoMap(typeof(Student),ReverseMap = true)]
public class StudentVm
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}
