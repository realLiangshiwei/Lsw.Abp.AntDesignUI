using Xunit;

namespace BookStoreWebApp.MongoDB;

[CollectionDefinition(BookStoreWebAppTestConsts.CollectionDefinitionName)]
public class BookStoreWebAppMongoCollection : BookStoreWebAppMongoDbCollectionFixtureBase
{

}
