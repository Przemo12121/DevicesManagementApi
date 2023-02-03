﻿using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers;

public interface ICommandsFactory<T> where T : ICommand
{
    public T From(RegisterCommandRequest request);
}
