using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryManager.Test
{
    [CollectionDefinition("Integration Tests")]
    public class IntegrationTestCollection : ICollectionFixture<TestServerFixture>
    {

    }
}