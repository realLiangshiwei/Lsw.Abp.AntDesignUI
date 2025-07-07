using BookStore.Samples;
using Xunit;

namespace BookStore.MongoDB.Domains;

[Collection(BookStoreTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<BookStoreMongoDbTestModule>
{

}
