using Database.Models.Interfaces;
using Database.Repositories.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Database.T_CommandsRepository.UpdateBuilders;

internal class DescriptionUpdateBuilder : IUpdatableModelBuilder<ICommand>
{
    ICommand _command;

    public DescriptionUpdateBuilder(ICommand command)
    {
        _command = command;
    }

    public DescriptionUpdateBuilder SetDescription(string description)
    {
        _command.Description = description;
        return this;
    }

    public ICommand Build()
    {
        return _command;
    }
}
