﻿
using Database.Models.Interfaces;

namespace Database.Repositories.Interfaces;

public interface ICommandsRepository<T, U> 
    where T : ICommand
    where U : ICommandHistory
{
    void Update(T entity);
    void Delete(T entity);
    void AddCommandHistory(T command, U commandHistory);
}