using Xunit;

namespace BookStore.MongoDB;

[CollectionDefinition(BookStoreTestConsts.CollectionDefinitionName)]
public class BookStoreMongoCollection : BookStoreMongoDbCollectionFixtureBase
{

}
