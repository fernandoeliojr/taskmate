using AutoMapper;
using TaskMate.Api.Dtos;
using TaskMate.Domain.Entities;

namespace TaskMate.Api.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // Entity -> DTO
            CreateMap<TaskItem, TaskReadDto>();

            // DTO -> Entity
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();
                
        }
        
    }
}