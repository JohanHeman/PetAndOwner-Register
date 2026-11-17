using Moq;
using PetAndOwner.Domain.Entities;
using PetAndOwner.Domain.Interface.Repository;
using PetAndOwner.Infrastructure.Data;

namespace PetAndOwner.Tests.Infrastructure;

public class TestBaseRepository<T> : BaseRepository<T> where T : class 
{  
    public TestBaseRepository(ApplicationDbContext context) : base(context)
    {}
    
}