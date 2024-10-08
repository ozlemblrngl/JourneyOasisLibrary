﻿using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Language : Entity<Guid>
{
    public string Name { get; set; }

    public ICollection<LanguageBook> LanguageBooks { get; set; }
}
