using BookStoreWebApp.Samples;
using Xunit;

namespace BookStoreWebApp.MongoDB.Domains;

[Collection(BookStoreWebAppTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<BookStoreWebAppMongoDbTestModule>
{

}
