﻿using System.Threading.Tasks;
using OutOfSchool.Services.Models;
using OutOfSchool.WebApi.Models.ModelsDto;

namespace OutOfSchool.WebApi.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> CreateAsync(TeacherDto teacherDto);
    }
}